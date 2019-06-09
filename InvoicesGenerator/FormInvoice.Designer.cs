namespace InvoicesGenerator
{
    partial class FormInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInvoice));
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.LabelInputAmount = new System.Windows.Forms.Label();
            this.LabelAmount = new System.Windows.Forms.Label();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GroupBoxDescription = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GroupBoxPricing = new System.Windows.Forms.GroupBox();
            this.labelMetricUnit = new System.Windows.Forms.Label();
            this.comboBoxMetricUnit = new System.Windows.Forms.ComboBox();
            this.txtFinalPrice = new System.Windows.Forms.TextBox();
            this.txtVat = new System.Windows.Forms.TextBox();
            this.groupBoxExistingDescriptions = new System.Windows.Forms.GroupBox();
            this.DataGridViewExistingDescription = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBoxDescription.SuspendLayout();
            this.GroupBoxPricing.SuspendLayout();
            this.groupBoxExistingDescriptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewExistingDescription)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(114, 20);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(62, 20);
            this.txtUnitPrice.TabIndex = 0;
            this.txtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice.Click += new System.EventHandler(this.TextBoxAmount_Click);
            this.txtUnitPrice.TextChanged += new System.EventHandler(this.txtUnitPrice_TextChanged);
            this.txtUnitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxDescription_KeyPress);
            this.txtUnitPrice.Leave += new System.EventHandler(this.TextBoxAmount_Leave);
            // 
            // LabelInputAmount
            // 
            this.LabelInputAmount.AutoSize = true;
            this.LabelInputAmount.Location = new System.Drawing.Point(50, 49);
            this.LabelInputAmount.Name = "LabelInputAmount";
            this.LabelInputAmount.Size = new System.Drawing.Size(61, 13);
            this.LabelInputAmount.TabIndex = 16;
            this.LabelInputAmount.Text = "Ποσότητα:";
            // 
            // LabelAmount
            // 
            this.LabelAmount.AutoSize = true;
            this.LabelAmount.Location = new System.Drawing.Point(31, 23);
            this.LabelAmount.Name = "LabelAmount";
            this.LabelAmount.Size = new System.Drawing.Size(80, 13);
            this.LabelAmount.TabIndex = 14;
            this.LabelAmount.Text = "Τιμή Μονάδας:";
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDescription.Location = new System.Drawing.Point(6, 19);
            this.TextBoxDescription.MaxLength = 400;
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.Size = new System.Drawing.Size(311, 113);
            this.TextBoxDescription.TabIndex = 0;
            this.TextBoxDescription.Click += new System.EventHandler(this.TextBoxDescription_Click);
            this.TextBoxDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxDescription_KeyPress);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(114, 43);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(62, 20);
            this.txtQuantity.TabIndex = 1;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.Click += new System.EventHandler(this.TextBoxInputAmount_Click);
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxDescription_KeyPress);
            // 
            // button1
            // 
            this.button1.Image = global::InvoicesGenerator.Properties.Resources.disk;
            this.button1.Location = new System.Drawing.Point(212, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Αποθήκευση";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // GroupBoxDescription
            // 
            this.GroupBoxDescription.Controls.Add(this.TextBoxDescription);
            this.GroupBoxDescription.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxDescription.Name = "GroupBoxDescription";
            this.GroupBoxDescription.Size = new System.Drawing.Size(323, 138);
            this.GroupBoxDescription.TabIndex = 0;
            this.GroupBoxDescription.TabStop = false;
            this.GroupBoxDescription.Text = "Νέα Αιτιολογία";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label8.Location = new System.Drawing.Point(65, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Σύνολο:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label6.Location = new System.Drawing.Point(68, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Φ.Π.Α.:";
            // 
            // GroupBoxPricing
            // 
            this.GroupBoxPricing.Controls.Add(this.labelMetricUnit);
            this.GroupBoxPricing.Controls.Add(this.comboBoxMetricUnit);
            this.GroupBoxPricing.Controls.Add(this.txtFinalPrice);
            this.GroupBoxPricing.Controls.Add(this.txtVat);
            this.GroupBoxPricing.Controls.Add(this.LabelInputAmount);
            this.GroupBoxPricing.Controls.Add(this.txtUnitPrice);
            this.GroupBoxPricing.Controls.Add(this.LabelAmount);
            this.GroupBoxPricing.Controls.Add(this.txtQuantity);
            this.GroupBoxPricing.Controls.Add(this.label8);
            this.GroupBoxPricing.Controls.Add(this.label6);
            this.GroupBoxPricing.Location = new System.Drawing.Point(341, 12);
            this.GroupBoxPricing.Name = "GroupBoxPricing";
            this.GroupBoxPricing.Size = new System.Drawing.Size(181, 138);
            this.GroupBoxPricing.TabIndex = 1;
            this.GroupBoxPricing.TabStop = false;
            this.GroupBoxPricing.Text = "Τιμολόγηση";
            this.GroupBoxPricing.Enter += new System.EventHandler(this.GroupBoxPricing_Enter);
            // 
            // labelMetricUnit
            // 
            this.labelMetricUnit.AutoSize = true;
            this.labelMetricUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.labelMetricUnit.Location = new System.Drawing.Point(7, 114);
            this.labelMetricUnit.Name = "labelMetricUnit";
            this.labelMetricUnit.Size = new System.Drawing.Size(104, 13);
            this.labelMetricUnit.TabIndex = 30;
            this.labelMetricUnit.Text = "Μονάδα Μέτρησης:";
            this.labelMetricUnit.Visible = false;
            // 
            // comboBoxMetricUnit
            // 
            this.comboBoxMetricUnit.FormattingEnabled = true;
            this.comboBoxMetricUnit.Location = new System.Drawing.Point(114, 112);
            this.comboBoxMetricUnit.Name = "comboBoxMetricUnit";
            this.comboBoxMetricUnit.Size = new System.Drawing.Size(62, 21);
            this.comboBoxMetricUnit.TabIndex = 29;
            this.comboBoxMetricUnit.Visible = false;
            // 
            // txtFinalPrice
            // 
            this.txtFinalPrice.Location = new System.Drawing.Point(114, 89);
            this.txtFinalPrice.Name = "txtFinalPrice";
            this.txtFinalPrice.Size = new System.Drawing.Size(62, 20);
            this.txtFinalPrice.TabIndex = 28;
            this.txtFinalPrice.Text = "0 €";
            this.txtFinalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFinalPrice.TextChanged += new System.EventHandler(this.txtFinalPrice_TextChanged);
            // 
            // txtVat
            // 
            this.txtVat.Location = new System.Drawing.Point(114, 66);
            this.txtVat.Name = "txtVat";
            this.txtVat.ReadOnly = true;
            this.txtVat.Size = new System.Drawing.Size(62, 20);
            this.txtVat.TabIndex = 27;
            this.txtVat.Text = "0 €";
            this.txtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxExistingDescriptions
            // 
            this.groupBoxExistingDescriptions.Controls.Add(this.DataGridViewExistingDescription);
            this.groupBoxExistingDescriptions.Location = new System.Drawing.Point(12, 156);
            this.groupBoxExistingDescriptions.Name = "groupBoxExistingDescriptions";
            this.groupBoxExistingDescriptions.Size = new System.Drawing.Size(510, 175);
            this.groupBoxExistingDescriptions.TabIndex = 3;
            this.groupBoxExistingDescriptions.TabStop = false;
            this.groupBoxExistingDescriptions.Text = "Επιλογή απο υπάρχουσα αιτιολογία";
            // 
            // DataGridViewExistingDescription
            // 
            this.DataGridViewExistingDescription.AllowUserToAddRows = false;
            this.DataGridViewExistingDescription.AllowUserToDeleteRows = false;
            this.DataGridViewExistingDescription.AllowUserToOrderColumns = true;
            this.DataGridViewExistingDescription.AllowUserToResizeColumns = false;
            this.DataGridViewExistingDescription.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridViewExistingDescription.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewExistingDescription.BackgroundColor = System.Drawing.Color.White;
            this.DataGridViewExistingDescription.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewExistingDescription.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description});
            this.DataGridViewExistingDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewExistingDescription.Location = new System.Drawing.Point(3, 16);
            this.DataGridViewExistingDescription.Name = "DataGridViewExistingDescription";
            this.DataGridViewExistingDescription.ReadOnly = true;
            this.DataGridViewExistingDescription.RowHeadersVisible = false;
            this.DataGridViewExistingDescription.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewExistingDescription.Size = new System.Drawing.Size(504, 156);
            this.DataGridViewExistingDescription.TabIndex = 9;
            this.DataGridViewExistingDescription.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewExistingDescription_CellDoubleClick);
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "ExistingDescription";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Description.DefaultCellStyle = dataGridViewCellStyle2;
            this.Description.HeaderText = "Αιτιολογία";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // FormInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 372);
            this.Controls.Add(this.groupBoxExistingDescriptions);
            this.Controls.Add(this.GroupBoxPricing);
            this.Controls.Add(this.GroupBoxDescription);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInvoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Αιτιολογία";
            this.Load += new System.EventHandler(this.FormInvoice_Load);
            this.GroupBoxDescription.ResumeLayout(false);
            this.GroupBoxDescription.PerformLayout();
            this.GroupBoxPricing.ResumeLayout(false);
            this.GroupBoxPricing.PerformLayout();
            this.groupBoxExistingDescriptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewExistingDescription)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label LabelInputAmount;
        private System.Windows.Forms.Label LabelAmount;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox TextBoxDescription;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox GroupBoxDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox GroupBoxPricing;
        private System.Windows.Forms.TextBox txtFinalPrice;
        private System.Windows.Forms.TextBox txtVat;
        private System.Windows.Forms.ComboBox comboBoxMetricUnit;
        private System.Windows.Forms.Label labelMetricUnit;
        private System.Windows.Forms.GroupBox groupBoxExistingDescriptions;
        private System.Windows.Forms.DataGridView DataGridViewExistingDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;

    }
}