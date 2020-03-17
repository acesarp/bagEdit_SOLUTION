using System;
using System.ComponentModel;
using Bagging.BagEdit.CustomControls;

namespace Bagging.BagEdit {
    partial class FormReseed {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReseed));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PopulateGridFromDBWorker = new System.ComponentModel.BackgroundWorker();
            this.titleLabel = new System.Windows.Forms.Label();
            this.percentageLabel1 = new System.Windows.Forms.Label();
            this.statusLabel1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closeWindowButton = new System.Windows.Forms.Button();
            this.reloadListButton = new System.Windows.Forms.Button();
            this.addNewReseedButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.reseedGridView = new Bagging.BagEdit.CustomControls.MyDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reseedDate = new Bagging.BagEdit.CustomControls.CalendarColumn();
            this.bagNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originalSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reseedGridView)).BeginInit();
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
            this.titleLabel.Location = new System.Drawing.Point(186, 17);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(347, 65);
            this.titleLabel.TabIndex = 15;
            this.titleLabel.Text = "Reseed Bag";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // percentageLabel1
            // 
            this.percentageLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.percentageLabel1.AutoSize = true;
            this.percentageLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.percentageLabel1.Location = new System.Drawing.Point(1331, -3);
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
            this.statusLabel1.Location = new System.Drawing.Point(1420, -1);
            this.statusLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
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
            this.panel1.Controls.Add(this.splitter4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Controls.Add(this.closeWindowButton);
            this.panel1.Controls.Add(this.reloadListButton);
            this.panel1.Controls.Add(this.addNewReseedButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1574, 100);
            this.panel1.TabIndex = 21;
            // 
            // splitter4
            // 
            this.splitter4.BackColor = System.Drawing.SystemColors.Control;
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter4.Location = new System.Drawing.Point(0, 95);
            this.splitter4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(1574, 5);
            this.splitter4.TabIndex = 23;
            this.splitter4.TabStop = false;
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
            this.reloadListButton.Location = new System.Drawing.Point(896, 8);
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
            // addNewReseedButton
            // 
            this.addNewReseedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewReseedButton.BackColor = System.Drawing.SystemColors.Control;
            this.addNewReseedButton.Enabled = false;
            this.addNewReseedButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.addNewReseedButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.addNewReseedButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.addNewReseedButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addNewReseedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewReseedButton.Image = ((System.Drawing.Image)(resources.GetObject("addNewReseedButton.Image")));
            this.addNewReseedButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addNewReseedButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addNewReseedButton.Location = new System.Drawing.Point(664, 7);
            this.addNewReseedButton.Margin = new System.Windows.Forms.Padding(0);
            this.addNewReseedButton.MaximumSize = new System.Drawing.Size(218, 80);
            this.addNewReseedButton.MinimumSize = new System.Drawing.Size(218, 80);
            this.addNewReseedButton.Name = "addNewReseedButton";
            this.addNewReseedButton.Size = new System.Drawing.Size(218, 80);
            this.addNewReseedButton.TabIndex = 1;
            this.addNewReseedButton.Text = "Add New Reseed";
            this.addNewReseedButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.addNewReseedButton.UseCompatibleTextRendering = true;
            this.addNewReseedButton.UseVisualStyleBackColor = true;
            this.addNewReseedButton.Click += new System.EventHandler(this.AddNewBagButton_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.statusLabel1);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.percentageLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.progressBar.Location = new System.Drawing.Point(613, 4);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(700, 19);
            this.progressBar.Step = 5;
            this.progressBar.TabIndex = 16;
            // 
            // reseedGridView
            // 
            this.reseedGridView.AllowUserToAddRows = false;
            this.reseedGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.reseedGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.reseedGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reseedGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.reseedGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.reseedGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.reseedGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reseedGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entryno,
            this.reseedDate,
            this.bagNo,
            this.originalSite,
            this.notes});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reseedGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.reseedGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reseedGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.reseedGridView.GridColor = System.Drawing.SystemColors.ControlLight;
            this.reseedGridView.Location = new System.Drawing.Point(2, 102);
            this.reseedGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reseedGridView.MultiSelect = false;
            this.reseedGridView.Name = "reseedGridView";
            this.reseedGridView.ReadOnly = true;
            this.reseedGridView.RowHeadersWidth = 50;
            this.reseedGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reseedGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.reseedGridView.RowTemplate.Height = 28;
            this.reseedGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.reseedGridView.Size = new System.Drawing.Size(1574, 1067);
            this.reseedGridView.TabIndex = 12;
            this.reseedGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BagGridView_CellDoubleClick);
            this.reseedGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.ReseedGridView_DataError);
            this.reseedGridView.MouseHover += new System.EventHandler(this.BagGridView_MouseHover);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "bagNo";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.NullValue = "-";
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = 0;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.FillWeight = 70F;
            this.dataGridViewTextBoxColumn3.HeaderText = "ProductWeight";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 9767;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.FillWeight = 150F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Notes";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // entryno
            // 
            this.entryno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = "-";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.entryno.DefaultCellStyle = dataGridViewCellStyle3;
            this.entryno.DividerWidth = 2;
            this.entryno.FillWeight = 26.25F;
            this.entryno.Frozen = true;
            this.entryno.HeaderText = "Entry Number";
            this.entryno.MaxInputLength = 5000;
            this.entryno.MinimumWidth = 8;
            this.entryno.Name = "entryno";
            this.entryno.ReadOnly = true;
            this.entryno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.entryno.Width = 200;
            // 
            // reseedDate
            // 
            this.reseedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.reseedDate.DataPropertyName = "product Date";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.reseedDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.reseedDate.DividerWidth = 2;
            this.reseedDate.FillWeight = 65F;
            this.reseedDate.Frozen = true;
            this.reseedDate.HeaderText = "Reseed Date";
            this.reseedDate.MinimumWidth = 8;
            this.reseedDate.Name = "reseedDate";
            this.reseedDate.ReadOnly = true;
            this.reseedDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.reseedDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.reseedDate.Width = 264;
            // 
            // bagNo
            // 
            this.bagNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bagNo.DividerWidth = 2;
            this.bagNo.FillWeight = 30F;
            this.bagNo.Frozen = true;
            this.bagNo.HeaderText = "Bag Number";
            this.bagNo.MinimumWidth = 8;
            this.bagNo.Name = "bagNo";
            this.bagNo.ReadOnly = true;
            this.bagNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bagNo.Width = 200;
            // 
            // originalSite
            // 
            this.originalSite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.NullValue = "\"\"";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.originalSite.DefaultCellStyle = dataGridViewCellStyle5;
            this.originalSite.DividerWidth = 2;
            this.originalSite.FillWeight = 30F;
            this.originalSite.Frozen = true;
            this.originalSite.HeaderText = "Original Site";
            this.originalSite.MinimumWidth = 8;
            this.originalSite.Name = "originalSite";
            this.originalSite.ReadOnly = true;
            this.originalSite.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.originalSite.ToolTipText = "Please enter site number:";
            this.originalSite.Width = 200;
            // 
            // notes
            // 
            this.notes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.notes.DefaultCellStyle = dataGridViewCellStyle6;
            this.notes.DividerWidth = 2;
            this.notes.FillWeight = 200F;
            this.notes.HeaderText = "Notes";
            this.notes.MinimumWidth = 8;
            this.notes.Name = "notes";
            this.notes.ReadOnly = true;
            this.notes.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FormReseed
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1578, 1171);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.reseedGridView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(2000, 1800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "FormReseed";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bagging App";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReseed_FormClosing);
            this.Load += new System.EventHandler(this.BagReseedForm_Load);
            this.Shown += new System.EventHandler(this.BagReseedForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reseedGridView)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button addNewReseedButton;
        private System.Windows.Forms.Button closeWindowButton;
        private System.Windows.Forms.Button reloadListButton;
        private MyDataGridView reseedGridView;
        private System.Windows.Forms.ToolTip toolTip1;

        private System.ComponentModel.BackgroundWorker PopulateGridFromDBWorker;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label percentageLabel1;
        private System.Windows.Forms.Label statusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryno;
        private CalendarColumn reseedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn bagNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn notes;
    }
}