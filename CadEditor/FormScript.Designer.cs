namespace CadEditor
{
    partial class FormScript
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
            this.btRun = new System.Windows.Forms.Button();
            this.tbScriptFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ofScript = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(7, 57);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(545, 23);
            this.btRun.TabIndex = 0;
            this.btRun.Text = "Run";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // tbScriptFile
            // 
            this.tbScriptFile.Location = new System.Drawing.Point(7, 29);
            this.tbScriptFile.Name = "tbScriptFile";
            this.tbScriptFile.Size = new System.Drawing.Size(545, 22);
            this.tbScriptFile.TabIndex = 1;
            this.tbScriptFile.Click += new System.EventHandler(this.tbScriptFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(546, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select script file (File must contain Script class with Execute(FormScript form) " +
    "method)";
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(7, 106);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(545, 295);
            this.tbLog.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Script log:";
            // 
            // ofScript
            // 
            this.ofScript.FileName = "Script.cs";
            this.ofScript.Filter = "Scripts | *.cs";
            // 
            // FormScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 413);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbScriptFile);
            this.Controls.Add(this.btRun);
            this.Name = "FormScript";
            this.Text = "FormScript";
            this.Load += new System.EventHandler(this.FormScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.TextBox tbScriptFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofScript;
    }
}