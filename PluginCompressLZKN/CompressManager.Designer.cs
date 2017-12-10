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
            this.lbMaxLength = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbInsert = new System.Windows.Forms.CheckBox();
            this.cbAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.pnParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCompress
            // 
            this.btCompress.Location = new System.Drawing.Point(17, 97);
            this.btCompress.Name = "btCompress";
            this.btCompress.Size = new System.Drawing.Size(428, 23);
            this.btCompress.TabIndex = 0;
            this.btCompress.Text = "Compress";
            this.btCompress.UseVisualStyleBackColor = true;
            this.btCompress.Click += new System.EventHandler(this.btCompress_Click);
            // 
            // pnParams
            // 
            this.pnParams.Controls.Add(this.lbMaxLength);
            this.pnParams.Controls.Add(this.label2);
            this.pnParams.Controls.Add(this.cbInsert);
            this.pnParams.Controls.Add(this.cbAddress);
            this.pnParams.Controls.Add(this.label1);
            this.pnParams.Controls.Add(this.btCompress);
            this.pnParams.Location = new System.Drawing.Point(12, 12);
            this.pnParams.Name = "pnParams";
            this.pnParams.Size = new System.Drawing.Size(458, 133);
            this.pnParams.TabIndex = 1;
            // 
            // lbMaxLength
            // 
            this.lbMaxLength.AutoSize = true;
            this.lbMaxLength.Enabled = false;
            this.lbMaxLength.Location = new System.Drawing.Point(335, 50);
            this.lbMaxLength.Name = "lbMaxLength";
            this.lbMaxLength.Size = new System.Drawing.Size(21, 17);
            this.lbMaxLength.TabIndex = 6;
            this.lbMaxLength.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(168, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max compressed length:";
            // 
            // cbInsert
            // 
            this.cbInsert.AutoSize = true;
            this.cbInsert.Checked = true;
            this.cbInsert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInsert.Location = new System.Drawing.Point(17, 15);
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
            this.cbAddress.FormattingEnabled = true;
            this.cbAddress.Location = new System.Drawing.Point(338, 13);
            this.cbAddress.Name = "cbAddress";
            this.cbAddress.Size = new System.Drawing.Size(107, 24);
            this.cbAddress.TabIndex = 3;
            this.cbAddress.SelectedIndexChanged += new System.EventHandler(this.cbAddress_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Address to insert:";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 167);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(458, 272);
            this.tbLog.TabIndex = 2;
            this.tbLog.Text = "Press compress to recompress dump file";
            // 
            // CompressManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 451);
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
    }
}