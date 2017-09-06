namespace PluginExportScreens
{
    partial class ExportTMX
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
            this.cbLayout = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnLevelParam = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btExport = new System.Windows.Forms.Button();
            this.sfSave = new System.Windows.Forms.SaveFileDialog();
            this.pnLevelParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLayout
            // 
            this.cbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayout.FormattingEnabled = true;
            this.cbLayout.Location = new System.Drawing.Point(86, 8);
            this.cbLayout.Name = "cbLayout";
            this.cbLayout.Size = new System.Drawing.Size(367, 24);
            this.cbLayout.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Laoyut";
            // 
            // pnLevelParam
            // 
            this.pnLevelParam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnLevelParam.Controls.Add(this.btExport);
            this.pnLevelParam.Controls.Add(this.label2);
            this.pnLevelParam.Controls.Add(this.label3);
            this.pnLevelParam.Controls.Add(this.cbLayout);
            this.pnLevelParam.Location = new System.Drawing.Point(0, 12);
            this.pnLevelParam.Name = "pnLevelParam";
            this.pnLevelParam.Size = new System.Drawing.Size(463, 104);
            this.pnLevelParam.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(464, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "*View parameters (Blocks, Scale, Borders) will be read from main window";
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(3, 76);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(459, 23);
            this.btExport.TabIndex = 6;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // sfSave
            // 
            this.sfSave.FileName = "CadLevel.tmx";
            this.sfSave.Filter = "TMX|*.tmx";
            this.sfSave.InitialDirectory = "./exportTmx/";
            // 
            // ExportTMX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 124);
            this.Controls.Add(this.pnLevelParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ExportTMX";
            this.Text = "Export TMX";
            this.Load += new System.EventHandler(this.ExportTMX_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExportTMX_KeyDown);
            this.pnLevelParam.ResumeLayout(false);
            this.pnLevelParam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbLayout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnLevelParam;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.SaveFileDialog sfSave;
        private System.Windows.Forms.Label label2;
    }
}