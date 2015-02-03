namespace CadEditor
{
    partial class EditColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditColor));
            this.pbColors = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).BeginInit();
            this.SuspendLayout();
            // 
            // pbColors
            // 
            this.pbColors.Location = new System.Drawing.Point(1, 2);
            this.pbColors.Name = "pbColors";
            this.pbColors.Size = new System.Drawing.Size(256, 256);
            this.pbColors.TabIndex = 0;
            this.pbColors.TabStop = false;
            this.pbColors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbColors_MouseClick);
            // 
            // EditColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 262);
            this.Controls.Add(this.pbColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditColor";
            this.Text = "Choose Color";
            this.Load += new System.EventHandler(this.EditColor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbColors;
    }
}