namespace CadEditor
{
    partial class BigBlockEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BigBlockEdit));
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.cbTileset = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subPalletes = new System.Windows.Forms.ImageList(this.components);
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBigBlock = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.cbPart = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnBlocks = new System.Windows.Forms.Panel();
            this.blocksScreen = new System.Windows.Forms.PictureBox();
            this.cbHierarchyLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbActive = new System.Windows.Forms.Label();
            this.lbBigBlockNo = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbExport = new System.Windows.Forms.ToolStripButton();
            this.tbbImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbDeleteAll = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            this.pnGeneric.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnBlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(4, 4);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(683, 630);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            this.mapScreen.MouseLeave += new System.EventHandler(this.mapScreen_MouseLeave);
            this.mapScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseMove);
            // 
            // cbTileset
            // 
            this.cbTileset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTileset.FormattingEnabled = true;
            this.cbTileset.Items.AddRange(new object[] {
            "Tileset0 (3000)",
            "Tileset1 (7000)",
            "Tileset2 (B000)",
            "Tileset3 (F000)",
            "Tileset4 (13000)",
            "Tileset5 (17000)",
            "Tileset6 (1B000)"});
            this.cbTileset.Location = new System.Drawing.Point(8, 64);
            this.cbTileset.Margin = new System.Windows.Forms.Padding(4);
            this.cbTileset.Name = "cbTileset";
            this.cbTileset.Size = new System.Drawing.Size(257, 24);
            this.cbTileset.TabIndex = 8;
            this.cbTileset.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Blocks:";
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(299, 220);
            this.pbActive.Margin = new System.Windows.Forms.Padding(4);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(43, 39);
            this.pbActive.TabIndex = 13;
            this.pbActive.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 258);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Currect block:";
            // 
            // subPalletes
            // 
            this.subPalletes.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.subPalletes.ImageSize = new System.Drawing.Size(16, 16);
            this.subPalletes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnGeneric
            // 
            this.pnGeneric.Controls.Add(this.label8);
            this.pnGeneric.Controls.Add(this.cbBigBlock);
            this.pnGeneric.Controls.Add(this.label9);
            this.pnGeneric.Controls.Add(this.cbPaletteNo);
            this.pnGeneric.Controls.Add(this.label7);
            this.pnGeneric.Controls.Add(this.cbVideoNo);
            this.pnGeneric.Location = new System.Drawing.Point(8, 110);
            this.pnGeneric.Margin = new System.Windows.Forms.Padding(4);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(264, 144);
            this.pnGeneric.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 53);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 50;
            this.label8.Text = "Big blocks:";
            // 
            // cbBigBlock
            // 
            this.cbBigBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBigBlock.FormattingEnabled = true;
            this.cbBigBlock.Items.AddRange(new object[] {
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
            this.cbBigBlock.Location = new System.Drawing.Point(8, 69);
            this.cbBigBlock.Margin = new System.Windows.Forms.Padding(4);
            this.cbBigBlock.Name = "cbBigBlock";
            this.cbBigBlock.Size = new System.Drawing.Size(249, 24);
            this.cbBigBlock.TabIndex = 49;
            this.cbBigBlock.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 101);
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
            this.cbPaletteNo.Location = new System.Drawing.Point(8, 117);
            this.cbPaletteNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(249, 24);
            this.cbPaletteNo.TabIndex = 47;
            this.cbPaletteNo.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 4);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 42;
            this.label7.Text = "Video:";
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
            this.cbVideoNo.Location = new System.Drawing.Point(7, 23);
            this.cbVideoNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(250, 24);
            this.cbVideoNo.TabIndex = 41;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // cbPart
            // 
            this.cbPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPart.FormattingEnabled = true;
            this.cbPart.Location = new System.Drawing.Point(1031, 2);
            this.cbPart.Margin = new System.Windows.Forms.Padding(4);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(49, 24);
            this.cbPart.TabIndex = 55;
            this.cbPart.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(984, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 56;
            this.label5.Text = "Part:";
            // 
            // cbViewType
            // 
            this.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViewType.DropDownWidth = 128;
            this.cbViewType.FormattingEnabled = true;
            this.cbViewType.Items.AddRange(new object[] {
            "Blocks",
            "Block Types",
            "Block numbers"});
            this.cbViewType.Location = new System.Drawing.Point(88, 267);
            this.cbViewType.Margin = new System.Windows.Forms.Padding(4);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(84, 24);
            this.cbViewType.TabIndex = 58;
            this.cbViewType.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 270);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 57;
            this.label6.Text = "View type:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(4, 31);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnBlocks);
            this.splitContainer1.Panel1.Controls.Add(this.cbHierarchyLevel);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lbActive);
            this.splitContainer1.Panel1.Controls.Add(this.cbTileset);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.pnGeneric);
            this.splitContainer1.Panel1.Controls.Add(this.pbActive);
            this.splitContainer1.Panel1.Controls.Add(this.cbViewType);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.lbBigBlockNo);
            this.splitContainer1.Panel2.Controls.Add(this.mapScreen);
            this.splitContainer1.Size = new System.Drawing.Size(1077, 673);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 62;
            // 
            // pnBlocks
            // 
            this.pnBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocks.AutoScroll = true;
            this.pnBlocks.Controls.Add(this.blocksScreen);
            this.pnBlocks.Location = new System.Drawing.Point(8, 298);
            this.pnBlocks.Name = "pnBlocks";
            this.pnBlocks.Size = new System.Drawing.Size(261, 370);
            this.pnBlocks.TabIndex = 62;
            this.pnBlocks.SizeChanged += new System.EventHandler(this.pnBlocks_SizeChanged);
            // 
            // blocksScreen
            // 
            this.blocksScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksScreen.Location = new System.Drawing.Point(3, 3);
            this.blocksScreen.Name = "blocksScreen";
            this.blocksScreen.Size = new System.Drawing.Size(254, 332);
            this.blocksScreen.TabIndex = 5;
            this.blocksScreen.TabStop = false;
            this.blocksScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksScreen_Paint);
            this.blocksScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocksScreen_MouseDown);
            // 
            // cbHierarchyLevel
            // 
            this.cbHierarchyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHierarchyLevel.FormattingEnabled = true;
            this.cbHierarchyLevel.Location = new System.Drawing.Point(8, 21);
            this.cbHierarchyLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cbHierarchyLevel.Name = "cbHierarchyLevel";
            this.cbHierarchyLevel.Size = new System.Drawing.Size(257, 24);
            this.cbHierarchyLevel.TabIndex = 60;
            this.cbHierarchyLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 61;
            this.label2.Text = "Hierarchy level:";
            // 
            // lbActive
            // 
            this.lbActive.AutoSize = true;
            this.lbActive.Location = new System.Drawing.Point(273, 240);
            this.lbActive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbActive.Name = "lbActive";
            this.lbActive.Size = new System.Drawing.Size(18, 17);
            this.lbActive.TabIndex = 59;
            this.lbActive.Text = "()";
            // 
            // lbBigBlockNo
            // 
            this.lbBigBlockNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBigBlockNo.AutoSize = true;
            this.lbBigBlockNo.Location = new System.Drawing.Point(760, 650);
            this.lbBigBlockNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBigBlockNo.Name = "lbBigBlockNo";
            this.lbBigBlockNo.Size = new System.Drawing.Size(18, 17);
            this.lbBigBlockNo.TabIndex = 59;
            this.lbBigBlockNo.Text = "()";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbSave,
            this.toolStripSeparator2,
            this.tbbExport,
            this.tbbImport,
            this.toolStripSeparator1,
            this.tbbDeleteAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1087, 27);
            this.toolStrip1.TabIndex = 63;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbbSave.Image")));
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(24, 24);
            this.tbbSave.Text = "Save";
            this.tbbSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tbbExport
            // 
            this.tbbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExport.Image = ((System.Drawing.Image)(resources.GetObject("tbbExport.Image")));
            this.tbbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExport.Name = "tbbExport";
            this.tbbExport.Size = new System.Drawing.Size(24, 24);
            this.tbbExport.Text = "Export";
            this.tbbExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // tbbImport
            // 
            this.tbbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbImport.Enabled = false;
            this.tbbImport.Image = ((System.Drawing.Image)(resources.GetObject("tbbImport.Image")));
            this.tbbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbImport.Name = "tbbImport";
            this.tbbImport.Size = new System.Drawing.Size(24, 24);
            this.tbbImport.Text = "Import";
            this.tbbImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tbbDeleteAll
            // 
            this.tbbDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("tbbDeleteAll.Image")));
            this.tbbDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbDeleteAll.Name = "tbbDeleteAll";
            this.tbbDeleteAll.Size = new System.Drawing.Size(24, 24);
            this.tbbDeleteAll.Text = "Clear All";
            this.tbbDeleteAll.Click += new System.EventHandler(this.btClear_Click);
            // 
            // BigBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 715);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cbPart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "BigBlockEdit";
            this.Text = "Macro Blocks Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BigBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BigBlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnBlocks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.ComboBox cbTileset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList subPalletes;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPaletteNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.ComboBox cbPart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBigBlock;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripButton tbbExport;
        private System.Windows.Forms.ToolStripButton tbbImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbbDeleteAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lbBigBlockNo;
        private System.Windows.Forms.Label lbActive;
        private System.Windows.Forms.ComboBox cbHierarchyLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnBlocks;
        private System.Windows.Forms.PictureBox blocksScreen;
    }
}