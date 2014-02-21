namespace CadEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.bigBlocks = new System.Windows.Forms.ImageList(this.components);
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.lbActiveBlock = new System.Windows.Forms.Label();
            this.smallBlocks = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.cbScreenNo = new System.Windows.Forms.ComboBox();
            this.cbShowNeighborns = new System.Windows.Forms.CheckBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBlockNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBigBlockNo = new System.Windows.Forms.ComboBox();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnCad = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbShowAxis = new System.Windows.Forms.CheckBox();
            this.btHex = new System.Windows.Forms.Button();
            this.smallBlocks1 = new System.Windows.Forms.ImageList(this.components);
            this.smallBlocks2 = new System.Windows.Forms.ImageList(this.components);
            this.smallBlocks3 = new System.Windows.Forms.ImageList(this.components);
            this.smallBlocks4 = new System.Windows.Forms.ImageList(this.components);
            this.pnView = new System.Windows.Forms.Panel();
            this.cbScale = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbCoords = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bttOpen = new System.Windows.Forms.ToolStripButton();
            this.bttSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bttExport = new System.Windows.Forms.ToolStripButton();
            this.bttImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bttBigBlocks = new System.Windows.Forms.ToolStripButton();
            this.bttBlocks = new System.Windows.Forms.ToolStripButton();
            this.bttEnemies = new System.Windows.Forms.ToolStripButton();
            this.bttLayout = new System.Windows.Forms.ToolStripButton();
            this.bttVideo = new System.Windows.Forms.ToolStripButton();
            this.bttMap = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnCad.SuspendLayout();
            this.pnView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bigBlocks
            // 
            this.bigBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.bigBlocks.ImageSize = new System.Drawing.Size(64, 64);
            this.bigBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // blocksPanel
            // 
            this.blocksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.blocksPanel.AutoScroll = true;
            this.blocksPanel.Location = new System.Drawing.Point(12, 29);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(324, 534);
            this.blocksPanel.TabIndex = 2;
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(0, 4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(640, 512);
            this.mapScreen.TabIndex = 4;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            this.mapScreen.MouseLeave += new System.EventHandler(this.mapScreen_MouseLeave);
            this.mapScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseMove);
            // 
            // activeBlock
            // 
            this.activeBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeBlock.Location = new System.Drawing.Point(339, 45);
            this.activeBlock.Name = "activeBlock";
            this.activeBlock.Size = new System.Drawing.Size(64, 64);
            this.activeBlock.TabIndex = 5;
            this.activeBlock.TabStop = false;
            // 
            // lbActiveBlock
            // 
            this.lbActiveBlock.AutoSize = true;
            this.lbActiveBlock.Location = new System.Drawing.Point(339, 29);
            this.lbActiveBlock.Name = "lbActiveBlock";
            this.lbActiveBlock.Size = new System.Drawing.Size(49, 13);
            this.lbActiveBlock.TabIndex = 16;
            this.lbActiveBlock.Text = "Active: ()";
            // 
            // smallBlocks
            // 
            this.smallBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(336, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Screen No:";
            // 
            // cbScreenNo
            // 
            this.cbScreenNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenNo.FormattingEnabled = true;
            this.cbScreenNo.Location = new System.Drawing.Point(339, 128);
            this.cbScreenNo.Name = "cbScreenNo";
            this.cbScreenNo.Size = new System.Drawing.Size(64, 21);
            this.cbScreenNo.TabIndex = 27;
            this.cbScreenNo.SelectedIndexChanged += new System.EventHandler(this.cbScreenNo_SelectedIndexChanged);
            // 
            // cbShowNeighborns
            // 
            this.cbShowNeighborns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowNeighborns.AutoSize = true;
            this.cbShowNeighborns.Checked = true;
            this.cbShowNeighborns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowNeighborns.Location = new System.Drawing.Point(936, 554);
            this.cbShowNeighborns.Name = "cbShowNeighborns";
            this.cbShowNeighborns.Size = new System.Drawing.Size(108, 17);
            this.cbShowNeighborns.TabIndex = 32;
            this.cbShowNeighborns.Text = "Show neighborns";
            this.cbShowNeighborns.UseVisualStyleBackColor = true;
            this.cbShowNeighborns.CheckedChanged += new System.EventHandler(this.cbShowNeighborns_CheckedChanged);
            // 
            // pnGeneric
            // 
            this.pnGeneric.Controls.Add(this.label9);
            this.pnGeneric.Controls.Add(this.cbPaletteNo);
            this.pnGeneric.Controls.Add(this.label8);
            this.pnGeneric.Controls.Add(this.cbBlockNo);
            this.pnGeneric.Controls.Add(this.label5);
            this.pnGeneric.Controls.Add(this.cbBigBlockNo);
            this.pnGeneric.Controls.Add(this.cbVideoNo);
            this.pnGeneric.Controls.Add(this.label1);
            this.pnGeneric.Location = new System.Drawing.Point(337, 195);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(72, 162);
            this.pnGeneric.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
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
            this.cbPaletteNo.Location = new System.Drawing.Point(6, 131);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(64, 21);
            this.cbPaletteNo.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
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
            this.cbBlockNo.Location = new System.Drawing.Point(6, 91);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(64, 21);
            this.cbBlockNo.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "BigBlock:";
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
            this.cbBigBlockNo.Location = new System.Drawing.Point(6, 51);
            this.cbBigBlockNo.Name = "cbBigBlockNo";
            this.cbBigBlockNo.Size = new System.Drawing.Size(64, 21);
            this.cbBigBlockNo.TabIndex = 43;
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
            this.cbVideoNo.Location = new System.Drawing.Point(5, 11);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(64, 21);
            this.cbVideoNo.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, -5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "VideoBlock:";
            // 
            // pnCad
            // 
            this.pnCad.Controls.Add(this.label7);
            this.pnCad.Controls.Add(this.cbDoor);
            this.pnCad.Controls.Add(this.label2);
            this.pnCad.Controls.Add(this.cbLevel);
            this.pnCad.Location = new System.Drawing.Point(337, 195);
            this.pnCad.Name = "pnCad";
            this.pnCad.Size = new System.Drawing.Size(72, 93);
            this.pnCad.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Door:";
            // 
            // cbDoor
            // 
            this.cbDoor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDoor.FormattingEnabled = true;
            this.cbDoor.Items.AddRange(new object[] {
            "None",
            "Door 1",
            "Door 2",
            "Door 3",
            "Door 4",
            "Door 5",
            "Door 6",
            "Door 7",
            "Door 8",
            "Door 9",
            "Door A",
            "Door B",
            "Door C",
            "Door D",
            "Door E",
            "Door F",
            "Door 10",
            "Door 11",
            "Door 12",
            "Door 13",
            "Door 14",
            "Door 15",
            "Door 16",
            "Door 17",
            "Door 18",
            "Door 19"});
            this.cbDoor.Location = new System.Drawing.Point(7, 53);
            this.cbDoor.Name = "cbDoor";
            this.cbDoor.Size = new System.Drawing.Size(64, 21);
            this.cbDoor.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, -3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "View as:";
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "Level 0",
            "Level A",
            "Level B",
            "Level C",
            "Level D",
            "Level E",
            "Level F",
            "Level G",
            "Level H",
            "Level I",
            "Level J"});
            this.cbLevel.Location = new System.Drawing.Point(6, 13);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(64, 21);
            this.cbLevel.TabIndex = 31;
            // 
            // cbViewType
            // 
            this.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViewType.DropDownWidth = 128;
            this.cbViewType.FormattingEnabled = true;
            this.cbViewType.Items.AddRange(new object[] {
            "Tiles",
            "Obj types",
            "Obj numbers"});
            this.cbViewType.Location = new System.Drawing.Point(339, 168);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(64, 21);
            this.cbViewType.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "View type:";
            // 
            // cbShowAxis
            // 
            this.cbShowAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowAxis.AutoSize = true;
            this.cbShowAxis.Checked = true;
            this.cbShowAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowAxis.Location = new System.Drawing.Point(856, 554);
            this.cbShowAxis.Name = "cbShowAxis";
            this.cbShowAxis.Size = new System.Drawing.Size(74, 17);
            this.cbShowAxis.TabIndex = 51;
            this.cbShowAxis.Text = "Show axis";
            this.cbShowAxis.UseVisualStyleBackColor = true;
            this.cbShowAxis.CheckedChanged += new System.EventHandler(this.cbShowAxis_CheckedChanged);
            // 
            // btHex
            // 
            this.btHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btHex.Location = new System.Drawing.Point(684, 550);
            this.btHex.Name = "btHex";
            this.btHex.Size = new System.Drawing.Size(96, 22);
            this.btHex.TabIndex = 52;
            this.btHex.Text = "open hex editor";
            this.btHex.UseVisualStyleBackColor = true;
            this.btHex.Click += new System.EventHandler(this.btHex_Click);
            // 
            // smallBlocks1
            // 
            this.smallBlocks1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks1.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // smallBlocks2
            // 
            this.smallBlocks2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks2.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // smallBlocks3
            // 
            this.smallBlocks3.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks3.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // smallBlocks4
            // 
            this.smallBlocks4.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks4.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks4.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnView
            // 
            this.pnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnView.AutoScroll = true;
            this.pnView.Controls.Add(this.mapScreen);
            this.pnView.Location = new System.Drawing.Point(412, 29);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(640, 519);
            this.pnView.TabIndex = 53;
            // 
            // cbScale
            // 
            this.cbScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScale.DropDownWidth = 128;
            this.cbScale.FormattingEnabled = true;
            this.cbScale.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbScale.Location = new System.Drawing.Point(495, 551);
            this.cbScale.Name = "cbScale";
            this.cbScale.Size = new System.Drawing.Size(29, 21);
            this.cbScale.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(452, 554);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "Scale:";
            // 
            // lbCoords
            // 
            this.lbCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCoords.AutoSize = true;
            this.lbCoords.Location = new System.Drawing.Point(530, 554);
            this.lbCoords.Name = "lbCoords";
            this.lbCoords.Size = new System.Drawing.Size(64, 13);
            this.lbCoords.TabIndex = 56;
            this.lbCoords.Text = "Coords:(0,0)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttOpen,
            this.bttSave,
            this.toolStripSeparator1,
            this.bttExport,
            this.bttImport,
            this.toolStripSeparator2,
            this.bttBigBlocks,
            this.bttBlocks,
            this.bttEnemies,
            this.bttLayout,
            this.bttVideo,
            this.bttMap});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1058, 25);
            this.toolStrip1.TabIndex = 57;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bttOpen
            // 
            this.bttOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttOpen.Image = ((System.Drawing.Image)(resources.GetObject("bttOpen.Image")));
            this.bttOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttOpen.Name = "bttOpen";
            this.bttOpen.Size = new System.Drawing.Size(23, 22);
            this.bttOpen.Text = "Open";
            this.bttOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // bttSave
            // 
            this.bttSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttSave.Image = ((System.Drawing.Image)(resources.GetObject("bttSave.Image")));
            this.bttSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(23, 22);
            this.bttSave.Text = "Save";
            this.bttSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bttExport
            // 
            this.bttExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttExport.Image = ((System.Drawing.Image)(resources.GetObject("bttExport.Image")));
            this.bttExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttExport.Name = "bttExport";
            this.bttExport.Size = new System.Drawing.Size(23, 22);
            this.bttExport.Text = "Export";
            this.bttExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // bttImport
            // 
            this.bttImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttImport.Image = ((System.Drawing.Image)(resources.GetObject("bttImport.Image")));
            this.bttImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttImport.Name = "bttImport";
            this.bttImport.Size = new System.Drawing.Size(23, 22);
            this.bttImport.Text = "Import";
            this.bttImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bttBigBlocks
            // 
            this.bttBigBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttBigBlocks.Image = ((System.Drawing.Image)(resources.GetObject("bttBigBlocks.Image")));
            this.bttBigBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttBigBlocks.Name = "bttBigBlocks";
            this.bttBigBlocks.Size = new System.Drawing.Size(23, 22);
            this.bttBigBlocks.Text = "Edit BigBlocks";
            this.bttBigBlocks.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttBlocks
            // 
            this.bttBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttBlocks.Image = ((System.Drawing.Image)(resources.GetObject("bttBlocks.Image")));
            this.bttBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttBlocks.Name = "bttBlocks";
            this.bttBlocks.Size = new System.Drawing.Size(23, 22);
            this.bttBlocks.Text = "Edit Blocks";
            this.bttBlocks.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttEnemies
            // 
            this.bttEnemies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttEnemies.Image = ((System.Drawing.Image)(resources.GetObject("bttEnemies.Image")));
            this.bttEnemies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttEnemies.Name = "bttEnemies";
            this.bttEnemies.Size = new System.Drawing.Size(23, 22);
            this.bttEnemies.Text = "Edit Enemies";
            this.bttEnemies.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttLayout
            // 
            this.bttLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttLayout.Image = ((System.Drawing.Image)(resources.GetObject("bttLayout.Image")));
            this.bttLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttLayout.Name = "bttLayout";
            this.bttLayout.Size = new System.Drawing.Size(23, 22);
            this.bttLayout.Text = "Edit Layout";
            this.bttLayout.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttVideo
            // 
            this.bttVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttVideo.Image = ((System.Drawing.Image)(resources.GetObject("bttVideo.Image")));
            this.bttVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttVideo.Name = "bttVideo";
            this.bttVideo.Size = new System.Drawing.Size(23, 22);
            this.bttVideo.Text = "Edit Video";
            this.bttVideo.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttMap
            // 
            this.bttMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttMap.Image = ((System.Drawing.Image)(resources.GetObject("bttMap.Image")));
            this.bttMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttMap.Name = "bttMap";
            this.bttMap.Size = new System.Drawing.Size(23, 22);
            this.bttMap.Text = "Edit Map";
            this.bttMap.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 573);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbCoords);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbScale);
            this.Controls.Add(this.pnView);
            this.Controls.Add(this.btHex);
            this.Controls.Add(this.cbShowAxis);
            this.Controls.Add(this.cbViewType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnGeneric);
            this.Controls.Add(this.cbShowNeighborns);
            this.Controls.Add(this.cbScreenNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbActiveBlock);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.blocksPanel);
            this.Controls.Add(this.pnCad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "CAD Editor v2.4.1 by spiiin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.pnCad.ResumeLayout(false);
            this.pnCad.PerformLayout();
            this.pnView.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Label lbActiveBlock;
        private System.Windows.Forms.ImageList bigBlocks;
        private System.Windows.Forms.ImageList smallBlocks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbScreenNo;
        private System.Windows.Forms.CheckBox cbShowNeighborns;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPaletteNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBlockNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBigBlockNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.Panel pnCad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDoor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbShowAxis;
        private System.Windows.Forms.Button btHex;
        private System.Windows.Forms.ImageList smallBlocks1;
        private System.Windows.Forms.ImageList smallBlocks2;
        private System.Windows.Forms.ImageList smallBlocks3;
        private System.Windows.Forms.ImageList smallBlocks4;
        private System.Windows.Forms.Panel pnView;
        private System.Windows.Forms.ComboBox cbScale;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbCoords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bttOpen;
        private System.Windows.Forms.ToolStripButton bttSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bttExport;
        private System.Windows.Forms.ToolStripButton bttImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bttBigBlocks;
        private System.Windows.Forms.ToolStripButton bttBlocks;
        private System.Windows.Forms.ToolStripButton bttEnemies;
        private System.Windows.Forms.ToolStripButton bttLayout;
        private System.Windows.Forms.ToolStripButton bttVideo;
        private System.Windows.Forms.ToolStripButton bttMap;
    }
}

