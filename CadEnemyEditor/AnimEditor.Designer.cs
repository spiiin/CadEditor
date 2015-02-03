namespace CadEnemyEditor
{
    partial class AnimEditor
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.pbFrame = new System.Windows.Forms.PictureBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            this.tvAnims = new System.Windows.Forms.TreeView();
            this.cbVideo = new System.Windows.Forms.ComboBox();
            this.lvTiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbTileIndex = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnTileProperties = new System.Windows.Forms.Panel();
            this.cbFlipY = new System.Windows.Forms.CheckBox();
            this.cbFlipX = new System.Windows.Forms.CheckBox();
            this.btSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pbPal = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.pnTileProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(274, 75);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(256, 256);
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            this.pbVideo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbVideo_MouseClick);
            // 
            // pbFrame
            // 
            this.pbFrame.Location = new System.Drawing.Point(536, 17);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(512, 512);
            this.pbFrame.TabIndex = 1;
            this.pbFrame.TabStop = false;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList4
            // 
            this.imageList4.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList4.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tvAnims
            // 
            this.tvAnims.Location = new System.Drawing.Point(12, 17);
            this.tvAnims.Name = "tvAnims";
            this.tvAnims.Size = new System.Drawing.Size(241, 512);
            this.tvAnims.TabIndex = 2;
            this.tvAnims.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAnims_NodeMouseDoubleClick);
            // 
            // cbVideo
            // 
            this.cbVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVideo.FormattingEnabled = true;
            this.cbVideo.Items.AddRange(new object[] {
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
            this.cbVideo.Location = new System.Drawing.Point(324, 48);
            this.cbVideo.Name = "cbVideo";
            this.cbVideo.Size = new System.Drawing.Size(203, 21);
            this.cbVideo.TabIndex = 3;
            this.cbVideo.SelectedIndexChanged += new System.EventHandler(this.cbVideo_SelectedIndexChanged);
            // 
            // lvTiles
            // 
            this.lvTiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvTiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTiles.Location = new System.Drawing.Point(274, 365);
            this.lvTiles.Name = "lvTiles";
            this.lvTiles.Size = new System.Drawing.Size(119, 164);
            this.lvTiles.TabIndex = 4;
            this.lvTiles.UseCompatibleStateImageBehavior = false;
            this.lvTiles.View = System.Windows.Forms.View.Details;
            this.lvTiles.SelectedIndexChanged += new System.EventHandler(this.lvTiles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 120;
            // 
            // cbTileIndex
            // 
            this.cbTileIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTileIndex.FormattingEnabled = true;
            this.cbTileIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbTileIndex.Location = new System.Drawing.Point(173, 7);
            this.cbTileIndex.Name = "cbTileIndex";
            this.cbTileIndex.Size = new System.Drawing.Size(64, 21);
            this.cbTileIndex.TabIndex = 7;
            this.cbTileIndex.SelectedIndexChanged += new System.EventHandler(this.cbTileIndex_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "pal:";
            // 
            // pnTileProperties
            // 
            this.pnTileProperties.Controls.Add(this.cbFlipY);
            this.pnTileProperties.Controls.Add(this.cbFlipX);
            this.pnTileProperties.Controls.Add(this.label1);
            this.pnTileProperties.Controls.Add(this.cbTileIndex);
            this.pnTileProperties.Location = new System.Drawing.Point(274, 337);
            this.pnTileProperties.Name = "pnTileProperties";
            this.pnTileProperties.Size = new System.Drawing.Size(256, 33);
            this.pnTileProperties.TabIndex = 9;
            // 
            // cbFlipY
            // 
            this.cbFlipY.AutoSize = true;
            this.cbFlipY.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlipY.Location = new System.Drawing.Point(72, 9);
            this.cbFlipY.Name = "cbFlipY";
            this.cbFlipY.Size = new System.Drawing.Size(47, 17);
            this.cbFlipY.TabIndex = 10;
            this.cbFlipY.Text = "flip y";
            this.cbFlipY.UseVisualStyleBackColor = true;
            this.cbFlipY.CheckedChanged += new System.EventHandler(this.cbFlipX_CheckedChanged);
            // 
            // cbFlipX
            // 
            this.cbFlipX.AutoSize = true;
            this.cbFlipX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlipX.Location = new System.Drawing.Point(3, 9);
            this.cbFlipX.Name = "cbFlipX";
            this.cbFlipX.Size = new System.Drawing.Size(47, 17);
            this.cbFlipX.TabIndex = 9;
            this.cbFlipX.Text = "flip x";
            this.cbFlipX.UseVisualStyleBackColor = true;
            this.cbFlipX.CheckedChanged += new System.EventHandler(this.cbFlipX_CheckedChanged);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(399, 365);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(131, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Video No:";
            // 
            // pbPal
            // 
            this.pbPal.Location = new System.Drawing.Point(399, 394);
            this.pbPal.Name = "pbPal";
            this.pbPal.Size = new System.Drawing.Size(128, 128);
            this.pbPal.TabIndex = 13;
            this.pbPal.TabStop = false;
            this.pbPal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPal_MouseClick);
            // 
            // AnimEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 541);
            this.Controls.Add(this.pbPal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.pnTileProperties);
            this.Controls.Add(this.lvTiles);
            this.Controls.Add(this.cbVideo);
            this.Controls.Add(this.tvAnims);
            this.Controls.Add(this.pbFrame);
            this.Controls.Add(this.pbVideo);
            this.Name = "AnimEditor";
            this.Text = "CAD Enemy Editor v0.2 by spiiin";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.pnTileProperties.ResumeLayout(false);
            this.pnTileProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.PictureBox pbFrame;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.ImageList imageList4;
        private System.Windows.Forms.TreeView tvAnims;
        private System.Windows.Forms.ComboBox cbVideo;
        private System.Windows.Forms.ListView lvTiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox cbTileIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnTileProperties;
        private System.Windows.Forms.CheckBox cbFlipX;
        private System.Windows.Forms.CheckBox cbFlipY;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbPal;
    }
}

