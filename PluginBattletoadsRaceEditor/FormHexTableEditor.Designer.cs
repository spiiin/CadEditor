namespace BtRaceEditor
{
    partial class FormHexTableEditor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHexTableEditor));
            this.dgvGameObjects = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.ofDialog = new System.Windows.Forms.OpenFileDialog();
            this.sfDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGameObjects)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGameObjects
            // 
            this.dgvGameObjects.AllowUserToOrderColumns = true;
            this.dgvGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGameObjects.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvGameObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGameObjects.Location = new System.Drawing.Point(0, 30);
            this.dgvGameObjects.Name = "dgvGameObjects";
            this.dgvGameObjects.RowTemplate.Height = 24;
            this.dgvGameObjects.Size = new System.Drawing.Size(757, 462);
            this.dgvGameObjects.TabIndex = 2;
            this.dgvGameObjects.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvGameObjects_CellFormatting);
            this.dgvGameObjects.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvGameObjects_CellParsing);
            this.dgvGameObjects.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGameObjects_CellValueChanged);
            this.dgvGameObjects.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvGameObjects_DataError);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(757, 27);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbSave
            // 
            this.tbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbSave.Enabled = false;
            this.tbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbSave.Image")));
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(24, 24);
            this.tbSave.Text = "Save";
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // ofDialog
            // 
            this.ofDialog.FileName = "Battletoads (U) [!].nes";
            // 
            // sfDialog
            // 
            this.sfDialog.FileName = "Battletoads (U) [!].nes";
            // 
            // FormHexTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 492);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvGameObjects);
            this.Name = "FormHexTableEditor";
            this.Text = "Hex Table Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHexTableEditor_FormClosing);
            this.Load += new System.EventHandler(this.FormHexTableEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGameObjects)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGameObjects;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbSave;
        private System.Windows.Forms.OpenFileDialog ofDialog;
        private System.Windows.Forms.SaveFileDialog sfDialog;
    }
}

