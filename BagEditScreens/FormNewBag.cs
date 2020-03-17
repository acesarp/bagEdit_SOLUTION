
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Bagging.Controller.Controllers;
using Bagging.Controller.Models;
using log4net;

namespace Bagging.BagEdit {
    public partial class FormNewBag : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormNewBag));

        private string msg = string.Empty;
        private readonly string qualityPlaceHolder = "-Enter quality-";
        public BagModel NewBag { get; private set; }
        Dictionary<int, string> qualityValues;

        internal FormNewBag() {
            InitializeComponent();
            qualityValues = EditController.GetQualityValues();
        }

        private async void SaveButton_Click(object sender, EventArgs e) {
            Console.WriteLine(prodDatePicker.Value);
            if (!IsFormValid()) {
                MessageBox.Show($"Invalid data!\n{msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg = string.Empty;
                this.DialogResult = DialogResult.Retry;
                return;
            }
            BagModel newBag = new BagModel();
            try {
                newBag = new BagModel();
                newBag.SiteId = MainController.SITE_ID;
                newBag.ProductDate = this.prodDatePicker.Value;
                newBag.ProductType = this.prodTypeComboBox.SelectedItem.ToString().Substring(MainController.PRODUCT_PREFIX.Length);
                newBag.ProductWeight = Math.Round(MainController.LbsToKgs(Convert.ToDecimal(this.weightTextBox.Text)), 2);
                newBag.Notes = notesTextBox.Text != null ? this.notesTextBox.Text : string.Empty;
                newBag.QualityFlag = EditController.GetQualityValues().Keys.First(key => qualityValues[key] == this.qualityFlagNameComboBox.SelectedItem.ToString());
                newBag.QualityFlagName = this.qualityFlagNameComboBox.SelectedItem.ToString();

                var result = await EditController.SaveNewBagAsync(newBag);
                result.ProductType = $"{MainController.PRODUCT_PREFIX}{result.ProductType}";

                if (result != null && result.UniqueIndentifier > 0) {
                    NewBag = result;
                    NewBag.ProductWeight = Math.Round(MainController.KgToLbs(NewBag.ProductWeight), 2);
                    MessageBox.Show("Bag saved successfully", "Bag saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Database offline. Bag saved to file", "Bag saved to file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(FormatException ex) {
                MessageBox.Show(ex.Message, "Format error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void NewBagForm_Load(object sender, EventArgs e) {

            qualityFlagNameComboBox.Items.Clear();
            qualityFlagNameComboBox.Text = qualityPlaceHolder;
            qualityFlagNameComboBox.Items.AddRange(qualityValues.Values.ToArray());
            qualityFlagNameComboBox.SelectedItem = qualityValues.Values.ToArray()[0];

            prodTypeComboBox.Items.Clear();
            foreach (string item in MainController.PRODUCT_TYPE_LIST) {
                prodTypeComboBox.Items.Add($"{MainController.PRODUCT_PREFIX}{item}");
            }
            prodTypeComboBox.SelectedItem = prodTypeComboBox.Items[0];

            prodDatePicker.MinDate = DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST);
        }

        private void ProdDatePicker_ValueChanged(object sender, EventArgs e) {
            DateTimePicker sender_ = sender as DateTimePicker;
            if (this.prodDatePicker.Value > DateTime.Now) {
                SetColorBad(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid date, it can't be a future date!");
            }
            else if (this.prodDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST) ) {
                SetColorBad(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid date, it can't be earlier than {MainController.DAYS_IN_THE_PAST} days back!");
            }
            else {
                SetColorGood(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, $"Date the bag was made. it can't be more than {MainController.DAYS_IN_THE_PAST} days back or a date in the future.");
            }
        }

        /// <summary>
        /// Makes sure only numeric values are entered
        /// </summary>
        private void WeightTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
            else if (e.KeyChar == '.') {
                e.Handled = true;
            }
        }


        private void WeightTextBox_Leave(object sender, EventArgs e) {
            TextBox sender_ = sender as TextBox;
            if (string.IsNullOrEmpty(sender_.Text) || string.IsNullOrWhiteSpace(sender_.Text)){
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Weight field can't be blank");
                SetColorBad(sender_);
            }
            else if(Convert.ToDecimal(sender_.Text) < MainController.MIN_WEIGHT || Convert.ToDecimal(sender_.Text) > MainController.MAX_WEIGHT) {
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

        private void SetColorBad(Control sender_) {
            sender_.ForeColor = Color.FromArgb(156, 0, 6);
            sender_.BackColor = Color.FromArgb(255, 199, 206);
        }
        private void SetColorGood(Control sender_) {
            sender_.ForeColor = Color.Black;
            sender_.BackColor = Color.White;
        }

        private void ProdTypeComboBox_Leave(object sender, EventArgs e) {
            ComboBox sender_ = sender as ComboBox;
            if (string.IsNullOrEmpty(sender_.SelectedItem.ToString())) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid product type");
                SetColorBad(sender_);
            } else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Product Type");
                SetColorGood(sender_);
            }
        }

        private void QualityFlagNameComboBox_Leave(object sender, EventArgs e) {
            ComboBox sender_ = sender as ComboBox;
            if (sender_.SelectedItem == null || string.IsNullOrEmpty(sender_.SelectedItem.ToString()) || string.IsNullOrWhiteSpace(sender_.SelectedItem.ToString()) || sender_.SelectedItem.ToString() == qualityPlaceHolder) {
                toolTip1.SetToolTip(sender_, "Select a quality flag type name");
                SetColorBad(sender_);
            }
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                SetColorGood(sender_);
            }
        }

        private bool IsFormValid() {
            bool result = true;
            StringBuilder builder = new StringBuilder();

            if (this.prodTypeComboBox.SelectedItem == null || this.prodTypeComboBox.SelectedItem.Equals(0)) {
                SetColorBad(this.prodTypeComboBox);
                builder.Append("Product type not selected.\n");
                result = false;
            }
            else {
                SetColorGood(this.prodTypeComboBox);
            }

            if (string.IsNullOrEmpty(this.weightTextBox.Text) || string.IsNullOrWhiteSpace(this.weightTextBox.Text) || Convert.ToDecimal(this.weightTextBox.Text) == 0) {
                builder.Append($"Weight field can't be blank or zero\n");
                SetColorBad(weightTextBox);
                result = false;
            } else if(Convert.ToDecimal(this.weightTextBox.Text) > MainController.MAX_WEIGHT) {
                SetColorBad(this.weightTextBox);
                builder.Append($"Weight can't be greater than {MainController.MAX_WEIGHT}\n");
                result = false;

            } 
            else if (Convert.ToDecimal(this.weightTextBox.Text) < MainController.MIN_WEIGHT) {
                SetColorBad(weightTextBox);
                builder.Append($"Weight can't be lower than {MainController.MIN_WEIGHT}\n");
                result = false;
            } else {
                SetColorGood(this.weightTextBox);
            }

            if(this.prodDatePicker.Value > DateTime.Now) {
                SetColorBad(weightTextBox);
                builder.Append("Date can't be a future date.\n");
                result = false;
            }
            else if (this.prodDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                SetColorBad(weightTextBox);
                builder.Append($"Date can't be a more than {MainController.DAYS_IN_THE_PAST} days back.\n");
                result = false;
            } 
            else {
                SetColorGood(this.weightTextBox);
            }

            if (this.qualityFlagNameComboBox.SelectedItem == null) {
                SetColorBad(this.qualityFlagNameComboBox);
                builder.Append("Quality flag not selected.\n");
                result = false;
            } else {
                SetColorGood(this.weightTextBox);
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

        private void NewBagForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult.Retry == this.DialogResult) {
                e.Cancel = true;
            }
        }
    }
}
