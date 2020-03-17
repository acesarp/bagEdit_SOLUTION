using System;
using System.ComponentModel;
using System.Globalization;
using Bagging.BagEdit.CustomControls;

namespace Bagging.BagEdit {
    partial class FormBagEdit {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBagEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PopulateGridFromDBWorker = new System.ComponentModel.BackgroundWorker();
            this.titleLabel = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.percentageLabel1 = new System.Windows.Forms.Label();
            this.statusLabel1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closeWindowButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.reloadListButton = new System.Windows.Forms.Button();
            this.SaveBagButton = new System.Windows.Forms.Button();
            this.addNewBagButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.bagGridView = new Bagging.BagEdit.CustomControls.MyDataGridView();
            this.bagNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productDate = new Bagging.BagEdit.CustomControls.CalendarColumn();
            this.productType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.productWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualityFlagName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bagGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // PopulateGridFromDBWorker
            // 
            this.PopulateGridFromDBWorker.WorkerReportsProgress = true;
            this.PopulateGridFromDBWorker.WorkerSupportsCancellation = true;
            this.PopulateGridFromDBWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PopulateGridFromDB_DoWork);
            this.PopulateGridFromDBWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.PopulateGridFromDB_ProgressChanged);
            this.PopulateGridFromDBWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PopulateGridFromDB_RunWorkerCompleted);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(186, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(255, 65);
            this.titleLabel.TabIndex = 15;
            this.titleLabel.Text = "Bag Edit";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.updateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.updateButton.Location = new System.Drawing.Point(183, 7);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(248, 80);
            this.updateButton.TabIndex = 18;
            this.updateButton.Text = "Update Selected Bag";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Visible = false;
            // 
            // percentageLabel1
            // 
            this.percentageLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.percentageLabel1.AutoSize = true;
            this.percentageLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.percentageLabel1.Location = new System.Drawing.Point(2905, -3);
            this.percentageLabel1.MinimumSize = new System.Drawing.Size(80, 0);
            this.percentageLabel1.Name = "percentageLabel1";
            this.percentageLabel1.Size = new System.Drawing.Size(80, 29);
            this.percentageLabel1.TabIndex = 23;
            this.percentageLabel1.Text = "%";
            this.percentageLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel1
            // 
            this.statusLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.statusLabel1.AutoSize = true;
            this.statusLabel1.Location = new System.Drawing.Point(1421, 0);
            this.statusLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.statusLabel1.MaximumSize = new System.Drawing.Size(150, 29);
            this.statusLabel1.MinimumSize = new System.Drawing.Size(150, 29);
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(150, 29);
            this.statusLabel1.TabIndex = 24;
            this.statusLabel1.Text = "status";
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Controls.Add(this.closeWindowButton);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.printButton);
            this.panel1.Controls.Add(this.reloadListButton);
            this.panel1.Controls.Add(this.SaveBagButton);
            this.panel1.Controls.Add(this.addNewBagButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1574, 100);
            this.panel1.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Bagging.BagEdit.Properties.Resources.ostara_o_logo;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // closeWindowButton
            // 
            this.closeWindowButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.closeWindowButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeWindowButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeWindowButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.closeWindowButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.closeWindowButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.closeWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.closeWindowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeWindowButton.Image = ((System.Drawing.Image)(resources.GetObject("closeWindowButton.Image")));
            this.closeWindowButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.closeWindowButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.closeWindowButton.Location = new System.Drawing.Point(1352, 7);
            this.closeWindowButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeWindowButton.MaximumSize = new System.Drawing.Size(218, 80);
            this.closeWindowButton.MinimumSize = new System.Drawing.Size(218, 80);
            this.closeWindowButton.Name = "closeWindowButton";
            this.closeWindowButton.Size = new System.Drawing.Size(218, 80);
            this.closeWindowButton.TabIndex = 5;
            this.closeWindowButton.Text = "Close";
            this.closeWindowButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.closeWindowButton.UseCompatibleTextRendering = true;
            this.closeWindowButton.UseVisualStyleBackColor = true;
            this.closeWindowButton.Click += new System.EventHandler(this.CloseWindowButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.BackColor = System.Drawing.SystemColors.Control;
            this.printButton.Enabled = false;
            this.printButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.printButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.printButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.printButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.Image = ((System.Drawing.Image)(resources.GetObject("printButton.Image")));
            this.printButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.printButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.printButton.Location = new System.Drawing.Point(1123, 7);
            this.printButton.Margin = new System.Windows.Forms.Padding(0);
            this.printButton.MaximumSize = new System.Drawing.Size(218, 80);
            this.printButton.MinimumSize = new System.Drawing.Size(218, 80);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(218, 80);
            this.printButton.TabIndex = 3;
            this.printButton.Text = "Print";
            this.printButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.printButton.UseCompatibleTextRendering = true;
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // reloadListButton
            // 
            this.reloadListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadListButton.BackColor = System.Drawing.SystemColors.Control;
            this.reloadListButton.Enabled = false;
            this.reloadListButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.reloadListButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.reloadListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reloadListButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reloadListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reloadListButton.Image = ((System.Drawing.Image)(resources.GetObject("reloadListButton.Image")));
            this.reloadListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.reloadListButton.Location = new System.Drawing.Point(896, 7);
            this.reloadListButton.Margin = new System.Windows.Forms.Padding(0);
            this.reloadListButton.MaximumSize = new System.Drawing.Size(218, 80);
            this.reloadListButton.MinimumSize = new System.Drawing.Size(218, 80);
            this.reloadListButton.Name = "reloadListButton";
            this.reloadListButton.Size = new System.Drawing.Size(218, 80);
            this.reloadListButton.TabIndex = 4;
            this.reloadListButton.Text = "Reload";
            this.reloadListButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.reloadListButton.UseCompatibleTextRendering = true;
            this.reloadListButton.UseVisualStyleBackColor = true;
            this.reloadListButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SaveBagButton
            // 
            this.SaveBagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveBagButton.BackColor = System.Drawing.SystemColors.Control;
            this.SaveBagButton.Enabled = false;
            this.SaveBagButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveBagButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.SaveBagButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveBagButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveBagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.SaveBagButton.Image = global::Bagging.BagEdit.Properties.Resources.save_icon;
            this.SaveBagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveBagButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SaveBagButton.Location = new System.Drawing.Point(444, 7);
            this.SaveBagButton.Margin = new System.Windows.Forms.Padding(0);
            this.SaveBagButton.Name = "SaveBagButton";
            this.SaveBagButton.Size = new System.Drawing.Size(211, 78);
            this.SaveBagButton.TabIndex = 2;
            this.SaveBagButton.Text = "Save";
            this.SaveBagButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SaveBagButton.UseCompatibleTextRendering = true;
            this.SaveBagButton.UseVisualStyleBackColor = true;
            this.SaveBagButton.Visible = false;
            this.SaveBagButton.Click += new System.EventHandler(this.SaveUpdateBagButton_Click);
            // 
            // addNewBagButton
            // 
            this.addNewBagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewBagButton.BackColor = System.Drawing.SystemColors.Control;
            this.addNewBagButton.Enabled = false;
            this.addNewBagButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.addNewBagButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.addNewBagButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.addNewBagButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addNewBagButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewBagButton.Image = global::Bagging.BagEdit.Properties.Resources.bag64pxTransp;
            this.addNewBagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addNewBagButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addNewBagButton.Location = new System.Drawing.Point(664, 7);
            this.addNewBagButton.Margin = new System.Windows.Forms.Padding(0);
            this.addNewBagButton.MaximumSize = new System.Drawing.Size(218, 80);
            this.addNewBagButton.MinimumSize = new System.Drawing.Size(218, 80);
            this.addNewBagButton.Name = "addNewBagButton";
            this.addNewBagButton.Size = new System.Drawing.Size(218, 80);
            this.addNewBagButton.TabIndex = 1;
            this.addNewBagButton.Text = "Add New Bag";
            this.addNewBagButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.addNewBagButton.UseCompatibleTextRendering = true;
            this.addNewBagButton.UseVisualStyleBackColor = true;
            this.addNewBagButton.Click += new System.EventHandler(this.AddNewBagButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.statusLabel1);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.percentageLabel1);
            this.panel2.Location = new System.Drawing.Point(2, 1142);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.MaximumSize = new System.Drawing.Size(0, 27);
            this.panel2.MinimumSize = new System.Drawing.Size(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1574, 27);
            this.panel2.TabIndex = 23;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.progressBar.ForeColor = System.Drawing.Color.Green;
            this.progressBar.Location = new System.Drawing.Point(712, 5);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(700, 19);
            this.progressBar.Step = 5;
            this.progressBar.TabIndex = 16;
            // 
            // bagGridView
            // 
            this.bagGridView.AllowUserToAddRows = false;
            this.bagGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bagGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.bagGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bagGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.bagGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bagGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.bagGridView.ColumnHeadersHeight = 40;
            this.bagGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.bagGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bagNo,
            this.productDate,
            this.productType,
            this.productWeight,
            this.qualityFlagName,
            this.notes});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bagGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.bagGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bagGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.bagGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.bagGridView.Location = new System.Drawing.Point(2, 102);
            this.bagGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bagGridView.MultiSelect = false;
            this.bagGridView.Name = "bagGridView";
            this.bagGridView.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bagGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.bagGridView.RowHeadersWidth = 50;
            this.bagGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bagGridView.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.bagGridView.RowTemplate.Height = 28;
            this.bagGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bagGridView.Size = new System.Drawing.Size(1574, 1067);
            this.bagGridView.TabIndex = 12;
            this.bagGridView.EditModeChanged += new System.EventHandler(this.BagGridView_EditModeChanged);
            this.bagGridView.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.BagGridView_CancelRowEdit);
            this.bagGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BagGridView_CellDoubleClick);
            this.bagGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.BagGridView_DataError);
            this.bagGridView.MouseHover += new System.EventHandler(this.BagGridView_MouseHover);
            // 
            // bagNo
            // 
            this.bagNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bagNo.DataPropertyName = "bagNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = "-";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bagNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.bagNo.DividerWidth = 2;
            this.bagNo.FillWeight = 18F;
            this.bagNo.Frozen = true;
            this.bagNo.HeaderText = "Bag Number";
            this.bagNo.MaxInputLength = 5000;
            this.bagNo.MinimumWidth = 180;
            this.bagNo.Name = "bagNo";
            this.bagNo.ReadOnly = true;
            this.bagNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bagNo.Width = 180;
            // 
            // productDate
            // 
            this.productDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.productDate.DataPropertyName = "product Date";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.productDate.DividerWidth = 2;
            this.productDate.FillWeight = 65F;
            this.productDate.Frozen = true;
            this.productDate.HeaderText = "Product Date";
            this.productDate.MinimumWidth = 280;
            this.productDate.Name = "productDate";
            this.productDate.ReadOnly = true;
            this.productDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.productDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.productDate.Width = 280;
            // 
            // productType
            // 
            this.productType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.productType.DataPropertyName = "product Type";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productType.DefaultCellStyle = dataGridViewCellStyle5;
            this.productType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.productType.DividerWidth = 2;
            this.productType.FillWeight = 20F;
            this.productType.Frozen = true;
            this.productType.HeaderText = "Product Type";

            this.productType.MaxDropDownItems = 15;
            this.productType.MinimumWidth = 175;
            this.productType.Name = "productType";
            this.productType.ReadOnly = true;
            this.productType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.productType.Sorted = true;
            this.productType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.productType.ToolTipText = "Product type";
            this.productType.Width = 175;
            // 
            // productWeight
            // 
            this.productWeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.productWeight.DataPropertyName = "product Weight";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = "0.00";
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.productWeight.DefaultCellStyle = dataGridViewCellStyle6;
            this.productWeight.DividerWidth = 2;
            this.productWeight.FillWeight = 30F;
            this.productWeight.Frozen = true;
            this.productWeight.HeaderText = "Product Weight (lbs)";
            this.productWeight.MaxInputLength = 9767;
            this.productWeight.MinimumWidth = 200;
            this.productWeight.Name = "productWeight";
            this.productWeight.ReadOnly = true;
            this.productWeight.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.productWeight.Width = 206;
            // 
            // qualityFlagName
            // 
            this.qualityFlagName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.NullValue = "Enter Value";
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.qualityFlagName.DefaultCellStyle = dataGridViewCellStyle7;
            this.qualityFlagName.DividerWidth = 2;
            this.qualityFlagName.FillWeight = 55F;
            this.qualityFlagName.Frozen = true;
            this.qualityFlagName.HeaderText = "Quality Flag Name";
            this.qualityFlagName.MaxDropDownItems = 20;
            this.qualityFlagName.MinimumWidth = 12;
            this.qualityFlagName.Name = "qualityFlagName";
            this.qualityFlagName.ReadOnly = true;
            this.qualityFlagName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.qualityFlagName.Sorted = true;
            this.qualityFlagName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.qualityFlagName.Width = 148;
            // 
            // notes
            // 
            this.notes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.notes.DefaultCellStyle = dataGridViewCellStyle8;
            this.notes.DividerWidth = 2;
            this.notes.FillWeight = 190F;
            this.notes.HeaderText = "Notes";
            this.notes.MinimumWidth = 495;
            this.notes.Name = "notes";
            this.notes.ReadOnly = true;
            this.notes.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "bagNo";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.NullValue = "-";
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn1.DividerWidth = 2;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Bag ID";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 5000;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "product Weight";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = 0;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn3.FillWeight = 70F;
            this.dataGridViewTextBoxColumn3.HeaderText = "ProductWeight";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 9767;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn4.FillWeight = 150F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Notes";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // FormBagEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1578, 1171);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bagGridView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(2000, 1800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "FormBagEdit";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bagging App";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BagEditForm_Load);
            this.Shown += new System.EventHandler(this.BagEditForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bagGridView)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button addNewBagButton;
        private System.Windows.Forms.Button closeWindowButton;
        private System.Windows.Forms.Button SaveBagButton;
        private System.Windows.Forms.Button reloadListButton;
        private System.Windows.Forms.Button printButton;
        private MyDataGridView bagGridView;
        private System.Windows.Forms.ToolTip toolTip1;

        private System.ComponentModel.BackgroundWorker PopulateGridFromDBWorker;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label percentageLabel1;
        private System.Windows.Forms.Label statusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn bagNo;
        private CalendarColumn productDate;
        private System.Windows.Forms.DataGridViewComboBoxColumn productType;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWeight;
        private System.Windows.Forms.DataGridViewComboBoxColumn qualityFlagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn notes;
    }
}