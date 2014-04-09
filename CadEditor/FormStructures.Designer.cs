namespace CadEditor
{
    partial class FormStructures
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStructures));
            this.blocksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.lbStructures = new System.Windows.Forms.ListBox();
            this.bigBlocks = new System.Windows.Forms.ImageList(this.components);
            this.cbWidth = new System.Windows.Forms.ComboBox();
            this.cbHeight = new System.Windows.Forms.ComboBox();
            this.btAddStructure = new System.Windows.Forms.Button();
            this.btRemoveStructure = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // blocksPanel
            // 
            this.blocksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksPanel.AutoScroll = true;
            this.blocksPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blocksPanel.Location = new System.Drawing.Point(231, 9);
            this.blocksPanel.Margin = new System.Windows.Forms.Padding(0);
            this.blocksPanel.Name = "blocksPanel";
            this.blocksPanel.Size = new System.Drawing.Size(327, 512);
            this.blocksPanel.TabIndex = 3;
            // 
            // mapScreen
            // 
            this.mapScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapScreen.Location = new System.Drawing.Point(561, 9);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(512, 512);
            this.mapScreen.TabIndex = 5;
            this.mapScreen.TabStop = false;
            this.mapScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.mapScreen_Paint);
            this.mapScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapScreen_MouseClick);
            // 
            // lbStructures
            // 
            this.lbStructures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStructures.FormattingEnabled = true;
            this.lbStructures.Location = new System.Drawing.Point(12, 9);
            this.lbStructures.Name = "lbStructures";
            this.lbStructures.Size = new System.Drawing.Size(216, 511);
            this.lbStructures.TabIndex = 0;
            this.lbStructures.SelectedIndexChanged += new System.EventHandler(this.lbStructures_SelectedIndexChanged);
            this.lbStructures.DoubleClick += new System.EventHandler(this.lbStructures_DoubleClick);
            // 
            // bigBlocks
            // 
            this.bigBlocks.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.bigBlocks.ImageSize = new System.Drawing.Size(64, 64);
            this.bigBlocks.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbWidth
            // 
            this.cbWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWidth.FormattingEnabled = true;
            this.cbWidth.Location = new System.Drawing.Point(561, 527);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(45, 21);
            this.cbWidth.TabIndex = 6;
            this.cbWidth.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // cbHeight
            // 
            this.cbHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeight.FormattingEnabled = true;
            this.cbHeight.Location = new System.Drawing.Point(612, 527);
            this.cbHeight.Name = "cbHeight";
            this.cbHeight.Size = new System.Drawing.Size(45, 21);
            this.cbHeight.TabIndex = 7;
            this.cbHeight.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // btAddStructure
            // 
            this.btAddStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddStructure.Location = new System.Drawing.Point(12, 527);
            this.btAddStructure.Name = "btAddStructure";
            this.btAddStructure.Size = new System.Drawing.Size(38, 23);
            this.btAddStructure.TabIndex = 8;
            this.btAddStructure.Text = "+";
            this.btAddStructure.UseVisualStyleBackColor = true;
            this.btAddStructure.Click += new System.EventHandler(this.btAddStructure_Click);
            // 
            // btRemoveStructure
            // 
            this.btRemoveStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRemoveStructure.Location = new System.Drawing.Point(56, 527);
            this.btRemoveStructure.Name = "btRemoveStructure";
            this.btRemoveStructure.Size = new System.Drawing.Size(38, 23);
            this.btRemoveStructure.TabIndex = 9;
            this.btRemoveStructure.Text = "-";
            this.btRemoveStructure.UseVisualStyleBackColor = true;
            this.btRemoveStructure.Click += new System.EventHandler(this.btRemoveStructure_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.Location = new System.Drawing.Point(12, 556);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(38, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btLoad
            // 
            this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btLoad.Location = new System.Drawing.Point(56, 556);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(38, 23);
            this.btLoad.TabIndex = 11;
            this.btLoad.Text = "load";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "structures.bin";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "structures.bin";
            // 
            // FormStructures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 598);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btRemoveStructure);
            this.Controls.Add(this.btAddStructure);
            this.Controls.Add(this.cbHeight);
            this.Controls.Add(this.cbWidth);
            this.Controls.Add(this.lbStructures);
            this.Controls.Add(this.mapScreen);
            this.Controls.Add(this.blocksPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStructures";
            this.Text = "Structures Editor (beta)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStructures_FormClosing);
            this.Load += new System.EventHandler(this.FormStructures_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel blocksPanel;
        private System.Windows.Forms.ListBox lbStructures;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.ImageList bigBlocks;
        private System.Windows.Forms.ComboBox cbWidth;
        private System.Windows.Forms.ComboBox cbHeight;
        private System.Windows.Forms.Button btAddStructure;
        private System.Windows.Forms.Button btRemoveStructure;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}