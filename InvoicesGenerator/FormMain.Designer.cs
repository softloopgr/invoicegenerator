namespace InvoicesGenerator
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.ToolStrip();
            this.ButtonNewExchangeGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.ButtonTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.previewInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllInvoices = new System.Windows.Forms.ToolStripMenuItem();
            this.searchAfm = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.companySeal = new System.Windows.Forms.ToolStripMenuItem();
            this.loginButton = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.GroupBoxSearch = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBoxInvoiceType = new System.Windows.Forms.ComboBox();
            this.TextBoxTaxNumber = new System.Windows.Forms.TextBox();
            this.DateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxCompany = new System.Windows.Forms.TextBox();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.DataGridViewInvoices = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fpa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.εξαγωγήΔεδομένωνToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.εισαγωγήΔεδομένωνToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.LabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            this.GroupBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewInvoices)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonNewExchangeGroup,
            this.toolStripSeparator2,
            this.ButtonHelp,
            this.ButtonTools,
            this.searchAfm,
            this.settingsButton});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(3);
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenu.Size = new System.Drawing.Size(762, 29);
            this.mainMenu.TabIndex = 1;
            // 
            // ButtonNewExchangeGroup
            // 
            this.ButtonNewExchangeGroup.Image = global::InvoicesGenerator.Properties.Resources.add;
            this.ButtonNewExchangeGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonNewExchangeGroup.Name = "ButtonNewExchangeGroup";
            this.ButtonNewExchangeGroup.Size = new System.Drawing.Size(123, 20);
            this.ButtonNewExchangeGroup.Text = "Νέο Παραστατικό";
            this.ButtonNewExchangeGroup.Click += new System.EventHandler(this.ButtonNewInvoiceGroup_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // ButtonHelp
            // 
            this.ButtonHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonHelp.Image = global::InvoicesGenerator.Properties.Resources.help;
            this.ButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonHelp.Name = "ButtonHelp";
            this.ButtonHelp.Size = new System.Drawing.Size(23, 20);
            this.ButtonHelp.Tag = "";
            this.ButtonHelp.Text = "toolStripButton4";
            this.ButtonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
            // 
            // ButtonTools
            // 
            this.ButtonTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewInvoice,
            this.printAllInvoices});
            this.ButtonTools.Image = global::InvoicesGenerator.Properties.Resources.wrench;
            this.ButtonTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonTools.Name = "ButtonTools";
            this.ButtonTools.Size = new System.Drawing.Size(83, 20);
            this.ButtonTools.Text = "Εργαλεία";
            // 
            // previewInvoice
            // 
            this.previewInvoice.Image = global::InvoicesGenerator.Properties.Resources.report_magnify;
            this.previewInvoice.Name = "previewInvoice";
            this.previewInvoice.Size = new System.Drawing.Size(239, 22);
            this.previewInvoice.Text = "Προεκτύπωση Παραστατικών";
            this.previewInvoice.Visible = false;
            this.previewInvoice.Click += new System.EventHandler(this.ButtonPrePrint_Click);
            // 
            // printAllInvoices
            // 
            this.printAllInvoices.Image = global::InvoicesGenerator.Properties.Resources.printer;
            this.printAllInvoices.Name = "printAllInvoices";
            this.printAllInvoices.Size = new System.Drawing.Size(239, 22);
            this.printAllInvoices.Text = "Εκτύπωση Συγκεντρωτικής";
            this.printAllInvoices.Click += new System.EventHandler(this.tsBtnCentralized_Click);
            // 
            // searchAfm
            // 
            this.searchAfm.Image = global::InvoicesGenerator.Properties.Resources.transmit_blue;
            this.searchAfm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchAfm.Name = "searchAfm";
            this.searchAfm.Size = new System.Drawing.Size(118, 20);
            this.searchAfm.Text = "Αναζήτηση ΑΦΜ";
            this.searchAfm.Click += new System.EventHandler(this.ButtonTaxNumberCheck_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companySeal,
            this.loginButton,
            this.settingsToolStripMenuItem});
            this.settingsButton.Image = global::InvoicesGenerator.Properties.Resources.cog;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(88, 20);
            this.settingsButton.Text = "Ρυθμίσεις";
            // 
            // companySeal
            // 
            this.companySeal.Image = global::InvoicesGenerator.Properties.Resources.user_suit;
            this.companySeal.Name = "companySeal";
            this.companySeal.Size = new System.Drawing.Size(211, 22);
            this.companySeal.Text = "Εταιρικά Στοιχεια";
            this.companySeal.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // loginButton
            // 
            this.loginButton.Image = global::InvoicesGenerator.Properties.Resources.bullet_key;
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(211, 22);
            this.loginButton.Text = "Ρυθμίσεις Ασφαλείας";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::InvoicesGenerator.Properties.Resources.bricks;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.settingsToolStripMenuItem.Text = "Ρυθμίσεις Παραστατικών";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabel2.Text = "10/05/2012 13:33";
            // 
            // GroupBoxSearch
            // 
            this.GroupBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxSearch.Controls.Add(this.label5);
            this.GroupBoxSearch.Controls.Add(this.ComboBoxInvoiceType);
            this.GroupBoxSearch.Controls.Add(this.TextBoxTaxNumber);
            this.GroupBoxSearch.Controls.Add(this.DateTimePickerTo);
            this.GroupBoxSearch.Controls.Add(this.label4);
            this.GroupBoxSearch.Controls.Add(this.label3);
            this.GroupBoxSearch.Controls.Add(this.DateTimePickerFrom);
            this.GroupBoxSearch.Controls.Add(this.label2);
            this.GroupBoxSearch.Controls.Add(this.label1);
            this.GroupBoxSearch.Controls.Add(this.TextBoxCompany);
            this.GroupBoxSearch.Controls.Add(this.ButtonSearch);
            this.GroupBoxSearch.Location = new System.Drawing.Point(585, 32);
            this.GroupBoxSearch.Name = "GroupBoxSearch";
            this.GroupBoxSearch.Size = new System.Drawing.Size(172, 181);
            this.GroupBoxSearch.TabIndex = 6;
            this.GroupBoxSearch.TabStop = false;
            this.GroupBoxSearch.Text = "Αναζήτηση";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Τύπος Παρ.";
            // 
            // ComboBoxInvoiceType
            // 
            this.ComboBoxInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxInvoiceType.FormattingEnabled = true;
            this.ComboBoxInvoiceType.Location = new System.Drawing.Point(72, 123);
            this.ComboBoxInvoiceType.Name = "ComboBoxInvoiceType";
            this.ComboBoxInvoiceType.Size = new System.Drawing.Size(94, 21);
            this.ComboBoxInvoiceType.TabIndex = 13;
            // 
            // TextBoxTaxNumber
            // 
            this.TextBoxTaxNumber.Location = new System.Drawing.Point(72, 97);
            this.TextBoxTaxNumber.Name = "TextBoxTaxNumber";
            this.TextBoxTaxNumber.Size = new System.Drawing.Size(94, 20);
            this.TextBoxTaxNumber.TabIndex = 11;
            // 
            // DateTimePickerTo
            // 
            this.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerTo.Location = new System.Drawing.Point(72, 45);
            this.DateTimePickerTo.Name = "DateTimePickerTo";
            this.DateTimePickerTo.Size = new System.Drawing.Size(94, 20);
            this.DateTimePickerTo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Έως";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Επωνυμία";
            // 
            // DateTimePickerFrom
            // 
            this.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePickerFrom.Location = new System.Drawing.Point(72, 19);
            this.DateTimePickerFrom.Name = "DateTimePickerFrom";
            this.DateTimePickerFrom.Size = new System.Drawing.Size(94, 20);
            this.DateTimePickerFrom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "ΑΦΜ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Από";
            // 
            // TextBoxCompany
            // 
            this.TextBoxCompany.Location = new System.Drawing.Point(72, 71);
            this.TextBoxCompany.Name = "TextBoxCompany";
            this.TextBoxCompany.Size = new System.Drawing.Size(94, 20);
            this.TextBoxCompany.TabIndex = 1;
            this.TextBoxCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxSearch_KeyPress);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSearch.Image")));
            this.ButtonSearch.Location = new System.Drawing.Point(35, 152);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(103, 23);
            this.ButtonSearch.TabIndex = 0;
            this.ButtonSearch.Text = "Αναζήτηση";
            this.ButtonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "calendar.png");
            this.imageList.Images.SetKeyName(1, "application_side_list.png");
            // 
            // DataGridViewInvoices
            // 
            this.DataGridViewInvoices.AllowUserToAddRows = false;
            this.DataGridViewInvoices.AllowUserToDeleteRows = false;
            this.DataGridViewInvoices.AllowUserToOrderColumns = true;
            this.DataGridViewInvoices.AllowUserToResizeColumns = false;
            this.DataGridViewInvoices.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridViewInvoices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewInvoices.BackgroundColor = System.Drawing.Color.White;
            this.DataGridViewInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.InvoiceNumber,
            this.InvoiceDate,
            this.afm,
            this.customer_name,
            this.amount,
            this.fpa,
            this.total_amount});
            this.DataGridViewInvoices.Location = new System.Drawing.Point(0, 32);
            this.DataGridViewInvoices.Name = "DataGridViewInvoices";
            this.DataGridViewInvoices.ReadOnly = true;
            this.DataGridViewInvoices.RowHeadersVisible = false;
            this.DataGridViewInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewInvoices.Size = new System.Drawing.Size(579, 428);
            this.DataGridViewInvoices.TabIndex = 8;
            this.DataGridViewInvoices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewInvoices_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // InvoiceNumber
            // 
            this.InvoiceNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InvoiceNumber.DataPropertyName = "Number";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InvoiceNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.InvoiceNumber.HeaderText = "A/A";
            this.InvoiceNumber.Name = "InvoiceNumber";
            this.InvoiceNumber.ReadOnly = true;
            this.InvoiceNumber.Width = 51;
            // 
            // InvoiceDate
            // 
            this.InvoiceDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InvoiceDate.DataPropertyName = "Date";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.InvoiceDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.InvoiceDate.HeaderText = "Ημ/νία";
            this.InvoiceDate.Name = "InvoiceDate";
            this.InvoiceDate.ReadOnly = true;
            this.InvoiceDate.Width = 66;
            // 
            // afm
            // 
            this.afm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.afm.DataPropertyName = "Customer_taxnumber";
            this.afm.HeaderText = "ΑΦΜ";
            this.afm.Name = "afm";
            this.afm.ReadOnly = true;
            this.afm.Width = 57;
            // 
            // customer_name
            // 
            this.customer_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "Πελάτης";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.amount.DataPropertyName = "Subtotal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.amount.DefaultCellStyle = dataGridViewCellStyle4;
            this.amount.HeaderText = "Ποσό";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 58;
            // 
            // fpa
            // 
            this.fpa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fpa.DataPropertyName = "Tax";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            this.fpa.DefaultCellStyle = dataGridViewCellStyle5;
            this.fpa.HeaderText = "ΦΠΑ";
            this.fpa.Name = "fpa";
            this.fpa.ReadOnly = true;
            this.fpa.Width = 56;
            // 
            // total_amount
            // 
            this.total_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.total_amount.DataPropertyName = "totalVF";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.total_amount.DefaultCellStyle = dataGridViewCellStyle6;
            this.total_amount.HeaderText = "Σύνολο";
            this.total_amount.Name = "total_amount";
            this.total_amount.ReadOnly = true;
            this.total_amount.Width = 68;
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(170, 20);
            this.toolStripButton8.Text = "Δημιουργία Νέου Πακέτου";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.εξαγωγήΔεδομένωνToolStripMenuItem,
            this.εισαγωγήΔεδομένωνToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(83, 20);
            this.toolStripDropDownButton1.Text = "Εργαλεία";
            // 
            // εξαγωγήΔεδομένωνToolStripMenuItem
            // 
            this.εξαγωγήΔεδομένωνToolStripMenuItem.Name = "εξαγωγήΔεδομένωνToolStripMenuItem";
            this.εξαγωγήΔεδομένωνToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.εξαγωγήΔεδομένωνToolStripMenuItem.Text = "Εξαγωγή Δεδομένων";
            // 
            // εισαγωγήΔεδομένωνToolStripMenuItem
            // 
            this.εισαγωγήΔεδομένωνToolStripMenuItem.Name = "εισαγωγήΔεδομένωνToolStripMenuItem";
            this.εισαγωγήΔεδομένωνToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.εισαγωγήΔεδομένωνToolStripMenuItem.Text = "Εισαγωγή Δεδομένων";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(72, 20);
            this.toolStripButton2.Text = "Χρήστες";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStatus,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 463);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(762, 22);
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "statusStrip1";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(747, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 485);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.DataGridViewInvoices);
            this.Controls.Add(this.GroupBoxSearch);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InvoiceGenerator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.GroupBoxSearch.ResumeLayout(false);
            this.GroupBoxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewInvoices)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainMenu;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.GroupBox GroupBoxSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox TextBoxCompany;
        private System.Windows.Forms.Button ButtonSearch;
        private System.Windows.Forms.DateTimePicker DateTimePickerFrom;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem εξαγωγήΔεδομένωνToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem εισαγωγήΔεδομένωνToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton ButtonNewExchangeGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ButtonHelp;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.DataGridView DataGridViewInvoices;
        private System.Windows.Forms.TextBox TextBoxTaxNumber;
        private System.Windows.Forms.DateTimePicker DateTimePickerTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel LabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton searchAfm;
        private System.Windows.Forms.ComboBox ComboBoxInvoiceType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripDropDownButton settingsButton;
        private System.Windows.Forms.ToolStripMenuItem companySeal;
        private System.Windows.Forms.ToolStripMenuItem loginButton;
        private System.Windows.Forms.ToolStripDropDownButton ButtonTools;
        private System.Windows.Forms.ToolStripMenuItem previewInvoice;
        private System.Windows.Forms.ToolStripMenuItem printAllInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn afm;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn fpa;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_amount;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;

    }
}

