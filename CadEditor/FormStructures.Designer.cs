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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStructures));
            this.mapScreen = new System.Windows.Forms.PictureBox();
            this.lbStructures = new System.Windows.Forms.ListBox();
            this.cbWidth = new System.Windows.Forms.ComboBox();
            this.cbHeight = new System.Windows.Forms.ComboBox();
            this.btAddStructure = new System.Windows.Forms.Button();
            this.btRemoveStructure = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pnBlocks = new System.Windows.Forms.Panel();
            this.blocksScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).BeginInit();
            this.pnBlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // mapScreen
            // 
            this.mapScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapScreen.Location = new System.Drawing.Point(748, 11);
            this.mapScreen.Margin = new System.Windows.Forms.Padding(4);
            this.mapScreen.Name = "mapScreen";
            this.mapScreen.Size = new System.Drawing.Size(683, 630);
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
            this.lbStructures.ItemHeight = 16;
            this.lbStructures.Location = new System.Drawing.Point(16, 11);
            this.lbStructures.Margin = new System.Windows.Forms.Padding(4);
            this.lbStructures.Name = "lbStructures";
            this.lbStructures.Size = new System.Drawing.Size(287, 628);
            this.lbStructures.TabIndex = 0;
            this.lbStructures.SelectedIndexChanged += new System.EventHandler(this.lbStructures_SelectedIndexChanged);
            this.lbStructures.DoubleClick += new System.EventHandler(this.lbStructures_DoubleClick);
            // 
            // cbWidth
            // 
            this.cbWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWidth.FormattingEnabled = true;
            this.cbWidth.Location = new System.Drawing.Point(748, 649);
            this.cbWidth.Margin = new System.Windows.Forms.Padding(4);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(59, 24);
            this.cbWidth.TabIndex = 6;
            this.cbWidth.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // cbHeight
            // 
            this.cbHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeight.FormattingEnabled = true;
            this.cbHeight.Location = new System.Drawing.Point(816, 649);
            this.cbHeight.Margin = new System.Windows.Forms.Padding(4);
            this.cbHeight.Name = "cbHeight";
            this.cbHeight.Size = new System.Drawing.Size(59, 24);
            this.cbHeight.TabIndex = 7;
            this.cbHeight.SelectedIndexChanged += new System.EventHandler(this.cbWidth_SelectedIndexChanged);
            // 
            // btAddStructure
            // 
            this.btAddStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddStructure.Location = new System.Drawing.Point(16, 649);
            this.btAddStructure.Margin = new System.Windows.Forms.Padding(4);
            this.btAddStructure.Name = "btAddStructure";
            this.btAddStructure.Size = new System.Drawing.Size(51, 28);
            this.btAddStructure.TabIndex = 8;
            this.btAddStructure.Text = "+";
            this.btAddStructure.UseVisualStyleBackColor = true;
            this.btAddStructure.Click += new System.EventHandler(this.btAddStructure_Click);
            // 
            // btRemoveStructure
            // 
            this.btRemoveStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btRemoveStructure.Location = new System.Drawing.Point(75, 649);
            this.btRemoveStructure.Margin = new System.Windows.Forms.Padding(4);
            this.btRemoveStructure.Name = "btRemoveStructure";
            this.btRemoveStructure.Size = new System.Drawing.Size(51, 28);
            this.btRemoveStructure.TabIndex = 9;
            this.btRemoveStructure.Text = "-";
            this.btRemoveStructure.UseVisualStyleBackColor = true;
            this.btRemoveStructure.Click += new System.EventHandler(this.btRemoveStructure_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.Location = new System.Drawing.Point(16, 684);
            this.btSave.Margin = new System.Windows.Forms.Padding(4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(51, 28);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btLoad
            // 
            this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btLoad.Location = new System.Drawing.Point(75, 684);
            this.btLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(51, 28);
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
            // pnBlocks
            // 
            this.pnBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnBlocks.AutoScroll = true;
            this.pnBlocks.Controls.Add(this.blocksScreen);
            this.pnBlocks.Location = new System.Drawing.Point(310, 12);
            this.pnBlocks.Name = "pnBlocks";
            this.pnBlocks.Size = new System.Drawing.Size(431, 628);
            this.pnBlocks.TabIndex = 62;
            // 
            // blocksScreen
            // 
            this.blocksScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksScreen.Location = new System.Drawing.Point(3, 3);
            this.blocksScreen.Name = "blocksScreen";
            this.blocksScreen.Size = new System.Drawing.Size(425, 332);
            this.blocksScreen.TabIndex = 5;
            this.blocksScreen.TabStop = false;
            this.blocksScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.blocksScreen_Paint);
            this.blocksScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocksScreen_MouseDown);
            // 
            // FormStructures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 736);
            this.Controls.Add(this.pnBlocks);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btRemoveStructure);
            this.Controls.Add(this.btAddStructure);
            this.Controls.Add(this.cbHeight);
            this.Controls.Add(this.cbWidth);
            this.Controls.Add(this.lbStructures);
            this.Controls.Add(this.mapScreen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormStructures";
            this.Text = "Structures Editor (beta)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStructures_FormClosing);
            this.Load += new System.EventHandler(this.FormStructures_Load);
            this.Resize += new System.EventHandler(this.FormStructures_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.mapScreen)).EndInit();
            this.pnBlocks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbStructures;
        private System.Windows.Forms.PictureBox mapScreen;
        private System.Windows.Forms.ComboBox cbWidth;
        private System.Windows.Forms.ComboBox cbHeight;
        private System.Windows.Forms.Button btAddStructure;
        private System.Windows.Forms.Button btRemoveStructure;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel pnBlocks;
        private System.Windows.Forms.PictureBox blocksScreen;
    }
}