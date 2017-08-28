namespace CadEditor
{
    partial class EnemyEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnemyEditor));
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.objPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectSprites = new System.Windows.Forms.ImageList(this.components);
            this.btSortUp = new System.Windows.Forms.Button();
            this.btSortDown = new System.Windows.Forms.Button();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbScale = new System.Windows.Forms.ComboBox();
            this.cbPlus256 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbLayoutNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBlockNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBigBlockNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.lbActive = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbObjectsCount = new System.Windows.Forms.Label();
            this.pnView = new System.Windows.Forms.Panel();
            this.pnObjectList = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvObjects = new System.Windows.Forms.DataGridView();
            this.cbObjectList = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btSort = new System.Windows.Forms.Button();
            this.pnObjects = new System.Windows.Forms.Panel();
            this.pnBigObjects = new System.Windows.Forms.Panel();
            this.pbBigObject = new System.Windows.Forms.PictureBox();
            this.cbBigObjectNo = new System.Windows.Forms.ComboBox();
            this.cbUseBigPictures = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bttSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bttExport = new System.Windows.Forms.ToolStripButton();
            this.bttImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ofJson = new System.Windows.Forms.OpenFileDialog();
            this.sfJson = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbTool = new System.Windows.Forms.ToolStripComboBox();
            this.bttAlign8 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnView.SuspendLayout();
            this.pnObjectList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).BeginInit();
            this.pnObjects.SuspendLayout();
            this.pnBigObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBigObject)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(0, 5);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(683, 630);
            this.mapScreen.TabIndex = 30;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            this.mapScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseDown);
            this.mapScreen.MouseLeave += new System.EventHandler(this.mapScreen_MouseLeave);
            this.mapScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseMove);
            this.mapScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseUp);
            // 
            // objPanel
            // 
            this.objPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objPanel.AutoScroll = true;
            this.objPanel.Location = new System.Drawing.Point(9, 260);
            this.objPanel.Margin = new System.Windows.Forms.Padding(4);
            this.objPanel.Name = "objPanel";
            this.objPanel.Size = new System.Drawing.Size(229, 389);
            this.objPanel.TabIndex = 37;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 170;
            // 
            // objectSprites
            // 
            this.objectSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.objectSprites.ImageSize = new System.Drawing.Size(16, 16);
            this.objectSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btSortUp
            // 
            this.btSortUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSortUp.Location = new System.Drawing.Point(109, 485);
            this.btSortUp.Margin = new System.Windows.Forms.Padding(4);
            this.btSortUp.Name = "btSortUp";
            this.btSortUp.Size = new System.Drawing.Size(74, 27);
            this.btSortUp.TabIndex = 47;
            this.btSortUp.Text = "↑";
            this.btSortUp.UseVisualStyleBackColor = true;
            this.btSortUp.Click += new System.EventHandler(this.btSortUp_Click);
            // 
            // btSortDown
            // 
            this.btSortDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSortDown.Location = new System.Drawing.Point(191, 484);
            this.btSortDown.Margin = new System.Windows.Forms.Padding(4);
            this.btSortDown.Name = "btSortDown";
            this.btSortDown.Size = new System.Drawing.Size(77, 27);
            this.btSortDown.TabIndex = 48;
            this.btSortDown.Text = "↓";
            this.btSortDown.UseVisualStyleBackColor = true;
            this.btSortDown.Click += new System.EventHandler(this.btSortDown_Click);
            // 
            // pnGeneric
            // 
            this.pnGeneric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnGeneric.Controls.Add(this.label14);
            this.pnGeneric.Controls.Add(this.cbScale);
            this.pnGeneric.Controls.Add(this.cbPlus256);
            this.pnGeneric.Controls.Add(this.label11);
            this.pnGeneric.Controls.Add(this.cbLayoutNo);
            this.pnGeneric.Controls.Add(this.label9);
            this.pnGeneric.Controls.Add(this.cbPaletteNo);
            this.pnGeneric.Controls.Add(this.label8);
            this.pnGeneric.Controls.Add(this.cbBlockNo);
            this.pnGeneric.Controls.Add(this.label2);
            this.pnGeneric.Controls.Add(this.cbBigBlockNo);
            this.pnGeneric.Controls.Add(this.label4);
            this.pnGeneric.Controls.Add(this.cbVideoNo);
            this.pnGeneric.Location = new System.Drawing.Point(5, 17);
            this.pnGeneric.Margin = new System.Windows.Forms.Padding(4);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(233, 164);
            this.pnGeneric.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(155, 62);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 17);
            this.label14.TabIndex = 57;
            this.label14.Text = "Scale:";
            // 
            // cbScale
            // 
            this.cbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScale.FormattingEnabled = true;
            this.cbScale.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbScale.Location = new System.Drawing.Point(157, 81);
            this.cbScale.Margin = new System.Windows.Forms.Padding(4);
            this.cbScale.Name = "cbScale";
            this.cbScale.Size = new System.Drawing.Size(59, 24);
            this.cbScale.TabIndex = 56;
            this.cbScale.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // cbPlus256
            // 
            this.cbPlus256.AutoSize = true;
            this.cbPlus256.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbPlus256.Location = new System.Drawing.Point(128, 5);
            this.cbPlus256.Margin = new System.Windows.Forms.Padding(4);
            this.cbPlus256.Name = "cbPlus256";
            this.cbPlus256.Size = new System.Drawing.Size(98, 38);
            this.cbPlus256.TabIndex = 55;
            this.cbPlus256.Text = "+256 screens";
            this.cbPlus256.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 1);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 17);
            this.label11.TabIndex = 54;
            this.label11.Text = "Layout";
            // 
            // cbLayoutNo
            // 
            this.cbLayoutNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutNo.DropDownWidth = 220;
            this.cbLayoutNo.FormattingEnabled = true;
            this.cbLayoutNo.Items.AddRange(new object[] {
            "0x1DFA0 (17x4)",
            "0x1DFE4 (17x4)",
            "0x1E028 (17x4)",
            "0x1E0E4 (10x12)",
            "0x1E11D (19x3)",
            "0x1E06C (19x3)",
            "0x1E156  (19x3)"});
            this.cbLayoutNo.Location = new System.Drawing.Point(5, 20);
            this.cbLayoutNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbLayoutNo.Name = "cbLayoutNo";
            this.cbLayoutNo.Size = new System.Drawing.Size(113, 24);
            this.cbLayoutNo.TabIndex = 53;
            this.cbLayoutNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 112);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 48;
            this.label9.Text = "Pallete:";
            // 
            // cbPaletteNo
            // 
            this.cbPaletteNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaletteNo.FormattingEnabled = true;
            this.cbPaletteNo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cbPaletteNo.Location = new System.Drawing.Point(11, 132);
            this.cbPaletteNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(69, 24);
            this.cbPaletteNo.TabIndex = 47;
            this.cbPaletteNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(83, 111);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 46;
            this.label8.Text = "Block:";
            // 
            // cbBlockNo
            // 
            this.cbBlockNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlockNo.FormattingEnabled = true;
            this.cbBlockNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbBlockNo.Location = new System.Drawing.Point(88, 130);
            this.cbBlockNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(59, 24);
            this.cbBlockNo.TabIndex = 45;
            this.cbBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 44;
            this.label2.Text = "BigBlock:";
            // 
            // cbBigBlockNo
            // 
            this.cbBigBlockNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBigBlockNo.FormattingEnabled = true;
            this.cbBigBlockNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbBigBlockNo.Location = new System.Drawing.Point(88, 81);
            this.cbBigBlockNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBigBlockNo.Name = "cbBigBlockNo";
            this.cbBigBlockNo.Size = new System.Drawing.Size(59, 24);
            this.cbBigBlockNo.TabIndex = 43;
            this.cbBigBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "VideoBlock:";
            // 
            // cbVideoNo
            // 
            this.cbVideoNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVideoNo.FormattingEnabled = true;
            this.cbVideoNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbVideoNo.Location = new System.Drawing.Point(8, 80);
            this.cbVideoNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(72, 24);
            this.cbVideoNo.TabIndex = 41;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // lbActive
            // 
            this.lbActive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbActive.AutoSize = true;
            this.lbActive.Location = new System.Drawing.Point(213, 238);
            this.lbActive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbActive.Name = "lbActive";
            this.lbActive.Size = new System.Drawing.Size(18, 17);
            this.lbActive.TabIndex = 55;
            this.lbActive.Text = "()";
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(7, 514);
            this.btDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(155, 30);
            this.btDelete.TabIndex = 56;
            this.btDelete.Text = "delete selected";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 569);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(288, 17);
            this.label7.TabIndex = 57;
            this.label7.Text = "Press CTRL or SHIFT to select many objects";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 490);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 17);
            this.label10.TabIndex = 58;
            this.label10.Text = "Objects order:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(893, 537);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 17);
            this.label12.TabIndex = 59;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 585);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(240, 17);
            this.label13.TabIndex = 60;
            this.label13.Text = "Press DEL to delete selected objects";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 628);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(244, 17);
            this.label15.TabIndex = 62;
            this.label15.Text = "Don\'t forget sort objects in right order";
            // 
            // lbObjectsCount
            // 
            this.lbObjectsCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbObjectsCount.AutoSize = true;
            this.lbObjectsCount.Location = new System.Drawing.Point(7, 548);
            this.lbObjectsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbObjectsCount.Name = "lbObjectsCount";
            this.lbObjectsCount.Size = new System.Drawing.Size(123, 17);
            this.lbObjectsCount.TabIndex = 67;
            this.lbObjectsCount.Text = "Objects count: 0/0";
            // 
            // pnView
            // 
            this.pnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnView.AutoScroll = true;
            this.pnView.Controls.Add(this.mapScreen);
            this.pnView.Location = new System.Drawing.Point(8, 4);
            this.pnView.Margin = new System.Windows.Forms.Padding(4);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(692, 626);
            this.pnView.TabIndex = 68;
            // 
            // pnObjectList
            // 
            this.pnObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnObjectList.Controls.Add(this.label6);
            this.pnObjectList.Controls.Add(this.dgvObjects);
            this.pnObjectList.Controls.Add(this.cbObjectList);
            this.pnObjectList.Controls.Add(this.label19);
            this.pnObjectList.Controls.Add(this.btSort);
            this.pnObjectList.Controls.Add(this.lbObjectsCount);
            this.pnObjectList.Controls.Add(this.btSortUp);
            this.pnObjectList.Controls.Add(this.label15);
            this.pnObjectList.Controls.Add(this.btSortDown);
            this.pnObjectList.Controls.Add(this.btDelete);
            this.pnObjectList.Controls.Add(this.label13);
            this.pnObjectList.Controls.Add(this.label7);
            this.pnObjectList.Controls.Add(this.label10);
            this.pnObjectList.Location = new System.Drawing.Point(4, 4);
            this.pnObjectList.Margin = new System.Windows.Forms.Padding(4);
            this.pnObjectList.Name = "pnObjectList";
            this.pnObjectList.Size = new System.Drawing.Size(363, 620);
            this.pnObjectList.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 7);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 59;
            this.label6.Text = "Object list:";
            // 
            // dgvObjects
            // 
            this.dgvObjects.AllowUserToOrderColumns = true;
            this.dgvObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvObjects.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjects.Location = new System.Drawing.Point(3, 31);
            this.dgvObjects.Name = "dgvObjects";
            this.dgvObjects.RowTemplate.Height = 24;
            this.dgvObjects.Size = new System.Drawing.Size(357, 446);
            this.dgvObjects.TabIndex = 0;
            this.dgvObjects.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvObjects_CellFormatting);
            this.dgvObjects.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvObjects_CellParsing);
            this.dgvObjects.SelectionChanged += new System.EventHandler(this.dgvObjects_SelectionChanged);
            this.dgvObjects.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvObjects_KeyUp);
            // 
            // cbObjectList
            // 
            this.cbObjectList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbObjectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjectList.DropDownWidth = 220;
            this.cbObjectList.FormattingEnabled = true;
            this.cbObjectList.Location = new System.Drawing.Point(127, 4);
            this.cbObjectList.Margin = new System.Windows.Forms.Padding(4);
            this.cbObjectList.Name = "cbObjectList";
            this.cbObjectList.Size = new System.Drawing.Size(228, 24);
            this.cbObjectList.TabIndex = 58;
            this.cbObjectList.SelectedIndexChanged += new System.EventHandler(this.cbObjectList_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 600);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(265, 17);
            this.label19.TabIndex = 70;
            this.label19.Text = "Right click for dragging objects at screen";
            // 
            // btSort
            // 
            this.btSort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSort.Location = new System.Drawing.Point(170, 514);
            this.btSort.Margin = new System.Windows.Forms.Padding(4);
            this.btSort.Name = "btSort";
            this.btSort.Size = new System.Drawing.Size(159, 30);
            this.btSort.TabIndex = 69;
            this.btSort.Text = "autosort objects";
            this.btSort.UseVisualStyleBackColor = true;
            this.btSort.Click += new System.EventHandler(this.btSort_Click);
            // 
            // pnObjects
            // 
            this.pnObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnObjects.Controls.Add(this.pnBigObjects);
            this.pnObjects.Controls.Add(this.objPanel);
            this.pnObjects.Controls.Add(this.cbUseBigPictures);
            this.pnObjects.Controls.Add(this.lbActive);
            this.pnObjects.Controls.Add(this.pnGeneric);
            this.pnObjects.Location = new System.Drawing.Point(12, 26);
            this.pnObjects.Margin = new System.Windows.Forms.Padding(4);
            this.pnObjects.Name = "pnObjects";
            this.pnObjects.Size = new System.Drawing.Size(244, 662);
            this.pnObjects.TabIndex = 31;
            // 
            // pnBigObjects
            // 
            this.pnBigObjects.Controls.Add(this.pbBigObject);
            this.pnBigObjects.Controls.Add(this.cbBigObjectNo);
            this.pnBigObjects.Location = new System.Drawing.Point(5, 263);
            this.pnBigObjects.Margin = new System.Windows.Forms.Padding(4);
            this.pnBigObjects.Name = "pnBigObjects";
            this.pnBigObjects.Size = new System.Drawing.Size(228, 386);
            this.pnBigObjects.TabIndex = 31;
            this.pnBigObjects.Visible = false;
            // 
            // pbBigObject
            // 
            this.pbBigObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBigObject.Location = new System.Drawing.Point(4, 41);
            this.pbBigObject.Margin = new System.Windows.Forms.Padding(4);
            this.pbBigObject.Name = "pbBigObject";
            this.pbBigObject.Size = new System.Drawing.Size(113, 107);
            this.pbBigObject.TabIndex = 68;
            this.pbBigObject.TabStop = false;
            // 
            // cbBigObjectNo
            // 
            this.cbBigObjectNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBigObjectNo.FormattingEnabled = true;
            this.cbBigObjectNo.Location = new System.Drawing.Point(1, 9);
            this.cbBigObjectNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBigObjectNo.Name = "cbBigObjectNo";
            this.cbBigObjectNo.Size = new System.Drawing.Size(96, 24);
            this.cbBigObjectNo.TabIndex = 30;
            this.cbBigObjectNo.SelectedIndexChanged += new System.EventHandler(this.cbBigObjectNo_SelectedIndexChanged);
            // 
            // cbUseBigPictures
            // 
            this.cbUseBigPictures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUseBigPictures.AutoSize = true;
            this.cbUseBigPictures.Location = new System.Drawing.Point(5, 234);
            this.cbUseBigPictures.Margin = new System.Windows.Forms.Padding(4);
            this.cbUseBigPictures.Name = "cbUseBigPictures";
            this.cbUseBigPictures.Size = new System.Drawing.Size(128, 21);
            this.cbUseBigPictures.TabIndex = 65;
            this.cbUseBigPictures.Text = "Alternative view";
            this.cbUseBigPictures.UseVisualStyleBackColor = true;
            this.cbUseBigPictures.CheckedChanged += new System.EventHandler(this.cbUseBigPictures_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttSave,
            this.toolStripSeparator1,
            this.bttExport,
            this.bttImport,
            this.toolStripSeparator2,
            this.cbTool,
            this.bttAlign8});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1348, 28);
            this.toolStrip1.TabIndex = 69;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bttSave
            // 
            this.bttSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttSave.Image = ((System.Drawing.Image)(resources.GetObject("bttSave.Image")));
            this.bttSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(24, 25);
            this.bttSave.Text = "Save";
            this.bttSave.Click += new System.EventHandler(this.tbbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // bttExport
            // 
            this.bttExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(24, 25);
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.bttExport_Click);
            // 
            // bttImport
            // 
            this.bttImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttImport.Image = ((System.Drawing.Image)(resources.GetObject("bttImport.Image")));
            this.bttImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttImport.Name = "bttImport";
            this.bttImport.Size = new System.Drawing.Size(24, 25);
            this.bttImport.Text = "Import";
            this.bttImport.Click += new System.EventHandler(this.bttImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // ofJson
            // 
            this.ofJson.FileName = "objects.json";
            this.ofJson.Filter = "json|*.json";
            // 
            // sfJson
            // 
            this.sfJson.FileName = "objects.json";
            this.sfJson.Filter = "json|*.json";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(263, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnObjectList);
            this.splitContainer1.Size = new System.Drawing.Size(1073, 636);
            this.splitContainer1.SplitterDistance = 706;
            this.splitContainer1.TabIndex = 70;
            // 
            // cbTool
            // 
            this.cbTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTool.Items.AddRange(new object[] {
            "Create",
            "Select",
            "Delete"});
            this.cbTool.Name = "cbTool";
            this.cbTool.Size = new System.Drawing.Size(121, 28);
            this.cbTool.SelectedIndexChanged += new System.EventHandler(this.cbTool_SelectedIndexChanged);
            // 
            // bttAlign8
            // 
            this.bttAlign8.CheckOnClick = true;
            this.bttAlign8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttAlign8.Image = ((System.Drawing.Image)(resources.GetObject("bttAlign8.Image")));
            this.bttAlign8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttAlign8.Name = "bttAlign8";
            this.bttAlign8.Size = new System.Drawing.Size(24, 25);
            this.bttAlign8.Text = "8-pixel align";
            this.bttAlign8.Click += new System.EventHandler(this.bttAlign8_Click);
            // 
            // EnemyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 693);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnObjects);
            this.Controls.Add(this.label12);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "EnemyEditor";
            this.Text = "Enemy Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnemyEditor_FormClosing);
            this.Load += new System.EventHandler(this.EnemyEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.pnView.ResumeLayout(false);
            this.pnObjectList.ResumeLayout(false);
            this.pnObjectList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).EndInit();
            this.pnObjects.ResumeLayout(false);
            this.pnObjects.PerformLayout();
            this.pnBigObjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBigObject)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.FlowLayoutPanel objPanel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList objectSprites;
        private System.Windows.Forms.Button btSortUp;
        private System.Windows.Forms.Button btSortDown;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbLayoutNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPaletteNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBlockNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBigBlockNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.CheckBox cbPlus256;
        private System.Windows.Forms.Label lbActive;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbObjectsCount;
        private System.Windows.Forms.Panel pnView;
        private System.Windows.Forms.Panel pnObjectList;
        private System.Windows.Forms.Panel pnObjects;
        private System.Windows.Forms.Button btSort;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox cbUseBigPictures;
        private System.Windows.Forms.Panel pnBigObjects;
        private System.Windows.Forms.ComboBox cbBigObjectNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbScale;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbObjectList;
        private System.Windows.Forms.DataGridView dgvObjects;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bttSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bttExport;
        private System.Windows.Forms.ToolStripButton bttImport;
        private System.Windows.Forms.OpenFileDialog ofJson;
        private System.Windows.Forms.SaveFileDialog sfJson;
        private System.Windows.Forms.PictureBox pbBigObject;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cbTool;
        private System.Windows.Forms.ToolStripButton bttAlign8;
    }
}