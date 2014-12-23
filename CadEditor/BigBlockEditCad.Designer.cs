namespace CadEditor
{
    partial class BigBlockEditCad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BigBlockEditCad));
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTileset = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.smallBlocks = new System.Windows.Forms.ImageList(this.components);
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subPalletes = new System.Windows.Forms.ImageList(this.components);
            this.pnEditCad = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.cbPart = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.pnEditCad.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(3, 3);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(512, 512);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            this.mapScreen.MouseLeave += new System.EventHandler(this.mapScreen_MouseLeave);
            this.mapScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseMove);
            // 
            // blocksPanel
            // 
            this.blocksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksPanel.AutoScroll = true;
            this.blocksPanel.Location = new System.Drawing.Point(8, 211);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(249, 330);
            this.blocksPanel.TabIndex = 7;
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
            this.cbTileset.Location = new System.Drawing.Point(8, 19);
            this.cbTileset.Name = "cbTileset";
            this.cbTileset.Size = new System.Drawing.Size(251, 21);
            this.cbTileset.TabIndex = 8;
            this.cbTileset.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Blocks:";
            // 
            // smallBlocks
            // 
            this.smallBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(224, 179);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(32, 32);
            this.pbActive.TabIndex = 13;
            this.pbActive.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Currect block:";
            // 
            // subPalletes
            // 
            this.subPalletes.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.subPalletes.ImageSize = new System.Drawing.Size(16, 16);
            this.subPalletes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnEditCad
            // 
            this.pnEditCad.Controls.Add(this.label4);
            this.pnEditCad.Controls.Add(this.cbDoor);
            this.pnEditCad.Controls.Add(this.label2);
            this.pnEditCad.Controls.Add(this.cbLevel);
            this.pnEditCad.Location = new System.Drawing.Point(8, 46);
            this.pnEditCad.Name = "pnEditCad";
            this.pnEditCad.Size = new System.Drawing.Size(251, 81);
            this.pnEditCad.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Door:";
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
            this.cbDoor.Location = new System.Drawing.Point(6, 58);
            this.cbDoor.Name = "cbDoor";
            this.cbDoor.Size = new System.Drawing.Size(236, 21);
            this.cbDoor.TabIndex = 19;
            this.cbDoor.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "View with level:";
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
            this.cbLevel.Location = new System.Drawing.Point(6, 18);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(236, 21);
            this.cbLevel.TabIndex = 17;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // cbPart
            // 
            this.cbPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPart.FormattingEnabled = true;
            this.cbPart.Location = new System.Drawing.Point(773, 2);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(38, 21);
            this.cbPart.TabIndex = 55;
            this.cbPart.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(738, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
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
            this.cbViewType.Location = new System.Drawing.Point(76, 184);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(64, 21);
            this.cbViewType.TabIndex = 58;
            this.cbViewType.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
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
            this.splitContainer1.Location = new System.Drawing.Point(3, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.blocksPanel);
            this.splitContainer1.Panel1.Controls.Add(this.cbTileset);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.pbActive);
            this.splitContainer1.Panel1.Controls.Add(this.cbViewType);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.pnEditCad);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbBigBlockNo);
            this.splitContainer1.Panel2.Controls.Add(this.mapScreen);
            this.splitContainer1.Size = new System.Drawing.Size(808, 547);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 62;
            // 
            // lbBigBlockNo
            // 
            this.lbBigBlockNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBigBlockNo.AutoSize = true;
            this.lbBigBlockNo.Location = new System.Drawing.Point(502, 528);
            this.lbBigBlockNo.Name = "lbBigBlockNo";
            this.lbBigBlockNo.Size = new System.Drawing.Size(13, 13);
            this.lbBigBlockNo.TabIndex = 59;
            this.lbBigBlockNo.Text = "()";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbSave,
            this.toolStripSeparator2,
            this.tbbExport,
            this.tbbImport,
            this.toolStripSeparator1,
            this.tbbDeleteAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(815, 25);
            this.toolStrip1.TabIndex = 63;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbbSave.Image")));
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(23, 22);
            this.tbbSave.Text = "Save";
            this.tbbSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbbExport
            // 
            this.tbbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExport.Image = ((System.Drawing.Image)(resources.GetObject("tbbExport.Image")));
            this.tbbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExport.Name = "tbbExport";
            this.tbbExport.Size = new System.Drawing.Size(23, 22);
            this.tbbExport.Text = "Export";
            this.tbbExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // tbbImport
            // 
            this.tbbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbImport.Image = ((System.Drawing.Image)(resources.GetObject("tbbImport.Image")));
            this.tbbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbImport.Name = "tbbImport";
            this.tbbImport.Size = new System.Drawing.Size(23, 22);
            this.tbbImport.Text = "Import";
            this.tbbImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbbDeleteAll
            // 
            this.tbbDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("tbbDeleteAll.Image")));
            this.tbbDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbDeleteAll.Name = "tbbDeleteAll";
            this.tbbDeleteAll.Size = new System.Drawing.Size(23, 22);
            this.tbbDeleteAll.Text = "Clear All";
            this.tbbDeleteAll.Click += new System.EventHandler(this.btClear_Click);
            // 
            // BigBlockEditCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 581);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cbPart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BigBlockEditCad";
            this.Text = "Macro Blocks Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BigBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BigBlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            this.pnEditCad.ResumeLayout(false);
            this.pnEditCad.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.ComboBox cbTileset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList smallBlocks;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList subPalletes;
        private System.Windows.Forms.Panel pnEditCad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDoor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.ComboBox cbPart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripButton tbbExport;
        private System.Windows.Forms.ToolStripButton tbbImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbbDeleteAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lbBigBlockNo;
    }
}