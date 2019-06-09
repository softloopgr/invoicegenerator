namespace ExchangesGenerator
{
    partial class FormExchange
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
            this.label5 = new System.Windows.Forms.Label();
            this.TextBoxAmount = new System.Windows.Forms.TextBox();
            this.LabelEndDate = new System.Windows.Forms.Label();
            this.LabelAmount = new System.Windows.Forms.Label();
            this.DateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.checkBoxHold = new System.Windows.Forms.CheckBox();
            this.groupBoxManagment = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxInformation.SuspendLayout();
            this.groupBoxManagment.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "€";
            // 
            // TextBoxAmount
            // 
            this.TextBoxAmount.Location = new System.Drawing.Point(54, 45);
            this.TextBoxAmount.Name = "TextBoxAmount";
            this.TextBoxAmount.Size = new System.Drawing.Size(52, 20);
            this.TextBoxAmount.TabIndex = 19;
            // 
            // LabelEndDate
            // 
            this.LabelEndDate.AutoSize = true;
            this.LabelEndDate.Location = new System.Drawing.Point(14, 21);
            this.LabelEndDate.Name = "LabelEndDate";
            this.LabelEndDate.Size = new System.Drawing.Size(31, 13);
            this.LabelEndDate.TabIndex = 16;
            this.LabelEndDate.Text = "Λήξη";
            // 
            // LabelAmount
            // 
            this.LabelAmount.AutoSize = true;
            this.LabelAmount.Location = new System.Drawing.Point(15, 48);
            this.LabelAmount.Name = "LabelAmount";
            this.LabelAmount.Size = new System.Drawing.Size(33, 13);
            this.LabelAmount.TabIndex = 14;
            this.LabelAmount.Text = "Ποσό";
            // 
            // DateTimePickerEndDate
            // 
            this.DateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerEndDate.Location = new System.Drawing.Point(54, 19);
            this.DateTimePickerEndDate.Name = "DateTimePickerEndDate";
            this.DateTimePickerEndDate.Size = new System.Drawing.Size(96, 20);
            this.DateTimePickerEndDate.TabIndex = 23;
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Controls.Add(this.LabelEndDate);
            this.groupBoxInformation.Controls.Add(this.checkBoxHold);
            this.groupBoxInformation.Controls.Add(this.label5);
            this.groupBoxInformation.Controls.Add(this.LabelAmount);
            this.groupBoxInformation.Controls.Add(this.TextBoxAmount);
            this.groupBoxInformation.Controls.Add(this.DateTimePickerEndDate);
            this.groupBoxInformation.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(159, 113);
            this.groupBoxInformation.TabIndex = 26;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Πληροφορίες";
            // 
            // checkBoxHold
            // 
            this.checkBoxHold.AutoSize = true;
            this.checkBoxHold.Enabled = false;
            this.checkBoxHold.Location = new System.Drawing.Point(54, 71);
            this.checkBoxHold.Name = "checkBoxHold";
            this.checkBoxHold.Size = new System.Drawing.Size(96, 17);
            this.checkBoxHold.TabIndex = 25;
            this.checkBoxHold.Text = "Παρακράτηση";
            this.checkBoxHold.UseVisualStyleBackColor = true;
            // 
            // groupBoxManagment
            // 
            this.groupBoxManagment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxManagment.Controls.Add(this.buttonDelete);
            this.groupBoxManagment.Controls.Add(this.buttonCancel);
            this.groupBoxManagment.Controls.Add(this.buttonSave);
            this.groupBoxManagment.Location = new System.Drawing.Point(177, 12);
            this.groupBoxManagment.Name = "groupBoxManagment";
            this.groupBoxManagment.Size = new System.Drawing.Size(126, 113);
            this.groupBoxManagment.TabIndex = 35;
            this.groupBoxManagment.TabStop = false;
            this.groupBoxManagment.Text = "Διαχείριση";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Image = global::ExchangesGenerator.Properties.Resources.delete;
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.Location = new System.Drawing.Point(6, 77);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(114, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Tag = "200";
            this.buttonDelete.Text = "Διαγραφή";
            this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Image = global::ExchangesGenerator.Properties.Resources.cancel;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(6, 48);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Tag = "200";
            this.buttonCancel.Text = "Ακύρωση";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Image = global::ExchangesGenerator.Properties.Resources.disk;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(6, 19);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Αποθήκευση";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // FormExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 138);
            this.Controls.Add(this.groupBoxManagment);
            this.Controls.Add(this.groupBoxInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExchange";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Συναλλαγματική";
            this.Load += new System.EventHandler(this.FormExchange_Load);
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxInformation.PerformLayout();
            this.groupBoxManagment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxAmount;
        private System.Windows.Forms.Label LabelEndDate;
        private System.Windows.Forms.Label LabelAmount;
        private System.Windows.Forms.DateTimePicker DateTimePickerEndDate;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.GroupBox groupBoxManagment;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxHold;

    }
}