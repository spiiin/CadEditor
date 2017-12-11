namespace CadEditor
{
    partial class SegaBlockEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SegaBlockEdit));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnBlocks = new System.Windows.Forms.Panel();
            this.pnBlocksScroll = new System.Windows.Forms.Panel();
            this.blocksScreen = new System.Windows.Forms.PictureBox();
            this.pnLeftBottom = new System.Windows.Forms.Panel();
            this.cbPalSubpart = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnView = new System.Windows.Forms.Panel();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.pnRightBottom = new System.Windows.Forms.Panel();
            this.cbBlockNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnMapping = new System.Windows.Forms.Panel();
            this.cbPrior = new System.Windows.Forms.CheckBox();
            this.cbVFlip = new System.Windows.Forms.CheckBox();
            this.cbHFlip = new System.Windows.Forms.CheckBox();
            this.cbPal = new System.Windows.Forms.ComboBox();
            this.cbTile = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pnViewScroll = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnBlocks.SuspendLayout();
            this.pnBlocksScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).BeginInit();
            this.pnLeftBottom.SuspendLayout();
            this.pnView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.pnRightBottom.SuspendLayout();
            this.pnMapping.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnViewScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnLeftBottom);
            this.splitContainer1.Panel1.Controls.Add(this.pnBlocks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnMapping);
            this.splitContainer1.Panel2.Controls.Add(this.pnRightBottom);
            this.splitContainer1.Panel2.Controls.Add(this.pnView);
            this.splitContainer1.Size = new System.Drawing.Size(992, 529);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnBlocks
            // 
            this.pnBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocks.AutoScroll = true;
            this.pnBlocks.Controls.Add(this.pnBlocksScroll);
            this.pnBlocks.Location = new System.Drawing.Point(4, 3);
            this.pnBlocks.Name = "pnBlocks";
            this.pnBlocks.Size = new System.Drawing.Size(348, 468);
            this.pnBlocks.TabIndex = 999;
            this.pnBlocks.SizeChanged += new System.EventHandler(this.pnBlocks_SizeChanged);
            // 
            // pnBlocksScroll
            // 
            this.pnBlocksScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocksScroll.AutoScroll = true;
            this.pnBlocksScroll.Controls.Add(this.blocksScreen);
            this.pnBlocksScroll.Location = new System.Drawing.Point(0, 4);
            this.pnBlocksScroll.Name = "pnBlocksScroll";
            this.pnBlocksScroll.Size = new System.Drawing.Size(348, 461);
            this.pnBlocksScroll.TabIndex = 63;
            // 
            // blocksScreen
            // 
            this.blocksScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksScreen.Location = new System.Drawing.Point(3, 9);
            this.blocksScreen.Name = "blocksScreen";
            this.blocksScreen.Size = new System.Drawing.Size(332, 449);
            this.blocksScreen.TabIndex = 5;
            this.blocksScreen.TabStop = false;
            this.blocksScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksScreen_Paint);
            this.blocksScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocksScreen_MouseDown);
            // 
            // pnLeftBottom
            // 
            this.pnLeftBottom.Controls.Add(this.cbPalSubpart);
            this.pnLeftBottom.Controls.Add(this.label2);
            this.pnLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnLeftBottom.Location = new System.Drawing.Point(0, 478);
            this.pnLeftBottom.Margin = new System.Windows.Forms.Padding(4);
            this.pnLeftBottom.Name = "pnLeftBottom";
            this.pnLeftBottom.Size = new System.Drawing.Size(355, 49);
            this.pnLeftBottom.TabIndex = 3;
            // 
            // cbPalSubpart
            // 
            this.cbPalSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalSubpart.FormattingEnabled = true;
            this.cbPalSubpart.Location = new System.Drawing.Point(103, 9);
            this.cbPalSubpart.Margin = new System.Windows.Forms.Padding(4);
            this.cbPalSubpart.Name = "cbPalSubpart";
            this.cbPalSubpart.Size = new System.Drawing.Size(61, 24);
            this.cbPalSubpart.TabIndex = 4;
            this.cbPalSubpart.SelectedIndexChanged += new System.EventHandler(this.cbPalNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "View with pal:";
            // 
            // pnView
            // 
            this.pnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnView.AutoScroll = true;
            this.pnView.Controls.Add(this.pnViewScroll);
            this.pnView.Location = new System.Drawing.Point(4, 4);
            this.pnView.Margin = new System.Windows.Forms.Padding(4);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(602, 311);
            this.pnView.TabIndex = 7;
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(0, 0);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(171, 158);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // pnRightBottom
            // 
            this.pnRightBottom.Controls.Add(this.cbBlockNo);
            this.pnRightBottom.Controls.Add(this.label5);
            this.pnRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnRightBottom.Location = new System.Drawing.Point(0, 480);
            this.pnRightBottom.Margin = new System.Windows.Forms.Padding(4);
            this.pnRightBottom.Name = "pnRightBottom";
            this.pnRightBottom.Size = new System.Drawing.Size(628, 47);
            this.pnRightBottom.TabIndex = 7;
            // 
            // cbBlockNo
            // 
            this.cbBlockNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlockNo.FormattingEnabled = true;
            this.cbBlockNo.Location = new System.Drawing.Point(83, 9);
            this.cbBlockNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(61, 24);
            this.cbBlockNo.TabIndex = 4;
            this.cbBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbBlockNo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "BlockNo:";
            // 
            // pnMapping
            // 
            this.pnMapping.Controls.Add(this.cbPrior);
            this.pnMapping.Controls.Add(this.cbVFlip);
            this.pnMapping.Controls.Add(this.cbHFlip);
            this.pnMapping.Controls.Add(this.cbPal);
            this.pnMapping.Controls.Add(this.cbTile);
            this.pnMapping.Controls.Add(this.label4);
            this.pnMapping.Controls.Add(this.label3);
            this.pnMapping.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnMapping.Location = new System.Drawing.Point(0, 369);
            this.pnMapping.Margin = new System.Windows.Forms.Padding(4);
            this.pnMapping.Name = "pnMapping";
            this.pnMapping.Size = new System.Drawing.Size(628, 111);
            this.pnMapping.TabIndex = 6;
            // 
            // cbPrior
            // 
            this.cbPrior.AutoSize = true;
            this.cbPrior.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPrior.Location = new System.Drawing.Point(76, 86);
            this.cbPrior.Margin = new System.Windows.Forms.Padding(4);
            this.cbPrior.Name = "cbPrior";
            this.cbPrior.Size = new System.Drawing.Size(74, 21);
            this.cbPrior.TabIndex = 9;
            this.cbPrior.Text = "Priority";
            this.cbPrior.UseVisualStyleBackColor = true;
            this.cbPrior.CheckedChanged += new System.EventHandler(this.cbPrior_CheckedChanged);
            // 
            // cbVFlip
            // 
            this.cbVFlip.AutoSize = true;
            this.cbVFlip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbVFlip.Location = new System.Drawing.Point(87, 64);
            this.cbVFlip.Margin = new System.Windows.Forms.Padding(4);
            this.cbVFlip.Name = "cbVFlip";
            this.cbVFlip.Size = new System.Drawing.Size(61, 21);
            this.cbVFlip.TabIndex = 8;
            this.cbVFlip.Text = "VFlip";
            this.cbVFlip.UseVisualStyleBackColor = true;
            this.cbVFlip.CheckedChanged += new System.EventHandler(this.cbVFlip_CheckedChanged);
            // 
            // cbHFlip
            // 
            this.cbHFlip.AutoSize = true;
            this.cbHFlip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbHFlip.Location = new System.Drawing.Point(8, 64);
            this.cbHFlip.Margin = new System.Windows.Forms.Padding(4);
            this.cbHFlip.Name = "cbHFlip";
            this.cbHFlip.Size = new System.Drawing.Size(62, 21);
            this.cbHFlip.TabIndex = 7;
            this.cbHFlip.Text = "HFlip";
            this.cbHFlip.UseVisualStyleBackColor = true;
            this.cbHFlip.CheckedChanged += new System.EventHandler(this.cbHFlip_CheckedChanged);
            // 
            // cbPal
            // 
            this.cbPal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPal.FormattingEnabled = true;
            this.cbPal.Location = new System.Drawing.Point(89, 34);
            this.cbPal.Margin = new System.Windows.Forms.Padding(4);
            this.cbPal.Name = "cbPal";
            this.cbPal.Size = new System.Drawing.Size(61, 24);
            this.cbPal.TabIndex = 6;
            this.cbPal.SelectedIndexChanged += new System.EventHandler(this.cbPal_SelectedIndexChanged);
            // 
            // cbTile
            // 
            this.cbTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTile.FormattingEnabled = true;
            this.cbTile.Location = new System.Drawing.Point(89, 7);
            this.cbTile.Margin = new System.Windows.Forms.Padding(4);
            this.cbTile.Name = "cbTile";
            this.cbTile.Size = new System.Drawing.Size(61, 24);
            this.cbTile.TabIndex = 5;
            this.cbTile.SelectedIndexChanged += new System.EventHandler(this.cbTile_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "TileNo";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbSave,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1005, 27);
            this.toolStrip1.TabIndex = 64;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbbSave.Image")));
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(24, 24);
            this.tbbSave.Text = "Save";
            this.tbbSave.Click += new System.EventHandler(this.tbbSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // pnViewScroll
            // 
            this.pnViewScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnViewScroll.AutoScroll = true;
            this.pnViewScroll.Controls.Add(this.mapScreen);
            this.pnViewScroll.Location = new System.Drawing.Point(8, 8);
            this.pnViewScroll.Margin = new System.Windows.Forms.Padding(4);
            this.pnViewScroll.Name = "pnViewScroll";
            this.pnViewScroll.Size = new System.Drawing.Size(590, 299);
            this.pnViewScroll.TabIndex = 8;
            // 
            // SegaBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 576);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SegaBlockEdit";
            this.Text = "Sega Block Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SegaBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.SegaBlockEdit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnBlocks.ResumeLayout(false);
            this.pnBlocksScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).EndInit();
            this.pnLeftBottom.ResumeLayout(false);
            this.pnLeftBottom.PerformLayout();
            this.pnView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.pnRightBottom.ResumeLayout(false);
            this.pnRightBottom.PerformLayout();
            this.pnMapping.ResumeLayout(false);
            this.pnMapping.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnViewScroll.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel pnLeftBottom;
        private System.Windows.Forms.ComboBox cbPalSubpart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.Panel pnRightBottom;
        private System.Windows.Forms.ComboBox cbBlockNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnMapping;
        private System.Windows.Forms.CheckBox cbVFlip;
        private System.Windows.Forms.CheckBox cbHFlip;
        private System.Windows.Forms.ComboBox cbPal;
        private System.Windows.Forms.ComboBox cbTile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbPrior;
        private System.Windows.Forms.Panel pnView;
        private System.Windows.Forms.Panel pnBlocks;
        private System.Windows.Forms.PictureBox blocksScreen;
        private System.Windows.Forms.Panel pnBlocksScroll;
        private System.Windows.Forms.Panel pnViewScroll;
    }
}