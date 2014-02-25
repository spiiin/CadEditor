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
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTileset = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.smallBlocks = new System.Windows.Forms.ImageList(this.components);
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subPalletes = new System.Windows.Forms.ImageList(this.components);
            this.pnEditCad = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSmallBlock = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.cbPart = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbReadOnly = new System.Windows.Forms.Label();
            this.btClear = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            this.pnEditCad.SuspendLayout();
            this.pnGeneric.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(291, 42);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(512, 512);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // blocksPanel
            // 
            this.blocksPanel.AutoScroll = true;
            this.blocksPanel.Location = new System.Drawing.Point(15, 236);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(251, 318);
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
            this.cbTileset.Location = new System.Drawing.Point(15, 44);
            this.cbTileset.Name = "cbTileset";
            this.cbTileset.Size = new System.Drawing.Size(251, 21);
            this.cbTileset.TabIndex = 8;
            this.cbTileset.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select tileset:";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(12, 4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(83, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // smallBlocks
            // 
            this.smallBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(231, 204);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(32, 32);
            this.pbActive.TabIndex = 13;
            this.pbActive.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 212);
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
            this.pnEditCad.Location = new System.Drawing.Point(15, 71);
            this.pnEditCad.Name = "pnEditCad";
            this.pnEditCad.Size = new System.Drawing.Size(251, 81);
            this.pnEditCad.TabIndex = 51;
            this.pnEditCad.Visible = false;
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
            // pnGeneric
            // 
            this.pnGeneric.Controls.Add(this.label8);
            this.pnGeneric.Controls.Add(this.cbSmallBlock);
            this.pnGeneric.Controls.Add(this.label9);
            this.pnGeneric.Controls.Add(this.cbPaletteNo);
            this.pnGeneric.Controls.Add(this.label7);
            this.pnGeneric.Controls.Add(this.cbVideoNo);
            this.pnGeneric.Location = new System.Drawing.Point(15, 81);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(251, 117);
            this.pnGeneric.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Small blocks:";
            // 
            // cbSmallBlock
            // 
            this.cbSmallBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSmallBlock.FormattingEnabled = true;
            this.cbSmallBlock.Items.AddRange(new object[] {
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
            this.cbSmallBlock.Location = new System.Drawing.Point(6, 56);
            this.cbSmallBlock.Name = "cbSmallBlock";
            this.cbSmallBlock.Size = new System.Drawing.Size(236, 21);
            this.cbSmallBlock.TabIndex = 49;
            this.cbSmallBlock.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 82);
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
            this.cbPaletteNo.Location = new System.Drawing.Point(6, 95);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(236, 21);
            this.cbPaletteNo.TabIndex = 47;
            this.cbPaletteNo.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "VideoBlock:";
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
            this.cbVideoNo.Location = new System.Drawing.Point(5, 19);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(237, 21);
            this.cbVideoNo.TabIndex = 41;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // cbPart
            // 
            this.cbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPart.FormattingEnabled = true;
            this.cbPart.Location = new System.Drawing.Point(301, 4);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(38, 21);
            this.cbPart.TabIndex = 55;
            this.cbPart.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 7);
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
            "Tiles",
            "Tile types",
            "Tile numbers"});
            this.cbViewType.Location = new System.Drawing.Point(83, 209);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(64, 21);
            this.cbViewType.TabIndex = 58;
            this.cbViewType.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "View type:";
            // 
            // lbReadOnly
            // 
            this.lbReadOnly.AutoSize = true;
            this.lbReadOnly.Location = new System.Drawing.Point(21, 9);
            this.lbReadOnly.Name = "lbReadOnly";
            this.lbReadOnly.Size = new System.Drawing.Size(69, 13);
            this.lbReadOnly.TabIndex = 23;
            this.lbReadOnly.Text = "READ ONLY";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(345, 2);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 59;
            this.btClear.Text = "Clear all";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(96, 4);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(83, 23);
            this.btExport.TabIndex = 60;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(180, 4);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(83, 23);
            this.btImport.TabIndex = 61;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // BigBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 569);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.lbReadOnly);
            this.Controls.Add(this.pnGeneric);
            this.Controls.Add(this.cbViewType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnEditCad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbPart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbActive);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTileset);
            this.Controls.Add(this.blocksPanel);
            this.Controls.Add(this.mapScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BigBlockEdit";
            this.Text = "Macro Blocks Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BigBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BigBlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            this.pnEditCad.ResumeLayout(false);
            this.pnEditCad.PerformLayout();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.ComboBox cbTileset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ImageList smallBlocks;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList subPalletes;
        private System.Windows.Forms.Panel pnEditCad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDoor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLevel;
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
        private System.Windows.Forms.ComboBox cbSmallBlock;
        private System.Windows.Forms.Label lbReadOnly;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Button btImport;
    }
}