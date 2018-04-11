namespace CadEditor
{
    partial class EditMap
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
            this.btSave = new System.Windows.Forms.Button();
            this.cbShowAxis = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbScreenNo = new System.Windows.Forms.ComboBox();
            this.sfSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.mapScreen2 = new System.Windows.Forms.PictureBox();
            this.blocksScreen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSubpalette = new System.Windows.Forms.ComboBox();
            this.subpalSprites = new System.Windows.Forms.ImageList(this.components);
            this.lbActiveBlock = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbShowSecondNametable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.mapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(4, 4);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(639, 536);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.Location = new System.Drawing.Point(16, 683);
            this.btSave.Margin = new System.Windows.Forms.Padding(4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(68, 28);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // cbShowAxis
            // 
            this.cbShowAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowAxis.AutoSize = true;
            this.cbShowAxis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowAxis.Checked = true;
            this.cbShowAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowAxis.Location = new System.Drawing.Point(246, 688);
            this.cbShowAxis.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowAxis.Name = "cbShowAxis";
            this.cbShowAxis.Size = new System.Drawing.Size(93, 21);
            this.cbShowAxis.TabIndex = 9;
            this.cbShowAxis.Text = "Show Axis";
            this.cbShowAxis.UseVisualStyleBackColor = true;
            this.cbShowAxis.CheckedChanged += new System.EventHandler(this.cbShowAxis_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 412);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 44;
            this.label7.Text = "Screen no:";
            // 
            // cbScreenNo
            // 
            this.cbScreenNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenNo.FormattingEnabled = true;
            this.cbScreenNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbScreenNo.Location = new System.Drawing.Point(131, 409);
            this.cbScreenNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbScreenNo.Name = "cbScreenNo";
            this.cbScreenNo.Size = new System.Drawing.Size(119, 24);
            this.cbScreenNo.TabIndex = 43;
            this.cbScreenNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // sfSaveDialog
            // 
            this.sfSaveDialog.FileName = "map.bin";
            // 
            // mapPanel
            // 
            this.mapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPanel.AutoScroll = true;
            this.mapPanel.Controls.Add(this.mapScreen);
            this.mapPanel.Controls.Add(this.mapScreen2);
            this.mapPanel.Location = new System.Drawing.Point(383, 18);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(826, 691);
            this.mapPanel.TabIndex = 45;
            // 
            // mapScreen2
            // 
            this.mapScreen2.Location = new System.Drawing.Point(183, 4);
            this.mapScreen2.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen2.Name = "mapScreen2";
            this.mapScreen2.Size = new System.Drawing.Size(639, 536);
            this.mapScreen2.TabIndex = 6;
            this.mapScreen2.TabStop = false;
            this.mapScreen2.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen2_Paint);
            // 
            // blocksScreen
            // 
            this.blocksScreen.Location = new System.Drawing.Point(12, 18);
            this.blocksScreen.Name = "blocksScreen";
            this.blocksScreen.Size = new System.Drawing.Size(360, 322);
            this.blocksScreen.TabIndex = 46;
            this.blocksScreen.TabStop = false;
            this.blocksScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksScreen_Paint);
            this.blocksScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocksScreen_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 48;
            this.label1.Text = "View with subpal:";
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
            this.cbSubpalette.Location = new System.Drawing.Point(131, 378);
            this.cbSubpalette.Margin = new System.Windows.Forms.Padding(4);
            this.cbSubpalette.Name = "cbSubpalette";
            this.cbSubpalette.Size = new System.Drawing.Size(119, 23);
            this.cbSubpalette.TabIndex = 49;
            this.cbSubpalette.SelectedIndexChanged += new System.EventHandler(this.cbSubpalette_SelectedIndexChanged);
            // 
            // subpalSprites
            // 
            this.subpalSprites.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.subpalSprites.ImageSize = new System.Drawing.Size(64, 16);
            this.subpalSprites.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lbActiveBlock
            // 
            this.lbActiveBlock.AutoSize = true;
            this.lbActiveBlock.Location = new System.Drawing.Point(132, 346);
            this.lbActiveBlock.Name = "lbActiveBlock";
            this.lbActiveBlock.Size = new System.Drawing.Size(26, 17);
            this.lbActiveBlock.TabIndex = 50;
            this.lbActiveBlock.Text = "(0)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 51;
            this.label2.Text = "ActiveBlock:";
            // 
            // cbShowSecondNametable
            // 
            this.cbShowSecondNametable.AutoSize = true;
            this.cbShowSecondNametable.Location = new System.Drawing.Point(16, 440);
            this.cbShowSecondNametable.Name = "cbShowSecondNametable";
            this.cbShowSecondNametable.Size = new System.Drawing.Size(188, 21);
            this.cbShowSecondNametable.TabIndex = 52;
            this.cbShowSecondNametable.Text = "Show second name table";
            this.cbShowSecondNametable.UseVisualStyleBackColor = true;
            this.cbShowSecondNametable.CheckedChanged += new System.EventHandler(this.cbShowSecondNametable_CheckedChanged);
            // 
            // EditMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 716);
            this.Controls.Add(this.cbShowSecondNametable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbActiveBlock);
            this.Controls.Add(this.cbSubpalette);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blocksScreen);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbScreenNo);
            this.Controls.Add(this.cbShowAxis);
            this.Controls.Add(this.btSave);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditMap";
            this.Text = "Map Editor";
            this.Load += new System.EventHandler(this.EditMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.mapPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.CheckBox cbShowAxis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbScreenNo;
        private System.Windows.Forms.SaveFileDialog sfSaveDialog;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.PictureBox blocksScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSubpalette;
        private System.Windows.Forms.ImageList subpalSprites;
        private System.Windows.Forms.Label lbActiveBlock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox mapScreen2;
        private System.Windows.Forms.CheckBox cbShowSecondNametable;
    }
}