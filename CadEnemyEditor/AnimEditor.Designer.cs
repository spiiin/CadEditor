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
            this.cmTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btLoadChr = new System.Windows.Forms.Button();
            this.btDefaultPal = new System.Windows.Forms.Button();
            this.btLoadPal = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btExportPng = new System.Windows.Forms.Button();
            this.cbShowBack = new System.Windows.Forms.CheckBox();
            this.cbScale = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cdBackColor = new System.Windows.Forms.ColorDialog();
            this.sfExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.ofOpenPal = new System.Windows.Forms.OpenFileDialog();
            this.ofOpenChr = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).BeginInit();
            this.cmTree.SuspendLayout();
            this.pnTileProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(8, 8);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(17, 29);
            this.pbVideo.Margin = new System.Windows.Forms.Padding(4);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(341, 315);
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            this.pbVideo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbVideo_MouseClick);
            // 
            // pbFrame
            // 
            this.pbFrame.Location = new System.Drawing.Point(715, 21);
            this.pbFrame.Margin = new System.Windows.Forms.Padding(4);
            this.pbFrame.Name = "pbFrame";
            this.pbFrame.Size = new System.Drawing.Size(683, 656);
            this.pbFrame.TabIndex = 1;
            this.pbFrame.TabStop = false;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(8, 8);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(8, 8);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList4
            // 
            this.imageList4.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList4.ImageSize = new System.Drawing.Size(8, 8);
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tvAnims
            // 
            this.tvAnims.ContextMenuStrip = this.cmTree;
            this.tvAnims.Location = new System.Drawing.Point(16, 21);
            this.tvAnims.Margin = new System.Windows.Forms.Padding(4);
            this.tvAnims.Name = "tvAnims";
            this.tvAnims.Size = new System.Drawing.Size(320, 656);
            this.tvAnims.TabIndex = 2;
            this.tvAnims.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAnims_NodeMouseDoubleClick);
            // 
            // cmTree
            // 
            this.cmTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.cmTree.Name = "cmTree";
            this.cmTree.Size = new System.Drawing.Size(158, 52);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
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
            this.cbVideo.Location = new System.Drawing.Point(82, 3);
            this.cbVideo.Margin = new System.Windows.Forms.Padding(4);
            this.cbVideo.Name = "cbVideo";
            this.cbVideo.Size = new System.Drawing.Size(90, 24);
            this.cbVideo.TabIndex = 3;
            this.cbVideo.SelectedIndexChanged += new System.EventHandler(this.cbVideo_SelectedIndexChanged);
            // 
            // lvTiles
            // 
            this.lvTiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvTiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTiles.Location = new System.Drawing.Point(17, 401);
            this.lvTiles.Margin = new System.Windows.Forms.Padding(4);
            this.lvTiles.Name = "lvTiles";
            this.lvTiles.Size = new System.Drawing.Size(157, 186);
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
            this.cbTileIndex.Location = new System.Drawing.Point(250, 8);
            this.cbTileIndex.Margin = new System.Windows.Forms.Padding(4);
            this.cbTileIndex.Name = "cbTileIndex";
            this.cbTileIndex.Size = new System.Drawing.Size(84, 24);
            this.cbTileIndex.TabIndex = 7;
            this.cbTileIndex.SelectedIndexChanged += new System.EventHandler(this.cbTileIndex_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "subpal:";
            // 
            // pnTileProperties
            // 
            this.pnTileProperties.Controls.Add(this.cbFlipY);
            this.pnTileProperties.Controls.Add(this.cbFlipX);
            this.pnTileProperties.Controls.Add(this.label1);
            this.pnTileProperties.Controls.Add(this.cbTileIndex);
            this.pnTileProperties.Location = new System.Drawing.Point(17, 352);
            this.pnTileProperties.Margin = new System.Windows.Forms.Padding(4);
            this.pnTileProperties.Name = "pnTileProperties";
            this.pnTileProperties.Size = new System.Drawing.Size(341, 41);
            this.pnTileProperties.TabIndex = 9;
            // 
            // cbFlipY
            // 
            this.cbFlipY.AutoSize = true;
            this.cbFlipY.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlipY.Location = new System.Drawing.Point(96, 11);
            this.cbFlipY.Margin = new System.Windows.Forms.Padding(4);
            this.cbFlipY.Name = "cbFlipY";
            this.cbFlipY.Size = new System.Drawing.Size(59, 21);
            this.cbFlipY.TabIndex = 10;
            this.cbFlipY.Text = "flip y";
            this.cbFlipY.UseVisualStyleBackColor = true;
            this.cbFlipY.CheckedChanged += new System.EventHandler(this.cbFlipX_CheckedChanged);
            // 
            // cbFlipX
            // 
            this.cbFlipX.AutoSize = true;
            this.cbFlipX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlipX.Location = new System.Drawing.Point(4, 11);
            this.cbFlipX.Margin = new System.Windows.Forms.Padding(4);
            this.cbFlipX.Name = "cbFlipX";
            this.cbFlipX.Size = new System.Drawing.Size(58, 21);
            this.cbFlipX.TabIndex = 9;
            this.cbFlipX.Text = "flip x";
            this.cbFlipX.UseVisualStyleBackColor = true;
            this.cbFlipX.CheckedChanged += new System.EventHandler(this.cbFlipX_CheckedChanged);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(184, 401);
            this.btSave.Margin = new System.Windows.Forms.Padding(4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(175, 28);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Video No:";
            // 
            // pbPal
            // 
            this.pbPal.Location = new System.Drawing.Point(243, 467);
            this.pbPal.Margin = new System.Windows.Forms.Padding(0);
            this.pbPal.Name = "pbPal";
            this.pbPal.Size = new System.Drawing.Size(96, 86);
            this.pbPal.TabIndex = 13;
            this.pbPal.TabStop = false;
            this.pbPal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPal_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btLoadChr);
            this.panel1.Controls.Add(this.btDefaultPal);
            this.panel1.Controls.Add(this.btLoadPal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btExportPng);
            this.panel1.Controls.Add(this.cbShowBack);
            this.panel1.Controls.Add(this.cbScale);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pbBack);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pbPal);
            this.panel1.Controls.Add(this.pbVideo);
            this.panel1.Controls.Add(this.cbVideo);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.lvTiles);
            this.panel1.Controls.Add(this.pnTileProperties);
            this.panel1.Location = new System.Drawing.Point(343, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 656);
            this.panel1.TabIndex = 14;
            // 
            // btLoadChr
            // 
            this.btLoadChr.Location = new System.Drawing.Point(176, 0);
            this.btLoadChr.Margin = new System.Windows.Forms.Padding(4);
            this.btLoadChr.Name = "btLoadChr";
            this.btLoadChr.Size = new System.Drawing.Size(175, 28);
            this.btLoadChr.TabIndex = 23;
            this.btLoadChr.Text = "Load CHR from file";
            this.btLoadChr.UseVisualStyleBackColor = true;
            this.btLoadChr.Click += new System.EventHandler(this.btLoadChr_Click);
            // 
            // btDefaultPal
            // 
            this.btDefaultPal.Location = new System.Drawing.Point(182, 593);
            this.btDefaultPal.Margin = new System.Windows.Forms.Padding(4);
            this.btDefaultPal.Name = "btDefaultPal";
            this.btDefaultPal.Size = new System.Drawing.Size(175, 28);
            this.btDefaultPal.TabIndex = 22;
            this.btDefaultPal.Text = "Default pal";
            this.btDefaultPal.UseVisualStyleBackColor = true;
            this.btDefaultPal.Click += new System.EventHandler(this.btDefaultPal_Click);
            // 
            // btLoadPal
            // 
            this.btLoadPal.Location = new System.Drawing.Point(182, 557);
            this.btLoadPal.Margin = new System.Windows.Forms.Padding(4);
            this.btLoadPal.Name = "btLoadPal";
            this.btLoadPal.Size = new System.Drawing.Size(175, 28);
            this.btLoadPal.TabIndex = 21;
            this.btLoadPal.Text = "Load pal from file";
            this.btLoadPal.UseVisualStyleBackColor = true;
            this.btLoadPal.Click += new System.EventHandler(this.btLoadPal_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Pal:";
            // 
            // btExportPng
            // 
            this.btExportPng.Location = new System.Drawing.Point(183, 431);
            this.btExportPng.Margin = new System.Windows.Forms.Padding(4);
            this.btExportPng.Name = "btExportPng";
            this.btExportPng.Size = new System.Drawing.Size(175, 28);
            this.btExportPng.TabIndex = 19;
            this.btExportPng.Text = "Export png";
            this.btExportPng.UseVisualStyleBackColor = true;
            this.btExportPng.Click += new System.EventHandler(this.btExportPng_Click);
            // 
            // cbShowBack
            // 
            this.cbShowBack.AutoSize = true;
            this.cbShowBack.Checked = true;
            this.cbShowBack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowBack.Location = new System.Drawing.Point(128, 600);
            this.cbShowBack.Name = "cbShowBack";
            this.cbShowBack.Size = new System.Drawing.Size(18, 17);
            this.cbShowBack.TabIndex = 18;
            this.cbShowBack.UseVisualStyleBackColor = true;
            this.cbShowBack.CheckedChanged += new System.EventHandler(this.cbShowBack_CheckedChanged);
            // 
            // cbScale
            // 
            this.cbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScale.FormattingEnabled = true;
            this.cbScale.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbScale.Location = new System.Drawing.Point(102, 619);
            this.cbScale.Name = "cbScale";
            this.cbScale.Size = new System.Drawing.Size(44, 24);
            this.cbScale.TabIndex = 17;
            this.cbScale.SelectedIndexChanged += new System.EventHandler(this.cbScale_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 626);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Scale";
            // 
            // pbBack
            // 
            this.pbBack.Location = new System.Drawing.Point(102, 595);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(24, 22);
            this.pbBack.TabIndex = 15;
            this.pbBack.TabStop = false;
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 600);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Back color:";
            // 
            // sfExportDialog
            // 
            this.sfExportDialog.FileName = "anim.png";
            this.sfExportDialog.Filter = "png|*.png";
            this.sfExportDialog.InitialDirectory = "*";
            // 
            // ofOpenPal
            // 
            this.ofOpenPal.FileName = "pal.bin";
            this.ofOpenPal.InitialDirectory = ".";
            // 
            // ofOpenChr
            // 
            this.ofOpenChr.FileName = "chr.bin";
            this.ofOpenChr.InitialDirectory = ".";
            // 
            // AnimEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 690);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvAnims);
            this.Controls.Add(this.pbFrame);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnimEditor";
            this.Text = "Capcom Anim Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrame)).EndInit();
            this.cmTree.ResumeLayout(false);
            this.pnTileProperties.ResumeLayout(false);
            this.pnTileProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog cdBackColor;
        private System.Windows.Forms.ComboBox cbScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbShowBack;
        private System.Windows.Forms.Button btExportPng;
        private System.Windows.Forms.SaveFileDialog sfExportDialog;
        private System.Windows.Forms.Button btLoadPal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog ofOpenPal;
        private System.Windows.Forms.Button btDefaultPal;
        private System.Windows.Forms.Button btLoadChr;
        private System.Windows.Forms.OpenFileDialog ofOpenChr;
        private System.Windows.Forms.ContextMenuStrip cmTree;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
    }
}

