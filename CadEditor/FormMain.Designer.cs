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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.lbActiveBlock = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbScreenNo = new System.Windows.Forms.ComboBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.pnGroups = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.cbAdvanced = new System.Windows.Forms.CheckBox();
            this.pnAdvancedParams = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.cbBigBlockNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.cbBlockNo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnView = new System.Windows.Forms.Panel();
            this.lbCoords = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bttOpen = new System.Windows.Forms.ToolStripButton();
            this.bttSave = new System.Windows.Forms.ToolStripButton();
            this.bttReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bttBigBlocks = new System.Windows.Forms.ToolStripButton();
            this.bttBlocks = new System.Windows.Forms.ToolStripButton();
            this.bttEnemies = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bttStructures = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.bttShowNei = new System.Windows.Forms.ToolStripButton();
            this.bttAxis = new System.Windows.Forms.ToolStripButton();
            this.bttShowBrush = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bttScale = new System.Windows.Forms.ToolStripSplitButton();
            this.x025ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.x3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.bttShowLayer1 = new System.Windows.Forms.ToolStripButton();
            this.bttShowLayer2 = new System.Windows.Forms.ToolStripButton();
            this.bttLayer = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbShowPluginInfo = new System.Windows.Forms.ToolStripButton();
            this.tbbShowInfo = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnElements = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLevelNo = new System.Windows.Forms.ComboBox();
            this.cbUseStructs = new System.Windows.Forms.CheckBox();
            this.pnBlocks = new System.Windows.Forms.Panel();
            this.blocksScreen = new System.Windows.Forms.PictureBox();
            this.lbStructures = new System.Windows.Forms.ListBox();
            this.pnViewScroll = new System.Windows.Forms.Panel();
            this.lbPalBytesAddr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnGroups.SuspendLayout();
            this.pnAdvancedParams.SuspendLayout();
            this.pnView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnElements.SuspendLayout();
            this.pnBlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).BeginInit();
            this.pnViewScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(4, 3);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(508, 300);
            this.mapScreen.TabIndex = 4;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            this.mapScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseDown);
            this.mapScreen.MouseLeave += new System.EventHandler(this.mapScreen_MouseLeave);
            this.mapScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseMove);
            this.mapScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseUp);
            // 
            // activeBlock
            // 
            this.activeBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeBlock.Location = new System.Drawing.Point(4, 20);
            this.activeBlock.Margin = new System.Windows.Forms.Padding(4);
            this.activeBlock.Name = "activeBlock";
            this.activeBlock.Size = new System.Drawing.Size(85, 78);
            this.activeBlock.TabIndex = 5;
            this.activeBlock.TabStop = false;
            // 
            // lbActiveBlock
            // 
            this.lbActiveBlock.AutoSize = true;
            this.lbActiveBlock.Location = new System.Drawing.Point(4, 0);
            this.lbActiveBlock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbActiveBlock.Name = "lbActiveBlock";
            this.lbActiveBlock.Size = new System.Drawing.Size(64, 17);
            this.lbActiveBlock.TabIndex = 16;
            this.lbActiveBlock.Text = "Active: ()";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 102);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Screen No:";
            // 
            // cbScreenNo
            // 
            this.cbScreenNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenNo.FormattingEnabled = true;
            this.cbScreenNo.Location = new System.Drawing.Point(4, 122);
            this.cbScreenNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbScreenNo.Name = "cbScreenNo";
            this.cbScreenNo.Size = new System.Drawing.Size(84, 24);
            this.cbScreenNo.TabIndex = 27;
            this.cbScreenNo.SelectedIndexChanged += new System.EventHandler(this.cbScreenNo_SelectedIndexChanged);
            // 
            // pnGeneric
            // 
            this.pnGeneric.Controls.Add(this.pnGroups);
            this.pnGeneric.Controls.Add(this.cbAdvanced);
            this.pnGeneric.Controls.Add(this.pnAdvancedParams);
            this.pnGeneric.Location = new System.Drawing.Point(1, 262);
            this.pnGeneric.Margin = new System.Windows.Forms.Padding(4);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(96, 294);
            this.pnGeneric.TabIndex = 42;
            // 
            // pnGroups
            // 
            this.pnGroups.Controls.Add(this.label7);
            this.pnGroups.Controls.Add(this.cbGroup);
            this.pnGroups.Location = new System.Drawing.Point(5, 236);
            this.pnGroups.Name = "pnGroups";
            this.pnGroups.Size = new System.Drawing.Size(90, 59);
            this.pnGroups.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 50;
            this.label7.Text = "GROUP:";
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.DropDownWidth = 200;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(4, 29);
            this.cbGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(84, 24);
            this.cbGroup.TabIndex = 51;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // cbAdvanced
            // 
            this.cbAdvanced.AutoSize = true;
            this.cbAdvanced.Location = new System.Drawing.Point(2, 4);
            this.cbAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.cbAdvanced.Name = "cbAdvanced";
            this.cbAdvanced.Size = new System.Drawing.Size(93, 21);
            this.cbAdvanced.TabIndex = 60;
            this.cbAdvanced.Text = "Advanced";
            this.cbAdvanced.UseVisualStyleBackColor = true;
            this.cbAdvanced.CheckedChanged += new System.EventHandler(this.cbAdvanced_CheckedChanged);
            // 
            // pnAdvancedParams
            // 
            this.pnAdvancedParams.Controls.Add(this.label1);
            this.pnAdvancedParams.Controls.Add(this.cbVideoNo);
            this.pnAdvancedParams.Controls.Add(this.cbBigBlockNo);
            this.pnAdvancedParams.Controls.Add(this.label9);
            this.pnAdvancedParams.Controls.Add(this.label5);
            this.pnAdvancedParams.Controls.Add(this.cbPaletteNo);
            this.pnAdvancedParams.Controls.Add(this.cbBlockNo);
            this.pnAdvancedParams.Controls.Add(this.label8);
            this.pnAdvancedParams.Location = new System.Drawing.Point(3, 32);
            this.pnAdvancedParams.Name = "pnAdvancedParams";
            this.pnAdvancedParams.Size = new System.Drawing.Size(94, 199);
            this.pnAdvancedParams.TabIndex = 5;
            this.pnAdvancedParams.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "VideoBlock:";
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
            this.cbVideoNo.Location = new System.Drawing.Point(8, 20);
            this.cbVideoNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(84, 24);
            this.cbVideoNo.TabIndex = 41;
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
            this.cbBigBlockNo.Location = new System.Drawing.Point(9, 69);
            this.cbBigBlockNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBigBlockNo.Name = "cbBigBlockNo";
            this.cbBigBlockNo.Size = new System.Drawing.Size(84, 24);
            this.cbBigBlockNo.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 148);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 48;
            this.label9.Text = "Pallete:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 44;
            this.label5.Text = "BigBlock:";
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
            this.cbPaletteNo.Location = new System.Drawing.Point(9, 167);
            this.cbPaletteNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(84, 24);
            this.cbPaletteNo.TabIndex = 47;
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
            this.cbBlockNo.Location = new System.Drawing.Point(9, 118);
            this.cbBlockNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(84, 24);
            this.cbBlockNo.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 98);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 46;
            this.label8.Text = "Block:";
            // 
            // cbViewType
            // 
            this.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViewType.DropDownWidth = 128;
            this.cbViewType.FormattingEnabled = true;
            this.cbViewType.Items.AddRange(new object[] {
            "Blocks",
            "Block types",
            "Block numbers",
            "Small blocks numbers"});
            this.cbViewType.Location = new System.Drawing.Point(4, 229);
            this.cbViewType.Margin = new System.Windows.Forms.Padding(4);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(84, 24);
            this.cbViewType.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 209);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 44;
            this.label4.Text = "View type:";
            // 
            // pnView
            // 
            this.pnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnView.AutoScroll = true;
            this.pnView.Controls.Add(this.mapScreen);
            this.pnView.Location = new System.Drawing.Point(4, 4);
            this.pnView.Margin = new System.Windows.Forms.Padding(4);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(1028, 615);
            this.pnView.TabIndex = 53;
            // 
            // lbCoords
            // 
            this.lbCoords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCoords.AutoSize = true;
            this.lbCoords.Location = new System.Drawing.Point(7, 610);
            this.lbCoords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCoords.Name = "lbCoords";
            this.lbCoords.Size = new System.Drawing.Size(87, 17);
            this.lbCoords.TabIndex = 56;
            this.lbCoords.Text = "Coords:(0,0)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bttOpen,
            this.bttSave,
            this.bttReload,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.bttBigBlocks,
            this.bttBlocks,
            this.bttEnemies,
            this.toolStripSeparator3,
            this.bttStructures,
            this.toolStripSeparator6,
            this.bttShowNei,
            this.bttAxis,
            this.bttShowBrush,
            this.toolStripSeparator4,
            this.bttScale,
            this.toolStripSeparator5,
            this.bttShowLayer1,
            this.bttShowLayer2,
            this.bttLayer,
            this.toolStripSeparator7,
            this.tbbShowPluginInfo,
            this.tbbShowInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1431, 27);
            this.toolStrip1.TabIndex = 57;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bttOpen
            // 
            this.bttOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttOpen.Image = ((System.Drawing.Image)(resources.GetObject("bttOpen.Image")));
            this.bttOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttOpen.Name = "bttOpen";
            this.bttOpen.Size = new System.Drawing.Size(24, 24);
            this.bttOpen.Text = "Open";
            this.bttOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // bttSave
            // 
            this.bttSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttSave.Image = ((System.Drawing.Image)(resources.GetObject("bttSave.Image")));
            this.bttSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(24, 24);
            this.bttSave.Text = "Save";
            this.bttSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // bttReload
            // 
            this.bttReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttReload.Image = ((System.Drawing.Image)(resources.GetObject("bttReload.Image")));
            this.bttReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttReload.Name = "bttReload";
            this.bttReload.Size = new System.Drawing.Size(24, 24);
            this.bttReload.Text = "Reload";
            this.bttReload.Click += new System.EventHandler(this.bttReload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // bttBigBlocks
            // 
            this.bttBigBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttBigBlocks.Image = ((System.Drawing.Image)(resources.GetObject("bttBigBlocks.Image")));
            this.bttBigBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttBigBlocks.Name = "bttBigBlocks";
            this.bttBigBlocks.Size = new System.Drawing.Size(24, 24);
            this.bttBigBlocks.Text = "Edit BigBlocks";
            this.bttBigBlocks.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttBlocks
            // 
            this.bttBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttBlocks.Image = ((System.Drawing.Image)(resources.GetObject("bttBlocks.Image")));
            this.bttBlocks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttBlocks.Name = "bttBlocks";
            this.bttBlocks.Size = new System.Drawing.Size(24, 24);
            this.bttBlocks.Text = "Edit Blocks";
            this.bttBlocks.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // bttEnemies
            // 
            this.bttEnemies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttEnemies.Image = ((System.Drawing.Image)(resources.GetObject("bttEnemies.Image")));
            this.bttEnemies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttEnemies.Name = "bttEnemies";
            this.bttEnemies.Size = new System.Drawing.Size(24, 24);
            this.bttEnemies.Text = "Edit Enemies";
            this.bttEnemies.Click += new System.EventHandler(this.btSubeditor_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // bttStructures
            // 
            this.bttStructures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttStructures.Image = ((System.Drawing.Image)(resources.GetObject("bttStructures.Image")));
            this.bttStructures.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttStructures.Name = "bttStructures";
            this.bttStructures.Size = new System.Drawing.Size(24, 24);
            this.bttStructures.Text = "Make structures";
            this.bttStructures.Click += new System.EventHandler(this.bttStructures_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // bttShowNei
            // 
            this.bttShowNei.Checked = true;
            this.bttShowNei.CheckOnClick = true;
            this.bttShowNei.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bttShowNei.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttShowNei.Image = ((System.Drawing.Image)(resources.GetObject("bttShowNei.Image")));
            this.bttShowNei.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttShowNei.Name = "bttShowNei";
            this.bttShowNei.Size = new System.Drawing.Size(24, 24);
            this.bttShowNei.Text = "Show neighborns screens ";
            this.bttShowNei.CheckedChanged += new System.EventHandler(this.cbShowNeighborns_CheckedChanged);
            // 
            // bttAxis
            // 
            this.bttAxis.Checked = true;
            this.bttAxis.CheckOnClick = true;
            this.bttAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bttAxis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttAxis.Image = ((System.Drawing.Image)(resources.GetObject("bttAxis.Image")));
            this.bttAxis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttAxis.Name = "bttAxis";
            this.bttAxis.Size = new System.Drawing.Size(24, 24);
            this.bttAxis.Text = "Axis";
            this.bttAxis.CheckedChanged += new System.EventHandler(this.cbShowAxis_CheckedChanged);
            // 
            // bttShowBrush
            // 
            this.bttShowBrush.Checked = true;
            this.bttShowBrush.CheckOnClick = true;
            this.bttShowBrush.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bttShowBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttShowBrush.Image = ((System.Drawing.Image)(resources.GetObject("bttShowBrush.Image")));
            this.bttShowBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttShowBrush.Name = "bttShowBrush";
            this.bttShowBrush.Size = new System.Drawing.Size(24, 24);
            this.bttShowBrush.Text = "Show active element";
            this.bttShowBrush.CheckedChanged += new System.EventHandler(this.bttShowBrush_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // bttScale
            // 
            this.bttScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttScale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x025ToolStripMenuItem,
            this.x05ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.x3ToolStripMenuItem,
            this.x4ToolStripMenuItem});
            this.bttScale.Image = ((System.Drawing.Image)(resources.GetObject("bttScale.Image")));
            this.bttScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttScale.Name = "bttScale";
            this.bttScale.Size = new System.Drawing.Size(39, 24);
            this.bttScale.Text = "Scale";
            this.bttScale.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bttScale_DropDownItemClicked);
            // 
            // x025ToolStripMenuItem
            // 
            this.x025ToolStripMenuItem.Name = "x025ToolStripMenuItem";
            this.x025ToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.x025ToolStripMenuItem.Text = "x0.25";
            // 
            // x05ToolStripMenuItem
            // 
            this.x05ToolStripMenuItem.Name = "x05ToolStripMenuItem";
            this.x05ToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.x05ToolStripMenuItem.Text = "x0.5";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(118, 26);
            this.toolStripMenuItem2.Text = "x1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(118, 26);
            this.toolStripMenuItem3.Text = "x2";
            // 
            // x3ToolStripMenuItem
            // 
            this.x3ToolStripMenuItem.Name = "x3ToolStripMenuItem";
            this.x3ToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.x3ToolStripMenuItem.Text = "x3";
            // 
            // x4ToolStripMenuItem
            // 
            this.x4ToolStripMenuItem.Name = "x4ToolStripMenuItem";
            this.x4ToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.x4ToolStripMenuItem.Text = "x4";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // bttShowLayer1
            // 
            this.bttShowLayer1.Checked = true;
            this.bttShowLayer1.CheckOnClick = true;
            this.bttShowLayer1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bttShowLayer1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttShowLayer1.Image = ((System.Drawing.Image)(resources.GetObject("bttShowLayer1.Image")));
            this.bttShowLayer1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttShowLayer1.Name = "bttShowLayer1";
            this.bttShowLayer1.Size = new System.Drawing.Size(24, 24);
            this.bttShowLayer1.Text = "Show Layer 1";
            this.bttShowLayer1.CheckedChanged += new System.EventHandler(this.bttShowLayer1_CheckedChanged);
            // 
            // bttShowLayer2
            // 
            this.bttShowLayer2.Checked = true;
            this.bttShowLayer2.CheckOnClick = true;
            this.bttShowLayer2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bttShowLayer2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttShowLayer2.Image = ((System.Drawing.Image)(resources.GetObject("bttShowLayer2.Image")));
            this.bttShowLayer2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttShowLayer2.Name = "bttShowLayer2";
            this.bttShowLayer2.Size = new System.Drawing.Size(24, 24);
            this.bttShowLayer2.Text = "Show Layer 2";
            this.bttShowLayer2.CheckedChanged += new System.EventHandler(this.bttShowLayer2_CheckedChanged);
            // 
            // bttLayer
            // 
            this.bttLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bttLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.bttLayer.Image = ((System.Drawing.Image)(resources.GetObject("bttLayer.Image")));
            this.bttLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bttLayer.Name = "bttLayer";
            this.bttLayer.Size = new System.Drawing.Size(34, 24);
            this.bttLayer.Text = "Select layer";
            this.bttLayer.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bttLayer_DropDownItemClicked);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(92, 26);
            this.toolStripMenuItem4.Text = "1";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(92, 26);
            this.toolStripMenuItem5.Text = "2";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // tbbShowPluginInfo
            // 
            this.tbbShowPluginInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbShowPluginInfo.Image = ((System.Drawing.Image)(resources.GetObject("tbbShowPluginInfo.Image")));
            this.tbbShowPluginInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbShowPluginInfo.Name = "tbbShowPluginInfo";
            this.tbbShowPluginInfo.Size = new System.Drawing.Size(24, 24);
            this.tbbShowPluginInfo.Text = "Show plugins info";
            this.tbbShowPluginInfo.Click += new System.EventHandler(this.tbbShowPluginInfo_Click);
            // 
            // tbbShowInfo
            // 
            this.tbbShowInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbShowInfo.Image = ((System.Drawing.Image)(resources.GetObject("tbbShowInfo.Image")));
            this.tbbShowInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbShowInfo.Name = "tbbShowInfo";
            this.tbbShowInfo.Size = new System.Drawing.Size(24, 24);
            this.tbbShowInfo.Text = "Show info";
            this.tbbShowInfo.Click += new System.EventHandler(this.tbbShowInfo_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnElements);
            this.splitContainer1.Panel1.Controls.Add(this.pnBlocks);
            this.splitContainer1.Panel1.Controls.Add(this.lbStructures);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnViewScroll);
            this.splitContainer1.Size = new System.Drawing.Size(1428, 640);
            this.splitContainer1.SplitterDistance = 373;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // pnElements
            // 
            this.pnElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnElements.Controls.Add(this.label3);
            this.pnElements.Controls.Add(this.cbLevelNo);
            this.pnElements.Controls.Add(this.cbUseStructs);
            this.pnElements.Controls.Add(this.lbCoords);
            this.pnElements.Controls.Add(this.lbActiveBlock);
            this.pnElements.Controls.Add(this.label4);
            this.pnElements.Controls.Add(this.activeBlock);
            this.pnElements.Controls.Add(this.cbViewType);
            this.pnElements.Controls.Add(this.label6);
            this.pnElements.Controls.Add(this.cbScreenNo);
            this.pnElements.Controls.Add(this.pnGeneric);
            this.pnElements.Location = new System.Drawing.Point(272, 4);
            this.pnElements.Margin = new System.Windows.Forms.Padding(4);
            this.pnElements.Name = "pnElements";
            this.pnElements.Size = new System.Drawing.Size(103, 634);
            this.pnElements.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 58;
            this.label3.Text = "Level No:";
            // 
            // cbLevelNo
            // 
            this.cbLevelNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelNo.FormattingEnabled = true;
            this.cbLevelNo.Location = new System.Drawing.Point(7, 171);
            this.cbLevelNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbLevelNo.Name = "cbLevelNo";
            this.cbLevelNo.Size = new System.Drawing.Size(84, 24);
            this.cbLevelNo.TabIndex = 59;
            this.cbLevelNo.SelectedIndexChanged += new System.EventHandler(this.cbLevelNo_SelectedIndexChanged);
            // 
            // cbUseStructs
            // 
            this.cbUseStructs.AutoSize = true;
            this.cbUseStructs.Location = new System.Drawing.Point(4, 564);
            this.cbUseStructs.Margin = new System.Windows.Forms.Padding(4);
            this.cbUseStructs.Name = "cbUseStructs";
            this.cbUseStructs.Size = new System.Drawing.Size(101, 21);
            this.cbUseStructs.TabIndex = 57;
            this.cbUseStructs.Text = "Use structs";
            this.cbUseStructs.UseVisualStyleBackColor = true;
            this.cbUseStructs.CheckedChanged += new System.EventHandler(this.cbUseStructs_CheckedChanged);
            // 
            // pnBlocks
            // 
            this.pnBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocks.AutoScroll = true;
            this.pnBlocks.Controls.Add(this.blocksScreen);
            this.pnBlocks.Location = new System.Drawing.Point(4, 5);
            this.pnBlocks.Name = "pnBlocks";
            this.pnBlocks.Size = new System.Drawing.Size(261, 628);
            this.pnBlocks.TabIndex = 61;
            this.pnBlocks.SizeChanged += new System.EventHandler(this.pnBlocks_SizeChanged);
            // 
            // blocksScreen
            // 
            this.blocksScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksScreen.Location = new System.Drawing.Point(3, 3);
            this.blocksScreen.Name = "blocksScreen";
            this.blocksScreen.Size = new System.Drawing.Size(238, 332);
            this.blocksScreen.TabIndex = 5;
            this.blocksScreen.TabStop = false;
            this.blocksScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksScreen_Paint);
            this.blocksScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocksScreen_MouseDown);
            // 
            // lbStructures
            // 
            this.lbStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStructures.FormattingEnabled = true;
            this.lbStructures.ItemHeight = 16;
            this.lbStructures.Location = new System.Drawing.Point(4, 5);
            this.lbStructures.Margin = new System.Windows.Forms.Padding(4);
            this.lbStructures.Name = "lbStructures";
            this.lbStructures.Size = new System.Drawing.Size(261, 628);
            this.lbStructures.TabIndex = 6;
            this.lbStructures.Visible = false;
            this.lbStructures.SelectedIndexChanged += new System.EventHandler(this.lbStructures_SelectedIndexChanged);
            // 
            // pnViewScroll
            // 
            this.pnViewScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnViewScroll.AutoScroll = true;
            this.pnViewScroll.Controls.Add(this.pnView);
            this.pnViewScroll.Location = new System.Drawing.Point(4, 8);
            this.pnViewScroll.Name = "pnViewScroll";
            this.pnViewScroll.Size = new System.Drawing.Size(1036, 623);
            this.pnViewScroll.TabIndex = 5;
            // 
            // lbPalBytesAddr
            // 
            this.lbPalBytesAddr.AutoSize = true;
            this.lbPalBytesAddr.Location = new System.Drawing.Point(688, 9);
            this.lbPalBytesAddr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPalBytesAddr.Name = "lbPalBytesAddr";
            this.lbPalBytesAddr.Size = new System.Drawing.Size(96, 17);
            this.lbPalBytesAddr.TabIndex = 60;
            this.lbPalBytesAddr.Text = "Pal byte addr:";
            this.lbPalBytesAddr.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 674);
            this.Controls.Add(this.lbPalBytesAddr);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "-";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.FormMain_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.pnGroups.ResumeLayout(false);
            this.pnGroups.PerformLayout();
            this.pnAdvancedParams.ResumeLayout(false);
            this.pnAdvancedParams.PerformLayout();
            this.pnView.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnElements.ResumeLayout(false);
            this.pnElements.PerformLayout();
            this.pnBlocks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).EndInit();
            this.pnViewScroll.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Label lbActiveBlock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbScreenNo;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPaletteNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBlockNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBigBlockNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnView;
        private System.Windows.Forms.Label lbCoords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bttOpen;
        private System.Windows.Forms.ToolStripButton bttSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bttBigBlocks;
        private System.Windows.Forms.ToolStripButton bttBlocks;
        private System.Windows.Forms.ToolStripButton bttEnemies;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnElements;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton bttScale;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripButton bttShowNei;
        private System.Windows.Forms.ToolStripButton bttAxis;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem x3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton bttShowBrush;
        private System.Windows.Forms.ToolStripMenuItem x025ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x05ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton bttShowLayer1;
        private System.Windows.Forms.ToolStripButton bttShowLayer2;
        private System.Windows.Forms.ToolStripDropDownButton bttLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripButton bttStructures;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.CheckBox cbUseStructs;
        private System.Windows.Forms.ListBox lbStructures;
        private System.Windows.Forms.ToolStripButton bttReload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLevelNo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tbbShowPluginInfo;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton tbbShowInfo;
        private System.Windows.Forms.CheckBox cbAdvanced;
        private System.Windows.Forms.Panel pnAdvancedParams;
        private System.Windows.Forms.Panel pnGroups;
        private System.Windows.Forms.Label lbPalBytesAddr;
        private System.Windows.Forms.Panel pnBlocks;
        private System.Windows.Forms.PictureBox blocksScreen;
        private System.Windows.Forms.Panel pnViewScroll;
    }
}

