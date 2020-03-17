using System.Windows.Forms;

namespace Bagging.BagEdit {
    partial class FormUpdateReseed {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateReseed));
            this.reseedDatePicker = new System.Windows.Forms.DateTimePicker();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.productDatelabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.siteIdTextBox = new System.Windows.Forms.TextBox();
            this.bagNumberTextBox = new System.Windows.Forms.TextBox();
            this.entryNoTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.originalSitetextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // reseedDatePicker
            // 
            this.reseedDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.reseedDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.reseedDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reseedDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.reseedDatePicker.Location = new System.Drawing.Point(265, 372);
            this.reseedDatePicker.Name = "reseedDatePicker";
            this.reseedDatePicker.Size = new System.Drawing.Size(419, 44);
            this.reseedDatePicker.TabIndex = 4;
            this.reseedDatePicker.Value = new System.DateTime(2020, 1, 25, 23, 59, 59, 0);
            this.reseedDatePicker.ValueChanged += new System.EventHandler(this.ReseedDatePicker_ValueChanged);
            // 
            // notesTextBox
            // 
            this.notesTextBox.AcceptsTab = true;
            this.notesTextBox.AllowDrop = true;
            this.notesTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.notesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesTextBox.Location = new System.Drawing.Point(262, 443);
            this.notesTextBox.MaxLength = 300;
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(544, 139);
            this.notesTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.CausesValidation = false;
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(465, 614);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 83);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseCompatibleTextRendering = true;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(655, 614);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 83);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseCompatibleTextRendering = true;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(303, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 44);
            this.label1.TabIndex = 10;
            this.label1.Text = "Update Reseed";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // productDatelabel
            // 
            this.productDatelabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.productDatelabel.AutoSize = true;
            this.productDatelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productDatelabel.Location = new System.Drawing.Point(56, 372);
            this.productDatelabel.Name = "productDatelabel";
            this.productDatelabel.Size = new System.Drawing.Size(204, 37);
            this.productDatelabel.TabIndex = 5;
            this.productDatelabel.Text = "Reseed date:";
            this.productDatelabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(75, 443);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 37);
            this.label6.TabIndex = 5;
            this.label6.Text = "Comments:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label7.Location = new System.Drawing.Point(69, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 37);
            this.label7.TabIndex = 9;
            this.label7.Text = "Bag number: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(69, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 37);
            this.label3.TabIndex = 12;
            this.label3.Text = "Site number: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // siteIdTextBox
            // 
            this.siteIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.siteIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteIdTextBox.Location = new System.Drawing.Point(269, 252);
            this.siteIdTextBox.Name = "siteIdTextBox";
            this.siteIdTextBox.Size = new System.Drawing.Size(100, 44);
            this.siteIdTextBox.TabIndex = 2;
            // 
            // bagNumberTextBox
            // 
            this.bagNumberTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bagNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bagNumberTextBox.Location = new System.Drawing.Point(269, 312);
            this.bagNumberTextBox.Name = "bagNumberTextBox";
            this.bagNumberTextBox.Size = new System.Drawing.Size(100, 44);
            this.bagNumberTextBox.TabIndex = 3;
            // 
            // entryNoTextBox
            // 
            this.entryNoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.entryNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.entryNoTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryNoTextBox.Location = new System.Drawing.Point(467, 129);
            this.entryNoTextBox.Name = "entryNoTextBox";
            this.entryNoTextBox.ReadOnly = true;
            this.entryNoTextBox.Size = new System.Drawing.Size(100, 37);
            this.entryNoTextBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(243, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 37);
            this.label2.TabIndex = 15;
            this.label2.Text = "Entry number:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // originalSitetextBox
            // 
            this.originalSitetextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.originalSitetextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalSitetextBox.Location = new System.Drawing.Point(270, 193);
            this.originalSitetextBox.Name = "originalSitetextBox";
            this.originalSitetextBox.ReadOnly = true;
            this.originalSitetextBox.Size = new System.Drawing.Size(100, 44);
            this.originalSitetextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.Location = new System.Drawing.Point(61, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 37);
            this.label4.TabIndex = 18;
            this.label4.Text = "Original site:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.Image = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.InitialImage = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // FormUpdateReseed
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(844, 704);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.originalSitetextBox);
            this.Controls.Add(this.entryNoTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bagNumberTextBox);
            this.Controls.Add(this.siteIdTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.productDatelabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.reseedDatePicker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(866, 760);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(866, 760);
            this.Name = "FormUpdateReseed";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Reseed";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateBagForm_FormClosing);
            this.Load += new System.EventHandler(this.UpdateReseedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DateTimePicker reseedDatePicker;
        internal TextBox notesTextBox;
        private Button saveButton;
        private Button cancelButton;
        private Label label1;
        private Label productDatelabel;
        private Label label6;
        private ToolTip toolTip1;
        private Label label7;
        private Label label3;
        private TextBox siteIdTextBox;
        private TextBox bagNumberTextBox;
        private TextBox entryNoTextBox;
        private Label label2;
        private TextBox originalSitetextBox;
        private Label label4;
        private PictureBox pictureBox2;
    }
}