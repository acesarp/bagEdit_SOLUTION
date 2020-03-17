using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Bagging.BagEdit.CustomControls;
using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;

namespace Bagging.BagEdit {
    public partial class FormReseed : Form {

        private static ILog log = LogManager.GetLogger(typeof(FormReseed));

        private List<string> originalRow;
        private bool DbOnline = true;
        private FormWait wait;

        public FormReseed() {
            log.Debug("Initializing...");
            InitializeComponent();
            log.Debug("Initialized successfully!");
        }
        private void BagReseedForm_Load(object sender, EventArgs e) {
            log.Debug("BagReseedForm loading...");
            reseedDate.DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
        }

        private void BagEditForm_Move(object sender, EventArgs e) {
            if (wait != null)  {
                if (!wait.IsDisposed){
                    wait.Invoke((Action)delegate () {
                        wait.Location = new Point(this.Location.X + this.Width / 2 - wait.Width / 2, this.Location.Y + this.Height / 2 - wait.Height / 2);
                    });
                }
            }
        }

        private void BagReseedForm_Shown(object sender, EventArgs e) {
            log.Debug("Loading...");
            wait = new FormWait(this);
            wait.Show(this);
            statusLabel1.Text = "Connecting...";

            Task.Run(async () => {

                this.Move += BagEditForm_Move;
                DbOnline = await MainController.CheckDBConnection();
                if (!DbOnline) {
                    statusLabel1.Invoke((Action)delegate () { statusLabel1.Text = "Db offline";
                                statusLabel1.BackColor = Color.FromArgb(255, 186, 186);
                                });
                } else {
                    string message = await MainController.RunPendingqueriesAsync();
                    if (!string.IsNullOrEmpty(message)) {
                        MessageBox.Show(message, "Pending queries", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                ReseedModel[] reseeds = await ReseedController.GetReseedsAsync().ConfigureAwait(false);
                this.Invoke((Action) delegate() { this.PopulateGridFromDBWorker.RunWorkerAsync(reseeds); });
                addNewReseedButton.Invoke((Action) delegate() { addNewReseedButton.Focus(); addNewReseedButton.Enabled = true; });
                reloadListButton.Invoke((Action)delegate () { reloadListButton.Enabled = true; });
                this.Move -= BagEditForm_Move;
                wait.Invoke((Action) delegate() { wait.Close(); wait?.Dispose(); });
            });

        }


        private void PopulateGridFromDB_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            ReseedModel[] reseedValues = (ReseedModel[])e.Argument;
            PopulateGridFromDBWorker.ReportProgress(1, "Working.");

            if (reseedValues == null || reseedValues.Length == 0) {
                PopulateGridFromDBWorker.ReportProgress(100, "Complete...");
                log.Debug("ReseedGridView ppulated successfully!");
                return;
            }
            reseedGridView?.Invoke(new Action(() => {
                for (int i = 0; i < reseedValues.Length; ++i) {
                    try {
                        reseedGridView.Rows.Add(1);

                        reseedGridView.Rows[i].Cells[0].Value = reseedValues[i].UniqueIndentifier;
                        reseedGridView.Rows[i].Cells[1].Value = reseedValues[i].ReSeedDate;
                        reseedGridView.Rows[i].Cells[2].Value = reseedValues[i].BagNo;
                        reseedGridView.Rows[i].Cells[3].Value = reseedValues[i].OriginalSite;
                        reseedGridView.Rows[i].Cells[4].Value = reseedValues[i].Notes;
                    }
                    catch(Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    PopulateGridFromDBWorker.ReportProgress((100 * i) / reseedValues.Length, "Working...");
                }
            }));
            PopulateGridFromDBWorker.ReportProgress(100, "Complete...");
        }

        private async void RefreshButton_Click(object sender, EventArgs e) {

                progressBar.Value = 0;
                reseedGridView.Rows.Clear();
                PopulateGridFromDBWorker.RunWorkerAsync(await ReseedController.GetReseedsAsync());

        }

        #region Event handlers

        private void BagGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            //Program.ConsoleWriteLineColor("BagGridView_CellFormatting " + this.bagGridView.Columns[e.ColumnIndex].Name);
            Control sender_ = sender as Control;

            if (e == null) return;

            if (this.reseedGridView.Columns[e.ColumnIndex].Name == this.reseedGridView.Columns[1].Name) {
                if (e.Value != null) {
                    try {
                        e.Value = DateTime.Parse(e.Value.ToString()).ToLongDateString();
                        e.FormattingApplied = true;
                    } catch (FormatException ex) {
                        toolTip1.SetToolTip(sender_, ex.Message + $"\n{e.Value.ToString()} is not a valid date.");
                        sender_.BackColor = Color.FromArgb(255, 186, 186);

                    }
                }
            } else if (this.reseedGridView.Columns[e.ColumnIndex].Name == this.reseedGridView.Columns[3].Name) {

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
                FormNewReseed newBagForm = new FormNewReseed();
                newBagForm.TopLevel = true;
                DialogResult result = DialogResult.Retry;
                result = newBagForm.ShowDialog(this);
                if (result == DialogResult.OK) {
                    AddReseedToGrid(newBagForm.NewReseed);
                }
                newBagForm.Dispose();
            } catch (Exception ex) {
                log.Error("Exception thrown!", ex);
            }
        }


        private void ReseedGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            log.Warn($"{sender.GetType()}\nBagGridView_DataError\n{e.Exception.Message}\nType: {e.Exception.GetType()}\nColumn: {e.ColumnIndex}\nRow: {e.RowIndex}");
        }

        private void BagGridView_KeyPress(object sender, KeyPressEventArgs e) {
            //Program.ConsoleWriteLineColor($"BagGridView_KeyPress {e.KeyChar}", ConsoleColor.Yellow);
            if ((reseedGridView.CurrentCell.ColumnIndex == 2 || reseedGridView.CurrentCell.ColumnIndex == 3) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            } else if (reseedGridView.CurrentCell.ColumnIndex == 4) {
                e.Handled = true;
            } else if (char.IsSymbol(e.KeyChar)) {
                e.Handled = true;
            } else {
            }
            //Console.WriteLine(char.IsControl(e.KeyChar) + " " + char.IsSymbol(e.KeyChar) + " " + char.IsLetterOrDigit(e.KeyChar) + " " + char.IsPunctuation(e.KeyChar));
        }

        private void BagGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {

            originalRow = new List<string>();
            int i = reseedGridView.CurrentRow.Index;

            foreach (DataGridViewCell cell in reseedGridView.CurrentRow.Cells) {
                originalRow.Add(cell.Value == null ? "" : cell.Value.ToString());
            }

            Control control;
            if (e.Control.GetType() == typeof(CalendarEditingControl)) {
                control = e.Control as CalendarEditingControl;
            } 
            else {
                control = e.Control as TextBox;
            }

        }

        private ReseedModel GetSelectedReseedRow() {
            reseedGridView.CurrentRow.ErrorText = string.Empty;

            StringBuilder message = new StringBuilder();
            ReseedModel bag = new ReseedModel();
            if (MainController.SITE_ID >= 0) {
                bag.SiteId = Convert.ToUInt16(MainController.SITE_ID);
            } 
            else {
                message.Append("Invalid site Id\n");
            }
            if (reseedGridView.CurrentRow.Cells["EntryNo"].Value != null) {
                bag.UniqueIndentifier = Convert.ToUInt32(reseedGridView.CurrentRow.Cells["EntryNo"].Value);

            } else {
                message.Append("Invalid reseed number\n");
            }

            DateTime dateEntered = Convert.ToDateTime(reseedGridView.CurrentRow.Cells["reseedDate"].Value);
            if (dateEntered == null) {
                reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText = "Date can't be null\n";
                message.Append(reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText);
            } 
            else if (dateEntered > DateTime.Now) {
                reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText = "Date can't be in the future\n";
                message.Append(reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText);
            } 
            else if (dateEntered < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText = "Date can't be earlier than 30 days back\n";
                message.Append(reseedGridView.CurrentRow.Cells["reseedDate"].ErrorText);
            } 
            else {
                bag.ReSeedDate = Convert.ToDateTime(reseedGridView.CurrentRow.Cells["reseedDate"].Value);
            }

            if (message.Length > 0) {
                reseedGridView.CurrentRow.ErrorText = message.ToString();
                return null;
            }
            bag.Notes = reseedGridView.CurrentRow.Cells["notes"].Value != null ? reseedGridView.CurrentRow.Cells["notes"].Value.ToString() : string.Empty;
            return bag;
        }

        /// <summary>
        /// Get the current bag row without validating its fields
        /// </summary>
        /// <returns></returns>
        private ReseedModel GetSelectedBagRowNoValidation() {

            ReseedModel notValidatedBag = new ReseedModel();

            if (reseedGridView.CurrentRow.Cells["EntryNo"].Value != null) {
                notValidatedBag.UniqueIndentifier = Convert.ToUInt32(reseedGridView.CurrentRow.Cells["EntryNo"].Value);
                notValidatedBag.ReSeedDate = Convert.ToDateTime(reseedGridView.CurrentRow.Cells["reseedDate"].Value);
                notValidatedBag.BagNo = Convert.ToUInt32(reseedGridView.CurrentRow.Cells["bagNo"].Value);
                notValidatedBag.OriginalSite = Convert.ToUInt32(reseedGridView.CurrentRow.Cells["originalSite"].Value);
                notValidatedBag.SiteId = Convert.ToUInt16(MainController.SITE_ID);
                notValidatedBag.Notes = reseedGridView.CurrentRow.Cells["notes"].Value != null ? reseedGridView.CurrentRow.Cells["notes"].Value.ToString() : string.Empty;
            }
            return notValidatedBag;
        }

        private void AddReseedToGrid(ReseedModel bag_) {
            reseedGridView.Rows.Add(bag_.UniqueIndentifier, bag_.ReSeedDate, bag_.BagNo, bag_.OriginalSite, bag_.Notes);
        }

        private void UpdateReseedInGrid(ReseedModel bag_) {

            reseedGridView.CurrentRow.Cells[0].Value = bag_.UniqueIndentifier;
            reseedGridView.CurrentRow.Cells[1].Value = bag_.ReSeedDate;
            reseedGridView.CurrentRow.Cells[2].Value = bag_.BagNo;
            reseedGridView.CurrentRow.Cells[3].Value = bag_.OriginalSite;
            //reseedGridView.CurrentRow.Cells[4].Value = bag_.SiteId;
            reseedGridView.CurrentRow.Cells[4].Value = bag_.Notes;
        }

        private void CloseWindowButton_Click(object sender, EventArgs e) {
            this?.Dispose();
            Application.Exit();
        }

        #endregion

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
            reseedGridView.Sort(reseedGridView.Columns[0], System.ComponentModel.ListSortDirection.Descending);
        }

        private void BagGridView_Paint(object sender, PaintEventArgs e) {

        }

        private void BagGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            ReseedModel originalReseed = GetSelectedBagRowNoValidation();
            if(originalReseed == null) {
                return;
            }

            FormUpdateReseed updateForm = new FormUpdateReseed(originalReseed);
            updateForm.TopLevel = true;
            DialogResult result = DialogResult.Retry;
            result = updateForm.ShowDialog(this);

            if (result == DialogResult.OK) {
                UpdateReseedInGrid(updateForm.UpdatedReseed);
            }
            updateForm.Close();
        }

        private void BagGridView_MouseHover(object sender, EventArgs e) {
            toolTip1.SetToolTip(reseedGridView, "Double click on line to edit bag.");
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.Active = true;
        }

        private void FormReseed_FormClosing(object sender, FormClosingEventArgs e) {
            wait?.Dispose();
        }
    }
}
