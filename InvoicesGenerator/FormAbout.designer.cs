namespace InvoicesGenerator
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LinkLabelEmail = new System.Windows.Forms.LinkLabel();
            this.LabelPhone = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GroupBoxLicense = new System.Windows.Forms.GroupBox();
            this.LabelLicense = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.GroupBoxLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Έκδοση";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Τηλέφωνο";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Email";
            // 
            // LinkLabelEmail
            // 
            this.LinkLabelEmail.AutoSize = true;
            this.LinkLabelEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LinkLabelEmail.Location = new System.Drawing.Point(153, 32);
            this.LinkLabelEmail.Name = "LinkLabelEmail";
            this.LinkLabelEmail.Size = new System.Drawing.Size(122, 13);
            this.LinkLabelEmail.TabIndex = 30;
            this.LinkLabelEmail.TabStop = true;
            this.LinkLabelEmail.Text = "contact@softloop.gr";
            // 
            // LabelPhone
            // 
            this.LabelPhone.AutoSize = true;
            this.LabelPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelPhone.Location = new System.Drawing.Point(153, 52);
            this.LabelPhone.Name = "LabelPhone";
            this.LabelPhone.Size = new System.Drawing.Size(85, 13);
            this.LabelPhone.TabIndex = 31;
            this.LabelPhone.Text = "213.026.7712";
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelVersion.Location = new System.Drawing.Point(153, 12);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(69, 13);
            this.LabelVersion.TabIndex = 32;
            this.LabelVersion.Text = "Version ##";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InvoicesGenerator.Properties.Resources.softloop;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // GroupBoxLicense
            // 
            this.GroupBoxLicense.Controls.Add(this.LabelLicense);
            this.GroupBoxLicense.Location = new System.Drawing.Point(12, 78);
            this.GroupBoxLicense.Name = "GroupBoxLicense";
            this.GroupBoxLicense.Size = new System.Drawing.Size(263, 49);
            this.GroupBoxLicense.TabIndex = 35;
            this.GroupBoxLicense.TabStop = false;
            this.GroupBoxLicense.Text = "Άδεια Χρήσης";
            // 
            // LabelLicense
            // 
            this.LabelLicense.AutoSize = true;
            this.LabelLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LabelLicense.Location = new System.Drawing.Point(8, 22);
            this.LabelLicense.Name = "LabelLicense";
            this.LabelLicense.Size = new System.Drawing.Size(31, 13);
            this.LabelLicense.TabIndex = 36;
            this.LabelLicense.Text = "###";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 139);
            this.Controls.Add(this.GroupBoxLicense);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.LabelPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LinkLabelEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Πληροφορίες Εφαρμογής";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.GroupBoxLicense.ResumeLayout(false);
            this.GroupBoxLicense.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel LinkLabelEmail;
        private System.Windows.Forms.Label LabelPhone;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox GroupBoxLicense;
        private System.Windows.Forms.Label LabelLicense;

    }
}
