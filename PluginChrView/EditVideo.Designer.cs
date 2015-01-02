namespace CadEditor
{
    partial class EditVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditVideo));
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPalleteNo = new System.Windows.Forms.ComboBox();
            this.cbSubPal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbPal = new System.Windows.Forms.PictureBox();
            this.cbVideoNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShowNo = new System.Windows.Forms.CheckBox();
            this.btExport = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(177, 12);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(512, 512);
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btImport);
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.cbShowNo);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbPalleteNo);
            this.panel1.Controls.Add(this.cbSubPal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pbPal);
            this.panel1.Controls.Add(this.cbVideoNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 512);
            this.panel1.TabIndex = 1;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(3, 279);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(131, 23);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pallete:";
            // 
            // cbPalleteNo
            // 
            this.cbPalleteNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPalleteNo.FormattingEnabled = true;
            this.cbPalleteNo.Location = new System.Drawing.Point(67, 94);
            this.cbPalleteNo.Name = "cbPalleteNo";
            this.cbPalleteNo.Size = new System.Drawing.Size(67, 21);
            this.cbPalleteNo.TabIndex = 5;
            this.cbPalleteNo.SelectedIndexChanged += new System.EventHandler(this.cbPalleteNo_SelectedIndexChanged);
            // 
            // cbSubPal
            // 
            this.cbSubPal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubPal.FormattingEnabled = true;
            this.cbSubPal.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbSubPal.Location = new System.Drawing.Point(6, 66);
            this.cbSubPal.Name = "cbSubPal";
            this.cbSubPal.Size = new System.Drawing.Size(128, 21);
            this.cbSubPal.TabIndex = 4;
            this.cbSubPal.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "View with subpallete:";
            // 
            // pbPal
            // 
            this.pbPal.Location = new System.Drawing.Point(6, 121);
            this.pbPal.Name = "pbPal";
            this.pbPal.Size = new System.Drawing.Size(128, 128);
            this.pbPal.TabIndex = 2;
            this.pbPal.TabStop = false;
            this.pbPal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPal_MouseClick);
            // 
            // cbVideoNo
            // 
            this.cbVideoNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVideoNo.FormattingEnabled = true;
            this.cbVideoNo.Location = new System.Drawing.Point(6, 26);
            this.cbVideoNo.Name = "cbVideoNo";
            this.cbVideoNo.Size = new System.Drawing.Size(128, 21);
            this.cbVideoNo.TabIndex = 2;
            this.cbVideoNo.SelectedIndexChanged += new System.EventHandler(this.cbVideoNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select video block:";
            // 
            // cbShowNo
            // 
            this.cbShowNo.Location = new System.Drawing.Point(29, 255);
            this.cbShowNo.Name = "cbShowNo";
            this.cbShowNo.Size = new System.Drawing.Size(80, 24);
            this.cbShowNo.TabIndex = 6;
            this.cbShowNo.Text = "Show no";
            this.cbShowNo.UseVisualStyleBackColor = true;
            this.cbShowNo.CheckedChanged += new System.EventHandler(this.cbShowNo_CheckedChanged);
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(3, 308);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(131, 23);
            this.btExport.TabIndex = 7;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(3, 337);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(131, 23);
            this.btImport.TabIndex = 8;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // EditVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 536);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditVideo";
            this.Text = "Video Banks Editor";
            this.Load += new System.EventHandler(this.EditVideo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbVideoNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbPal;
        private System.Windows.Forms.ComboBox cbSubPal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPalleteNo;
        private System.Windows.Forms.CheckBox cbShowNo;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Button btExport;
    }
}