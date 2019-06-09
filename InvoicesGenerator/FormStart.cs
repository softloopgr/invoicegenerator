using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace InvoicesGenerator
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();

            this.Text = "InvoiceGenerator v." + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            labelVersion.Text = "version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            //// SoftloopTools.explicitDatabaseUpdateOnVersion1001();

            if (UserDAL.getUsers().Count == 0)
            {
                showRegister();
            }
            else
            {
                showLogin();
            }


            if (Trial.isTrial())
            {
                validateTrial();
            }

        }

        private void validateTrial()
        {
            switch (Trial.updateTrial())
            {
                case Trial.TrialStatus.EXPIRED:
                    showTrialTimeout();
                    break;
                case Trial.TrialStatus.LOCKED:
                    showTrialLockout();
                    break;
                default:
                    showDemo();
                    break;
            }
        }

        private void showDemo()
        {
            int usageDays = Trial.getTrialDays();

            if (usageDays <= 30)
            {
                LabelDemo.Text = "Δοκιμαστική Έκδοση\nΑπομένουν " + usageDays.ToString() + " μέρες.";
            }
            else
            {
                throw new Exception("TRIAL DAYS EXTRENDS MAX_TRIAL");
            }
        }

        private void showTrialLockout()
        {
            Label lbl = new Label();
            lbl.Text = "Έχει ανιχνευθεί πιθανή παράνομη χρήση λογισμικού! Παρακαλώ επικοινωνήστε με τον διαχειριστή.";
            lbl.ForeColor = Color.Red;
            lbl.AutoSize = false;
            lbl.Padding = new Padding(15, 100, 15, 0);
            lbl.Dock = DockStyle.Fill;
            lbl.Font = new Font(lbl.Font.FontFamily, 12, lbl.Font.Style & ~FontStyle.Bold);
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(lbl);

            LabelDemo.Text = "Παράνομο αντίγραφο";
        }

        void RegisterClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void showTrialTimeout()
        {
            FormTrialTimeout frm = new FormTrialTimeout();
            frm.TopLevel = false;
            frm.Show();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.FormClosed += trialTimeoutForm_Closed;
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(frm);

            LabelDemo.Text = "Δοκιμαστική έκδοση\nέχει λήξει.";
        }

        private void trialTimeoutForm_Closed(object sender, FormClosedEventArgs e)
        {
            FormTrialTimeout frmTrialTimeout = (FormTrialTimeout)sender;
            switch (frmTrialTimeout.selectedMethod)
            {
                case RegistrationMethod.PHONE:
                    showActivation();
                    break;
                case RegistrationMethod.INTERNET:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();

            }
        }

        private void showActivation()
        {
            FormActivation frm = new FormActivation();
            frm.TopLevel = false;
            frm.Show();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.FormClosed += activationForm_Closed;
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(frm);
        }

        private void activationForm_Closed(object sender, FormClosedEventArgs e)
        {
            showLogin();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showLogin()
        {
            FormLogin frm = new FormLogin();
            frm.TopLevel = false;
            frm.Show();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.FormClosed += loginForm_Closed;
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(frm);
        }

        private void loginForm_Closed(object sender, EventArgs e)
        {
            FormMain frmMain = new FormMain();
            frmMain.Show();
            this.Hide();
        }

        private void showRegister()
        {
            FormRegister frm = new FormRegister();
            frm.TopLevel = false;
            frm.Show();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.FormClosed += registerForm_Closed;
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(frm);
        }

        private void registerForm_Closed(object sender, EventArgs e)
        {
            showLogin();
        }

        private void LabelDemo_Click(object sender, EventArgs e)
        {
            showActivation();
        }
    }
}
