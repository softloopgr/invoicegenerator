using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace InvoicesGenerator
{
    public partial class FormShare : Form
    {
        MailMessage message = new MailMessage();
        private bool sent;
        private bool error;

        public FormShare()
        {
            InitializeComponent();
        }

        private string _company_name, _fileToSend;
        public FormShare(string company_name, string fileToSend)
        {
            InitializeComponent();
            _company_name = company_name;
            _fileToSend = fileToSend;
        }

        private void SendMail(object sender, DoWorkEventArgs e)
        {
            error = false;
            if (TextBoxFrom.Text == string.Empty)
            {
                General.ExclamationMessage("Παρακαλώ συμπληρώστε e-mail αποστολέα");
                error = true;
                return;
            }

            if (!General.emailCheck(TextBoxFrom.Text))
            {
                General.ExclamationMessage("Παρακαλώ δώστε σωστό e-mail αποστολέα");
                error = true;
                return;
            }

            if (TextBoxTo.Text == string.Empty)
            {
                General.ExclamationMessage("Παρακαλώ συμπληρώστε e-mail παραλήπτη(ων)");
                error = true;
                return;
            }

            if (!General.emailCheck(TextBoxTo.Text))
            {
                General.ExclamationMessage("Παρακαλώ δώστε σωστό e-mail παραλήπτη(ων)");
                error = true;
                return;
            }

            MailAddress from = new MailAddress(TextBoxFrom.Text, _company_name);
            string to = TextBoxTo.Text.Replace(" ", "");
            string subject = TextBoxSubject.Text;
            string body = TextBoxBody.Text;

            message.From = from;
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body + Environment.NewLine + "Αυτό το email εχει αποσταλλεί απο το Invoices Generator.";

            message.IsBodyHtml = true;

            Attachment data;
            data = new Attachment(_fileToSend);
            message.Attachments.Add(data);

            SmtpClient client = new SmtpClient();
            client.Host = "softloop.gr";
            client.Credentials = new NetworkCredential("parastatiko@softloop.gr", "*nacote7");

            try
            {
                client.Send(message);
                sent = true;
            }
            catch
            {
                sent = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PanelLoading.BringToFront();
            PanelLoading.Visible = true;
            buttonShare.Enabled = false;

            BackgroundWorker workerLogin = new BackgroundWorker();
            workerLogin.WorkerReportsProgress = true;
            workerLogin.DoWork += new DoWorkEventHandler(SendMail);
            workerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerLogin_RunWorkerCompleted);
            workerLogin.RunWorkerAsync();
        }

        void workerLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sent)
            {
                MessageBox.Show("Το μήνυμα στάλθηκε επιτυχώς.",
                                "Αποστολή Παραστατικού", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                if (!error)
                {
                    MessageBox.Show(
                        "Αποτυχία αποστολής του μηνύματος. Παρακαλώ προσπαθήστε αργότερα. Αν το πρόβλημα επιμένει επικοινωνήστε μαζί μας τηλεφωνικώς",
                        "Αποστολή Παραστατικού", MessageBoxButtons.OK);
                }
                PanelLoading.SendToBack();
                PanelLoading.Visible = false;
                buttonShare.Enabled = true;
            }
        }

        private void TextBoxFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonShare.PerformClick();
            }
        }
    }
}
