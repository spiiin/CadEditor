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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SegaBlockEdit));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnLeftBottom = new System.Windows.Forms.Panel();
            this.cbPalSubpart = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbActive = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnBlocksBase = new System.Windows.Forms.Panel();
            this.pnBlocks = new System.Windows.Forms.FlowLayoutPanel();
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
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ilSegaTiles = new System.Windows.Forms.ImageList(this.components);
            this.pnView = new System.Windows.Forms.Panel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnLeftBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).BeginInit();
            this.pnBlocksBase.SuspendLayout();
            this.pnRightBottom.SuspendLayout();
            this.pnMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.pnView.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnLeftBottom);
            this.splitContainer1.Panel1.Controls.Add(this.pnBlocksBase);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnView);
            this.splitContainer1.Panel2.Controls.Add(this.pnRightBottom);
            this.splitContainer1.Panel2.Controls.Add(this.pnMapping);
            this.splitContainer1.Size = new System.Drawing.Size(693, 498);
            this.splitContainer1.SplitterDistance = 451;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnLeftBottom
            // 
            this.pnLeftBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnLeftBottom.Controls.Add(this.cbPalSubpart);
            this.pnLeftBottom.Controls.Add(this.label2);
            this.pnLeftBottom.Controls.Add(this.pbActive);
            this.pnLeftBottom.Controls.Add(this.label1);
            this.pnLeftBottom.Location = new System.Drawing.Point(3, 455);
            this.pnLeftBottom.Name = "pnLeftBottom";
            this.pnLeftBottom.Size = new System.Drawing.Size(443, 38);
            this.pnLeftBottom.TabIndex = 3;
            // 
            // cbPalSubpart
            // 
            this.cbPalSubpart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalSubpart.FormattingEnabled = true;
            this.cbPalSubpart.Location = new System.Drawing.Point(77, 7);
            this.cbPalSubpart.Name = "cbPalSubpart";
            this.cbPalSubpart.Size = new System.Drawing.Size(47, 21);
            this.cbPalSubpart.TabIndex = 4;
            this.cbPalSubpart.SelectedIndexChanged += new System.EventHandler(this.cbPalNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "View with pal:";
            // 
            // pbActive
            // 
            this.pbActive.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbActive.Location = new System.Drawing.Point(408, 0);
            this.pbActive.Name = "pbActive";
            this.pbActive.Size = new System.Drawing.Size(32, 32);
            this.pbActive.TabIndex = 2;
            this.pbActive.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Active:";
            // 
            // pnBlocksBase
            // 
            this.pnBlocksBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocksBase.Controls.Add(this.pnBlocks);
            this.pnBlocksBase.Location = new System.Drawing.Point(3, 3);
            this.pnBlocksBase.Name = "pnBlocksBase";
            this.pnBlocksBase.Size = new System.Drawing.Size(443, 432);
            this.pnBlocksBase.TabIndex = 0;
            // 
            // pnBlocks
            // 
            this.pnBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocks.AutoScroll = true;
            this.pnBlocks.Location = new System.Drawing.Point(3, 3);
            this.pnBlocks.Name = "pnBlocks";
            this.pnBlocks.Size = new System.Drawing.Size(440, 429);
            this.pnBlocks.TabIndex = 0;
            // 
            // pnRightBottom
            // 
            this.pnRightBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnRightBottom.Controls.Add(this.cbBlockNo);
            this.pnRightBottom.Controls.Add(this.label5);
            this.pnRightBottom.Location = new System.Drawing.Point(3, 455);
            this.pnRightBottom.Name = "pnRightBottom";
            this.pnRightBottom.Size = new System.Drawing.Size(230, 38);
            this.pnRightBottom.TabIndex = 5;
            // 
            // cbBlockNo
            // 
            this.cbBlockNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlockNo.FormattingEnabled = true;
            this.cbBlockNo.Location = new System.Drawing.Point(62, 7);
            this.cbBlockNo.Name = "cbBlockNo";
            this.cbBlockNo.Size = new System.Drawing.Size(47, 21);
            this.cbBlockNo.TabIndex = 4;
            this.cbBlockNo.SelectedIndexChanged += new System.EventHandler(this.cbBlockNo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "BlockNo:";
            // 
            // pnMapping
            // 
            this.pnMapping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMapping.Controls.Add(this.cbPrior);
            this.pnMapping.Controls.Add(this.cbVFlip);
            this.pnMapping.Controls.Add(this.cbHFlip);
            this.pnMapping.Controls.Add(this.cbPal);
            this.pnMapping.Controls.Add(this.cbTile);
            this.pnMapping.Controls.Add(this.label4);
            this.pnMapping.Controls.Add(this.label3);
            this.pnMapping.Location = new System.Drawing.Point(3, 359);
            this.pnMapping.Name = "pnMapping";
            this.pnMapping.Size = new System.Drawing.Size(230, 90);
            this.pnMapping.TabIndex = 6;
            // 
            // cbPrior
            // 
            this.cbPrior.AutoSize = true;
            this.cbPrior.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPrior.Location = new System.Drawing.Point(57, 70);
            this.cbPrior.Name = "cbPrior";
            this.cbPrior.Size = new System.Drawing.Size(57, 17);
            this.cbPrior.TabIndex = 9;
            this.cbPrior.Text = "Priority";
            this.cbPrior.UseVisualStyleBackColor = true;
            this.cbPrior.CheckedChanged += new System.EventHandler(this.cbPrior_CheckedChanged);
            // 
            // cbVFlip
            // 
            this.cbVFlip.AutoSize = true;
            this.cbVFlip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbVFlip.Location = new System.Drawing.Point(65, 52);
            this.cbVFlip.Name = "cbVFlip";
            this.cbVFlip.Size = new System.Drawing.Size(49, 17);
            this.cbVFlip.TabIndex = 8;
            this.cbVFlip.Text = "VFlip";
            this.cbVFlip.UseVisualStyleBackColor = true;
            this.cbVFlip.CheckedChanged += new System.EventHandler(this.cbVFlip_CheckedChanged);
            // 
            // cbHFlip
            // 
            this.cbHFlip.AutoSize = true;
            this.cbHFlip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbHFlip.Location = new System.Drawing.Point(6, 52);
            this.cbHFlip.Name = "cbHFlip";
            this.cbHFlip.Size = new System.Drawing.Size(50, 17);
            this.cbHFlip.TabIndex = 7;
            this.cbHFlip.Text = "HFlip";
            this.cbHFlip.UseVisualStyleBackColor = true;
            this.cbHFlip.CheckedChanged += new System.EventHandler(this.cbHFlip_CheckedChanged);
            // 
            // cbPal
            // 
            this.cbPal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPal.FormattingEnabled = true;
            this.cbPal.Location = new System.Drawing.Point(67, 28);
            this.cbPal.Name = "cbPal";
            this.cbPal.Size = new System.Drawing.Size(47, 21);
            this.cbPal.TabIndex = 6;
            this.cbPal.SelectedIndexChanged += new System.EventHandler(this.cbPal_SelectedIndexChanged);
            // 
            // cbTile
            // 
            this.cbTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTile.FormattingEnabled = true;
            this.cbTile.Location = new System.Drawing.Point(67, 6);
            this.cbTile.Name = "cbTile";
            this.cbTile.Size = new System.Drawing.Size(47, 21);
            this.cbTile.TabIndex = 5;
            this.cbTile.SelectedIndexChanged += new System.EventHandler(this.cbTile_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "TileNo";
            // 
            // mapScreen
            // 
            this.mapScreen.Location = new System.Drawing.Point(0, 0);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(128, 128);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbSave,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(693, 25);
            this.toolStrip1.TabIndex = 64;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbbSave.Image")));
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(23, 22);
            this.tbbSave.Text = "Save";
            this.tbbSave.Click += new System.EventHandler(this.tbbSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ilSegaTiles
            // 
            this.ilSegaTiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilSegaTiles.ImageSize = new System.Drawing.Size(32, 32);
            this.ilSegaTiles.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnView
            // 
            this.pnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnView.AutoScroll = true;
            this.pnView.Controls.Add(this.mapScreen);
            this.pnView.Location = new System.Drawing.Point(3, 3);
            this.pnView.Name = "pnView";
            this.pnView.Size = new System.Drawing.Size(230, 356);
            this.pnView.TabIndex = 7;
            // 
            // SegaBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 525);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SegaBlockEdit";
            this.Text = "Block Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SegaBlockEdit_FormClosing);
            this.Load += new System.EventHandler(this.SegaBlockEdit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnLeftBottom.ResumeLayout(false);
            this.pnLeftBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbActive)).EndInit();
            this.pnBlocksBase.ResumeLayout(false);
            this.pnRightBottom.ResumeLayout(false);
            this.pnRightBottom.PerformLayout();
            this.pnMapping.ResumeLayout(false);
            this.pnMapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.FlowLayoutPanel pnBlocks;
        private System.Windows.Forms.Panel pnBlocksBase;
        private System.Windows.Forms.ImageList ilSegaTiles;
        private System.Windows.Forms.Panel pnLeftBottom;
        private System.Windows.Forms.PictureBox pbActive;
        private System.Windows.Forms.Label label1;
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

    }
}