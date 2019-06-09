namespace InvoicesGenerator
{
    partial class PrePrint
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
            this.RadioButtonOne = new System.Windows.Forms.RadioButton();
            this.RadioButtonTwo = new System.Windows.Forms.RadioButton();
            this.RadioButtonThree = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxFirstPage = new System.Windows.Forms.TextBox();
            this.TextBoxPageNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tsButtonPrint = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxInvoiceType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RadioButtonOne
            // 
            this.RadioButtonOne.AutoSize = true;
            this.RadioButtonOne.Checked = true;
            this.RadioButtonOne.Location = new System.Drawing.Point(150, 84);
            this.RadioButtonOne.Name = "RadioButtonOne";
            this.RadioButtonOne.Size = new System.Drawing.Size(75, 17);
            this.RadioButtonOne.TabIndex = 0;
            this.RadioButtonOne.TabStop = true;
            this.RadioButtonOne.Text = "Μονότυπο";
            this.RadioButtonOne.UseVisualStyleBackColor = true;
            this.RadioButtonOne.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.RadioButtonOne.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // RadioButtonTwo
            // 
            this.RadioButtonTwo.AutoSize = true;
            this.RadioButtonTwo.Location = new System.Drawing.Point(150, 102);
            this.RadioButtonTwo.Name = "RadioButtonTwo";
            this.RadioButtonTwo.Size = new System.Drawing.Size(76, 17);
            this.RadioButtonTwo.TabIndex = 1;
            this.RadioButtonTwo.Text = "Διπλότυπο";
            this.RadioButtonTwo.UseVisualStyleBackColor = true;
            this.RadioButtonTwo.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.RadioButtonTwo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // RadioButtonThree
            // 
            this.RadioButtonThree.AutoSize = true;
            this.RadioButtonThree.Location = new System.Drawing.Point(150, 120);
            this.RadioButtonThree.Name = "RadioButtonThree";
            this.RadioButtonThree.Size = new System.Drawing.Size(81, 17);
            this.RadioButtonThree.TabIndex = 2;
            this.RadioButtonThree.Text = "Τριπλότυπο";
            this.RadioButtonThree.UseVisualStyleBackColor = true;
            this.RadioButtonThree.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.RadioButtonThree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Αριθμός πρώτης σελίδας";
            // 
            // TextBoxFirstPage
            // 
            this.TextBoxFirstPage.Location = new System.Drawing.Point(150, 32);
            this.TextBoxFirstPage.Name = "TextBoxFirstPage";
            this.TextBoxFirstPage.Size = new System.Drawing.Size(57, 20);
            this.TextBoxFirstPage.TabIndex = 6;
            this.TextBoxFirstPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // TextBoxPageNumber
            // 
            this.TextBoxPageNumber.Location = new System.Drawing.Point(150, 58);
            this.TextBoxPageNumber.Name = "TextBoxPageNumber";
            this.TextBoxPageNumber.Size = new System.Drawing.Size(57, 20);
            this.TextBoxPageNumber.TabIndex = 8;
            this.TextBoxPageNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Πλήθος σελίδων";
            // 
            // tsButtonPrint
            // 
            this.tsButtonPrint.Image = global::InvoicesGenerator.Properties.Resources.printer;
            this.tsButtonPrint.Location = new System.Drawing.Point(81, 144);
            this.tsButtonPrint.Name = "tsButtonPrint";
            this.tsButtonPrint.Size = new System.Drawing.Size(96, 23);
            this.tsButtonPrint.TabIndex = 9;
            this.tsButtonPrint.Text = "Εκτύπωση";
            this.tsButtonPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.tsButtonPrint.UseVisualStyleBackColor = true;
            this.tsButtonPrint.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Τύπος εκτύπωσης";
            // 
            // ComboBoxInvoiceType
            // 
            this.ComboBoxInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxInvoiceType.FormattingEnabled = true;
            this.ComboBoxInvoiceType.Location = new System.Drawing.Point(75, 5);
            this.ComboBoxInvoiceType.Name = "ComboBoxInvoiceType";
            this.ComboBoxInvoiceType.Size = new System.Drawing.Size(156, 21);
            this.ComboBoxInvoiceType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Τύπος Παρ.";
            // 
            // PrePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 174);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboBoxInvoiceType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tsButtonPrint);
            this.Controls.Add(this.TextBoxPageNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxFirstPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadioButtonThree);
            this.Controls.Add(this.RadioButtonTwo);
            this.Controls.Add(this.RadioButtonOne);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PrePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Προεκτύπωση";
            this.Load += new System.EventHandler(this.PrePrint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RadioButtonOne;
        private System.Windows.Forms.RadioButton RadioButtonTwo;
        private System.Windows.Forms.RadioButton RadioButtonThree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxFirstPage;
        private System.Windows.Forms.TextBox TextBoxPageNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button tsButtonPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBoxInvoiceType;
        private System.Windows.Forms.Label label4;
    }
}