namespace SettingsViewer
{
    partial class SettingsViewer
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
            this.pgConfig = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btReload = new System.Windows.Forms.Button();
            this.tbConfigName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ofConfig = new System.Windows.Forms.OpenFileDialog();
            this.btExecute = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgConfig
            // 
            this.pgConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgConfig.DisabledItemForeColor = System.Drawing.SystemColors.ControlText;
            this.pgConfig.Location = new System.Drawing.Point(12, 100);
            this.pgConfig.Name = "pgConfig";
            this.pgConfig.Size = new System.Drawing.Size(464, 363);
            this.pgConfig.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btReload);
            this.panel1.Controls.Add(this.tbConfigName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 82);
            this.panel1.TabIndex = 1;
            // 
            // btReload
            // 
            this.btReload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btReload.Location = new System.Drawing.Point(17, 43);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(433, 23);
            this.btReload.TabIndex = 9;
            this.btReload.Text = "Reload";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // tbConfigName
            // 
            this.tbConfigName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfigName.Location = new System.Drawing.Point(129, 4);
            this.tbConfigName.Margin = new System.Windows.Forms.Padding(4);
            this.tbConfigName.Name = "tbConfigName";
            this.tbConfigName.Size = new System.Drawing.Size(321, 22);
            this.tbConfigName.TabIndex = 8;
            this.tbConfigName.Click += new System.EventHandler(this.tbConfigName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Config file name:";
            // 
            // btExecute
            // 
            this.btExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btExecute.Location = new System.Drawing.Point(12, 469);
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(464, 23);
            this.btExecute.TabIndex = 2;
            this.btExecute.Text = "Execute selected";
            this.btExecute.UseVisualStyleBackColor = true;
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // ConfigViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 504);
            this.Controls.Add(this.btExecute);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pgConfig);
            this.Name = "ConfigViewer";
            this.Text = "Config Viewer";
            this.Load += new System.EventHandler(this.ConfigViewer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgConfig;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbConfigName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.OpenFileDialog ofConfig;
        private System.Windows.Forms.Button btExecute;
    }
}

