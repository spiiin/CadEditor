namespace CadEditor
{
    partial class OpenFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFile));
            this.btOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.tbConfigName = new System.Windows.Forms.TextBox();
            this.ofOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.btClose = new System.Windows.Forms.Button();
            this.tbDumpName = new System.Windows.Forms.TextBox();
            this.lbDumpName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(16, 142);
            this.btOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(296, 28);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "ROM file name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Config file name:";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(126, 72);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(4);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(479, 22);
            this.tbFileName.TabIndex = 3;
            this.tbFileName.Click += new System.EventHandler(this.tbFileName_Click);
            // 
            // tbConfigName
            // 
            this.tbConfigName.Location = new System.Drawing.Point(126, 24);
            this.tbConfigName.Margin = new System.Windows.Forms.Padding(4);
            this.tbConfigName.Name = "tbConfigName";
            this.tbConfigName.Size = new System.Drawing.Size(479, 22);
            this.tbConfigName.TabIndex = 4;
            this.tbConfigName.Click += new System.EventHandler(this.tbConfigName_Click);
            // 
            // ofOpenDialog
            // 
            this.ofOpenDialog.InitialDirectory = "\"\"";
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(320, 142);
            this.btClose.Margin = new System.Windows.Forms.Padding(4);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(287, 28);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "Cancel";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tbDumpName
            // 
            this.tbDumpName.Location = new System.Drawing.Point(128, 112);
            this.tbDumpName.Margin = new System.Windows.Forms.Padding(4);
            this.tbDumpName.Name = "tbDumpName";
            this.tbDumpName.Size = new System.Drawing.Size(479, 22);
            this.tbDumpName.TabIndex = 7;
            this.tbDumpName.Click += new System.EventHandler(this.tbDumpName_Click);
            // 
            // lbDumpName
            // 
            this.lbDumpName.AutoSize = true;
            this.lbDumpName.Location = new System.Drawing.Point(13, 116);
            this.lbDumpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDumpName.Name = "lbDumpName";
            this.lbDumpName.Size = new System.Drawing.Size(110, 17);
            this.lbDumpName.TabIndex = 6;
            this.lbDumpName.Text = "Dump file name:";
            // 
            // OpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 185);
            this.Controls.Add(this.tbDumpName);
            this.Controls.Add(this.lbDumpName);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tbConfigName);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "OpenFile";
            this.Text = "Open File";
            this.Load += new System.EventHandler(this.OpenFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.TextBox tbConfigName;
        private System.Windows.Forms.OpenFileDialog ofOpenDialog;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox tbDumpName;
        private System.Windows.Forms.Label lbDumpName;
    }
}