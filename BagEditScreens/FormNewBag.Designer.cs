using System.Windows.Forms;

namespace Bagging.BagEdit {
    partial class FormNewBag {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewBag));
            this.prodDatePicker = new System.Windows.Forms.DateTimePicker();
            this.weightTextBox = new System.Windows.Forms.TextBox();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.productDatelabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.qualityFlagNameComboBox = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.prodTypeComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // prodDatePicker
            // 
            this.prodDatePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.prodDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.prodDatePicker.Location = new System.Drawing.Point(262, 122);
            this.prodDatePicker.Name = "prodDatePicker";
            this.prodDatePicker.Size = new System.Drawing.Size(416, 44);
            this.prodDatePicker.TabIndex = 1;
            this.prodDatePicker.ValueChanged += new System.EventHandler(this.ProdDatePicker_ValueChanged);
            // 
            // weightTextBox
            // 
            this.weightTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightTextBox.Location = new System.Drawing.Point(262, 275);
            this.weightTextBox.MaxLength = 5;
            this.weightTextBox.Name = "weightTextBox";
            this.weightTextBox.Size = new System.Drawing.Size(186, 44);
            this.weightTextBox.TabIndex = 3;
            this.weightTextBox.Text = "0";
            this.weightTextBox.WordWrap = false;
            this.weightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WeightTextBox_KeyPress);
            this.weightTextBox.Leave += new System.EventHandler(this.WeightTextBox_Leave);
            // 
            // notesTextBox
            // 
            this.notesTextBox.AcceptsTab = true;
            this.notesTextBox.AllowDrop = true;
            this.notesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesTextBox.Location = new System.Drawing.Point(262, 442);
            this.notesTextBox.MaxLength = 300;
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesTextBox.Size = new System.Drawing.Size(544, 139);
            this.notesTextBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(400, 618);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 83);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseCompatibleTextRendering = true;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(604, 618);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(202, 83);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseCompatibleTextRendering = true;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(368, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "New Bag";
            // 
            // productDatelabel
            // 
            this.productDatelabel.AutoSize = true;
            this.productDatelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productDatelabel.Location = new System.Drawing.Point(158, 125);
            this.productDatelabel.Name = "productDatelabel";
            this.productDatelabel.Size = new System.Drawing.Size(84, 37);
            this.productDatelabel.TabIndex = 5;
            this.productDatelabel.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 37);
            this.label3.TabIndex = 5;
            this.label3.Text = "Product Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(124, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 37);
            this.label4.TabIndex = 5;
            this.label4.Text = "Weight";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(52, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 37);
            this.label5.TabIndex = 5;
            this.label5.Text = "Quality Flag";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(69, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 37);
            this.label6.TabIndex = 5;
            this.label6.Text = "Comments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(453, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 37);
            this.label2.TabIndex = 6;
            this.label2.Text = "lbs";
            // 
            // qualityFlagNameComboBox
            // 
            this.qualityFlagNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qualityFlagNameComboBox.FormattingEnabled = true;
            this.qualityFlagNameComboBox.Location = new System.Drawing.Point(262, 352);
            this.qualityFlagNameComboBox.Name = "qualityFlagNameComboBox";
            this.qualityFlagNameComboBox.Size = new System.Drawing.Size(306, 45);
            this.qualityFlagNameComboBox.Sorted = true;
            this.qualityFlagNameComboBox.TabIndex = 4;
            this.qualityFlagNameComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProdTypeComboBox_KeyPress);
            this.qualityFlagNameComboBox.Leave += new System.EventHandler(this.QualityFlagNameComboBox_Leave);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // prodTypeComboBox
            // 
            this.prodTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodTypeComboBox.FormattingEnabled = true;
            this.prodTypeComboBox.Location = new System.Drawing.Point(264, 203);
            this.prodTypeComboBox.MaxDropDownItems = 20;
            this.prodTypeComboBox.Name = "prodTypeComboBox";
            this.prodTypeComboBox.Size = new System.Drawing.Size(184, 45);
            this.prodTypeComboBox.TabIndex = 2;
            this.prodTypeComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProdTypeComboBox_KeyPress);
            this.prodTypeComboBox.Leave += new System.EventHandler(this.ProdTypeComboBox_Leave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.Image = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.InitialImage = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox2.Location = new System.Drawing.Point(18, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // FormNewBag
            // 
            this.AcceptButton = this.saveButton;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(844, 732);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.prodTypeComboBox);
            this.Controls.Add(this.qualityFlagNameComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.productDatelabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.weightTextBox);
            this.Controls.Add(this.prodDatePicker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewBag";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewBagForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewBagForm_FormClosing);
            this.Load += new System.EventHandler(this.NewBagForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DateTimePicker prodDatePicker;
        internal TextBox weightTextBox;
        internal TextBox notesTextBox;
        internal ComboBox qualityFlagNameComboBox;
        private Button saveButton;
        private Button cancelButton;
        private Label label1;
        private Label productDatelabel;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label2;
        private ToolTip toolTip1;
        private ComboBox prodTypeComboBox;
        private PictureBox pictureBox2;
    }
}