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
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTileset = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.smallBlocks = new System.Windows.Forms.ImageList(this.components);
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subPalletes = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(269, 26);
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
            this.blocksPanel.Location = new System.Drawing.Point(15, 189);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(251, 349);
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
            this.cbTileset.Location = new System.Drawing.Point(12, 42);
            this.cbTileset.Name = "cbTileset";
            this.cbTileset.Size = new System.Drawing.Size(251, 21);
            this.cbTileset.TabIndex = 8;
            this.cbTileset.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select tileset:";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(15, 160);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(134, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
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
            this.cbLevel.Location = new System.Drawing.Point(12, 82);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(251, 21);
            this.cbLevel.TabIndex = 11;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "View with level:";
            // 
            // smallBlocks
            // 
            this.smallBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.smallBlocks.ImageSize = new System.Drawing.Size(16, 16);
            this.smallBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(231, 151);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(32, 32);
            this.pbActive.TabIndex = 13;
            this.pbActive.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 165);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 16;
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
            this.cbDoor.Location = new System.Drawing.Point(12, 122);
            this.cbDoor.Name = "cbDoor";
            this.cbDoor.Size = new System.Drawing.Size(251, 21);
            this.cbDoor.TabIndex = 15;
            this.cbDoor.SelectedIndexChanged += new System.EventHandler(this.cbLevelPair_SelectedIndexChanged);
            // 
            // BigBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 550);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbDoor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbActive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTileset);
            this.Controls.Add(this.blocksPanel);
            this.Controls.Add(this.mapScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BigBlockEdit";
            this.Text = "Big Blocks Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BigBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BigBlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.ComboBox cbTileset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList smallBlocks;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList subPalletes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDoor;
    }
}