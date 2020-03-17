using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bagging.BagEdit {
    public partial class FormUpdateBag : Form {

        private static ILog log = LogManager.GetLogger(typeof(FormUpdateBag));

        private readonly Dictionary<int, string> qualityValues;
        private string msg = string.Empty;
        public BagModel OriginalBag { get; private set; }
        public BagModel NewBag { get; private set; }

        internal FormUpdateBag(BagModel originalBag) {
            log.Debug("FormUpdateBag initializing...");
            this.OriginalBag = originalBag;
            qualityValues = EditController.GetQualityValues();
            InitializeComponent();
        }

        private async void UpdateButton_Click(object sender, EventArgs e) {

            if (!IsFormValid()) {
                MessageBox.Show($"Invalid data!\n{msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg = string.Empty;
                this.DialogResult = DialogResult.Retry;
                return;
            }
            BagModel newBag = new BagModel();
            try {
                newBag.SiteId = this.OriginalBag.SiteId;
                newBag.UniqueIndentifier = this.OriginalBag.UniqueIndentifier;
                newBag.ProductDate = this.prodDatePicker.Value;
                newBag.ProductType = this.prodTypeComboBox.SelectedItem.ToString().Substring(MainController.PRODUCT_PREFIX.Length);
                newBag.ProductWeight = Math.Round(MainController.LbsToKgs(Convert.ToDecimal(this.weightTextBox.Text)), 2);
                newBag.Notes = notesTextBox.Text != null ? this.notesTextBox.Text : string.Empty;
                newBag.QualityFlag = qualityValues.Keys.First(key => qualityValues[key] == this.qualityFlagNameComboBox.SelectedItem.ToString());
                newBag.QualityFlagName = this.qualityFlagNameComboBox.SelectedItem.ToString();

                var result = await EditController.UpdateBagAsync(OriginalBag, newBag);
                result.ProductType = $"{MainController.PRODUCT_PREFIX}{result.ProductType}";
                if (result != null && result.UniqueIndentifier > 0) {
                    MessageBox.Show("Bag Updated successfully", "Bag saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NewBag = result;
                    NewBag.ProductWeight = Math.Round(MainController.KgToLbs(NewBag.ProductWeight), 2);
                }
                else if(result != null && result.UniqueIndentifier == 0){
                    MessageBox.Show("Database offline. Bag saved to file", "Bag saved to file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NewBag = result;
                } else {
                    MessageBox.Show("Unknown error.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(FormatException ex) {
                log.Error("Error...", ex);
                MessageBox.Show(ex.Message, "Format error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) {
                log.Error("Error...", ex);
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateBagForm_Load(object sender, EventArgs e) {
            
            qualityFlagNameComboBox.Items.Clear();
            qualityFlagNameComboBox.Items.AddRange(qualityValues.Values.ToArray());

            prodTypeComboBox.Items.Clear();
            foreach (string item in MainController.PRODUCT_TYPE_LIST) {
                prodTypeComboBox.Items.Add($"{MainController.PRODUCT_PREFIX}{item}");
            }

            this.bagNumber.Text = OriginalBag.UniqueIndentifier.ToString();
            this.prodDatePicker.Value = OriginalBag.ProductDate;
            this.prodTypeComboBox.SelectedItem = OriginalBag.ProductType;
            this.prodTypeComboBox.SelectedValue = OriginalBag.ProductType;
            this.weightTextBox.Text = OriginalBag.ProductWeight.ToString();
            this.qualityFlagNameComboBox.SelectedItem = OriginalBag.QualityFlagName;
            this.qualityFlagNameComboBox.SelectedValue = OriginalBag.QualityFlagName;
            this.notesTextBox.Text = OriginalBag.Notes;

            prodDatePicker.MinDate = DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST);
        }

        private void ProdDatePicker_ValueChanged(object sender, EventArgs e) {
            DateTimePicker sender_ = sender as DateTimePicker;
            if (this.prodDatePicker.Value > DateTime.Now) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid date, it can't be a future date!");
            }
            else if (this.prodDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST) ) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid date, it can't be earlier than {MainController.DAYS_IN_THE_PAST} days back!");
                sender_.BackColor = Color.FromArgb(255, 186, 186);
            }
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, $"Date the bag was made. it can't be more than {MainController.DAYS_IN_THE_PAST} days back or a date in the future.");
                sender_.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Makes sure only numeric values are entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }else if (e.KeyChar == '.') {
                e.Handled = true;
            }
        }

        private void WeightTextBox_Leave(object sender, EventArgs e) {
            saveButton.Enabled = true;
            TextBox sender_ = sender as TextBox;
            if (string.IsNullOrEmpty(sender_.Text) || string.IsNullOrWhiteSpace(sender_.Text)){
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Weight field can't be blank");
                SetColorBad(sender_);
                saveButton.Enabled = false;
            }
            else if (Convert.ToDecimal(sender_.Text) < MainController.MIN_WEIGHT || Convert.ToDecimal(sender_.Text) > MainController.MAX_WEIGHT){
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid entry.\n Weight must be between: {MainController.MIN_WEIGHT} and {MainController.MAX_WEIGHT} lbs");
                SetColorBad(sender_);

            }
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Weight in pounds (lbs)");
                SetColorGood(sender_);
            }
        }

        private void ProdTypeComboBox_Leave(object sender, EventArgs e) {
            ComboBox sender_ = sender as ComboBox;
            if (sender_.SelectedItem == null) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid product type");
                SetColorBad(sender_);
                saveButton.Enabled = false;
            } 
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Product Type");
                SetColorGood(sender_);
            }
        }

        private void SetColorBad(Control sender_) {
            sender_.ForeColor = Color.FromArgb(156, 0, 6);
            sender_.BackColor = Color.FromArgb(255, 199, 206);
        }
        private void SetColorGood(Control sender_) {
            sender_.ForeColor = Color.Black;
            sender_.BackColor = Color.White;
        }

        private void QualityFlagNameComboBox_Leave(object sender, EventArgs e) {
            ComboBox sender_ = sender as ComboBox;
            if (sender_.SelectedItem == null || string.IsNullOrEmpty(sender_.SelectedItem.ToString()) || string.IsNullOrWhiteSpace(sender_.SelectedItem.ToString())) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Select a quality flag type name");
                SetColorBad(sender_);
                saveButton.Enabled = false;
            }
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Quality flag");
                SetColorGood(sender_);
            }
        }

        private bool IsFormValid() {
            bool result = true;

            StringBuilder builder = new StringBuilder();

            if (this.prodTypeComboBox.SelectedItem == null || this.prodTypeComboBox.SelectedItem.Equals(0)) {
                this.prodTypeComboBox.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append("Product type not selected.\n");
                result = false;
            } else {
                this.prodTypeComboBox.BackColor = Color.White;
            }

            if (string.IsNullOrEmpty(this.weightTextBox.Text) || string.IsNullOrWhiteSpace(this.weightTextBox.Text) || Convert.ToDecimal(this.weightTextBox.Text) == 0) {
                this.weightTextBox.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append($"Weight field can't be blank or zero\n");
                result = false;
            } else if (Convert.ToDecimal(this.weightTextBox.Text) > MainController.MAX_WEIGHT) {
                this.weightTextBox.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append($"Weight can't be greater than {MainController.MAX_WEIGHT}\n");
                result = false;

            } else if (Convert.ToDecimal(this.weightTextBox.Text) < MainController.MIN_WEIGHT) {
                this.weightTextBox.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append($"Weight can't be lower than {MainController.MIN_WEIGHT}\n");
                result = false;
            } else {
                this.weightTextBox.BackColor = Color.White;
            }

            if (this.prodDatePicker.Value > DateTime.Now) {
                this.prodDatePicker.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append("Date can't be a future date.\n");
                result = false;
            } else if (this.prodDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                this.prodDatePicker.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append($"Date can't be a more than {MainController.DAYS_IN_THE_PAST} days back.\n");
                result = false;
            } else {
                this.prodDatePicker.BackColor = Color.White;
            }

            if (this.qualityFlagNameComboBox.SelectedItem == null) {
                this.qualityFlagNameComboBox.BackColor = Color.FromArgb(255, 186, 186);
                builder.Append("Quality flag not selected.\n");
                result = false;
            } else {
                this.qualityFlagNameComboBox.BackColor = Color.White;
            }
            msg = builder.ToString();
            return result;
        }

        private void ProdTypeComboBox_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = true;
        }

        private void QualityFlagNameComboBox_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = true;
        }

        private void WeightTextBox_TextChanged(object sender, EventArgs e) {
            TextBox sender_ = sender as TextBox;
            if (string.IsNullOrEmpty(sender_.Text) || Convert.ToDecimal(sender_.Text) < MainController.MIN_WEIGHT || Convert.ToDecimal(sender_.Text) > MainController.MAX_WEIGHT) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid entry.\n Weight must be between: {MainController.MIN_WEIGHT} and {MainController.MAX_WEIGHT} lbs");
                sender_.BackColor = Color.FromArgb(255, 186, 186);
                saveButton.Enabled = false;
            } else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Weight in pounds (lbs)");
                sender_.BackColor = Color.White;
                saveButton.Enabled = true;
            }
        }

        private void UpdateBagForm_FormClosing(object sender, FormClosingEventArgs e) {
            if(DialogResult.Retry == this.DialogResult) {
                e.Cancel = true;
            }
        }

    }
}
