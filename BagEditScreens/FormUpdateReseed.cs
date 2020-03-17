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
    public partial class FormUpdateReseed : Form {
        private static ILog log = LogManager.GetLogger(typeof(FormUpdateReseed));

        private string msg = string.Empty;
        public ReseedModel OriginalReseed { get; private set; }
        public ReseedModel UpdatedReseed { get; private set; }

        internal FormUpdateReseed(ReseedModel originalReseed) {
            this.OriginalReseed = originalReseed;
            InitializeComponent();
        }

        private async void UpdateButton_Click(object sender, EventArgs e) {

            if (!IsFormValid()) {
                MessageBox.Show($"Invalid data!\n{msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg = string.Empty;
                this.DialogResult = DialogResult.Retry;
                return;
            }
            UpdatedReseed = new ReseedModel();
            try {
                UpdatedReseed.UniqueIndentifier = this.OriginalReseed.UniqueIndentifier;
                UpdatedReseed.SiteId = this.OriginalReseed.SiteId;
                UpdatedReseed.OriginalSite = this.OriginalReseed.OriginalSite;
                UpdatedReseed.BagNo = this.OriginalReseed.BagNo;
                UpdatedReseed.ReSeedDate = this.reseedDatePicker.Value;
                UpdatedReseed.Notes = notesTextBox.Text != null ? this.notesTextBox.Text : string.Empty;

                var result = await ReseedController.UpdateReseedAsync(OriginalReseed, UpdatedReseed);
                if (result != null && result.BagNo > 0) {
                    MessageBox.Show("Reseed Updated successfully", "Reseed saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdatedReseed = result;
                }
                else if(result != null && result.UniqueIndentifier == 0){
                    MessageBox.Show("Database offline. Bag saved to file", "Bag saved to file.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdatedReseed = result;
                } else {
                    MessageBox.Show("Unknown error.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(FormatException ex) {
                MessageBox.Show(ex.Message, "Format error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateReseedForm_Load(object sender, EventArgs e) {
            this.entryNoTextBox.Text = OriginalReseed.UniqueIndentifier.ToString();
            this.bagNumberTextBox.Text = OriginalReseed.BagNo.ToString();
            this.originalSitetextBox.Text = OriginalReseed.OriginalSite.ToString();
            this.reseedDatePicker.Value = OriginalReseed.ReSeedDate;
            this.siteIdTextBox.Text = OriginalReseed.SiteId.ToString();
            this.notesTextBox.Text = OriginalReseed.Notes;
        }

        private void ReseedDatePicker_ValueChanged(object sender, EventArgs e) {
            DateTimePicker sender_ = sender as DateTimePicker;
            if (this.reseedDatePicker.Value > DateTime.Now) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, "Invalid date, it can't be a future date!");
            }
            else if (this.reseedDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST) ) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.SetToolTip(sender_, $"Invalid date, it can't be earlier than {MainController.DAYS_IN_THE_PAST} days back!");
                sender_.BackColor = Color.FromArgb(255, 186, 186);
            }
            else {
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                toolTip1.SetToolTip(sender_, "Date the bag was made.");
                sender_.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Makes sure only numeric values are entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }else if (e.KeyChar == '.') {
                e.Handled = true;
            }
        }

        private bool IsFormValid() {
            bool result = true;

            StringBuilder builder = new StringBuilder();

            if (string.IsNullOrEmpty(this.originalSitetextBox.Text) || string.IsNullOrWhiteSpace(this.originalSitetextBox.Text) || Convert.ToInt32(this.originalSitetextBox.Text) == 0) {
                SetColorBad(this.originalSitetextBox);
                builder.Append($"Original site ID field can't be blank or zero\n");
                result = false;
            } 
            else {
                SetColorGood(this.originalSitetextBox);
            }

            if (string.IsNullOrEmpty(this.siteIdTextBox.Text) || string.IsNullOrWhiteSpace(this.siteIdTextBox.Text) || Convert.ToInt32(this.siteIdTextBox.Text) == 0) {
                SetColorBad(this.siteIdTextBox);
                builder.Append($"Site ID field can't be blank or zero.\n");
                result = false;
            } 
            else {
                SetColorGood(this.siteIdTextBox);
            }

            if (string.IsNullOrEmpty(this.bagNumberTextBox.Text) || string.IsNullOrWhiteSpace(this.bagNumberTextBox.Text) || Convert.ToInt32(this.bagNumberTextBox.Text) == 0) {
                SetColorBad(this.bagNumberTextBox);
                builder.Append($"Bag number field can't be blank or zero.\n");
                result = false;
            } 
            else {
                SetColorGood(this.siteIdTextBox);
            }

            if (this.reseedDatePicker.Value > DateTime.Now) {
                SetColorBad(this.reseedDatePicker);
                builder.Append("Date can't be a future date.\n");
                result = false;
            } 
            else if (this.reseedDatePicker.Value < DateTime.Now.AddDays(-1 * MainController.DAYS_IN_THE_PAST)) {
                SetColorBad(this.reseedDatePicker);
                builder.Append($"Date can't be a more than {MainController.DAYS_IN_THE_PAST} days back.\n");
                result = false;
            } 
            else {
                SetColorGood(this.reseedDatePicker);
            }
            msg = builder.ToString();
            return result;
        }

        private void SetColorBad(Control sender_) {
            sender_.ForeColor = Color.FromArgb(156, 0, 6);
            sender_.BackColor = Color.FromArgb(255, 199, 206);
        }
        private void SetColorGood(Control sender_) {
            sender_.ForeColor = Color.Black;
            sender_.BackColor = Color.White;
        }

        private void UpdateBagForm_FormClosing(object sender, FormClosingEventArgs e) {
            if(DialogResult.Retry == this.DialogResult) {
                e.Cancel = true;
            }
        }

    }
}
