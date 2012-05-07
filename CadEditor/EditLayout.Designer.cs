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
            this.pbMap = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).BeginInit();
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
            this.cbLevel.Location = new System.Drawing.Point(333, 114);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(64, 21);
            this.cbLevel.TabIndex = 0;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Level:";
            // 
            // pbMap
            // 
            this.pbMap.Location = new System.Drawing.Point(403, 15);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(566, 512);
            this.pbMap.TabIndex = 1;
            this.pbMap.TabStop = false;
            this.pbMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            this.pbMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
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
            this.btSave.Location = new System.Drawing.Point(333, 141);
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
            this.objPanel.Location = new System.Drawing.Point(12, 364);
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
            this.cbShowScrolls.Location = new System.Drawing.Point(333, 169);
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
            this.lvObjects.Location = new System.Drawing.Point(973, 31);
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
            this.btPreview.Location = new System.Drawing.Point(973, 499);
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
            this.doorsPanel.Location = new System.Drawing.Point(12, 427);
            this.doorsPanel.Name = "doorsPanel";
            this.doorsPanel.Size = new System.Drawing.Size(325, 94);
            this.doorsPanel.TabIndex = 24;
            // 
            // btLevelParams
            // 
            this.btLevelParams.Location = new System.Drawing.Point(333, 206);
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
            this.label2.Location = new System.Drawing.Point(975, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Ingame screen order:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Select scroll type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 411);
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
            this.cbStopOnDoor.Location = new System.Drawing.Point(975, 473);
            this.cbStopOnDoor.Name = "cbStopOnDoor";
            this.cbStopOnDoor.Size = new System.Drawing.Size(90, 17);
            this.cbStopOnDoor.TabIndex = 33;
            this.cbStopOnDoor.Text = "stop on doors";
            this.cbStopOnDoor.UseVisualStyleBackColor = true;
            // 
            // EditLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 533);
            this.Controls.Add(this.cbStopOnDoor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btLevelParams);
            this.Controls.Add(this.doorsPanel);
            this.Controls.Add(this.btPreview);
            this.Controls.Add(this.lvObjects);
            this.Controls.Add(this.cbShowScrolls);
            this.Controls.Add(this.objPanel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activeBlock);
            this.Controls.Add(this.blocksPanel);
            this.Controls.Add(this.pbMap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditLayout";
            this.Text = "Layout Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeBlock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList screenImages;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbMap;
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
    }
}