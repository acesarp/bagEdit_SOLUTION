using Bagging.BagEdit.CustomControls;

namespace Bagging.BagEdit {
    partial class FormLabelPreview {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabelPreview));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OK = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.numberOfCopies = new System.Windows.Forms.NumericUpDown();
            this.label = new System.Windows.Forms.Label();
            this.rulerControl2 = new Bagging.BagEdit.CustomControls.RulerControl();
            this.rulerControl1 = new Bagging.BagEdit.CustomControls.RulerControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OK.Location = new System.Drawing.Point(-380, 688);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(160, 44);
            this.OK.TabIndex = 1;
            this.OK.Text = "Print";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(-197, 688);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(160, 44);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // numberOfCopies
            // 
            this.numberOfCopies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numberOfCopies.Location = new System.Drawing.Point(24, 696);
            this.numberOfCopies.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numberOfCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfCopies.Name = "numberOfCopies";
            this.numberOfCopies.ReadOnly = true;
            this.numberOfCopies.Size = new System.Drawing.Size(90, 35);
            this.numberOfCopies.TabIndex = 5;
            this.numberOfCopies.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numberOfCopies.ValueChanged += new System.EventHandler(this.NumberOfCopies_ValueChanged);
            this.numberOfCopies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOfCopies_KeyPress);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(16, 656);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(96, 29);
            this.label.TabIndex = 6;
            this.label.Text = "Copies:";
            // 
            // rulerControl2
            // 
            this.rulerControl2.ActualSize = true;
            this.rulerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rulerControl2.DivisionMarkFactor = 5;
            this.rulerControl2.Divisions = 10;
            this.rulerControl2.ForeColor = System.Drawing.Color.Black;
            this.rulerControl2.Location = new System.Drawing.Point(-43, 0);
            this.rulerControl2.MajorInterval = 10;
            this.rulerControl2.MiddleMarkFactor = 3;
            this.rulerControl2.MouseTrackingOn = false;
            this.rulerControl2.Name = "rulerControl2";
            this.rulerControl2.Orientation = Bagging.BagEdit.CustomControls.EnumOrientation.orVertical;
            this.rulerControl2.RulerAlignment = Bagging.BagEdit.CustomControls.EnumRulerAlignment.raBottomOrRight;
            this.rulerControl2.ScaleMode = Bagging.BagEdit.CustomControls.EnumScaleMode.smMillimetres;
            this.rulerControl2.Size = new System.Drawing.Size(39, 600);
            this.rulerControl2.StartValue = 0D;
            this.rulerControl2.TabIndex = 4;
            this.rulerControl2.Text = "rulerControl2";
            this.rulerControl2.VerticalNumbers = false;
            this.rulerControl2.ZoomFactor = 1D;
            // 
            // rulerControl1
            // 
            this.rulerControl1.ActualSize = true;
            this.rulerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rulerControl1.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.rulerControl1.DivisionMarkFactor = 5;
            this.rulerControl1.Divisions = 10;
            this.rulerControl1.ForeColor = System.Drawing.Color.Black;
            this.rulerControl1.Location = new System.Drawing.Point(0, 600);
            this.rulerControl1.MajorInterval = 10;
            this.rulerControl1.MiddleMarkFactor = 3;
            this.rulerControl1.MouseTrackingOn = false;
            this.rulerControl1.Name = "rulerControl1";
            this.rulerControl1.Orientation = Bagging.BagEdit.CustomControls.EnumOrientation.orHorizontal;
            this.rulerControl1.RulerAlignment = Bagging.BagEdit.CustomControls.EnumRulerAlignment.raBottomOrRight;
            this.rulerControl1.ScaleMode = Bagging.BagEdit.CustomControls.EnumScaleMode.smMillimetres;
            this.rulerControl1.Size = new System.Drawing.Size(0, 35);
            this.rulerControl1.StartValue = 0D;
            this.rulerControl1.TabIndex = 3;
            this.rulerControl1.Text = "rulerControl1";
            this.rulerControl1.VerticalNumbers = true;
            this.rulerControl1.ZoomFactor = 1D;
            // 
            // FormLabelPreview
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(0, 744);
            this.Controls.Add(this.label);
            this.Controls.Add(this.numberOfCopies);
            this.Controls.Add(this.rulerControl2);
            this.Controls.Add(this.rulerControl1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLabelPreview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LabelPreview";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LabelPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button cancelButton;
        private RulerControl rulerControl1;
        private RulerControl rulerControl2;
        private System.Windows.Forms.NumericUpDown numberOfCopies;
        private System.Windows.Forms.Label label;
    }
}