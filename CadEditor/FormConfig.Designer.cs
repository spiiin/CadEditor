namespace CadEditor
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.tbScrOffset = new System.Windows.Forms.TextBox();
            this.tbScrWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbScrHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Screens offset";
            // 
            // tbScrOffset
            // 
            this.tbScrOffset.Location = new System.Drawing.Point(105, 12);
            this.tbScrOffset.Name = "tbScrOffset";
            this.tbScrOffset.Size = new System.Drawing.Size(75, 20);
            this.tbScrOffset.TabIndex = 1;
            // 
            // tbScrWidth
            // 
            this.tbScrWidth.Location = new System.Drawing.Point(105, 40);
            this.tbScrWidth.Name = "tbScrWidth";
            this.tbScrWidth.Size = new System.Drawing.Size(75, 20);
            this.tbScrWidth.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Screen width";
            // 
            // tbScrHeight
            // 
            this.tbScrHeight.Location = new System.Drawing.Point(105, 68);
            this.tbScrHeight.Name = "tbScrHeight";
            this.tbScrHeight.Size = new System.Drawing.Size(75, 20);
            this.tbScrHeight.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Screen height";
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(24, 93);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 6;
            this.btOk.Text = "Apply";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(105, 93);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Close";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 118);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.tbScrHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbScrWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbScrOffset);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConfig";
            this.Text = "Config Editor";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbScrOffset;
        private System.Windows.Forms.TextBox tbScrWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbScrHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
    }
}