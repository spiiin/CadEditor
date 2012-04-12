namespace CadEditor
{
    partial class BlockEdit
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
            this.button1 = new System.Windows.Forms.Button();
            this.cbDoor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.paletteMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            this.SuspendLayout();
            // 
            // paletteMap
            // 
            this.paletteMap.Location = new System.Drawing.Point(12, 79);
            this.paletteMap.Name = "paletteMap";
            this.paletteMap.Size = new System.Drawing.Size(256, 16);
            this.paletteMap.TabIndex = 0;
            this.paletteMap.TabStop = false;
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(12, 122);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(256, 256);
            this.mapScreen.TabIndex = 6;
            this.mapScreen.TabStop = false;
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pallete:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 98);
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
            this.cbSubpalette.Location = new System.Drawing.Point(115, 95);
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
            this.mapObjects.Location = new System.Drawing.Point(290, 12);
            this.mapObjects.Name = "mapObjects";
            this.mapObjects.Size = new System.Drawing.Size(294, 366);
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
            this.cbLevelSelect.Location = new System.Drawing.Point(107, 8);
            this.cbLevelSelect.Name = "cbLevelSelect";
            this.cbLevelSelect.Size = new System.Drawing.Size(98, 21);
            this.cbLevelSelect.TabIndex = 11;
            this.cbLevelSelect.SelectedIndexChanged += new System.EventHandler(this.cbLevelSelect_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Level:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Active:";
            // 
            // pbActive
            // 
            this.pbActive.Location = new System.Drawing.Point(252, 95);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(16, 16);
            this.pbActive.TabIndex = 14;
            this.pbActive.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(214, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.cbDoor.Location = new System.Drawing.Point(107, 35);
            this.cbDoor.Name = "cbDoor";
            this.cbDoor.Size = new System.Drawing.Size(98, 21);
            this.cbDoor.TabIndex = 15;
            this.cbDoor.SelectedIndexChanged += new System.EventHandler(this.cbDoor_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "View with door:";
            // 
            // BlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 390);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDoor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.paletteMap);
            this.Controls.Add(this.pbActive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbLevelSelect);
            this.Controls.Add(this.mapObjects);
            this.Controls.Add(this.cbSubpalette);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BlockEdit";
            this.Text = "Block Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.BlockEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.paletteMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbDoor;
        private System.Windows.Forms.Label label5;
    }
}