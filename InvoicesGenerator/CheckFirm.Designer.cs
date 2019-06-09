namespace InvoicesGenerator
{
    partial class CheckFirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckFirm));
            this.TextBoxTaxOffice = new System.Windows.Forms.TextBox();
            this.TextBoxTaxNumber = new System.Windows.Forms.TextBox();
            this.TextBoxAddress = new System.Windows.Forms.TextBox();
            this.TextBoxPhone = new System.Windows.Forms.TextBox();
            this.TextBoxOccupation = new System.Windows.Forms.TextBox();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelLoading = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.LabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelConnectionShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelResults = new System.Windows.Forms.Panel();
            this.btnAddToInvoice = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.PanelLoading.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.PanelResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxTaxOffice
            // 
            this.TextBoxTaxOffice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TextBoxTaxOffice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxTaxOffice.Location = new System.Drawing.Point(76, 148);
            this.TextBoxTaxOffice.Name = "TextBoxTaxOffice";
            this.TextBoxTaxOffice.ReadOnly = true;
            this.TextBoxTaxOffice.Size = new System.Drawing.Size(137, 20);
            this.TextBoxTaxOffice.TabIndex = 17;
            this.TextBoxTaxOffice.Visible = false;
            // 
            // TextBoxTaxNumber
            // 
            this.TextBoxTaxNumber.Location = new System.Drawing.Point(96, 35);
            this.TextBoxTaxNumber.Name = "TextBoxTaxNumber";
            this.TextBoxTaxNumber.Size = new System.Drawing.Size(143, 20);
            this.TextBoxTaxNumber.TabIndex = 18;
            this.TextBoxTaxNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxTaxNumber_KeyPress);
            // 
            // TextBoxAddress
            // 
            this.TextBoxAddress.Location = new System.Drawing.Point(84, 44);
            this.TextBoxAddress.Name = "TextBoxAddress";
            this.TextBoxAddress.ReadOnly = true;
            this.TextBoxAddress.Size = new System.Drawing.Size(250, 20);
            this.TextBoxAddress.TabIndex = 16;
            // 
            // TextBoxPhone
            // 
            this.TextBoxPhone.Location = new System.Drawing.Point(76, 122);
            this.TextBoxPhone.Name = "TextBoxPhone";
            this.TextBoxPhone.ReadOnly = true;
            this.TextBoxPhone.Size = new System.Drawing.Size(257, 20);
            this.TextBoxPhone.TabIndex = 19;
            this.TextBoxPhone.Visible = false;
            // 
            // TextBoxOccupation
            // 
            this.TextBoxOccupation.Location = new System.Drawing.Point(76, 70);
            this.TextBoxOccupation.Multiline = true;
            this.TextBoxOccupation.Name = "TextBoxOccupation";
            this.TextBoxOccupation.ReadOnly = true;
            this.TextBoxOccupation.Size = new System.Drawing.Size(258, 46);
            this.TextBoxOccupation.TabIndex = 15;
            this.TextBoxOccupation.Visible = false;
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(84, 19);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.ReadOnly = true;
            this.TextBoxName.Size = new System.Drawing.Size(250, 20);
            this.TextBoxName.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Επάγγελμα";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Δ.Ο.Υ.";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Τηλέφωνα";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Α.Φ.Μ.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Διεύθυνση";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Επωνυμία";
            // 
            // PanelLoading
            // 
            this.PanelLoading.Controls.Add(this.label12);
            this.PanelLoading.Controls.Add(this.progressBar1);
            this.PanelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLoading.Location = new System.Drawing.Point(0, 66);
            this.PanelLoading.Name = "PanelLoading";
            this.PanelLoading.Size = new System.Drawing.Size(363, 137);
            this.PanelLoading.TabIndex = 20;
            this.PanelLoading.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(93, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 26);
            this.label12.TabIndex = 1;
            this.label12.Text = "Γίνεται επικοινωνία με τη VIES\r\nΠαρακαλώ περιμένετε...";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(131, 78);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.Location = new System.Drawing.Point(247, 33);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(103, 23);
            this.ButtonSearch.TabIndex = 21;
            this.ButtonSearch.Text = "Αναζήτηση";
            this.ButtonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.TextBoxTaxOffice);
            this.groupBoxResults.Controls.Add(this.TextBoxAddress);
            this.groupBoxResults.Controls.Add(this.label2);
            this.groupBoxResults.Controls.Add(this.label1);
            this.groupBoxResults.Controls.Add(this.label4);
            this.groupBoxResults.Controls.Add(this.TextBoxPhone);
            this.groupBoxResults.Controls.Add(this.label5);
            this.groupBoxResults.Controls.Add(this.TextBoxOccupation);
            this.groupBoxResults.Controls.Add(this.label6);
            this.groupBoxResults.Controls.Add(this.TextBoxName);
            this.groupBoxResults.Location = new System.Drawing.Point(12, 6);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(340, 70);
            this.groupBoxResults.TabIndex = 22;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Αποτελέσματα";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.cmbCountry);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.TextBoxTaxNumber);
            this.panel1.Controls.Add(this.ButtonSearch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 66);
            this.panel1.TabIndex = 23;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStatus,
            this.tsLabelConnectionShow});
            this.statusStrip.Location = new System.Drawing.Point(0, 181);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(363, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 24;
            this.statusStrip.Text = "statusStrip1";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tsLabelConnectionShow
            // 
            this.tsLabelConnectionShow.Image = global::InvoicesGenerator.Properties.Resources.arrow_refresh;
            this.tsLabelConnectionShow.Name = "tsLabelConnectionShow";
            this.tsLabelConnectionShow.Size = new System.Drawing.Size(312, 17);
            this.tsLabelConnectionShow.Text = "Έλεγχος σύνδεσης με την VIES. Παρακαλώ περιμένετε.";
            // 
            // PanelResults
            // 
            this.PanelResults.Controls.Add(this.btnAddToInvoice);
            this.PanelResults.Controls.Add(this.groupBoxResults);
            this.PanelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelResults.Location = new System.Drawing.Point(0, 66);
            this.PanelResults.Name = "PanelResults";
            this.PanelResults.Size = new System.Drawing.Size(363, 137);
            this.PanelResults.TabIndex = 23;
            // 
            // btnAddToInvoice
            // 
            this.btnAddToInvoice.Image = global::InvoicesGenerator.Properties.Resources.add;
            this.btnAddToInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddToInvoice.Location = new System.Drawing.Point(178, 80);
            this.btnAddToInvoice.Name = "btnAddToInvoice";
            this.btnAddToInvoice.Size = new System.Drawing.Size(174, 23);
            this.btnAddToInvoice.TabIndex = 23;
            this.btnAddToInvoice.Text = "Εισαγωγή στο παραστατικό";
            this.btnAddToInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddToInvoice.UseVisualStyleBackColor = true;
            this.btnAddToInvoice.Visible = false;
            this.btnAddToInvoice.Click += new System.EventHandler(this.btnAddToInvoice_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Χώρα";
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "AT-Austria",
            "BE-Belgium",
            "BG-Bulgaria",
            "CY-Cyprus",
            "CZ-Czech Republic",
            "DE-Germany",
            "DK-Denmark",
            "EE-Estonia",
            "EL-Greece",
            "ES-Spain",
            "FI-Finland",
            "FR-France ",
            "GB-United Kingdom",
            "HR-Croatia",
            "HU-Hungary",
            "IE-Ireland",
            "IT-Italy",
            "LT-Lithuania",
            "LU-Luxembourg",
            "LV-Latvia",
            "MT-Malta",
            "NL-The Netherlands",
            "PL-Poland",
            "PT-Portugal",
            "RO-Romania",
            "SE-Sweden",
            "SI-Slovenia",
            "SK-Slovakia"});
            this.cmbCountry.Location = new System.Drawing.Point(96, 6);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(143, 21);
            this.cmbCountry.TabIndex = 23;
            // 
            // CheckFirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 203);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.PanelResults);
            this.Controls.Add(this.PanelLoading);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CheckFirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Έλεγχος στοιχείων ΑΦΜ";
            this.Load += new System.EventHandler(this.CheckFirm_Load);
            this.PanelLoading.ResumeLayout(false);
            this.PanelLoading.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.PanelResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxTaxOffice;
        private System.Windows.Forms.TextBox TextBoxTaxNumber;
        private System.Windows.Forms.TextBox TextBoxAddress;
        private System.Windows.Forms.TextBox TextBoxPhone;
        private System.Windows.Forms.TextBox TextBoxOccupation;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PanelLoading;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button ButtonSearch;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel LabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelConnectionShow;
        private System.Windows.Forms.Panel PanelResults;
        private System.Windows.Forms.Button btnAddToInvoice;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label7;

    }
}