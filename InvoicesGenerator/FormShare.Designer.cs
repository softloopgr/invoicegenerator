namespace InvoicesGenerator
{
    partial class FormShare
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.TextBoxTo = new System.Windows.Forms.TextBox();
            this.TextBoxFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBoxBody = new System.Windows.Forms.TextBox();
            this.buttonShare = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tsEmailSendMessage = new System.Windows.Forms.Label();
            this.PanelLoading = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.PanelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxSubject);
            this.groupBox1.Controls.Add(this.TextBoxTo);
            this.groupBox1.Controls.Add(this.TextBoxFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Στοιχεία email";
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(81, 85);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(255, 20);
            this.TextBoxSubject.TabIndex = 2;
            this.TextBoxSubject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxFrom_KeyPress);
            // 
            // TextBoxTo
            // 
            this.TextBoxTo.Location = new System.Drawing.Point(79, 45);
            this.TextBoxTo.Multiline = true;
            this.TextBoxTo.Name = "TextBoxTo";
            this.TextBoxTo.Size = new System.Drawing.Size(257, 20);
            this.TextBoxTo.TabIndex = 1;
            this.TextBoxTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxFrom_KeyPress);
            // 
            // TextBoxFrom
            // 
            this.TextBoxFrom.Location = new System.Drawing.Point(79, 19);
            this.TextBoxFrom.Name = "TextBoxFrom";
            this.TextBoxFrom.Size = new System.Drawing.Size(257, 20);
            this.TextBoxFrom.TabIndex = 0;
            this.TextBoxFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxFrom_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Θέμα:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Πρός:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Από:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(79, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(257, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Πολλαπλά email χωρισμένα με κόμμα";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxBody
            // 
            this.TextBoxBody.Location = new System.Drawing.Point(8, 19);
            this.TextBoxBody.Multiline = true;
            this.TextBoxBody.Name = "TextBoxBody";
            this.TextBoxBody.Size = new System.Drawing.Size(328, 92);
            this.TextBoxBody.TabIndex = 0;
            this.TextBoxBody.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxFrom_KeyPress);
            // 
            // buttonShare
            // 
            this.buttonShare.Image = global::InvoicesGenerator.Properties.Resources.email;
            this.buttonShare.Location = new System.Drawing.Point(225, 256);
            this.buttonShare.Name = "buttonShare";
            this.buttonShare.Size = new System.Drawing.Size(129, 23);
            this.buttonShare.TabIndex = 3;
            this.buttonShare.Text = "Αποστολή";
            this.buttonShare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonShare.UseVisualStyleBackColor = true;
            this.buttonShare.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextBoxBody);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 117);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Συνοδευτικό μήνυμα";
            // 
            // tsEmailSendMessage
            // 
            this.tsEmailSendMessage.Location = new System.Drawing.Point(17, 260);
            this.tsEmailSendMessage.Name = "tsEmailSendMessage";
            this.tsEmailSendMessage.Size = new System.Drawing.Size(202, 18);
            this.tsEmailSendMessage.TabIndex = 6;
            // 
            // PanelLoading
            // 
            this.PanelLoading.Controls.Add(this.label12);
            this.PanelLoading.Controls.Add(this.progressBar1);
            this.PanelLoading.Location = new System.Drawing.Point(12, 12);
            this.PanelLoading.Name = "PanelLoading";
            this.PanelLoading.Size = new System.Drawing.Size(342, 238);
            this.PanelLoading.TabIndex = 40;
            this.PanelLoading.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(94, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(157, 26);
            this.label12.TabIndex = 1;
            this.label12.Text = "Γίνεται αποστολή μηνύματος \r\nΠαρακαλώ περιμένετε...";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(114, 125);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(106, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // FormShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 291);
            this.Controls.Add(this.tsEmailSendMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonShare);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PanelLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormShare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Αποστολή με email";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.PanelLoading.ResumeLayout(false);
            this.PanelLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TextBoxBody;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.TextBox TextBoxTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonShare;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label tsEmailSendMessage;
        private System.Windows.Forms.Panel PanelLoading;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}