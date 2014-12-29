namespace CadEditor
{
    partial class BlockEditCad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockEditCad));
            this.paletteMap = new System.Windows.Forms.PictureBox();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSubpalette = new System.Windows.Forms.ComboBox();
            this.videoSprites1 = new System.Windows.Forms.ImageList(this.components);
            this.subpalSprites = new System.Windows.Forms.ImageList(this.components);
            this.mapObjects = new System.Windows.Forms.FlowLayoutPanel();
            this.videoSprites2 = new System.Windows.Forms.ImageList(this.components);
            this.videoSprites3 = new System.Windows.Forms.ImageList(this.components);
            this.videoSprites4 = new System.Windows.Forms.ImageList(this.components);
            this.cbLevelSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.btSave = new System.Windows.Forms.Button();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pbBacks = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.editBack = new System.Windows.Forms.Button();
            this.pnCad = new System.Windows.Forms.Panel();
            this.pnBacks = new System.Windows.Forms.Panel();
            this.lbReadOnly = new System.Windows.Forms.Label();
            this.btClear = new System.Windows.Forms.Button();
            this.btFlipHorizontal = new System.Windows.Forms.Button();
            this.btFlipVertical = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.cbShowAxis = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.paletteMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBacks)).BeginInit();
            this.pnCad.SuspendLayout();
            this.pnBacks.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteMap
            // 
            this.paletteMap.Location = new System.Drawing.Point(15, 135);
            this.paletteMap.Name = "paletteMap";
            this.paletteMap.Size = new System.Drawing.Size(256, 16);
            this.paletteMap.TabIndex = 0;
            this.paletteMap.TabStop = false;
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(15, 178);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(256, 256);
            this.mapScreen.TabIndex = 6;
            this.mapScreen.TabStop = false;
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pallete:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "View with subpallete:";
            // 
            // cbSubpalette
            // 
            this.cbSubpalette.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbSubpalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubpalette.FormattingEnabled = true;
            this.cbSubpalette.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbSubpalette.Location = new System.Drawing.Point(118, 151);
            this.cbSubpalette.Name = "cbSubpalette";
            this.cbSubpalette.Size = new System.Drawing.Size(90, 21);
            this.cbSubpalette.TabIndex = 9;
            this.cbSubpalette.SelectedIndexChanged += new System.EventHandler(this.cbSubpalette_SelectedIndexChanged);
            // 
            // videoSprites1
            // 
            this.videoSprites1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.videoSprites1.ImageSize = new System.Drawing.Size(16, 16);
            this.videoSprites1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // subpalSprites
            // 
            this.subpalSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.subpalSprites.ImageSize = new System.Drawing.Size(64, 16);
            this.subpalSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mapObjects
            // 
            this.mapObjects.AutoScroll = true;
            this.mapObjects.Location = new System.Drawing.Point(290, 35);
            this.mapObjects.Name = "mapObjects";
            this.mapObjects.Size = new System.Drawing.Size(370, 444);
            this.mapObjects.TabIndex = 10;
            // 
            // videoSprites2
            // 
            this.videoSprites2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.videoSprites2.ImageSize = new System.Drawing.Size(16, 16);
            this.videoSprites2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // videoSprites3
            // 
            this.videoSprites3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.videoSprites3.ImageSize = new System.Drawing.Size(16, 16);
            this.videoSprites3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // videoSprites4
            // 
            this.videoSprites4.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.videoSprites4.ImageSize = new System.Drawing.Size(16, 16);
            this.videoSprites4.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbLevelSelect
            // 
            this.cbLevelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelSelect.FormattingEnabled = true;
            this.cbLevelSelect.Items.AddRange(new object[] {
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
            this.cbLevelSelect.Location = new System.Drawing.Point(88, 6);
            this.cbLevelSelect.Name = "cbLevelSelect";
            this.cbLevelSelect.Size = new System.Drawing.Size(111, 21);
            this.cbLevelSelect.TabIndex = 11;
            this.cbLevelSelect.SelectedIndexChanged += new System.EventHandler(this.cbLevelSelect_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Level:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Active:";
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(255, 151);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(16, 16);
            this.pbActive.TabIndex = 14;
            this.pbActive.TabStop = false;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(12, 4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(59, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
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
            "Door 18"});
            this.cbDoor.Location = new System.Drawing.Point(88, 27);
            this.cbDoor.Name = "cbDoor";
            this.cbDoor.Size = new System.Drawing.Size(111, 21);
            this.cbDoor.TabIndex = 15;
            this.cbDoor.SelectedIndexChanged += new System.EventHandler(this.VisibleOnlyChange_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "View with door:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tiles:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Pallete:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(436, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(551, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Back tile:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(292, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "No:";
            // 
            // pbBacks
            // 
            this.pbBacks.Location = new System.Drawing.Point(-1, 28);
            this.pbBacks.Name = "pbBacks";
            this.pbBacks.Size = new System.Drawing.Size(264, 64);
            this.pbBacks.TabIndex = 22;
            this.pbBacks.TabStop = false;
            this.pbBacks.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBacks_Paint);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Tiles under boxes:";
            // 
            // editBack
            // 
            this.editBack.Location = new System.Drawing.Point(103, 2);
            this.editBack.Name = "editBack";
            this.editBack.Size = new System.Drawing.Size(59, 23);
            this.editBack.TabIndex = 24;
            this.editBack.Text = "edit";
            this.editBack.UseVisualStyleBackColor = true;
            this.editBack.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnCad
            // 
            this.pnCad.Controls.Add(this.label3);
            this.pnCad.Controls.Add(this.cbLevelSelect);
            this.pnCad.Controls.Add(this.label5);
            this.pnCad.Controls.Add(this.cbDoor);
            this.pnCad.Location = new System.Drawing.Point(12, 29);
            this.pnCad.Name = "pnCad";
            this.pnCad.Size = new System.Drawing.Size(264, 87);
            this.pnCad.TabIndex = 0;
            // 
            // pnBacks
            // 
            this.pnBacks.Controls.Add(this.label11);
            this.pnBacks.Controls.Add(this.pbBacks);
            this.pnBacks.Controls.Add(this.editBack);
            this.pnBacks.Location = new System.Drawing.Point(15, 469);
            this.pnBacks.Name = "pnBacks";
            this.pnBacks.Size = new System.Drawing.Size(263, 92);
            this.pnBacks.TabIndex = 18;
            // 
            // lbReadOnly
            // 
            this.lbReadOnly.AutoSize = true;
            this.lbReadOnly.Location = new System.Drawing.Point(207, 9);
            this.lbReadOnly.Name = "lbReadOnly";
            this.lbReadOnly.Size = new System.Drawing.Size(69, 13);
            this.lbReadOnly.TabIndex = 22;
            this.lbReadOnly.Text = "READ ONLY";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(585, 485);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 23;
            this.btClear.Text = "Clear all";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btFlipHorizontal
            // 
            this.btFlipHorizontal.Location = new System.Drawing.Point(15, 440);
            this.btFlipHorizontal.Name = "btFlipHorizontal";
            this.btFlipHorizontal.Size = new System.Drawing.Size(75, 23);
            this.btFlipHorizontal.TabIndex = 0;
            this.btFlipHorizontal.Text = "Flip horiz-tal";
            this.btFlipHorizontal.UseVisualStyleBackColor = true;
            this.btFlipHorizontal.Click += new System.EventHandler(this.btFlipHorizontal_Click);
            // 
            // btFlipVertical
            // 
            this.btFlipVertical.Location = new System.Drawing.Point(93, 440);
            this.btFlipVertical.Name = "btFlipVertical";
            this.btFlipVertical.Size = new System.Drawing.Size(75, 23);
            this.btFlipVertical.TabIndex = 24;
            this.btFlipVertical.Text = "Flip vertical";
            this.btFlipVertical.UseVisualStyleBackColor = true;
            this.btFlipVertical.Click += new System.EventHandler(this.btFlipVertical_Click);
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(142, 4);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(59, 23);
            this.btImport.TabIndex = 25;
            this.btImport.Text = "import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(77, 4);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(59, 23);
            this.btExport.TabIndex = 26;
            this.btExport.Text = "export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // cbShowAxis
            // 
            this.cbShowAxis.AutoSize = true;
            this.cbShowAxis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowAxis.Checked = true;
            this.cbShowAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowAxis.Location = new System.Drawing.Point(505, 489);
            this.cbShowAxis.Name = "cbShowAxis";
            this.cbShowAxis.Size = new System.Drawing.Size(74, 17);
            this.cbShowAxis.TabIndex = 27;
            this.cbShowAxis.Text = "Show axis";
            this.cbShowAxis.UseVisualStyleBackColor = true;
            this.cbShowAxis.CheckedChanged += new System.EventHandler(this.cbShowAxis_CheckedChanged);
            // 
            // BlockEditCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 564);
            this.Controls.Add(this.cbShowAxis);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.btFlipVertical);
            this.Controls.Add(this.btFlipHorizontal);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.lbReadOnly);
            this.Controls.Add(this.pnBacks);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.paletteMap);
            this.Controls.Add(this.pbActive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mapObjects);
            this.Controls.Add(this.cbSubpalette);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapScreen);
            this.Controls.Add(this.pnCad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BlockEditCad";
            this.Text = "Blocks Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.paletteMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBacks)).EndInit();
            this.pnCad.ResumeLayout(false);
            this.pnCad.PerformLayout();
            this.pnBacks.ResumeLayout(false);
            this.pnBacks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox paletteMap;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSubpalette;
        private System.Windows.Forms.ImageList videoSprites1;
        private System.Windows.Forms.ImageList subpalSprites;
        private System.Windows.Forms.FlowLayoutPanel mapObjects;
        private System.Windows.Forms.ImageList videoSprites2;
        private System.Windows.Forms.ImageList videoSprites3;
        private System.Windows.Forms.ImageList videoSprites4;
        private System.Windows.Forms.ComboBox cbLevelSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cbDoor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pbBacks;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button editBack;
        private System.Windows.Forms.Panel pnCad;
        private System.Windows.Forms.Panel pnBacks;
        private System.Windows.Forms.Label lbReadOnly;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btFlipHorizontal;
        private System.Windows.Forms.Button btFlipVertical;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.CheckBox cbShowAxis;
    }
}