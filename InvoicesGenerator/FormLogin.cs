using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Threading;
using InvoicesGenerator.Properties;
using System.Reflection;

namespace InvoicesGenerator
{
    public partial class FormLogin : Form
    {
        public bool _success = false;

        public FormLogin(UserControl.dummyType DummyParameter)
        {
            InitializeComponent();
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }

        private void login()
        {
            //BackgroundWorker workerLogin = new BackgroundWorker();
            //workerLogin.WorkerReportsProgress = true;
            //workerLogin.DoWork += new DoWorkEventHandler(doLogin);
            //workerLogin.ProgressChanged += new ProgressChangedEventHandler(reportLogin);
            //workerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeLogin);
            //workerLogin.RunWorkerAsync();
            //PanelLoading.BringToFront();
            //PanelLoading.Visible = true;
        
            BackgroundWorker workerLogin = new BackgroundWorker();
            workerLogin.WorkerReportsProgress = true;
            workerLogin.DoWork += new DoWorkEventHandler(login_process);
            workerLogin.ProgressChanged += new ProgressChangedEventHandler(login_report);
            workerLogin.RunWorkerAsync();

            textBoxUsername.Enabled = false;
            textBoxPassword.Enabled = false;
            buttonLogin.Enabled = false;

            LabelProgress.Visible = true;
        }

        public void login_report(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    LabelProgress.Invoke(new MethodInvoker(delegate { LabelProgress.Text = e.UserState.ToString(); }));
                    break;
                case 50:

                    LabelProgress.Text = e.UserState.ToString();

                    textBoxUsername.Enabled = true;
                    textBoxPassword.Enabled = true;
                    buttonLogin.Enabled = true;

                    break;
                case 100:
                    if (_success)
                    {
                        this.Invoke(new MethodInvoker(this.Close));
                    }
                    break;
                default:
                    break;
            }
        }

        public void completeLogin(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void login_process(object sender, DoWorkEventArgs e)
        {
            SoftloopTools.TraceLog("Εναρξη σύνδεσης");
            ((BackgroundWorker)sender).ReportProgress(0, "Εναρξη σύνδεσης");
            //Program.connectionString =
            //    "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\","")+"\\Resources\\" +Settings.Default.db_dataSource+ ";" +
            //    "Persist Security Info=True;" +
            //    "Password=" + Settings.Default.db_pass + "; ";

            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                try
                {
                    ((BackgroundWorker)sender).ReportProgress(0, "Προσπάθεια σύνδεσης με την βάση δεδομένων σας");
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        ((BackgroundWorker)sender).ReportProgress(0, "Επιτυχής σύνδεση με βάση δεδομένων");
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"select users.USER_ID,hierarchy
                                            from users
                                            inner join user_levels on users.user_id =user_levels.user_id
                                            inner join auth_levels on auth_levels.AuthLevel_id =user_levels.user_level_id
                                            where username =@username and password=@password";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                        cmd.Parameters.AddWithValue("@password", General.CalculateHash(textBoxPassword.Text, textBoxUsername.Text));

                        ((BackgroundWorker)sender).ReportProgress(0, "Έλεγχος στοιχείων σύνδεσης");
                        using (SqlCeDataReader userReader = cmd.ExecuteReader())
                        {

                            if (userReader.Read())
                            {
                                Program.user.UserHierarchy = userReader.GetInt32(userReader.GetOrdinal("hierarchy"));
                                Program.user.UserID = userReader.GetInt32(userReader.GetOrdinal("USER_ID"));
                                _success = true;
                                ((BackgroundWorker)sender).ReportProgress(0, "Επιτυχής αυθεντικοποίηση");
                            }
                            else
                            {
                                ((BackgroundWorker)sender).ReportProgress(50, "Η αυθεντικοποίηση απέτυχε");
                            }
                            userReader.Close();
                        }
                    }
                }
                catch
                {
                    ((BackgroundWorker)sender).ReportProgress(50, "Η σύνδεση απέτυχε. Επικοινωνήστε με τον διαχειριστή.");
                }

                if (_success)
                {
                    try
                    {
                        if (Settings.Default.app_forceRebuildUserControls)
                        {
                            ((BackgroundWorker)sender).ReportProgress(0, "Εφαρμογή Ρυθμίσεων");
                            UserControl.initLevelDictionary();
                            UserControl.GetAllClasses(Settings.Default.app_name);
                            Settings.Default.app_forceRebuildUserControls = false;
                            Settings.Default.Save();
                        }
                    }
                    catch
                    {
                        _success = false;
                        ((BackgroundWorker)sender).ReportProgress(50, "Αποτυχία εφαρμογής ρυθμίσεων. Επικοινωνήστε με τον διαχειριστή.");
                    }
                }

                if (_success)
                {
                    ((BackgroundWorker)sender).ReportProgress(100, "Ολοκληρώθηκε");
                }
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_success)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            using (FormSeal frm = new FormSeal())
            {
                frm.ShowDialog();
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SoftloopTools.updateApplication();
        }

    }
}
