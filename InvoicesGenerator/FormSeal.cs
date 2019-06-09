using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator.Classes;
using InvoicesGenerator;
using InvoicesGenerator.Properties;
using InvoicesGenerator.gr.gsis.www1;

namespace InvoicesGenerator
{
    public partial class FormSeal : Form
    {
        public FormSeal()
        {
            InitializeComponent();
        }

        SealDetails _seal = new SealDetails();

        private void SealForm_Load(object sender, EventArgs e)
        {
            _seal = InvoiceDAL.getSeal();
            TextBoxTaxOffice.AutoCompleteCustomSource = InvoiceDAL.getTaxOffices();
            if (_seal == null || _seal.id == null || _seal.id < 0) return;

            TextBoxName.Text = _seal.Name;
            TextBoxOccupation.Text = _seal.Occupation;
            TextBoxAddress.Text = _seal.Address;
            TextBoxTaxNumber.Text = _seal.Taxnumber;
            TextBoxPhone.Text = _seal.Phone;
            TextBoxTaxOffice.Text = _seal.Taxoffice;
            TextBoxLocation.Text = _seal.Location;
            TextBoxVat.Text = Convert.ToDecimal(_seal.Vat * 100).ToString("0.00");
            CheckBoxHasOrder.Checked = (_seal.HasOrder == 1) ? true : false;

            if (!string.IsNullOrEmpty(Settings.Default.CompanyLogo))
            {
                try
                {
                    Image companyLogo = Image.FromFile(Settings.Default.CompanyLogo);
                    picBoxLogo.Image = companyLogo;
                }
                catch
                {

                }
            }

            TextBoxName.Focus();
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (TextBoxName.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε όνομα χρήστη");
                return;
            }
            if (TextBoxName.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε όνομα");
                return;
            }
            if (TextBoxOccupation.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε επάγγελμα");
                return;
            }
            if (TextBoxAddress.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε διεύθυνση");
                return;
            }
            if (TextBoxTaxNumber.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ΑΦΜ");
                return;
            }
            if (TextBoxPhone.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε τηλέφωνο");
                return;
            }
            if (TextBoxTaxOffice.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ΔΟΥ");
                return;
            }
            if (TextBoxLocation.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε Τόπο");
                return;
            }
            if (TextBoxVat.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε Φ.Π.Α.");
                return;
            }

            _seal.Name = TextBoxName.Text;
            _seal.Occupation = TextBoxOccupation.Text;
            _seal.Address = TextBoxAddress.Text;

            if (!General.CheckTaxNumber(TextBoxTaxNumber.Text))
            {
                General.ExclamationMessage("Δεν είναι σωστό το Α.Φ.Μ. που δώσατε");
                return;
            }
            _seal.Taxnumber = TextBoxTaxNumber.Text;

            _seal.Phone = TextBoxPhone.Text;
            _seal.Taxoffice = TextBoxTaxOffice.Text;
            _seal.Location = TextBoxLocation.Text;
            _seal.Vat = decimal.Parse(TextBoxVat.Text) / 100;
            _seal.HasOrder = CheckBoxHasOrder.Checked ? 1 : 0;

            if (General.QuestionMessage("Θα αποθηκευτεί ο χρήστης με τα στοιχεία χρήστη που ορίσατε. Θέλετε να συνεχίσετε;") == DialogResult.Yes)
            {
                //bool isFirst = false;
                ////σημαίνει οτι μπήκε πρώτη φορά, από εγκατάσταση του προγράμματος
                //if (_seal == null || _seal.id == null || _seal.id < 0)
                //{
                //    isFirst = true;
                //    _seal.Username = "";
                //    _seal.Password = "";
                //}

                if (_seal.save())
                {
                    // ΠΛΕΟΝ ΔΕΝ ΧΡΕΙΑΖΕΤΑΙ ΓΙΑΤΙ ΠΡΩΤΑ ΦΤΙΑΧΝΕΙΣ USER
                    //if (isFirst)
                    //{
                    //    using (FormRegister frmSec = new FormRegister())
                    //    {
                    //        if (frmSec.ShowDialog() == DialogResult.Cancel)
                    //        {
                    //            Application.Exit();
                    //            return;
                    //        }
                    //        this.DialogResult = DialogResult.OK;
                    //        Close();
                    //    }
                    //}
                    //else
                    //{
                    //    this.DialogResult = DialogResult.OK;
                    //}
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    General.ErrorMessage("Η αποθήκευση απέτυχε");
                }
            }
        }

        private void TextBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.tsbtnSave_Click(null, null);
            }
        }

        private void MaskedTextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.tsbtnSave_Click(null, null);
            }
        }

        /*RgWsBasStoixNRtUser u = new RgWsBasStoixNRtUser();
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (!General.CheckTaxNumber(TextBoxSearchTN.Text))
            {
                General.ExclamationMessage("Δεν είναι σωστό το Α.Φ.Μ. που δώσατε");
                return;
            }

            BackgroundWorker workerLogin = new BackgroundWorker();
            workerLogin.WorkerReportsProgress = true;
            workerLogin.DoWork += new DoWorkEventHandler(doSearch);
            workerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeSearch);
            workerLogin.RunWorkerAsync();

            TextBoxSearchTN.Enabled = false;
            ButtonSearch.Enabled = false;
            //GroupBoxAuthCred.Enabled = false;
            ButtonSave.Enabled = false;
            //PanelLoading.BringToFront();
            //PanelLoading.Visible = true;
        }
        public void completeSearch(object sender, RunWorkerCompletedEventArgs e)
        {
            //PanelLoading.Visible = false;
            TextBoxSearchTN.Enabled = true;
            ButtonSearch.Enabled = true;
            GroupBoxAuthCred.Enabled = true;
            ButtonSave.Enabled = true;

            if (u.onomasia != null)
            {
                TextBoxUsername.Focus();
            }
            else
            {
                TextBoxSearchTN.Focus();
            }

            if (!(bool)e.Result) return;

            if (u.onomasia != null)
            {
                TextBoxName.Text = (u.onomasia != null) ? u.onomasia.Trim() : string.Empty;
                TextBoxOccupation.Text = (u.actLongDescr != null) ? u.actLongDescr.Trim() : string.Empty;
                TextBoxAddress.Text =
                    ((u.postalAddress != null) ? u.postalAddress.Trim() : string.Empty) + " " +
                    ((u.postalAddressNo != null) ? u.postalAddressNo.Trim() : string.Empty) + ", " +
                    ((u.postalZipCode != null) ? u.postalZipCode.Trim() : string.Empty) + " " +
                    ((u.parDescription != null) ? u.parDescription.Trim() : string.Empty);
                TextBoxTaxNumber.Text = (u.afm != null) ? u.afm.Trim() : string.Empty;
                TextBoxPhone.Text = (u.firmPhone != null) ? u.firmPhone.Trim() : string.Empty;
                TextBoxTaxOffice.Text = (u.doyDescr != null) ? u.doyDescr.Trim() : string.Empty;

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

        public void doSearch(object sender, DoWorkEventArgs e)
        {
            RgWsBasStoixN c = new RgWsBasStoixN();
            GenWsErrorRtUser wse = new GenWsErrorRtUser();
            decimal d = 1;
            try
            {
                e.Result = true;
                c.rgWsBasStoixN(TextBoxSearchTN.Text, ref u, ref d, ref wse);
            }
            catch (Exception ex)
            {
                e.Result = false;
                General.ErrorMessage("Η εφαρμογή δεν μπόρεσε να επικοινωνήσει με την Γ.Γ.Π.Σ. Ελέγξε την σύνδεση σας στο internet.");
            }
        }*/

        private void TextBoxSearchTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ButtonSearch.PerformClick();
            }
        }

        private void ButtonImageSearchClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    try
                    {
                        //TextBoxImgPath.Text = dialog.FileName;

                        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\InvoicesGenerator";
                        string fileName = Path.GetFileName(dialog.FileName);
                        string appDataImageFile = appDataPath + "\\" + fileName;

                        File.Copy(dialog.FileName, appDataImageFile, true);

                        Image companyLogo = Image.FromFile(dialog.FileName);
                        picBoxLogo.Image = companyLogo;

                        Settings.Default.CompanyLogo = appDataImageFile;
                        Settings.Default.Save();
                    }
                    catch
                    {
                        General.InformationMessage("Η εικόνα που διαλέξατε χρησιμοποιείται ήδη σαν εταιρικό Logo, παρακαλώ επιλέξτε κάποια άλλη εικόνα.");
                    }
                }
            }
        }
    }
}
