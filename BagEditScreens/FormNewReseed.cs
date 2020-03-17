
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
    public partial class FormNewReseed : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormNewReseed));
        private string msg = string.Empty;
        public ReseedModel NewReseed { get; private set; }
        internal FormNewReseed() {
            log.Debug("Initializng...");
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, EventArgs e) {

            if (!IsFormValid()) {
                MessageBox.Show($"Invalid data!\n{msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg = string.Empty;
                this.DialogResult = DialogResult.Retry;
                return;
            }
            try {
                NewReseed = new ReseedModel();
                NewReseed.SiteId = MainController.SITE_ID;
                NewReseed.OriginalSite = Convert.ToUInt16(this.originalSiteTextBox.Text);
                NewReseed.BagNo = Convert.ToUInt32(this.numberTextBox.Text);
                NewReseed.ReSeedDate = this.reseddDatePicker.Value;
                NewReseed.Notes = notesTextBox.Text != null ? this.notesTextBox.Text : string.Empty;
                ReseedModel result = null;
                try {
                    result = await ReseedController.SaveNewReseedAsync(NewReseed);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex) {
                    if (ex.Message.Contains("Duplicate entry")) {
                        MessageBox.Show($"Reseed already exists\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Retry;
                        return;
                    }
                    else if (ex.Message.Contains("syntax")) {
                        MessageBox.Show($"Query error \n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Retry;
                        return;
                    }
                }
                if (result != null && result.UniqueIndentifier > 0) {
                    MessageBox.Show("Reseed saved successfully", "Reseed saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Database offline. Reseed saved to file", "Reseed saved to file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (FormatException ex) {
                log.Error("Exception thrown: ", ex);
                Console.WriteLine();
                this.DialogResult = DialogResult.Retry;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void NewReseedForm_Load(object sender, EventArgs e) {
            reseddDatePicker.MinDate = DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST);

        }

        private void ProdDatePicker_ValueChanged(object sender, EventArgs e) {
            DateTimePicker sender_ = sender as DateTimePicker;
            if (this.reseddDatePicker.Value > DateTime.Now) {
                SetColorBad(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid date, it can't be a future date!");
            }
            else if (this.reseddDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                SetColorBad(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid date, it can't be earlier than {MainController.DAYS_IN_THE_PAST} days back!");
            }
            else {
                SetColorGood(sender_);
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Date the bag was reseeded. It can't be a date in the future.");
            }
        }

        private static void SetColorBad(Control sender_) {
            sender_.ForeColor = Color.FromArgb(156, 0, 6);
            sender_.BackColor = Color.FromArgb(255, 199, 206);
        }
        private static void SetColorGood(Control sender_) {
            sender_.ForeColor = Color.Black;
            sender_.BackColor = Color.White;
        }

        private bool IsFormValid() {
            bool result = true;
            StringBuilder builder = new StringBuilder();

            if (string.IsNullOrEmpty(this.originalSiteTextBox.Text) || string.IsNullOrWhiteSpace(this.originalSiteTextBox.Text) || Convert.ToInt32(this.originalSiteTextBox.Text) == 0) {
                SetColorBad(this.originalSiteTextBox);
                builder.Append($"Original site number field can't be blank or zero.\n");
                result = false;
            }
            else {
                SetColorGood(this.originalSiteTextBox);
            }

            if (string.IsNullOrEmpty(this.numberTextBox.Text) || string.IsNullOrWhiteSpace(this.numberTextBox.Text) || Convert.ToInt32(this.numberTextBox.Text) == 0) {
                SetColorBad(this.numberTextBox);
                builder.Append($"Site number field can't be blank or zero.\n");
                result = false;
            }
            else {
                SetColorGood(this.numberTextBox);
            }


            if (this.reseddDatePicker.Value > DateTime.Now) {
                SetColorBad(this.reseddDatePicker);
                builder.Append("Date can't be a future date.\n");
                result = false;
            }
            else if (this.reseddDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                SetColorBad(this.reseddDatePicker);
                builder.Append($"Date can't be a more than {MainController.DAYS_IN_THE_PAST} days back.\n");
                result = false;
            }
            else {
                SetColorGood(this.reseddDatePicker);
            }
            msg = builder.ToString();
            return result;
        }

        private void NewReseedForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult.Retry == this.DialogResult) {
                e.Cancel = true;
            }
        }

        private void OriginalSiteTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            if (originalSiteTextBox.Text.Length > 2 && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }
        }

        /// <summary>
        /// Makes sure only numeric values are entered
        /// </summary>
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

            if (originalSiteTextBox.Text.Length > 4 && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
                return;
            }

        }

        private void OriginalSiteTextBox_TextChanged(object sender, EventArgs e) {
           // int result = Convert.ToInt32(((TextBox)sender).Text);
           // originalSiteTextBox.Text = string.Format("{0,0:000}", result);

           // Console.WriteLine(originalSiteTextBox.Text);
           // originalSiteTextBox.Select(originalSiteTextBox.Text.Length, 0);

        }

        private void NumberTextBox_TextChanged(object sender, EventArgs e) {
            //int result =  Convert.ToInt32( ((TextBox)sender).Text);
            //numberTextBox.Text = string.Format("{0,0:00000}", result);

            //Console.WriteLine(numberTextBox.Text);

            //numberTextBox.Select(numberTextBox.Text.Length, 0);
        }


    }
}
