namespace CadEditor
{
    partial class SaveScreensCount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveScreensCount));
            this.cbFirst = new System.Windows.Forms.ComboBox();
            this.cbCount = new System.Windows.Forms.ComboBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.ofOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.btImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbFirst
            // 
            this.cbFirst.FormattingEnabled = true;
            this.cbFirst.Location = new System.Drawing.Point(63, 30);
            this.cbFirst.Name = "cbFirst";
            this.cbFirst.Size = new System.Drawing.Size(164, 21);
            this.cbFirst.TabIndex = 0;
            this.cbFirst.Text = "0";
            // 
            // cbCount
            // 
            this.cbCount.FormattingEnabled = true;
            this.cbCount.Location = new System.Drawing.Point(63, 54);
            this.cbCount.Name = "cbCount";
            this.cbCount.Size = new System.Drawing.Size(164, 21);
            this.cbCount.TabIndex = 1;
            this.cbCount.Text = "100";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(63, 6);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(164, 20);
            this.tbFileName.TabIndex = 8;
            this.tbFileName.Text = "exportedScreens.bin";
            this.tbFileName.Click += new System.EventHandler(this.tbFileName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "File name:";
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(5, 81);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(222, 23);
            this.btOpen.TabIndex = 6;
            this.btOpen.Text = "Export";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "First";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(2, 57);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(35, 13);
            this.lbCount.TabIndex = 10;
            this.lbCount.Text = "Count";
            // 
            // ofOpenDialog
            // 
            this.ofOpenDialog.CheckFileExists = false;
            this.ofOpenDialog.FileName = "exportedScreens.bin";
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(5, 81);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(222, 23);
            this.btImport.TabIndex = 11;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // SaveScreensCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 113);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.cbCount);
            this.Controls.Add(this.cbFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SaveScreensCount";
            this.Text = "Export screens";
            this.Load += new System.EventHandler(this.SaveScreensCount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFirst;
        private System.Windows.Forms.ComboBox cbCount;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.OpenFileDialog ofOpenDialog;
        private System.Windows.Forms.Button btImport;
    }
}