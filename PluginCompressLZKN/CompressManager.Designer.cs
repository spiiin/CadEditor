namespace PluginCompressLZKN
{
    partial class CompressManager
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
            this.btCompress = new System.Windows.Forms.Button();
            this.pnParams = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbArchiveFile = new System.Windows.Forms.CheckBox();
            this.lbMaxLength = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbInsert = new System.Windows.Forms.CheckBox();
            this.cbAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.cbFillZero = new System.Windows.Forms.CheckBox();
            this.pnParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCompress
            // 
            this.btCompress.Location = new System.Drawing.Point(17, 142);
            this.btCompress.Name = "btCompress";
            this.btCompress.Size = new System.Drawing.Size(468, 23);
            this.btCompress.TabIndex = 0;
            this.btCompress.Text = "Compress";
            this.btCompress.UseVisualStyleBackColor = true;
            this.btCompress.Click += new System.EventHandler(this.btCompress_Click);
            // 
            // pnParams
            // 
            this.pnParams.Controls.Add(this.cbFillZero);
            this.pnParams.Controls.Add(this.label7);
            this.pnParams.Controls.Add(this.label6);
            this.pnParams.Controls.Add(this.label5);
            this.pnParams.Controls.Add(this.label4);
            this.pnParams.Controls.Add(this.label3);
            this.pnParams.Controls.Add(this.cbArchiveFile);
            this.pnParams.Controls.Add(this.lbMaxLength);
            this.pnParams.Controls.Add(this.label2);
            this.pnParams.Controls.Add(this.cbInsert);
            this.pnParams.Controls.Add(this.cbAddress);
            this.pnParams.Controls.Add(this.label1);
            this.pnParams.Controls.Add(this.btCompress);
            this.pnParams.Location = new System.Drawing.Point(12, 12);
            this.pnParams.Name = "pnParams";
            this.pnParams.Size = new System.Drawing.Size(515, 171);
            this.pnParams.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "*what to compress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "that can be inserted in ROM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(286, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "*maximum size to new archive,";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "*insert archive in ROM file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "*created *.lzkn1 file with archive";
            // 
            // cbArchiveFile
            // 
            this.cbArchiveFile.AutoSize = true;
            this.cbArchiveFile.Location = new System.Drawing.Point(17, 115);
            this.cbArchiveFile.Name = "cbArchiveFile";
            this.cbArchiveFile.Size = new System.Drawing.Size(144, 21);
            this.cbArchiveFile.TabIndex = 7;
            this.cbArchiveFile.Text = "Create archive file";
            this.cbArchiveFile.UseVisualStyleBackColor = true;
            // 
            // lbMaxLength
            // 
            this.lbMaxLength.AutoSize = true;
            this.lbMaxLength.Location = new System.Drawing.Point(181, 37);
            this.lbMaxLength.Name = "lbMaxLength";
            this.lbMaxLength.Size = new System.Drawing.Size(21, 17);
            this.lbMaxLength.TabIndex = 6;
            this.lbMaxLength.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max archive size (bytes):";
            // 
            // cbInsert
            // 
            this.cbInsert.AutoSize = true;
            this.cbInsert.Checked = true;
            this.cbInsert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInsert.Location = new System.Drawing.Point(17, 73);
            this.cbInsert.Name = "cbInsert";
            this.cbInsert.Size = new System.Drawing.Size(117, 21);
            this.cbInsert.TabIndex = 4;
            this.cbInsert.Text = "Insert to ROM";
            this.cbInsert.UseVisualStyleBackColor = true;
            this.cbInsert.CheckedChanged += new System.EventHandler(this.cbInsert_CheckedChanged);
            // 
            // cbAddress
            // 
            this.cbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddress.DropDownWidth = 240;
            this.cbAddress.FormattingEnabled = true;
            this.cbAddress.Location = new System.Drawing.Point(170, 10);
            this.cbAddress.Name = "cbAddress";
            this.cbAddress.Size = new System.Drawing.Size(110, 24);
            this.cbAddress.TabIndex = 3;
            this.cbAddress.SelectedIndexChanged += new System.EventHandler(this.cbAddress_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Address to insert:";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 189);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(515, 310);
            this.tbLog.TabIndex = 2;
            this.tbLog.Text = "Press compress to recompress dump file";
            // 
            // cbFillZero
            // 
            this.cbFillZero.AutoSize = true;
            this.cbFillZero.Checked = true;
            this.cbFillZero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFillZero.Location = new System.Drawing.Point(17, 95);
            this.cbFillZero.Name = "cbFillZero";
            this.cbFillZero.Size = new System.Drawing.Size(185, 21);
            this.cbFillZero.TabIndex = 13;
            this.cbFillZero.Text = "Fill free space with zeros";
            this.cbFillZero.UseVisualStyleBackColor = true;
            // 
            // CompressManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 511);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.pnParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CompressManager";
            this.Text = "Compress manager";
            this.Load += new System.EventHandler(this.CompressManager_Load);
            this.pnParams.ResumeLayout(false);
            this.pnParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCompress;
        private System.Windows.Forms.Panel pnParams;
        private System.Windows.Forms.CheckBox cbInsert;
        private System.Windows.Forms.ComboBox cbAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lbMaxLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbArchiveFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbFillZero;
    }
}