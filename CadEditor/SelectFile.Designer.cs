namespace CadEditor
{
    partial class SelectFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFile));
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btSelect = new System.Windows.Forms.Button();
            this.ofOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbExportType = new System.Windows.Forms.ComboBox();
            this.lbExportType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(81, 6);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(206, 20);
            this.tbFileName.TabIndex = 10;
            this.tbFileName.Text = "exportedScreens.bin";
            this.tbFileName.Click += new System.EventHandler(this.tbFileName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "File name:";
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(12, 64);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(275, 23);
            this.btSelect.TabIndex = 12;
            this.btSelect.Text = "Select";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // ofOpenDialog
            // 
            this.ofOpenDialog.CheckFileExists = false;
            this.ofOpenDialog.FileName = "openFileDialog1";
            // 
            // cbExportType
            // 
            this.cbExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExportType.FormattingEnabled = true;
            this.cbExportType.Items.AddRange(new object[] {
            "Binary",
            "Picture"});
            this.cbExportType.Location = new System.Drawing.Point(81, 32);
            this.cbExportType.Name = "cbExportType";
            this.cbExportType.Size = new System.Drawing.Size(206, 21);
            this.cbExportType.TabIndex = 13;
            this.cbExportType.SelectedIndexChanged += new System.EventHandler(this.cbExportType_SelectedIndexChanged);
            // 
            // lbExportType
            // 
            this.lbExportType.AutoSize = true;
            this.lbExportType.Location = new System.Drawing.Point(12, 35);
            this.lbExportType.Name = "lbExportType";
            this.lbExportType.Size = new System.Drawing.Size(63, 13);
            this.lbExportType.TabIndex = 14;
            this.lbExportType.Text = "Export type:";
            // 
            // SelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 99);
            this.Controls.Add(this.lbExportType);
            this.Controls.Add(this.cbExportType);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SelectFile";
            this.Text = "Select File";
            this.Load += new System.EventHandler(this.SelectFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.OpenFileDialog ofOpenDialog;
        private System.Windows.Forms.ComboBox cbExportType;
        private System.Windows.Forms.Label lbExportType;
    }
}