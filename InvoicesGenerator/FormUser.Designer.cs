namespace InvoicesGenerator
{
    partial class FormUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUser));
            this.groupBoxUserStats = new System.Windows.Forms.GroupBox();
            this.radioButtonInactive = new System.Windows.Forms.RadioButton();
            this.radioButtonActive = new System.Windows.Forms.RadioButton();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.labelDateID = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxUserInfo = new System.Windows.Forms.GroupBox();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.editMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.labelAppInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelError = new System.Windows.Forms.ToolStripLabel();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSurname = new System.Windows.Forms.Label();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.groupBoxUserStats.SuspendLayout();
            this.groupBoxUserInfo.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUserStats
            // 
            this.groupBoxUserStats.Controls.Add(this.radioButtonInactive);
            this.groupBoxUserStats.Controls.Add(this.radioButtonActive);
            this.groupBoxUserStats.Controls.Add(this.textBoxID);
            this.groupBoxUserStats.Controls.Add(this.labelDateID);
            this.groupBoxUserStats.Controls.Add(this.labelStatus);
            this.groupBoxUserStats.Location = new System.Drawing.Point(273, 12);
            this.groupBoxUserStats.Name = "groupBoxUserStats";
            this.groupBoxUserStats.Size = new System.Drawing.Size(255, 101);
            this.groupBoxUserStats.TabIndex = 0;
            this.groupBoxUserStats.TabStop = false;
            this.groupBoxUserStats.Text = "Πληροφορίες";
            // 
            // radioButtonInactive
            // 
            this.radioButtonInactive.AutoSize = true;
            this.radioButtonInactive.Location = new System.Drawing.Point(164, 46);
            this.radioButtonInactive.Name = "radioButtonInactive";
            this.radioButtonInactive.Size = new System.Drawing.Size(80, 17);
            this.radioButtonInactive.TabIndex = 7;
            this.radioButtonInactive.TabStop = true;
            this.radioButtonInactive.Text = "Ανενεργός";
            this.radioButtonInactive.UseVisualStyleBackColor = true;
            // 
            // radioButtonActive
            // 
            this.radioButtonActive.AutoSize = true;
            this.radioButtonActive.Location = new System.Drawing.Point(81, 46);
            this.radioButtonActive.Name = "radioButtonActive";
            this.radioButtonActive.Size = new System.Drawing.Size(67, 17);
            this.radioButtonActive.TabIndex = 6;
            this.radioButtonActive.TabStop = true;
            this.radioButtonActive.Text = "Ενεργός";
            this.radioButtonActive.UseVisualStyleBackColor = true;
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxID.Location = new System.Drawing.Point(81, 19);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(168, 20);
            this.textBoxID.TabIndex = 9;
            // 
            // labelDateID
            // 
            this.labelDateID.AutoSize = true;
            this.labelDateID.Location = new System.Drawing.Point(19, 22);
            this.labelDateID.Name = "labelDateID";
            this.labelDateID.Size = new System.Drawing.Size(59, 13);
            this.labelDateID.TabIndex = 8;
            this.labelDateID.Text = "ID Χρήστη";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(9, 48);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(66, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Κατάσταση";
            // 
            // groupBoxUserInfo
            // 
            this.groupBoxUserInfo.Controls.Add(this.textBoxSurname);
            this.groupBoxUserInfo.Controls.Add(this.labelSurname);
            this.groupBoxUserInfo.Controls.Add(this.comboBoxLevel);
            this.groupBoxUserInfo.Controls.Add(this.labelType);
            this.groupBoxUserInfo.Controls.Add(this.textBoxPassword);
            this.groupBoxUserInfo.Controls.Add(this.labelPassword);
            this.groupBoxUserInfo.Controls.Add(this.textBoxUsername);
            this.groupBoxUserInfo.Controls.Add(this.labelUsername);
            this.groupBoxUserInfo.Controls.Add(this.textBoxName);
            this.groupBoxUserInfo.Controls.Add(this.labelName);
            this.groupBoxUserInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxUserInfo.Name = "groupBoxUserInfo";
            this.groupBoxUserInfo.Size = new System.Drawing.Size(255, 155);
            this.groupBoxUserInfo.TabIndex = 1;
            this.groupBoxUserInfo.TabStop = false;
            this.groupBoxUserInfo.Text = "Στοιχεία Χρήστη";
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Location = new System.Drawing.Point(81, 123);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(168, 21);
            this.comboBoxLevel.TabIndex = 5;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(38, 126);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(37, 13);
            this.labelType.TabIndex = 8;
            this.labelType.Text = "Τύπος";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(81, 97);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(168, 20);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.Enter += new System.EventHandler(this.textBoxPassword_Enter);
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(28, 100);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(47, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Κωδικός";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUsername.Location = new System.Drawing.Point(81, 71);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(168, 20);
            this.textBoxUsername.TabIndex = 3;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(20, 74);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 4;
            this.labelUsername.Text = "Username";
            // 
            // editMenu
            // 
            this.editMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.editMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonDelete});
            this.editMenu.Image = ((System.Drawing.Image)(resources.GetObject("editMenu.Image")));
            this.editMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(29, 22);
            this.editMenu.Text = "toolStripDropDownButton1";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(128, 22);
            this.buttonDelete.Text = "Διαγραφή";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonSave
            // 
            this.buttonSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 22);
            this.buttonSave.Text = "Αποθήκευση";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelAppInfo
            // 
            this.labelAppInfo.Name = "labelAppInfo";
            this.labelAppInfo.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripLabelError
            // 
            this.toolStripLabelError.Name = "toolStripLabelError";
            this.toolStripLabelError.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMenu,
            this.toolStripSeparator2,
            this.buttonSave,
            this.labelAppInfo,
            this.toolStripLabelError});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 183);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenu.Size = new System.Drawing.Size(536, 25);
            this.toolStripMenu.TabIndex = 21;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(34, 22);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Όνομα";
            // 
            // labelSurname
            // 
            this.labelSurname.AutoSize = true;
            this.labelSurname.Location = new System.Drawing.Point(24, 48);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(51, 13);
            this.labelSurname.TabIndex = 10;
            this.labelSurname.Text = "Επώνυμο";
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSurname.Location = new System.Drawing.Point(81, 45);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(168, 20);
            this.textBoxSurname.TabIndex = 2;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(81, 19);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(168, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 208);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.groupBoxUserInfo);
            this.Controls.Add(this.groupBoxUserStats);
            this.Name = "FormUser";
            this.Text = "Καρτέλα Χρήστη";
            this.Load += new System.EventHandler(this.FormUser_Load);
            this.groupBoxUserStats.ResumeLayout(false);
            this.groupBoxUserStats.PerformLayout();
            this.groupBoxUserInfo.ResumeLayout(false);
            this.groupBoxUserInfo.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUserStats;
        private System.Windows.Forms.GroupBox groupBoxUserInfo;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label labelDateID;
        private System.Windows.Forms.RadioButton radioButtonInactive;
        private System.Windows.Forms.RadioButton radioButtonActive;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ToolStripDropDownButton editMenu;
        private System.Windows.Forms.ToolStripMenuItem buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripLabel labelAppInfo;
        private System.Windows.Forms.ToolStripLabel toolStripLabelError;
        private System.Windows.Forms.ToolStrip toolStripMenu;
    }
}