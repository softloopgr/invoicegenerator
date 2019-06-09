namespace InvoicesGenerator
{
    partial class FormWebViewPrint
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
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonSaveToDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUniqueCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 25);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.Size = new System.Drawing.Size(584, 737);
            this.WebBrowser.TabIndex = 3;
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.Image = global::InvoicesGenerator.Properties.Resources.printer;
            this.tsBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(85, 22);
            this.tsBtnPrint.Text = "Εκτύπωση";
            this.tsBtnPrint.Click += new System.EventHandler(this.TsBtnPrintClick);
            // 
            // tsSeperator
            // 
            this.tsSeperator.Name = "tsSeperator";
            this.tsSeperator.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonSaveToDoc
            // 
            this.ButtonSaveToDoc.Image = global::InvoicesGenerator.Properties.Resources.page_word;
            this.ButtonSaveToDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSaveToDoc.Name = "ButtonSaveToDoc";
            this.ButtonSaveToDoc.Size = new System.Drawing.Size(122, 22);
            this.ButtonSaveToDoc.Text = "Εξαγωγή σε Word";
            this.ButtonSaveToDoc.Click += new System.EventHandler(this.TsBtnSaveToDiskClick);
            // 
            // toolStripTop
            // 
            this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrint,
            this.tsSeperator,
            this.ButtonSaveToDoc});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(584, 25);
            this.toolStripTop.TabIndex = 4;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStatus,
            this.tsLblMessage,
            this.lblUniqueCode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 740);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 39;
            this.statusStrip1.Text = "StatusStripInformation";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tsLblMessage
            // 
            this.tsLblMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsLblMessage.Name = "tsLblMessage";
            this.tsLblMessage.Size = new System.Drawing.Size(538, 17);
            this.tsLblMessage.Spring = true;
            this.tsLblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUniqueCode
            // 
            this.lblUniqueCode.Name = "lblUniqueCode";
            this.lblUniqueCode.Size = new System.Drawing.Size(0, 17);
            // 
            // FormWebViewPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 762);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.WebBrowser);
            this.Controls.Add(this.toolStripTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormWebViewPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Εξαγωγή Παραστατικού";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowser;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripSeparator tsSeperator;
        private System.Windows.Forms.ToolStripButton ButtonSaveToDoc;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsLblMessage;
        private System.Windows.Forms.ToolStripStatusLabel lblUniqueCode;
    }
}