using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bagging.BagEdit.CustomControls;
using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;

namespace Bagging.BagEdit {
    public partial class FormBagEdit : Form {

        private static readonly ILog log = LogManager.GetLogger(typeof(FormBagEdit));

        private List<string> originalRow;
        private bool DbOnline = true;
        Dictionary<int, string> qualityValues = new Dictionary<int, string>();
        FormWait wait;

        public FormBagEdit() {
            log.Debug("FormBagEdit initializing...");
            InitializeComponent();
        }

        private void BagEditForm_Load(object sender, EventArgs e) {
            log.Debug("Loading BagEditForm...");
            productType.ValueType = typeof(string);
            qualityFlagName.ValueType = typeof(string);
            qualityFlagName.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            productDate.DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
        }

        private void BagEditForm_Move(object sender, EventArgs e) {

            Rectangle location = this.DesktopBounds;
            Rectangle activeScreen = Screen.FromControl(this).Bounds;

            if (wait != null)  {
                if (!wait.IsDisposed){
                    wait.Invoke((Action)delegate () {
                        wait.Location = new Point(this.Location.X + this.Width / 2 - wait.Width / 2, this.Location.Y + this.Height / 2 - wait.Height / 2);
                    });
                }
            }
        }

        private void BagEditForm_Shown(object sender, EventArgs e) {
            log.Debug("BagEditForm_Shown() method called.");
            wait = new FormWait(this);
            wait.Show(this);
            statusLabel1.Text = "Connecting...";
            Task.Run(async () => {

                this.Move += BagEditForm_Move;

                DbOnline = await MainController.CheckDBConnection();

                if (!DbOnline) {
                    printButton.Invoke((Action)delegate () { printButton.Enabled = false; });
                    statusLabel1.Invoke((Action)delegate () { statusLabel1.Text = "Db offline";
                                statusLabel1.BackColor = Color.FromArgb(255, 186, 186);
                                });
                        //MessageBox.Show("Error database didn't respond. Can't reach database\n", "Database is offline.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else {
                    string message = await MainController.RunPendingqueriesAsync();
                    if (!string.IsNullOrEmpty(message)) {
                        MessageBox.Show(message);
                    }
                    printButton.Invoke((Action)delegate () { printButton.Enabled = true; });
                }

                foreach (var item in MainController.PRODUCT_TYPE_LIST) {
                    productType.Items.Add($"{MainController.PRODUCT_PREFIX}{item}");
                }

                qualityValues = EditController.GetQualityValues();
                string[] items = qualityValues.Values.ToArray();
                qualityFlagName.Items.AddRange(items);
                try {
                    BagModel[] bags = await EditController.GetBagsAsync().ConfigureAwait(false);
                    this.Invoke((Action)delegate () { this.PopulateGridFromDBWorker.RunWorkerAsync(bags); });

                    addNewBagButton.Invoke((Action)delegate () { addNewBagButton.Focus(); addNewBagButton.Enabled = true; });
                    reloadListButton.Invoke((Action)delegate () { reloadListButton.Enabled = true; });
                    this.Move -= BagEditForm_Move;
                    wait.Invoke((Action)delegate () { wait.Close(); wait?.Dispose(); });
                }
                catch(Exception ex) {
                    log.Error("BagEditForm_Shown() Exception", ex);
                }
            });

            bagGridView.EditingControlShowing += BagGridView_EditingControlShowing;

        }

        private void PopulateGridFromDB_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            BagModel[] bagValues = (BagModel[])e.Argument;
            PopulateGridFromDBWorker.ReportProgress(1, "Working.");

            if (bagValues == null || bagValues.Length == 0) {
                PopulateGridFromDBWorker.ReportProgress(100, "Complete...");
                return;
            }
            bagGridView?.Invoke(new Action(() => {
                for (int i = 0; i < bagValues.Length; ++i) {

                    bagGridView.Rows.Add(1);

                    bagGridView.Rows[i].Cells[0].Value = bagValues[i].UniqueIndentifier;
                    bagGridView.Rows[i].Cells[1].Value = bagValues[i].ProductDate;

                    var comboBox = bagGridView.Rows[i].Cells[2] as DataGridViewComboBoxCell;
                    comboBox.Value = MainController.PRODUCT_PREFIX + bagValues[i].ProductType;
                    comboBox.ValueMember = MainController.PRODUCT_PREFIX;
                    bagGridView.Rows[i].Cells[3].Value = MainController.KgToLbs(bagValues[i].ProductWeight);

                    var comboBox2 = bagGridView.Rows[i].Cells[4] as DataGridViewComboBoxCell;
                    comboBox2.Value = bagValues[i].QualityFlagName;

                    bagGridView.Rows[i].Cells[5].Value = bagValues[i].Notes;

                    PopulateGridFromDBWorker.ReportProgress((100 * i) / bagValues.Length, "Working...");
                }
            }));
            PopulateGridFromDBWorker.ReportProgress(100, "Complete...");
            log.Debug("Datagridview populated successfully!");
        }

        #region Event handlers
        private async void RefreshButton_Click(object sender, EventArgs e) {

            progressBar.Value = 0;
            bagGridView.Rows.Clear();
            PopulateGridFromDBWorker.RunWorkerAsync(await EditController.GetBagsAsync());
            this.SaveBagButton.Enabled = false;

        }
        private void BagGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {

            Control sender_ = sender as Control;

            if (e == null) return;

            if (this.bagGridView.Columns[e.ColumnIndex].Name == this.bagGridView.Columns[1].Name) {
                if (e.Value != null) {
                    try {
                        e.Value = DateTime.Parse(e.Value.ToString()).ToLongDateString();
                        e.FormattingApplied = true;
                    } catch (FormatException ex) {
                        toolTip1.SetToolTip(sender_, ex.Message + $"\n{e.Value.ToString()} is not a valid date.");
                        sender_.BackColor = Color.FromArgb(255, 186, 186);
                    }
                }
            }
            else if (this.bagGridView.Columns[e.ColumnIndex].Name == this.bagGridView.Columns[3].Name) {
                try {
                    if (e.Value != null) {
                        bool result = decimal.TryParse(e.Value.ToString(), out decimal weight);
                        if (result && weight > MainController.MAX_WEIGHT && weight < MainController.MIN_WEIGHT) {
                            sender_.BackColor = Color.White;
                            e.Value = weight;
                            e.FormattingApplied = true;
                        }
                    } else {
                        e.FormattingApplied = false;
                    }
                } catch (FormatException ex) {
                    toolTip1.SetToolTip(sender_, ex.Message + $"\n{e.Value.ToString()} is not a valid weight.");
                    sender_.BackColor = Color.FromArgb(255, 186, 186);
                    e.FormattingApplied = false;
                }
            }
        }

        private void AddNewBagButton_Click(object sender, EventArgs e) {
            try {
                FormNewBag newBagForm = new FormNewBag();
                newBagForm.TopLevel = true;
                DialogResult result = DialogResult.Retry;
                result = newBagForm.ShowDialog(this);
                if (result == DialogResult.OK) {
                    AddBagToGrid(newBagForm.NewBag);
                    log.Debug("Adding bag to grid...");
                }
                newBagForm.Dispose();
            } catch (Exception ex) {
                log.Error("Exception thrown!", ex);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e) {
            BagModel bag = new BagModel();
            if(bagGridView.CurrentRow == null){
                return;
            }
            bag.UniqueIndentifier = Convert.ToUInt16(bagGridView.CurrentRow.Cells[0].Value);
            bag.ProductType = bagGridView.CurrentRow.Cells[2].Value.ToString();
            bag.ProductWeight = Convert.ToDecimal(bagGridView.CurrentRow.Cells[3].Value);
            bag.SiteId = (UInt16)MainController.SITE_ID;
            
            var img = EditController.BuildLabel(bag, Properties.Resources.CrystalGreenLogo);
            FormLabelPreview labelPreviewWindow = new FormLabelPreview(img);
            labelPreviewWindow.ShowDialog(this);
            var result = labelPreviewWindow.DialogResult;
            if (result == DialogResult.OK) {
                FormWait wait;
                wait = new FormWait(this);
                wait.Show(this);
                wait.TopMost = true;
                Task printTask = Task.Run(delegate () {

                    string errorMessage = EditController.Print(img, labelPreviewWindow.Copies);
                    wait.Invoke((Action)delegate () { wait.Close(); wait?.Dispose(); });
                    if (!string.IsNullOrEmpty(errorMessage)) {
                        MessageBox.Show(errorMessage, "Printer error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });

                labelPreviewWindow.Close();
                log.Debug("FormLabelPreview windows Closed.");
            }

        }

        private void BagGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            log.Warn($@"Sender type: {sender.GetType()}
                            BagGridView_DataError\n{e.Exception.Message}
                            Type: {e.Exception.GetType()}
                                    {e.Exception.StackTrace}
                                    {e.Exception.Data}    
                            Column: {e.ColumnIndex}  Row: {e.RowIndex}");
        }

        private void BagGridView_KeyPress(object sender, KeyPressEventArgs e) {

            if ((bagGridView.CurrentCell.ColumnIndex == 2 || bagGridView.CurrentCell.ColumnIndex == 3) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            } else if (bagGridView.CurrentCell.ColumnIndex == 4) {
                e.Handled = true;
            } else if (char.IsSymbol(e.KeyChar)) {
                e.Handled = true;
            } else {
                this.SaveBagButton.Enabled = true;
            }
        }

        private void BagGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {

            originalRow = new List<string>();
            int i = bagGridView.CurrentRow.Index;

            foreach (DataGridViewCell cell in bagGridView.CurrentRow.Cells) {
                originalRow.Add(cell.Value == null ? "" : cell.Value.ToString());
            }

            Control control;
            if (e.Control.GetType() == typeof(CalendarEditingControl)) {
                control = e.Control as CalendarEditingControl;
            } 
            else if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl)) {
                control = e.Control as DataGridViewComboBoxEditingControl;
            } 
            else {
                control = e.Control as TextBox;
            }

        }

        DataGridViewCellStyle previousStyle;
        private void BagGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            this.SaveBagButton.Enabled = true;
            if (bagGridView.CurrentCell.OwningColumn.Name == bagGridView.Columns[5].Name) {
                if (bagGridView.CurrentCell.Value == null || string.IsNullOrEmpty(bagGridView.CurrentCell.Value.ToString())) {
                    bagGridView.CurrentCell.Value = string.Empty;
                }

            } else if (bagGridView.CurrentCell.Value == null || string.IsNullOrEmpty(bagGridView.CurrentCell.Value.ToString()) && bagGridView.CurrentCell.OwningColumn.Name == bagGridView.Columns[3].Name) {

                bagGridView.CurrentCell.ErrorText = $"{bagGridView.CurrentCell.Value} is not a valid weight";
                previousStyle = bagGridView.CurrentCell.Style;
                bagGridView.CurrentCell.Style.BackColor = Color.FromArgb(255, 186, 186);

            } else {
                bagGridView.CurrentCell.ErrorText = string.Empty;
                bagGridView.CurrentCell.Style = previousStyle;
                previousStyle = null;
            }
            bagGridView.CurrentRow.DefaultCellStyle.BackColor = Color.LightYellow;
            bagGridView.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightYellow;
            bagGridView.CurrentRow.DefaultCellStyle.ForeColor = Color.Black;
            bagGridView.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private BagModel GetSelectedBagRow() {
            bagGridView.CurrentRow.ErrorText = string.Empty;
            //foreach (DataGridViewColumn cell in bagGridView.Columns) {
            //    Console.WriteLine(cell.Name);
            //}

            StringBuilder message = new StringBuilder();
            BagModel bag = new BagModel();
            if (MainController.SITE_ID >= 0) {
                bag.SiteId = Convert.ToUInt16(MainController.SITE_ID);
            } 
            else {
                message.Append("Invalid site Id\n");
            }
            if (bagGridView.CurrentRow.Cells["bagNo"].Value != null) {
                bag.UniqueIndentifier = Convert.ToUInt32(bagGridView.CurrentRow.Cells["bagNo"].Value);

            } else {
                message.Append("Invalid bag_ number\n");
            }

            DateTime dateEntered = Convert.ToDateTime(bagGridView.CurrentRow.Cells["productDate"].Value);
            if (dateEntered == null) {
                bagGridView.CurrentRow.Cells["productDate"].ErrorText = "Date can't be null\n";
                message.Append(bagGridView.CurrentRow.Cells["productDate"].ErrorText);
            } 
            else if (dateEntered > DateTime.Now) {
                bagGridView.CurrentRow.Cells["productDate"].ErrorText = "Date can't be in the future\n";
                message.Append(bagGridView.CurrentRow.Cells["productDate"].ErrorText);
            } 
            else if (dateEntered < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                bagGridView.CurrentRow.Cells["productDate"].ErrorText = "Date can't be earlier than 30 days back\n";
                message.Append(bagGridView.CurrentRow.Cells["productDate"].ErrorText);
            } 
            else {
                bag.ProductDate = Convert.ToDateTime(bagGridView.CurrentRow.Cells["productDate"].Value);
            }

            if (bagGridView.CurrentRow.Cells["productType"].Value != null) {
                bag.ProductType = bagGridView.CurrentRow.Cells["productType"].Value.ToString();
            } 
            else {
                bagGridView.CurrentRow.Cells["productType"].ErrorText = "Invalid product type\n";
                message.Append(bagGridView.CurrentRow.Cells["productType"].ErrorText);
            }

            if (bagGridView.CurrentRow.Cells["productWeight"].Value != null && bagGridView.CurrentRow.Cells["productWeight"].Value.ToString().Length > 0) {
                bag.ProductWeight = Convert.ToDecimal(bagGridView.CurrentRow.Cells["productWeight"].Value);
                
                if (bag.ProductWeight > MainController.MAX_WEIGHT) {
                    bagGridView.CurrentRow.Cells["productWeight"].ErrorText = $"Product weight can't be greater than {MainController.MAX_WEIGHT}\n";
                    message.Append($"{bagGridView.CurrentRow.Cells["productWeight"].ErrorText}\n");
                } 
                else if (bag.ProductWeight < MainController.MIN_WEIGHT) {
                    bagGridView.CurrentRow.Cells["productWeight"].ErrorText = $"Product weight can't be smaller than {MainController.MIN_WEIGHT}\n";
                    message.Append(bagGridView.CurrentRow.Cells["productWeight"].ErrorText);
                }
            } 
            else {
                bagGridView.CurrentRow.Cells["productWeight"].ErrorText = "Invalid product weight\n";
                message.Append(bagGridView.CurrentRow.Cells["productWeight"].ErrorText);
            }

            if (bagGridView.CurrentRow.Cells["qualityFlagName"].Value != null) {
                bag.QualityFlagName = bagGridView.CurrentRow.Cells["qualityFlagName"].Value.ToString();
                bag.QualityFlag = qualityValues.Keys.FirstOrDefault(key => qualityValues[key] == bagGridView.CurrentRow.Cells["qualityFlagName"].Value.ToString());
            } 
            else {
                bagGridView.CurrentRow.Cells["qualityFlagName"].ErrorText = "Invalid product weight\n";
                message.Append(bagGridView.CurrentRow.Cells["qualityFlagName"].ErrorText);
            }
            if (message.Length > 0) {
                bagGridView.CurrentRow.ErrorText = message.ToString();
                return null;
            }
            bag.Notes = bagGridView.CurrentRow.Cells["notes"].Value != null ? bagGridView.CurrentRow.Cells["notes"].Value.ToString() : string.Empty;
            return bag;
        }

        /// <summary>
        /// Get the current bag row without validating its fields
        /// </summary>
        /// <returns></returns>
        private BagModel GetSelectedBagRowNoValidation() {

            BagModel notValidatedBag = new BagModel();

            if (bagGridView.CurrentRow.Cells["bagNo"].Value != null) {
                notValidatedBag.SiteId = Convert.ToUInt16(MainController.SITE_ID);
                notValidatedBag.UniqueIndentifier = Convert.ToUInt32(bagGridView.CurrentRow.Cells["bagNo"].Value);
                notValidatedBag.ProductDate = Convert.ToDateTime(bagGridView.CurrentRow.Cells["productDate"].Value);
                notValidatedBag.ProductType = bagGridView.CurrentRow.Cells["productType"].Value.ToString();
                notValidatedBag.ProductWeight = Convert.ToDecimal(bagGridView.CurrentRow.Cells["productWeight"].Value);
                notValidatedBag.QualityFlagName = bagGridView.CurrentRow.Cells["qualityFlagName"].Value.ToString();
                notValidatedBag.QualityFlag = qualityValues.Keys.FirstOrDefault(key => qualityValues[key] == bagGridView.CurrentRow.Cells["qualityFlagName"].Value.ToString());
                notValidatedBag.Notes = bagGridView.CurrentRow.Cells["notes"].Value != null ? bagGridView.CurrentRow.Cells["notes"].Value.ToString() : string.Empty;
            }
            return notValidatedBag;
        }

        private void BagGridView_RowValidated(object sender, DataGridViewCellEventArgs e) {
            //Program.ConsoleWriteLineColor($"{sender.GetType()} BagGridView_RowValidated", ConsoleColor.Yellow);
            var result = MessageBox.Show("Discard edited values?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            bagGridView.CancelEdit();
            if (result == DialogResult.Yes) {
                bagGridView.CurrentRow.Selected = true;
                bagGridView.CurrentCell.Selected = true;
            }
        }


        private void AddBagToGrid(BagModel bag_) {
            bagGridView.Rows.Add(bag_.UniqueIndentifier, bag_.ProductDate, bag_.ProductType, bag_.ProductWeight, bag_.QualityFlagName, bag_.Notes);
            bagGridView.Sort(bagGridView.Columns[0], System.ComponentModel.ListSortDirection.Descending);
        }

        private void UpdateBagInGrid(BagModel bag_) {

            bagGridView.CurrentRow.Cells[0].Value = bag_.UniqueIndentifier;
            bagGridView.CurrentRow.Cells[1].Value = bag_.ProductDate;

            var comboBox = bagGridView.CurrentRow.Cells[2] as DataGridViewComboBoxCell;
            comboBox.Value = bag_.ProductType;

            bagGridView.CurrentRow.Cells[3].Value = bag_.ProductWeight;

            var comboBox2 = bagGridView.CurrentRow.Cells[4] as DataGridViewComboBoxCell;
            comboBox2.Value = bag_.QualityFlagName;

            bagGridView.CurrentRow.Cells[5].Value = bag_.Notes;
        }


        private void CloseWindowButton_Click(object sender, EventArgs e) {
            this?.Dispose();
            Application.Exit();
        }

        private void DataAdapter_RowUpdated(object sender, System.Data.Common.RowUpdatedEventArgs e) {
            Console.WriteLine("DataAdapter_RowUpdated");
        }

        #endregion

        private void BagGridView_EditModeChanged(object sender, EventArgs e) {
            Console.WriteLine("BagGridView_EditModeChanged");
        }

        private void BagGridView_CancelRowEdit(object sender, QuestionEventArgs e) {
            Console.WriteLine("BagGridView_CancelRowEdit");
        }

        private void PopulateGridFromDB_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) {
            progressBar.Value = e.ProgressPercentage;
            progressBar.Refresh();
            percentageLabel1.Text = $"{e.ProgressPercentage}%";
            percentageLabel1.Refresh();
            percentageLabel1.Update();
        }

        private void PopulateGridFromDB_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (DbOnline) {
                statusLabel1.Text = "Connected";
                statusLabel1.BackColor = Color.Green;
            }
            progressBar.Value = 100;
            percentageLabel1.Refresh();
            bagGridView.Sort(bagGridView.Columns[0], System.ComponentModel.ListSortDirection.Descending);
        }

        private void BagGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            BagModel originalBag = GetSelectedBagRowNoValidation();
            if(originalBag == null) {
                return;
            }

            FormUpdateBag updateForm = new FormUpdateBag(originalBag);
            updateForm.TopLevel = true;
            DialogResult result = updateForm.ShowDialog(this);

            if (result == DialogResult.OK) {
                UpdateBagInGrid(updateForm.NewBag);
            }
            updateForm.Close();
        }

        private void BagGridView_MouseHover(object sender, EventArgs e) {
            toolTip1.SetToolTip(bagGridView, "Double click on line to edit bag.");
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.Active = true;
        }

        private void SaveUpdateBagButton_Click(object sender, EventArgs e) {
            //string resultMessage = await UpdateCurrentRowToDB(GetSelectedBagRow());

            ////MessageBox.Show(resultMessage);
            //this.SaveBagButton.Enabled = false;
        }


        /* ========================== COMMENTED METHODS =============================================
          
        private async void BagGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e) {
            if (!leftRow) {
                BagGridView_RowLeave(bagGridView, new DataGridViewCellEventArgs(bagGridView.CurrentCell.ColumnIndex, bagGridView.CurrentRow.Index)); // manually calling event handler in case it doesn't get fired
                //return;
            }

            Program.ConsoleWriteLineColor($"BagGridView_RowValidating", ConsoleColor.Yellow);

            BagModel bag_ = GetBag();

            if (!string.IsNullOrEmpty(bagGridView.CurrentRow.ErrorText)) {
                bagGridView.CurrentRow.Selected = true;
                bagGridView.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightSalmon;
                //bagGridView.CurrentCell.Selected = true;
                MessageBox.Show(bagGridView.CurrentRow.ErrorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BagGridView_RowLeave(bagGridView, new DataGridViewCellEventArgs(bagGridView.CurrentCell.ColumnIndex, bagGridView.CurrentRow.Index));
                bagGridView.CancelEdit();
                e.Cancel = true;

                return;
            }

            string resultMessage = await UpdateCurrentRow(bag_);
            MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Debugrmation);
            this.SaveBagButton.Enabled = false;
            bagGridView.RowLeave -= BagGridView_RowLeave;
            bagGridView.RowValidating -= BagGridView_RowValidating;
            leftRow = false;
            bagGridView.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
            bagGridView.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            bagGridView.CurrentRow.DefaultCellStyle.ForeColor = Color.Black;
            bagGridView.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
   

        private void BagGridView_RowEnter(object sender, DataGridViewCellEventArgs e) {
            originalRow.Cells[0].Value = bagGridView.CurrentRow.Cells[0].Value;
            originalRow.Cells[1].Value = bagGridView.CurrentRow.Cells[1].Value;
            originalRow.Cells[2].Value = bagGridView.CurrentRow.Cells[2].Value;
            originalRow.Cells[3].Value = bagGridView.CurrentRow.Cells[3].Value;
            var comboBox = originalRow.Cells[4] as DataGridViewComboBoxCell;    
            comboBox.Value = bagGridView.CurrentRow.Cells[4].Value;
            originalRow.Cells[5].Value = bagGridView.CurrentRow.Cells[5].Value;
        }

                private async void BagGridView_RowLeave(object sender, DataGridViewCellEventArgs e) {
            Program.ConsoleWriteLineColor($"BagGridView_RowLeave", ConsoleColor.Yellow);
            BagModel bag = GetSelectedBagRow();
            if (!string.IsNullOrEmpty(bagGridView.CurrentRow.ErrorText)) {
                bagGridView.CurrentRow.Selected = true;
                bagGridView.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightSalmon;
                //bagGridView.CurrentCell.Selected = true;
                MessageBox.Show(bagGridView.CurrentRow.ErrorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bagGridView.CancelEdit();
                bagGridView.RowLeave -= BagGridView_RowLeave;
                return;
            }

            var result = MessageBox.Show($"Update:\n Bag number: {bagGridView.CurrentRow.Cells[0].Value}?", "Update bag_?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) {
                bagGridView.CancelEdit();
                bagGridView.CurrentRow.Selected = true;
                bagGridView.CurrentCell.Selected = true;
                //bagGridView.RowValidating -= BagGridView_RowValidating;
                //bagGridView.RowValidated -= BagGridView_RowValidated;
            } 
            else {

                string resultMessage = await UpdateCurrentRowToDB(bag);
                MessageBox.Show(resultMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Debugrmation);
                this.SaveBagButton.Enabled = false;
                //bagGridView.RowValidating -= BagGridView_RowValidating;
                leftRow = false;
                bagGridView.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                bagGridView.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                bagGridView.CurrentRow.DefaultCellStyle.ForeColor = Color.Black;
                bagGridView.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            bagGridView.RowLeave -= BagGridView_RowLeave;
            leftRow = true;
            bagGridView.RowLeave -= BagGridView_RowLeave;
           bagGridView.RowValidating -= BagGridView_RowValidating;
        }

        private void BagGridView_KeyDown(object sender, KeyEventArgs e) {
            Program.ConsoleWriteLineColor($"BagGridView_KeyDown", ConsoleColor.Yellow);
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter) {
                this.SaveBagButton.Focus();
                this.SaveBagButton.BackColor = Color.LawnGreen;
            }
        }

        private void BagGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
        Program.ConsoleWriteLineColor($"BagGridView_PreviewKeyDown KEY: {e.KeyCode}", ConsoleColor.Yellow);
        if (char.IsDigit(e.KeyCode.ToString(), 0)) {
            Console.WriteLine(e.IsInputKey + " " + e.KeyCode + " " + e.KeyData + " " + e.KeyValue);
        }
        if (e.KeyCode == Keys.Enter) {
            BagGridView_KeyDown(sender, new KeyEventArgs(e.KeyData));
        }
        }


        private void RollBackRowValues() {
            int i = bagGridView.CurrentRow.Index;
            bagGridView.Rows[i].Cells[0].Value = originalRow[0];
            bagGridView.UpdateCellValue(0, i);
            bagGridView.Rows[i].Cells[1].Value = originalRow[1];
            bagGridView.UpdateCellValue(1, i);
            bagGridView.Rows[i].Cells[2].Value = originalRow[2];
            bagGridView.UpdateCellValue(2, i);
            bagGridView.Rows[i].Cells[3].Value = originalRow[3];
            bagGridView.UpdateCellValue(3, i);
            var comboBox = bagGridView.Rows[i].Cells[4] as DataGridViewComboBoxCell;
            comboBox.Value = originalRow[4];
            bagGridView.UpdateCellValue(4, i);
            bagGridView.Rows[i].Cells[5].Value = originalRow[5];
            bagGridView.UpdateCellValue(5, i);
        }

        */
    }
}
