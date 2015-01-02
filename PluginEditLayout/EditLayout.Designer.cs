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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditLayout));
            this.screenImages = new System.Windows.Forms.ImageList(this.components);
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.activeBlock = new System.Windows.Forms.PictureBox();
            this.btSave = new System.Windows.Forms.Button();
            this.scrollSprites = new System.Windows.Forms.ImageList(this.components);
            this.objPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.doorSprites = new System.Windows.Forms.ImageList(this.components);
            this.cbShowScrolls = new System.Windows.Forms.CheckBox();
            this.dirSprites = new System.Windows.Forms.ImageList(this.components);
            this.doorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnLevelLayout = new System.Windows.Forms.Panel();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.pnGeneric = new System.Windows.Forms.Panel();
            this.cbLayoutNo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pnDoors = new System.Windows.Forms.Panel();
            this.pnSelectScroll = new System.Windows.Forms.Panel();
            this.btExport = new System.Windows.Forms.Button();
            this.pnParamGeneric = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPaletteNo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBlockNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbBigBlockNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
            this.pnLevelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.pnGeneric.SuspendLayout();
            this.pnDoors.SuspendLayout();
            this.pnSelectScroll.SuspendLayout();
            this.pnParamGeneric.SuspendLayout();
            this.SuspendLayout();
            // 
            // screenImages
            // 
            this.screenImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.screenImages.ImageSize = new System.Drawing.Size(64, 64);
            this.screenImages.TransparentColor = System.Drawing.Color.Transparent;
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
            this.btSave.Location = new System.Drawing.Point(337, 284);
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
            this.cbShowScrolls.Location = new System.Drawing.Point(334, 247);
            this.cbShowScrolls.Name = "cbShowScrolls";
            this.cbShowScrolls.Size = new System.Drawing.Size(68, 31);
            this.cbShowScrolls.TabIndex = 21;
            this.cbShowScrolls.Text = "show scrolls";
            this.cbShowScrolls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbShowScrolls.UseVisualStyleBackColor = true;
            this.cbShowScrolls.CheckedChanged += new System.EventHandler(this.cbShowScrolls_CheckedChanged);
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
            this.pnGeneric.Location = new System.Drawing.Point(332, 101);
            this.pnGeneric.Name = "pnGeneric";
            this.pnGeneric.Size = new System.Drawing.Size(72, 54);
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
            // pnDoors
            // 
            this.pnDoors.Controls.Add(this.label5);
            this.pnDoors.Controls.Add(this.doorsPanel);
            this.pnDoors.Location = new System.Drawing.Point(0, 411);
            this.pnDoors.Name = "pnDoors";
            this.pnDoors.Size = new System.Drawing.Size(337, 122);
            this.pnDoors.TabIndex = 3;
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
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(337, 312);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(64, 22);
            this.btExport.TabIndex = 52;
            this.btExport.Text = "export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // pnParamGeneric
            // 
            this.pnParamGeneric.Controls.Add(this.label10);
            this.pnParamGeneric.Controls.Add(this.label9);
            this.pnParamGeneric.Controls.Add(this.cbPaletteNo);
            this.pnParamGeneric.Controls.Add(this.label8);
            this.pnParamGeneric.Controls.Add(this.cbBlockNo);
            this.pnParamGeneric.Controls.Add(this.label6);
            this.pnParamGeneric.Controls.Add(this.cbBigBlockNo);
            this.pnParamGeneric.Controls.Add(this.label7);
            this.pnParamGeneric.Controls.Add(this.cbVideoNo);
            this.pnParamGeneric.Location = new System.Drawing.Point(337, 340);
            this.pnParamGeneric.Name = "pnParamGeneric";
            this.pnParamGeneric.Size = new System.Drawing.Size(72, 193);
            this.pnParamGeneric.TabIndex = 53;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "(for export)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 151);
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
            this.cbPaletteNo.Location = new System.Drawing.Point(4, 167);
            this.cbPaletteNo.Name = "cbPaletteNo";
            this.cbPaletteNo.Size = new System.Drawing.Size(64, 21);
            this.cbPaletteNo.TabIndex = 47;
            this.cbPaletteNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 111);
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
            this.cbBlockNo.Location = new System.Drawing.Point(4, 127);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(64, 21);
            this.cbBlockNo.TabIndex = 45;
            this.cbBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "BigBlock:";
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
            this.cbBigBlockNo.Location = new System.Drawing.Point(4, 87);
            this.cbBigBlockNo.Name = "cbBigBlockNo";
            this.cbBigBlockNo.Size = new System.Drawing.Size(64, 21);
            this.cbBigBlockNo.TabIndex = 43;
            this.cbBigBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 31);
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
            this.cbVideoNo.Location = new System.Drawing.Point(3, 47);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(64, 21);
            this.cbVideoNo.TabIndex = 41;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // EditLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 533);
            this.Controls.Add(this.pnParamGeneric);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.pnSelectScroll);
            this.Controls.Add(this.pnGeneric);
            this.Controls.Add(this.pnDoors);
            this.Controls.Add(this.pnLevelLayout);
            this.Controls.Add(this.cbShowScrolls);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.blocksPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.pnDoors.ResumeLayout(false);
            this.pnDoors.PerformLayout();
            this.pnSelectScroll.ResumeLayout(false);
            this.pnSelectScroll.PerformLayout();
            this.pnParamGeneric.ResumeLayout(false);
            this.pnParamGeneric.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList screenImages;
        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox activeBlock;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ImageList scrollSprites;
        private System.Windows.Forms.FlowLayoutPanel objPanel;
        private System.Windows.Forms.ImageList doorSprites;
        private System.Windows.Forms.CheckBox cbShowScrolls;
        private System.Windows.Forms.ImageList dirSprites;
        private System.Windows.Forms.FlowLayoutPanel doorsPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnLevelLayout;
        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.Panel pnGeneric;
        private System.Windows.Forms.Panel pnDoors;
        private System.Windows.Forms.ComboBox cbLayoutNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnSelectScroll;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Panel pnParamGeneric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPaletteNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBlockNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbBigBlockNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.Label label10;
    }
}