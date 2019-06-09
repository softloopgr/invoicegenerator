using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator.gr.gsis.www1;
using InvoicesGenerator.Vies;

namespace InvoicesGenerator
{
    public partial class CheckFirm : Form
    {
        private string _afm;
        RgWsBasStoixNRtUser u = new RgWsBasStoixNRtUser();
        bool _found = false;
        string _name = string.Empty;
        string _address = string.Empty;
        public string CustName = string.Empty;
        public string CustAddress = string.Empty;
        public string CustAfm = string.Empty;
        public string _country ;

        private bool connected;

        public CheckFirm(bool returnData = false)
        {
            InitializeComponent();
            if (returnData)
            {
                btnAddToInvoice.Visible = true;
            }
        }

        private void CheckFirm_Load(object sender, EventArgs e)
        {
            TextBoxTaxNumber.Focus();
            TextBoxTaxNumber.Enabled = false;
            ButtonSearch.Enabled = false;

            cmbCountry.SelectedIndex = 8;

            BackgroundWorker workerCheckConnection = new BackgroundWorker();
            workerCheckConnection.WorkerReportsProgress = true;
            workerCheckConnection.DoWork += new DoWorkEventHandler(CheckConnection);
            workerCheckConnection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCheckConnection_RunWorkerCompleted);
            workerCheckConnection.RunWorkerAsync();
        }
        
        private void CheckConnection(object sender, DoWorkEventArgs e)
        {
            if (General.IsConnected())
            {
                connected = true;
            }
            else
            {
                connected = false;
            }
        }
        
        private void workerCheckConnection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (connected)
            {
                tsLabelConnectionShow.Text = "Ενεργή σύνδεση με την VIES";
                tsLabelConnectionShow.Image = Properties.Resources.accept;
                TextBoxTaxNumber.Enabled = true;
                ButtonSearch.Enabled = true;
            }
            else
            {
                tsLabelConnectionShow.Text = "Μη ενεργή σύνδεση με την VIES";
                tsLabelConnectionShow.Image = Properties.Resources.cancel;
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            SearchTaxNumber();
        }
        
        private void TextBoxTaxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchTaxNumber();
            }
        }

        private void SearchTaxNumber()
        {
            if (!General.CheckTaxNumber(TextBoxTaxNumber.Text))
            {
                General.ExclamationMessage("Δεν είναι σωστό το Α.Φ.Μ. που δώσατε");
                return;
            }

            _afm = TextBoxTaxNumber.Text;
            object objCountry = cmbCountry.SelectedItem;
            if (objCountry != null)
            {
                _country = objCountry.ToString().Split(new string[] { "-" }, StringSplitOptions.None).Length > 0 ? objCountry.ToString().Split(new string[] { "-" }, StringSplitOptions.None)[0] : "";
            }
            else
            {
                _country = "";
            }

            BackgroundWorker workerLogin = new BackgroundWorker();
            workerLogin.WorkerReportsProgress = true;
            workerLogin.DoWork += new DoWorkEventHandler(searchVIES);
            workerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeSearchVIES);
            workerLogin.RunWorkerAsync();

            PanelLoading.BringToFront();
            PanelLoading.Visible = true;
            TextBoxTaxNumber.Enabled = false;
            ButtonSearch.Enabled = false;
        }

        public void searchVIES(object sender, DoWorkEventArgs e)
        {
            try
            {
                string afm = TextBoxTaxNumber.Text;
                checkVatPortTypeClient client = new checkVatPortTypeClient();
                client.checkVat(ref _country, ref afm, out _found, out _name, out _address);
            }
            catch (Exception ex)
            {
                General.ErrorMessage("Η εφαρμογή δεν μπόρεσε να επικοινωνήσει με την υπηρεσία VIES. Ελέγξε την σύνδεση σας στο internet.");
            }
        }

        public void completeSearchVIES(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_found)
            {
                TextBoxAddress.Text = _address;
                TextBoxName.Text = _name;
            }

            PanelLoading.Visible = false;
            TextBoxTaxNumber.Enabled = true;
            ButtonSearch.Enabled = true;
        }

        public void doSearch(object sender, DoWorkEventArgs e)
        {
            RgWsBasStoixN c = new RgWsBasStoixN();
            GenWsErrorRtUser wse = new GenWsErrorRtUser();
            decimal d = 1;
            try
            {
                c.rgWsBasStoixN(_afm, ref u, ref d, ref wse);
                _found = true;
            }
            catch (Exception ex)
            {
                _found = false;
                General.ErrorMessage("Η εφαρμογή δεν μπόρεσε να επικοινωνήσει με την Γ.Γ.Π.Σ. Ελέγξε την σύνδεση σας στο internet.");
            }
        }
        
        public void completeSearch(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_found)
            {
                if (u.onomasia != null)
                {
                    TextBoxName.Text = u.onomasia.Trim();
                    TextBoxOccupation.Text = u.actLongDescr.Trim();
                    TextBoxAddress.Text =
                        u.postalAddress.Trim() + " " + u.postalAddressNo.Trim() + ", " +
                        u.postalZipCode.Trim() + " " + u.parDescription.Trim();
                    TextBoxTaxNumber.Text = u.afm.Trim();
                    TextBoxPhone.Text = u.firmPhone.Trim();
                    TextBoxTaxOffice.Text = u.doyDescr.Trim();

                    if (u.stopDate != null)
                    {
                        General.ExclamationMessage("Το ΑΦΜ ανήκει σε απενεργοποιημένη εταιρεία");
                    }
                }
                else
                {
                    General.ExclamationMessage("Το ΑΦΜ ανήκει σε φυσικό πρόσωπο");
                }
            }

            PanelLoading.Visible = false;
            TextBoxTaxNumber.Enabled = true;
            ButtonSearch.Enabled = true;
        }

        private void btnAddToInvoice_Click(object sender, EventArgs e)
        {
            CustName = _name;
            CustAddress = _address;
            CustAfm = TextBoxTaxNumber.Text;
            this.Close();
        }
    }
}
