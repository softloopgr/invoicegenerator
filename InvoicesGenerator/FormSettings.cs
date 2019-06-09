using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using InvoicesGenerator.Properties;

namespace InvoicesGenerator
{
    public partial class FormSettings : Form
    {
        public bool _isLoggedIn = false;

        public FormSettings(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }

        public FormSettings(bool isLoggedIn)
        {
            InitializeComponent();

            _isLoggedIn = isLoggedIn;
            if (isLoggedIn)
            {
                UserControl.InitControls(this);
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //if (_isLoggedIn)
            //{
            //    //this.loadUsers();
            //    loadSaveTab();
            //    tabControlSettings.TabPages.RemoveByKey("tabUpdate");
            //    tabControlSettings.TabPages.RemoveByKey("tabImportExport");
            //    tabControlSettings.TabPages.RemoveByKey("tabUsers");
            //    tabControlSettings.TabPages.RemoveByKey("tabAdministration");
            //}
            //else
            //{
            //    tabControlSettings.TabPages.RemoveByKey("tabUpdate");
            //    tabControlSettings.TabPages.RemoveByKey("tabImportExport");
            //    tabControlSettings.TabPages.RemoveByKey("tabUsers");
            //}

            tabControlSettings.TabPages.RemoveByKey("tabUpdate");
            tabControlSettings.TabPages.RemoveByKey("tabUsers");
            tabControlSettings.TabPages.RemoveByKey("tabAdministration");

            loadSaveTab();
            this.loadAdminTab();
        }

        #region UpdateTab
        private void ButtonUpdateApplication_Click(object sender, EventArgs e)
        {
            SoftloopTools.updateApplication();
        }
        #endregion

        #region UsersTab
        private string error = string.Empty;
        private SortableBindingList<User> _users = new SortableBindingList<User>();
        public void loadUsers()
        {
            dataGridViewUsers.AutoGenerateColumns = false;
            _users = new SortableBindingList<User>(User.getUsers());
            if (_users.Count != 0)
            {
                dataGridViewUsers.DataSource = _users;
            }
        }
        private void dataGridViewUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int UserID = (int)dataGridViewUsers.Rows[e.RowIndex].Cells["ID"].Value;
            using (FormUser frm = new FormUser(UserID))
            {
                frm.ShowDialog(this);
            }
        }
        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            using (FormUser frm = new FormUser(-1))
            {
                frm.ShowDialog(this);
            }
        }
        #endregion

        #region AdminTab
        private void TextBoxAdminPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                SimpleAES aes = new SimpleAES();
                if (TextBoxAdminPassword.Text == aes.DecryptString(Properties.Settings.Default.app_adminPassword))
                {
                    panelAdminLogin.Visible = false;
                    panelAdminPanel.Visible = true;
                }
                else
                {
                    labelAdminPassword.Text = "Λάθος Κωδικός";
                    labelAdminPassword.ForeColor = Color.Red;
                }
            }
            else
            {
                labelAdminPassword.Text = "Κωδικός Διαχειριστή";
                labelAdminPassword.ForeColor = Color.Black;
            }
        }
        private void loadAdminTab()
        {
            textBoxBackupPath.Text = Settings.Default.path_backupPath;
            textBoxLogPath.Text = Settings.Default.path_logPath;
            checkBoxForceRebuild.Checked = Settings.Default.app_forceRebuildUserControls;
            textBoxDBDataSource.Text = Settings.Default.db_dataSource;
            textBoxDBPassword.Text = Settings.Default.db_pass;
        }
        private void ButtonAdminSave_Click(object sender, EventArgs e)
        {
            Settings.Default.path_backupPath = textBoxBackupPath.Text;
            Settings.Default.path_logPath = textBoxLogPath.Text;
            Settings.Default.app_forceRebuildUserControls = checkBoxForceRebuild.Checked;
            Settings.Default.db_dataSource = textBoxDBDataSource.Text;
            Settings.Default.db_pass = textBoxDBPassword.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
        #endregion

        #region SaveSettingsTab
        private void loadSaveTab()
        {
            TextBoxInitialDialog.Text = Settings.Default.initial_directory;
            TextBoxAdobePath.Text = Settings.Default.adobe_path;
            TextBoxFoxitPath.Text = Settings.Default.foxit_path;
            chkAllowCompanyInfo.Checked = Settings.Default.company_info_required;
            chkUseOldPrint.Checked = Settings.Default.use_old_print;
            txtDefaultInvoiceDescription.Text = Settings.Default.default_invoice_description;
            txtDefaultInvoiceComments.Text = Settings.Default.default_invoice_comment;
            checkBoxDisplayMetricUnit.Checked = Settings.Default.show_metric_unit;
            textBoxInvoiceLines.Text = Settings.Default.max_invoice_items;
            
            comboBoxTimesToPrint.SelectedItem = Settings.Default.times_to_print.ToString();
            textBoxPrintDesc1.Text = Settings.Default.print_desc_1;
            textBoxPrintDesc2.Text = Settings.Default.print_desc_2;
            textBoxPrintDesc3.Text = Settings.Default.print_desc_3;
            txtLocationFrom.Text= Settings.Default.locationFrom;
            txtLocationTo.Text = Settings.Default.locationTo;
            txtreasonoftransport.Text = Settings.Default.reasonoftransport;
        }

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.SelectedPath))
            {
                TextBoxInitialDialog.Text = dialog.SelectedPath;
            }
        }

        private void ButtonSaveSettings_Click(object sender, EventArgs e)
        {
            Settings.Default.initial_directory = TextBoxInitialDialog.Text;
            Settings.Default.adobe_path = TextBoxAdobePath.Text;
            Settings.Default.foxit_path = TextBoxFoxitPath.Text;
            Settings.Default.company_info_required = chkAllowCompanyInfo.Checked;
            Settings.Default.use_old_print = chkUseOldPrint.Checked;
            Settings.Default.default_invoice_description = txtDefaultInvoiceDescription.Text;
            Settings.Default.default_invoice_comment = txtDefaultInvoiceComments.Text;
            Settings.Default.show_metric_unit = checkBoxDisplayMetricUnit.Checked;
            Settings.Default.max_invoice_items = textBoxInvoiceLines.Text;
            Settings.Default.times_to_print = int.Parse(comboBoxTimesToPrint.SelectedItem.ToString());
            Settings.Default.print_desc_1 = textBoxPrintDesc1.Text;
            Settings.Default.print_desc_2 = textBoxPrintDesc2.Text;
            Settings.Default.print_desc_3 = textBoxPrintDesc3.Text;
            Settings.Default.locationFrom = txtLocationFrom.Text;
            Settings.Default.locationTo = txtLocationTo.Text;
            Settings.Default.reasonoftransport = txtreasonoftransport.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnAdobePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    try
                    {
                        TextBoxAdobePath.Text = dialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        General.InformationMessage("Ο φάκελος που επιλέξατε δεν μπορεί να χρησιμοποιηθεί.\n\n" + ex.Message);
                    }
                }
            }
        }

        private void btnFoxitPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    try
                    {
                        TextBoxFoxitPath.Text = dialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        General.InformationMessage("Ο φάκελος που επιλέξατε δεν μπορεί να χρησιμοποιηθεί.\n\n" + ex.Message);
                    }
                }
            }
        }
        #endregion
    }
}
