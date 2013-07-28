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
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.btLeft = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.cbScreenNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.objPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lvObjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectSprites = new System.Windows.Forms.ImageList(this.components);
            this.cbCoordY = new System.Windows.Forms.ComboBox();
            this.cbCoordX = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btClearObjs = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.btSave = new System.Windows.Forms.Button();
            this.cbManualSort = new System.Windows.Forms.CheckBox();
            this.btSortUp = new System.Windows.Forms.Button();
            this.btSortDown = new System.Windows.Forms.Button();
            this.cbStopOnDoors = new System.Windows.Forms.CheckBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
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
            this.pnCad = new System.Windows.Forms.Panel();
            this.pnSelectScreen = new System.Windows.Forms.Panel();
            this.lbScrNo = new System.Windows.Forms.Label();
            this.lbActive = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbReadOnly = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnCad.SuspendLayout();
            this.pnSelectScreen.SuspendLayout();
            this.SuspendLayout();
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
            this.cbLevel.Location = new System.Drawing.Point(69, 11);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(104, 21);
            this.cbLevel.TabIndex = 29;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Select level:";
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(193, 17);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(512, 512);
            this.mapScreen.TabIndex = 30;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // btLeft
            // 
            this.btLeft.Location = new System.Drawing.Point(14, 21);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(53, 22);
            this.btLeft.TabIndex = 31;
            this.btLeft.Text = "←";
            this.btLeft.UseVisualStyleBackColor = true;
            this.btLeft.Click += new System.EventHandler(this.btLeft_Click);
            // 
            // btRight
            // 
            this.btRight.Location = new System.Drawing.Point(124, 21);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(53, 22);
            this.btRight.TabIndex = 32;
            this.btRight.Text = "→";
            this.btRight.UseVisualStyleBackColor = true;
            this.btRight.Click += new System.EventHandler(this.btRight_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(69, 0);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(53, 22);
            this.btUp.TabIndex = 33;
            this.btUp.Text = "↑";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(69, 50);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(53, 22);
            this.btDown.TabIndex = 34;
            this.btDown.Text = "↓";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // cbScreenNo
            // 
            this.cbScreenNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenNo.FormattingEnabled = true;
            this.cbScreenNo.Location = new System.Drawing.Point(69, 23);
            this.cbScreenNo.Name = "cbScreenNo";
            this.cbScreenNo.Size = new System.Drawing.Size(53, 21);
            this.cbScreenNo.TabIndex = 35;
            this.cbScreenNo.SelectedIndexChanged += new System.EventHandler(this.cbScreenNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Screen No:";
            // 
            // objPanel
            // 
            this.objPanel.AutoScroll = true;
            this.objPanel.Location = new System.Drawing.Point(12, 294);
            this.objPanel.Name = "objPanel";
            this.objPanel.Size = new System.Drawing.Size(172, 177);
            this.objPanel.TabIndex = 37;
            // 
            // lvObjects
            // 
            this.lvObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjects.Location = new System.Drawing.Point(711, 17);
            this.lvObjects.Name = "lvObjects";
            this.lvObjects.Size = new System.Drawing.Size(231, 373);
            this.lvObjects.SmallImageList = this.objectSprites;
            this.lvObjects.TabIndex = 38;
            this.lvObjects.UseCompatibleStateImageBehavior = false;
            this.lvObjects.View = System.Windows.Forms.View.Details;
            this.lvObjects.SelectedIndexChanged += new System.EventHandler(this.lvObjects_SelectedIndexChanged);
            this.lvObjects.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbObjects_KeyUp);
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
            // cbCoordY
            // 
            this.cbCoordY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoordY.Enabled = false;
            this.cbCoordY.FormattingEnabled = true;
            this.cbCoordY.Items.AddRange(new object[] {
            "Tiles",
            "Obj types"});
            this.cbCoordY.Location = new System.Drawing.Point(877, 393);
            this.cbCoordY.Name = "cbCoordY";
            this.cbCoordY.Size = new System.Drawing.Size(66, 21);
            this.cbCoordY.TabIndex = 42;
            this.cbCoordY.SelectedIndexChanged += new System.EventHandler(this.cbCoordY_SelectedIndexChanged);
            // 
            // cbCoordX
            // 
            this.cbCoordX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoordX.Enabled = false;
            this.cbCoordX.FormattingEnabled = true;
            this.cbCoordX.Items.AddRange(new object[] {
            "Tiles",
            "Obj types"});
            this.cbCoordX.Location = new System.Drawing.Point(805, 393);
            this.cbCoordX.Name = "cbCoordX";
            this.cbCoordX.Size = new System.Drawing.Size(66, 21);
            this.cbCoordX.TabIndex = 41;
            this.cbCoordX.SelectedIndexChanged += new System.EventHandler(this.cbCoordX_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(712, 396);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Object coords:";
            // 
            // btClearObjs
            // 
            this.btClearObjs.Location = new System.Drawing.Point(805, 441);
            this.btClearObjs.Name = "btClearObjs";
            this.btClearObjs.Size = new System.Drawing.Size(138, 24);
            this.btClearObjs.TabIndex = 39;
            this.btClearObjs.Text = "clear all objects on screen";
            this.btClearObjs.UseVisualStyleBackColor = true;
            this.btClearObjs.Click += new System.EventHandler(this.btClearObjs_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 477);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Active:";
            // 
            // activeBlock
            // 
            this.activeBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeBlock.Location = new System.Drawing.Point(151, 477);
            this.activeBlock.Name = "activeBlock";
            this.activeBlock.Size = new System.Drawing.Size(32, 32);
            this.activeBlock.TabIndex = 43;
            this.activeBlock.TabStop = false;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(12, 484);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(73, 25);
            this.btSave.TabIndex = 45;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbManualSort
            // 
            this.cbManualSort.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbManualSort.Checked = true;
            this.cbManualSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbManualSort.Location = new System.Drawing.Point(855, 383);
            this.cbManualSort.Name = "cbManualSort";
            this.cbManualSort.Size = new System.Drawing.Size(87, 18);
            this.cbManualSort.TabIndex = 46;
            this.cbManualSort.Text = "Manual sort";
            this.cbManualSort.UseVisualStyleBackColor = true;
            this.cbManualSort.Visible = false;
            this.cbManualSort.CheckedChanged += new System.EventHandler(this.cbManualSort_CheckedChanged);
            // 
            // btSortUp
            // 
            this.btSortUp.Location = new System.Drawing.Point(805, 417);
            this.btSortUp.Name = "btSortUp";
            this.btSortUp.Size = new System.Drawing.Size(66, 22);
            this.btSortUp.TabIndex = 47;
            this.btSortUp.Text = "↑";
            this.btSortUp.UseVisualStyleBackColor = true;
            this.btSortUp.Click += new System.EventHandler(this.btSortUp_Click);
            // 
            // btSortDown
            // 
            this.btSortDown.Location = new System.Drawing.Point(877, 417);
            this.btSortDown.Name = "btSortDown";
            this.btSortDown.Size = new System.Drawing.Size(66, 22);
            this.btSortDown.TabIndex = 48;
            this.btSortDown.Text = "↓";
            this.btSortDown.UseVisualStyleBackColor = true;
            this.btSortDown.Click += new System.EventHandler(this.btSortDown_Click);
            // 
            // cbStopOnDoors
            // 
            this.cbStopOnDoors.AutoSize = true;
            this.cbStopOnDoors.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbStopOnDoors.Checked = true;
            this.cbStopOnDoors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStopOnDoors.Location = new System.Drawing.Point(12, 515);
            this.cbStopOnDoors.Name = "cbStopOnDoors";
            this.cbStopOnDoors.Size = new System.Drawing.Size(156, 17);
            this.cbStopOnDoors.TabIndex = 49;
            this.cbStopOnDoors.Text = "Branch type (stop on doors)";
            this.cbStopOnDoors.UseVisualStyleBackColor = true;
            // 
            // pnGeneric
            // 
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
            this.pnGeneric.Location = new System.Drawing.Point(14, 17);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(175, 133);
            this.pnGeneric.TabIndex = 50;
            // 
            // cbPlus256
            // 
            this.cbPlus256.AutoSize = true;
            this.cbPlus256.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbPlus256.Location = new System.Drawing.Point(96, 4);
            this.cbPlus256.Name = "cbPlus256";
            this.cbPlus256.Size = new System.Drawing.Size(75, 31);
            this.cbPlus256.TabIndex = 55;
            this.cbPlus256.Text = "+256 screens";
            this.cbPlus256.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
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
            this.cbLayoutNo.Location = new System.Drawing.Point(4, 16);
            this.cbLayoutNo.Name = "cbLayoutNo";
            this.cbLayoutNo.Size = new System.Drawing.Size(86, 21);
            this.cbLayoutNo.TabIndex = 53;
            this.cbLayoutNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 91);
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
            this.cbPaletteNo.Location = new System.Drawing.Point(8, 107);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(53, 21);
            this.cbPaletteNo.TabIndex = 47;
            this.cbPaletteNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 90);
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
            this.cbBlockNo.Location = new System.Drawing.Point(66, 106);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(45, 21);
            this.cbBlockNo.TabIndex = 45;
            this.cbBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
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
            this.cbBigBlockNo.Location = new System.Drawing.Point(66, 66);
            this.cbBigBlockNo.Name = "cbBigBlockNo";
            this.cbBigBlockNo.Size = new System.Drawing.Size(45, 21);
            this.cbBigBlockNo.TabIndex = 43;
            this.cbBigBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
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
            this.cbVideoNo.Location = new System.Drawing.Point(6, 65);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(55, 21);
            this.cbVideoNo.TabIndex = 41;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // pnCad
            // 
            this.pnCad.Controls.Add(this.label6);
            this.pnCad.Controls.Add(this.cbLevel);
            this.pnCad.Location = new System.Drawing.Point(17, 12);
            this.pnCad.Name = "pnCad";
            this.pnCad.Size = new System.Drawing.Size(175, 50);
            this.pnCad.TabIndex = 51;
            // 
            // pnSelectScreen
            // 
            this.pnSelectScreen.Controls.Add(this.lbScrNo);
            this.pnSelectScreen.Controls.Add(this.label1);
            this.pnSelectScreen.Controls.Add(this.btLeft);
            this.pnSelectScreen.Controls.Add(this.btRight);
            this.pnSelectScreen.Controls.Add(this.btUp);
            this.pnSelectScreen.Controls.Add(this.btDown);
            this.pnSelectScreen.Controls.Add(this.cbScreenNo);
            this.pnSelectScreen.Location = new System.Drawing.Point(9, 156);
            this.pnSelectScreen.Name = "pnSelectScreen";
            this.pnSelectScreen.Size = new System.Drawing.Size(178, 78);
            this.pnSelectScreen.TabIndex = 52;
            // 
            // lbScrNo
            // 
            this.lbScrNo.AutoSize = true;
            this.lbScrNo.Location = new System.Drawing.Point(128, 55);
            this.lbScrNo.Name = "lbScrNo";
            this.lbScrNo.Size = new System.Drawing.Size(13, 13);
            this.lbScrNo.TabIndex = 55;
            this.lbScrNo.Text = "()";
            // 
            // lbActive
            // 
            this.lbActive.AutoSize = true;
            this.lbActive.Location = new System.Drawing.Point(105, 492);
            this.lbActive.Name = "lbActive";
            this.lbActive.Size = new System.Drawing.Size(13, 13);
            this.lbActive.TabIndex = 55;
            this.lbActive.Text = "()";
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(712, 441);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(87, 24);
            this.btDelete.TabIndex = 56;
            this.btDelete.Text = "delete selected";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(715, 468);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Press CTRL or SHIFT to select many objects";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(712, 422);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 58;
            this.label10.Text = "Objects order:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(670, 436);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 59;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(715, 484);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(181, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Press DEL to delete selected objects";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(715, 500);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(215, 13);
            this.label14.TabIndex = 61;
            this.label14.Text = "Select object in the list for change its coords";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(716, 516);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(180, 13);
            this.label15.TabIndex = 62;
            this.label15.Text = "Don\'t forget sort objects in right order";
            // 
            // lbReadOnly
            // 
            this.lbReadOnly.AutoSize = true;
            this.lbReadOnly.Location = new System.Drawing.Point(13, 490);
            this.lbReadOnly.Name = "lbReadOnly";
            this.lbReadOnly.Size = new System.Drawing.Size(69, 13);
            this.lbReadOnly.TabIndex = 63;
            this.lbReadOnly.Text = "READ ONLY";
            // 
            // EnemyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 533);
            this.Controls.Add(this.lbReadOnly);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.pnCad);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.lbActive);
            this.Controls.Add(this.pnSelectScreen);
            this.Controls.Add(this.pnGeneric);
            this.Controls.Add(this.cbStopOnDoors);
            this.Controls.Add(this.btSortDown);
            this.Controls.Add(this.btSortUp);
            this.Controls.Add(this.cbManualSort);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.cbCoordY);
            this.Controls.Add(this.cbCoordX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btClearObjs);
            this.Controls.Add(this.lvObjects);
            this.Controls.Add(this.objPanel);
            this.Controls.Add(this.mapScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EnemyEditor";
            this.Text = "Enemy Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnemyEditor_FormClosing);
            this.Load += new System.EventHandler(this.EnemyEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.pnCad.ResumeLayout(false);
            this.pnCad.PerformLayout();
            this.pnSelectScreen.ResumeLayout(false);
            this.pnSelectScreen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.ComboBox cbScreenNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel objPanel;
        private System.Windows.Forms.ListView lvObjects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox cbCoordY;
        private System.Windows.Forms.ComboBox cbCoordX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btClearObjs;
        private System.Windows.Forms.ImageList objectSprites;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.CheckBox cbManualSort;
        private System.Windows.Forms.Button btSortUp;
        private System.Windows.Forms.Button btSortDown;
        private System.Windows.Forms.CheckBox cbStopOnDoors;
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
        private System.Windows.Forms.Panel pnCad;
        private System.Windows.Forms.Panel pnSelectScreen;
        private System.Windows.Forms.Label lbScrNo;
        private System.Windows.Forms.CheckBox cbPlus256;
        private System.Windows.Forms.Label lbActive;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbReadOnly;
    }
}