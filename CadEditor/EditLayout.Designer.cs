namespace CadEditor
{
    partial class EditLayout
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
            this.screenImages = new System.Windows.Forms.ImageList(this.components);
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.btSave = new System.Windows.Forms.Button();
            this.scrollSprites = new System.Windows.Forms.ImageList(this.components);
            this.objPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.doorSprites = new System.Windows.Forms.ImageList(this.components);
            this.cbShowScrolls = new System.Windows.Forms.CheckBox();
            this.lvObjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btPreview = new System.Windows.Forms.Button();
            this.dirSprites = new System.Windows.Forms.ImageList(this.components);
            this.doorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btLevelParams = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbStopOnDoor = new System.Windows.Forms.CheckBox();
            this.pnLevelLayout = new System.Windows.Forms.Panel();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.cbLayoutNo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbHeight = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbWidth = new System.Windows.Forms.ComboBox();
            this.pnCad = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cbGame = new System.Windows.Forms.ComboBox();
            this.pnDoors = new System.Windows.Forms.Panel();
            this.pnIngameScreenOrder = new System.Windows.Forms.Panel();
            this.pnSelectScroll = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.pnLevelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnCad.SuspendLayout();
            this.pnDoors.SuspendLayout();
            this.pnIngameScreenOrder.SuspendLayout();
            this.pnSelectScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // screenImages
            // 
            this.screenImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.screenImages.ImageSize = new System.Drawing.Size(64, 64);
            this.screenImages.TransparentColor = System.Drawing.Color.Transparent;
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
            this.cbLevel.Location = new System.Drawing.Point(3, 16);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(64, 21);
            this.cbLevel.TabIndex = 0;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Level:";
            // 
            // blocksPanel
            // 
            this.blocksPanel.AutoScroll = true;
            this.blocksPanel.Location = new System.Drawing.Point(12, 15);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(316, 329);
            this.blocksPanel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Active:";
            // 
            // activeBlock
            // 
            this.activeBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeBlock.Location = new System.Drawing.Point(333, 31);
            this.activeBlock.Name = "activeBlock";
            this.activeBlock.Size = new System.Drawing.Size(64, 64);
            this.activeBlock.TabIndex = 17;
            this.activeBlock.TabStop = false;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(336, 312);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(64, 22);
            this.btSave.TabIndex = 19;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // scrollSprites
            // 
            this.scrollSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.scrollSprites.ImageSize = new System.Drawing.Size(16, 16);
            this.scrollSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // objPanel
            // 
            this.objPanel.AutoScroll = true;
            this.objPanel.Location = new System.Drawing.Point(6, 20);
            this.objPanel.Name = "objPanel";
            this.objPanel.Size = new System.Drawing.Size(325, 44);
            this.objPanel.TabIndex = 20;
            // 
            // doorSprites
            // 
            this.doorSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.doorSprites.ImageSize = new System.Drawing.Size(16, 16);
            this.doorSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbShowScrolls
            // 
            this.cbShowScrolls.AutoSize = true;
            this.cbShowScrolls.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbShowScrolls.Checked = true;
            this.cbShowScrolls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowScrolls.Location = new System.Drawing.Point(336, 274);
            this.cbShowScrolls.Name = "cbShowScrolls";
            this.cbShowScrolls.Size = new System.Drawing.Size(68, 31);
            this.cbShowScrolls.TabIndex = 21;
            this.cbShowScrolls.Text = "show scrolls";
            this.cbShowScrolls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbShowScrolls.UseVisualStyleBackColor = true;
            this.cbShowScrolls.CheckedChanged += new System.EventHandler(this.cbShowScrolls_CheckedChanged);
            // 
            // lvObjects
            // 
            this.lvObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvObjects.Location = new System.Drawing.Point(6, 16);
            this.lvObjects.Name = "lvObjects";
            this.lvObjects.Size = new System.Drawing.Size(92, 436);
            this.lvObjects.TabIndex = 22;
            this.lvObjects.UseCompatibleStateImageBehavior = false;
            this.lvObjects.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 170;
            // 
            // btPreview
            // 
            this.btPreview.Location = new System.Drawing.Point(6, 481);
            this.btPreview.Name = "btPreview";
            this.btPreview.Size = new System.Drawing.Size(92, 28);
            this.btPreview.TabIndex = 23;
            this.btPreview.Text = "make preview";
            this.btPreview.UseVisualStyleBackColor = true;
            this.btPreview.Click += new System.EventHandler(this.btPreview_Click);
            // 
            // dirSprites
            // 
            this.dirSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.dirSprites.ImageSize = new System.Drawing.Size(64, 64);
            this.dirSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // doorsPanel
            // 
            this.doorsPanel.AutoScroll = true;
            this.doorsPanel.Location = new System.Drawing.Point(6, 27);
            this.doorsPanel.Name = "doorsPanel";
            this.doorsPanel.Size = new System.Drawing.Size(328, 94);
            this.doorsPanel.TabIndex = 24;
            // 
            // btLevelParams
            // 
            this.btLevelParams.Location = new System.Drawing.Point(3, 43);
            this.btLevelParams.Name = "btLevelParams";
            this.btLevelParams.Size = new System.Drawing.Size(64, 39);
            this.btLevelParams.TabIndex = 29;
            this.btLevelParams.Text = "edit level params";
            this.btLevelParams.UseVisualStyleBackColor = true;
            this.btLevelParams.Click += new System.EventHandler(this.btLevelParams_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Ingame screen order:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Select scroll type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Select door no:";
            // 
            // cbStopOnDoor
            // 
            this.cbStopOnDoor.AutoSize = true;
            this.cbStopOnDoor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbStopOnDoor.Checked = true;
            this.cbStopOnDoor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStopOnDoor.Location = new System.Drawing.Point(8, 458);
            this.cbStopOnDoor.Name = "cbStopOnDoor";
            this.cbStopOnDoor.Size = new System.Drawing.Size(90, 17);
            this.cbStopOnDoor.TabIndex = 33;
            this.cbStopOnDoor.Text = "stop on doors";
            this.cbStopOnDoor.UseVisualStyleBackColor = true;
            // 
            // pnLevelLayout
            // 
            this.pnLevelLayout.AutoScroll = true;
            this.pnLevelLayout.Controls.Add(this.pbMap);
            this.pnLevelLayout.Location = new System.Drawing.Point(407, 3);
            this.pnLevelLayout.Name = "pnLevelLayout";
            this.pnLevelLayout.Size = new System.Drawing.Size(548, 530);
            this.pnLevelLayout.TabIndex = 34;
            // 
            // pbMap
            // 
            this.pbMap.Location = new System.Drawing.Point(3, 4);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(2048, 1024);
            this.pbMap.TabIndex = 2;
            this.pbMap.TabStop = false;
            this.pbMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            this.pbMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // pnGeneric
            // 
            this.pnGeneric.Controls.Add(this.cbLayoutNo);
            this.pnGeneric.Controls.Add(this.label13);
            this.pnGeneric.Controls.Add(this.label11);
            this.pnGeneric.Controls.Add(this.cbHeight);
            this.pnGeneric.Controls.Add(this.label10);
            this.pnGeneric.Controls.Add(this.cbWidth);
            this.pnGeneric.Location = new System.Drawing.Point(333, 138);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(72, 132);
            this.pnGeneric.TabIndex = 43;
            // 
            // cbLayoutNo
            // 
            this.cbLayoutNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayoutNo.DropDownWidth = 200;
            this.cbLayoutNo.FormattingEnabled = true;
            this.cbLayoutNo.Items.AddRange(new object[] {
            "0x1DFA0 (17x4)",
            "0x1DFE4 (17x4)",
            "0x1E028 (17x4)",
            "0x1E0E4 (10x12)",
            "0x1E11D (19x3)",
            "0x1E06C (19x3)",
            "0x1E156  (19x3)"});
            this.cbLayoutNo.Location = new System.Drawing.Point(5, 19);
            this.cbLayoutNo.Name = "cbLayoutNo";
            this.cbLayoutNo.Size = new System.Drawing.Size(64, 21);
            this.cbLayoutNo.TabIndex = 53;
            this.cbLayoutNo.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 54;
            this.label13.Text = "Layout:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 52;
            this.label11.Text = "Height";
            // 
            // cbHeight
            // 
            this.cbHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeight.FormattingEnabled = true;
            this.cbHeight.Items.AddRange(new object[] {
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
            this.cbHeight.Location = new System.Drawing.Point(4, 99);
            this.cbHeight.Name = "cbHeight";
            this.cbHeight.Size = new System.Drawing.Size(64, 21);
            this.cbHeight.TabIndex = 51;
            this.cbHeight.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Width:";
            // 
            // cbWidth
            // 
            this.cbWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWidth.FormattingEnabled = true;
            this.cbWidth.Items.AddRange(new object[] {
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
            this.cbWidth.Location = new System.Drawing.Point(5, 59);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(64, 21);
            this.cbWidth.TabIndex = 49;
            this.cbWidth.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // pnCad
            // 
            this.pnCad.Controls.Add(this.label1);
            this.pnCad.Controls.Add(this.cbLevel);
            this.pnCad.Controls.Add(this.btLevelParams);
            this.pnCad.Location = new System.Drawing.Point(333, 141);
            this.pnCad.Name = "pnCad";
            this.pnCad.Size = new System.Drawing.Size(72, 100);
            this.pnCad.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(330, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 50;
            this.label12.Text = "Game:";
            // 
            // cbGame
            // 
            this.cbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGame.DropDownWidth = 200;
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Items.AddRange(new object[] {
            "Darkwing Duck",
            "Chip \'n Dale Rescue Rangers"});
            this.cbGame.Location = new System.Drawing.Point(333, 114);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(66, 21);
            this.cbGame.TabIndex = 49;
            this.cbGame.SelectedIndexChanged += new System.EventHandler(this.cbGame_SelectedIndexChanged);
            // 
            // pnDoors
            // 
            this.pnDoors.Controls.Add(this.label5);
            this.pnDoors.Controls.Add(this.doorsPanel);
            this.pnDoors.Location = new System.Drawing.Point(0, 411);
            this.pnDoors.Name = "pnDoors";
            this.pnDoors.Size = new System.Drawing.Size(337, 122);
            this.pnDoors.TabIndex = 3;
            // 
            // pnIngameScreenOrder
            // 
            this.pnIngameScreenOrder.Controls.Add(this.label2);
            this.pnIngameScreenOrder.Controls.Add(this.lvObjects);
            this.pnIngameScreenOrder.Controls.Add(this.cbStopOnDoor);
            this.pnIngameScreenOrder.Controls.Add(this.btPreview);
            this.pnIngameScreenOrder.Location = new System.Drawing.Point(961, 7);
            this.pnIngameScreenOrder.Name = "pnIngameScreenOrder";
            this.pnIngameScreenOrder.Size = new System.Drawing.Size(108, 514);
            this.pnIngameScreenOrder.TabIndex = 3;
            // 
            // pnSelectScroll
            // 
            this.pnSelectScroll.Controls.Add(this.label4);
            this.pnSelectScroll.Controls.Add(this.objPanel);
            this.pnSelectScroll.Location = new System.Drawing.Point(0, 351);
            this.pnSelectScroll.Name = "pnSelectScroll";
            this.pnSelectScroll.Size = new System.Drawing.Size(337, 65);
            this.pnSelectScroll.TabIndex = 51;
            // 
            // EditLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 533);
            this.Controls.Add(this.pnSelectScroll);
            this.Controls.Add(this.pnIngameScreenOrder);
            this.Controls.Add(this.pnGeneric);
            this.Controls.Add(this.pnDoors);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbGame);
            this.Controls.Add(this.pnCad);
            this.Controls.Add(this.pnLevelLayout);
            this.Controls.Add(this.cbShowScrolls);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.blocksPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditLayout";
            this.Text = "Layout Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.pnLevelLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.pnGeneric.ResumeLayout(false);
            this.pnGeneric.PerformLayout();
            this.pnCad.ResumeLayout(false);
            this.pnCad.PerformLayout();
            this.pnDoors.ResumeLayout(false);
            this.pnDoors.PerformLayout();
            this.pnIngameScreenOrder.ResumeLayout(false);
            this.pnIngameScreenOrder.PerformLayout();
            this.pnSelectScroll.ResumeLayout(false);
            this.pnSelectScroll.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList screenImages;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ImageList scrollSprites;
        private System.Windows.Forms.FlowLayoutPanel objPanel;
        private System.Windows.Forms.ImageList doorSprites;
        private System.Windows.Forms.CheckBox cbShowScrolls;
        private System.Windows.Forms.ListView lvObjects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btPreview;
        private System.Windows.Forms.ImageList dirSprites;
        private System.Windows.Forms.FlowLayoutPanel doorsPanel;
        private System.Windows.Forms.Button btLevelParams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbStopOnDoor;
        private System.Windows.Forms.Panel pnLevelLayout;
        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbWidth;
        private System.Windows.Forms.Panel pnCad;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbGame;
        private System.Windows.Forms.Panel pnDoors;
        private System.Windows.Forms.Panel pnIngameScreenOrder;
        private System.Windows.Forms.ComboBox cbLayoutNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnSelectScroll;
    }
}