using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using InvoicesGenerator;
using InvoicesGenerator.Classes;
using InvoicesGenerator.Properties;
using InvoicesGenerator.gr.gsis.www1;
using System.IO;

namespace InvoicesGenerator
{
    public partial class FormInvoiceGroup : Form
    {
        Invoice _invoice = new Invoice();
        int? _invoiceGroupID = null;
        public bool blank;
        RgWsBasStoixNRtUser u = new RgWsBasStoixNRtUser();
        private bool _afmIsFound;
        private bool _isInInitialization;

        private List<int> _itemsToDelete = new List<int>();

        public FormInvoiceGroup(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }

        public FormInvoiceGroup()
        {
            InitializeComponent();
            UserControl.InitControls(this);
            blank = true;
        }

        public FormInvoiceGroup(int invoiceGroupID)
        {
            InitializeComponent();
            UserControl.InitControls(this);
            _invoiceGroupID = invoiceGroupID;
            blank = false;
        }

        // Init Functions
        private void FormInvoiceGroup_Load(object sender, EventArgs e)
        {
            _isInInitialization = true;
            InitComboBoxInvoiceTypes();
            InitComboBoxEndeikseis();
            InitComboCustomers();
            //InitTextBoxTaxOffices();
            //InitTextBoxTaxNumbers();

            DataGridViewInvoiceItems.AutoGenerateColumns = false;

            TextBoxPublishedDate.Text = DateTime.Now.ToString("ddMMyyyy");
            TextBoxPublishedLocation.Text = "Αθήνα";

            //labelPrintStatus.Text = "-";

            TextBoxInvoiceNumber.Text = InvoiceDAL.getInvoiceNumber(
                ((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Key
                ).ToString();

            if (_invoiceGroupID != null)
            {
                _invoice = InvoiceDAL.getById((int)_invoiceGroupID);
                ComboBoxInvoiceType.SelectedValue = _invoice.Invoice_type;
                TextBoxInvoiceNumber.Text = _invoice.Number.ToString();
                TextBoxCustomerName.Text = _invoice.Customer_name;
                TextBoxCustomerAddress.Text = _invoice.Customer_address;
                TextBoxCustomerDescription.Text = _invoice.Customer_description;
                TextBoxCustomerTaxOffice.Text = _invoice.Customer_taxoffice;
                textBoxCustomerCity.Text = _invoice.CustomerCity;
                textBoxCustomerPostalCode.Text = _invoice.CustomerPostalCode;
                TextBoxCustomerPhone.Text = _invoice.CustomerPhone;

                if (_invoice.Customer_taxnumber.Split(' ').Count() > 1)
                {
                    TextBoxCustomerTaxNumber.Text = _invoice.Customer_taxnumber.Split(' ')[1];
                    // koitame na orisoume to dd me tis xores, th xora pou efere apo th vash
                    int index = 0;
                    foreach (string item in cmbCountry.Items)
                    {
                        if (item.Contains(_invoice.Customer_taxnumber.Split(' ')[0]))
                        {
                            cmbCountry.SelectedIndex = index;
                        }
                        index++;
                    }
                }
                else
                {
                    TextBoxCustomerTaxNumber.Text = _invoice.Customer_taxnumber;
                    // ean den vrethike xora tote vazoume thn ellhnikh san default
                    cmbCountry.SelectedIndex = 8;
                }
                ΤextBoxComments.Text = _invoice.UserComments;
                TextBoxVat.Text = Convert.ToDecimal(_invoice.Tax).ToString("0.00");
                CheckBoxHold.Checked = Convert.ToBoolean(_invoice.Enable_hold);
                TextBoxHold.Text = Convert.ToDecimal(_invoice.Hold_percent).ToString();
                cmbEndeiksh.SelectedIndex = _invoice.Is_credit.Value;
                TextBoxPublishedDate.Text = _invoice.Date.HasValue ? _invoice.Date.Value.ToString("ddMMyyyy") : null;
                TextBoxPublishedLocation.Text = _invoice.Location;
                this.Text = "Παραστατικό - [Αριθμός: " + _invoice.Number.ToString() + "]";

                if (_invoice.InvoiceOrder != null)
                {
                    LabelInvoiceOrder.Visible = true;
                    TextBoxInvoiceOrder.Visible = true;
                    TextBoxInvoiceOrder.Text = _invoice.InvoiceOrder;
                }

                TextBoxDestination.Text = _invoice.Destination;
                TextBoxLoadingPlace.Text = _invoice.LoadingPlace;
                TextBoxMovingPurpose.Text = _invoice.MovingPurpose;

                ComboBoxInvoiceType.Enabled = false;
                cmbEndeiksh.Enabled = false;

                refreshDataGridInvoiceItems();
            }
            else
            {
                ΤextBoxComments.Text = Settings.Default.default_invoice_comment;
                TextBoxVat.Text = Convert.ToDecimal(Program.seal.Vat * 100).ToString("0.00");
                TextBoxPublishedLocation.Text = Program.seal.Location;
                TextBoxMovingPurpose.Text = Settings.Default.reasonoftransport;
                TextBoxLoadingPlace.Text = Settings.Default.locationFrom;
                TextBoxDestination.Text = Settings.Default.locationTo;
                if (Program.seal.HasOrder == 1)
                {
                    LabelInvoiceOrder.Visible = true;
                    TextBoxInvoiceOrder.Visible = true;
                }
                cmbCountry.SelectedIndex = 8;
            }
            _isInInitialization = false;
        }

        // Form Functions
        private void ButtonSearchCustomer_Click(object sender, EventArgs e)
        {
            if (!General.CheckTaxNumber(TextBoxCustomerTaxNumber.Text))
            {
                General.ExclamationMessage("Δεν είναι σωστό το Α.Φ.Μ. που δώσατε");
                return;
            }
            GetAfmDetailsFromGSIS();
        }

        /// <summary>
        /// Αποθηκεύει την απόδειξη στη βάση
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (saveInvoiceGroup())
            {
                blank = false;


                SealDetails seal = InvoiceDAL.getSeal();
                if (seal.id.HasValue && _invoice.Id.HasValue)
                {
                    //SaveFileDialog dialog = new SaveFileDialog();
                    //if (!string.IsNullOrEmpty(Settings.Default.initial_directory))
                    //{
                    //    dialog.RestoreDirectory = true;
                    //    dialog.InitialDirectory = Settings.Default.initial_directory;
                    //}
                    //dialog.Filter = "Portable Document Format (PDF)|*.pdf";
                    //dialog.FileName = _invoice.Number + ". " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + _invoice.Customer_name;
                    //dialog.ShowDialog();
                    string path = Settings.Default.initial_directory;
                    //string path = Path.GetDirectoryName(dialog.FileName);
                    if (!string.IsNullOrEmpty(path))
                    {
                        string fileName = path + "\\" + _invoice.Number + ". " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + _invoice.Customer_name + ".pdf";
                        if (!Report.SaveToWord(seal, _invoice, fileName, true))
                        {
                            // kai ean den mporei tote proxoraei me ton palio tropo
                            //FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, false);
                            //frm.SaveToDoc(seal.id.Value, _invoice.Id.Value, fileName, true, true);
                            //lblAnyMessage.Text = frm.SavedLocation;
                            General.InformationMessage("Αυτή η λειτουργία δεν υποστηρίζεται.\n\nΕπικοινωνήστε με τον διαχειριστή στο 213-0267712");
                            return;
                        }

                        Report.SaveToPdf(seal.id.Value, _invoice.Id.Value, fileName);
                        lblAnyMessage.Text = "Το αρχείο αποθηκεύτηκε στο: " + path + ".";
                    }
                    else
                    {
                        // min kaneis tipota efoson den epelekse kapou na sosei to arxeio
                        lblAnyMessage.Text = "Δεν επιλέξατε που θα αποθηκεύσετε το αρχείο.";
                    }
                }


                refreshDataGridInvoiceItems();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (_invoice.Id == null)
            {
                General.InformationMessage("Δεν έχει ακόμα αποθηκευτεί το παραστατικό. Δεν υπαρχει λόγος διαγραφής");
                return;
            }
            if (General.QuestionMessage("Είστε σίγουρος ότι θέλετε να διαγράψετε το πακέτο. Η ενέργεια δεν μπορεί να αναιρεθεί.") == DialogResult.Yes)
            {
                if (InvoiceDAL.deleteByGroup((int)_invoice.Id))
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Εισαγωγή νέας αιτιολογίας / αγοράς
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewInvoiceItem_Click(object sender, EventArgs e)
        {
            using (FormInvoice invoice = new FormInvoice(null, _invoice.Tax.HasValue ? _invoice.Tax.Value : 0))
            {
                invoice.ShowDialog(this);
                if (invoice.isSaved)
                {
                    if (invoice.localInvoiceItem.amount > 0)
                    {
                        InvoiceItem tempExchange = invoice.localInvoiceItem;
                        if (tempExchange != null)
                        {
                            if (_invoice.InvoiceItems == null)
                            {
                                _invoice.InvoiceItems = new List<InvoiceItem>();
                            }
                            //removeTotal();
                            _invoice.InvoiceItems.Add(tempExchange);
                            //ButtonSave.PerformClick();
                            refreshDataGridInvoiceItems();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ανοίγει μία αιτιολογία για επεξεργασία
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewInvoiceItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = DataGridViewInvoiceItems.CurrentRow.Index;
            if (_invoice.InvoiceItems[index].id != -1000)
            {
                using (FormInvoice invoiceForm = new FormInvoice(_invoice.InvoiceItems[index], _invoice.Tax.HasValue ? _invoice.Tax.Value : 0))
                {
                    invoiceForm.status = _invoice.Status.HasValue ? _invoice.Status.Value : 0;
                    invoiceForm.ShowDialog();

                    if (invoiceForm.localInvoiceItem.is_deleted == 1)
                    {
                        _invoice.InvoiceItems.Remove(invoiceForm.localInvoiceItem);
                    }
                    else
                    {
                        if (invoiceForm.isSaved)
                        {
                            _invoice.Is_deleted = invoiceForm.localInvoiceItem.is_deleted;
                            _invoice.InvoiceItems[index].amount = invoiceForm.localInvoiceItem.amount;
                            _invoice.InvoiceItems[index].unit = invoiceForm.localInvoiceItem.unit;
                            _invoice.InvoiceItems[index].description = invoiceForm.localInvoiceItem.description;
                        }
                    }
                    refreshDataGridInvoiceItems();
                }
            }
        }

        /// <summary>
        /// Σβήνει όλες τις αιτιολογίες απο τη λίστα
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (_invoice.InvoiceItems != null || _invoice.InvoiceItems.Count <= 0)
                {
                    General.ExclamationMessage("Δεν υπάρχουν αιτιολογίες.");
                    return;
                }
            }
            catch
            {
                General.ExclamationMessage("Δεν υπάρχουν αιτιολογίες.");
                return;
            }

            if (General.QuestionMessage("Θέλετε να σβήσετε όλες τις αιτιολογίες;") == DialogResult.Yes)
            {
                _invoice.InvoiceItems.Clear();
                DataGridViewInvoiceItems.DataSource = null;
                LabelTotal.Text = "0 €";
                LabelVat.Text = "0 €";
                LabelTotalWithVat.Text = "0 €";
            }
        }


        private void buttonPreview_Click(object sender, EventArgs e)
        {
            List<InvoiceItem> toBePrinted = new List<InvoiceItem>();

            if (saveInvoiceGroup())
            {
                if (_invoice.Id != null)
                {
                    foreach (InvoiceItem item in _invoice.InvoiceItems)
                    {
                        if (item.is_deleted == 1 || item.is_deleted == null)
                        {
                            continue; // ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ ΚΑΙ ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                        }
                        toBePrinted.Add(item);
                    }
                }

                if (toBePrinted.Count == 0)
                {
                    General.ExclamationMessage("Δεν βρέθηκαν αιτιολογίες προς εκτύπωση");
                }
                else
                {
                    bool isPrePrint = false;//chkBoxPrePrint.Checked;
                    using (FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, isPrePrint))
                    {
                        frm.ShowDialog();
                    }
                }
                refreshDataGridInvoiceItems();
            }
        }

        // General Functions  

        /// <summary>
        /// Αποθηκεύει μια απόδειξη στην βάση
        /// </summary>
        private bool saveInvoiceGroup()
        {
            try
            {
                //svhsimo apo vash
                foreach (var id in _itemsToDelete)
                {
                    InvoiceDAL.delete(id);
                }
            }
            catch
            {
                //uphrkse thema me to svhsimo ton aitiologion
            }

            bool result = false;
            if (_invoice.InvoiceItems == null)
            {
                General.ExclamationMessage("Δεν έχετε εισάγει αιτιολογίες");
                return result;
            }

            _invoice.Customer_name = TextBoxCustomerName.Text;
            _invoice.Customer_address = TextBoxCustomerAddress.Text;
            _invoice.CustomerCity = textBoxCustomerCity.Text;
            _invoice.CustomerPhone = TextBoxCustomerPhone.Text;
            _invoice.CustomerPostalCode = textBoxCustomerPostalCode.Text;

            string taxnumber = "";

            if (Settings.Default.company_info_required || InvoiceDAL.isCompanyInfoRequired(((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Key)) //bilias settings
            {
                if (TextBoxCustomerTaxNumber.Text == string.Empty)
                {
                    General.ExclamationMessage("Δεν έχετε εισάγει Α.Φ.Μ.");
                    return result;
                }

                // ellada = 8
                if (cmbCountry.SelectedIndex == 8 && !General.CheckTaxNumber(TextBoxCustomerTaxNumber.Text))
                {
                    General.ExclamationMessage("Δεν είναι σωστό το Α.Φ.Μ. που δώσατε");
                    return result;
                }
                taxnumber = cmbCountry.SelectedItem.ToString().Split('-')[0] + " " + TextBoxCustomerTaxNumber.Text;
            }

            _invoice.Customer_taxnumber = taxnumber;
            _invoice.Customer_taxoffice = TextBoxCustomerTaxOffice.Text;
            _invoice.Customer_description = TextBoxCustomerDescription.Text;

            _invoice.Date = new DateTime(Int32.Parse(TextBoxPublishedDate.Text.Split(new char[] { '/' })[2]), Int32.Parse(TextBoxPublishedDate.Text.Split(new char[] { '/' })[1]), Int32.Parse(TextBoxPublishedDate.Text.Split(new char[] { '/' })[0]));

            _invoice.Number = (int?)int.Parse(TextBoxInvoiceNumber.Text);

            //afksanei ta noumera ton parastatikon kata 1
            if (!InvoiceDAL.updateInvoiceNumber(((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Key, _invoice.Number.Value))
            {
                General.ErrorMessage("Υπήρξε πρόβλημα με την αρίθμηση του παραστατικού.");
                return result;
            }

            _invoice.Location = TextBoxPublishedLocation.Text;

            _invoice.Enable_hold = CheckBoxHold.Checked ? 1 : 0;

            SetHoldPercentValue();

            _invoice.hold_value_database = _invoice.hold_value;

            _invoice.UserComments = SetTextBoxCommentsTax();

            _invoice.comments = string.Empty;

            _invoice.Invoice_type = ((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Key;
            //_invoice.Is_credit = RadioButtonCredit.Checked ? 1 : 0;
            _invoice.Is_credit = cmbEndeiksh.SelectedIndex;

            _invoice.Subtotal = _invoice.Total;

            decimal tax = 0;
            if (!TextBoxVat.Text.Equals(string.Empty))
            {
                decimal.TryParse(TextBoxVat.Text.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out tax);
            }
            _invoice.Tax = tax;
            _invoice.Is_deleted = 0;

            if (_invoice.Status != 1 || _invoice.Status == null)
            {
                _invoice.Status = 0;
            }

            // update invoice + invoice order not null OR
            // insert invoice + has order == 1
            if ((_invoice.Id.HasValue && _invoice.InvoiceOrder != null) ||
                (!_invoice.Id.HasValue && Program.seal.HasOrder == 1))
            {
                _invoice.InvoiceOrder = TextBoxInvoiceOrder.Text;
            }

            // ean einai gia deltio apostolhs mpainoun kai ta aparaithta pedia, allios menoun null
            if (GroupBoxDeltio.Enabled)
            {
                _invoice.Destination = TextBoxDestination.Text;
                _invoice.LoadingPlace = TextBoxLoadingPlace.Text;
                _invoice.MovingPurpose = TextBoxMovingPurpose.Text;
            }

            //removeTotal();
            if (_invoice.save())
            {
                //labelPrintStatus.Text = "Επιτυχής αποθήκευση";
                lblAnyMessage.Text = "Επιτυχής αποθήκευση";
                _invoice = InvoiceDAL.getById(_invoice.Id.HasValue ? _invoice.Id.Value : -1);
                result = true;
            }
            else
            {
                //labelPrintStatus.Text = "Η αποθήκευση απέτυχε";
                lblAnyMessage.Text = "Η αποθήκευση απέτυχε";
                //addTotal(); 
            }

            return result;
        }

        private void SetHoldPercentValue()
        {
            decimal hold = decimal.Zero;
            if (CheckBoxHold.Checked)
            {
                if (_isInInitialization) return;
                if (!decimal.TryParse(TextBoxHold.Text.Replace("%", ""), out hold))
                {
                    //General.ExclamationMessage("Δεν έχετε εισάγει αριθμητικό στο πεδίο παρακράτησης");
                    _invoice.Hold_percent = 0;
                    return;
                }
                _invoice.Hold_percent = hold / 100;
            }
            else
            {
                _invoice.Hold_percent = hold / 100;
            }
        }

        private string SetTextBoxCommentsTax(bool textChanged = false)
        {
            string comments = string.Empty;
            SetHoldPercentValue();
            string holdPercent = String.Format("{0:N}", _invoice.Hold_percent);

            if (textChanged && !_isInInitialization)
            {
                TextboxCommentsRemoveTax();
                comments = "Παρακρατήθηκε φόρος " + holdPercent + "% ίσος με " +
                                       General.formatAmount(_invoice.hold_value.Value) +
                                       Environment.NewLine + ΤextBoxComments.Text;
            }
            else
            {
                if (_invoice.hold_value.HasValue && _invoice.hold_value.Value > 0 && CheckBoxHold.Checked)
                {
                    if (string.IsNullOrEmpty(ΤextBoxComments.Text))
                    {
                        comments = "Παρακρατήθηκε φόρος " + holdPercent + "% ίσος με " + General.formatAmount(_invoice.hold_value.Value);
                    }
                    else
                    {
                        if (ΤextBoxComments.Text.Contains("Παρακρατήθηκε φόρος "))
                        {
                            comments = ΤextBoxComments.Text;
                        }
                        else
                        {
                            comments = "Παρακρατήθηκε φόρος " + holdPercent + "% ίσος με " +
                                       General.formatAmount(_invoice.hold_value.Value) +
                                       Environment.NewLine + ΤextBoxComments.Text;
                        }
                    }
                }
                else
                {
                    TextboxCommentsRemoveTax();
                    comments = string.IsNullOrEmpty(ΤextBoxComments.Text) ? string.Empty : ΤextBoxComments.Text;
                }
            }
            return comments;
        }

        private void TextboxCommentsRemoveTax()
        {
            if (ΤextBoxComments.Text.Contains("Παρακρατήθηκε φόρος "))
            {
                string[] splitParams = new string[] { "\r\n" };
                string commentsWithTax = ΤextBoxComments.Text.Split(splitParams, StringSplitOptions.None)[0] +
                                         Environment.NewLine;
                ΤextBoxComments.Text = ΤextBoxComments.Text.Replace(commentsWithTax, string.Empty);
            }
        }

        private void refreshDataGridInvoiceItems(bool isRowDel = false)
        {
            if (_invoice.InvoiceItems == null || _invoice.InvoiceItems.Count == 0)
            {
                InvoiceDAL.getInvoicesItems(_invoice.Id.HasValue ? _invoice.Id.Value : -1, _invoice.Tax);
            }
            else
            {
                if (isRowDel) //esvise prosorina
                {
                    _invoice.InvoiceItems.RemoveAll(delegate(InvoiceItem item) { return item.is_deleted == 1; });
                }
            }

            decimal tax = 0;
            if (!TextBoxVat.Text.Equals(string.Empty))
            {
                decimal.TryParse(TextBoxVat.Text.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out tax);
            }
            _invoice.Tax = tax;

            DataGridViewInvoiceItems.DataSource = null;

            if (_invoice.InvoiceItems.Count > 0)
            {
                //LabelTotal.Text = General.formatAmount(_invoice.Total.Value);
                //LabelVat.Text = General.formatAmount((_invoice.totalWithVat.Value - _invoice.Total.Value));
                //LabelTotalWithVat.Text = General.formatAmount(_invoice.totalWithVat.Value);

                LabelTotal.Text = _invoice.Total.Value.ToString(); //General.BankersRound.RoundAwayFromZero(_invoice.Total.Value, 2).ToString();
                LabelVat.Text = _invoice.vatPrice.Value.ToString(); //General.BankersRound.RoundAwayFromZero((_invoice.totalWithVat.Value - _invoice.Total.Value), 2).ToString();
                LabelTotalWithVat.Text = (_invoice.Total.Value + _invoice.vatPrice.Value).ToString();//General.BankersRound.RoundAwayFromZero(_invoice.totalWithVat.Value, 2).ToString();
            }
            else
            {
                LabelTotal.Text = "0 €";
                LabelVat.Text = "0 €";
                LabelTotalWithVat.Text = "0 €";
            }

            DataGridViewInvoiceItems.DataSource = _invoice.InvoiceItems;
            DataGridViewInvoiceItems.Invalidate();
        }

        private void InitComboBoxEndeikseis()
        {
            List<KeyValuePair<int, string>> choices = new List<KeyValuePair<int, string>>();
            choices.Add(new KeyValuePair<int, string>(0, "Μετρητοίς"));
            choices.Add(new KeyValuePair<int, string>(1, "Με πίστωση"));
            cmbEndeiksh.ValueMember = "Key";
            cmbEndeiksh.DisplayMember = "Value";
            cmbEndeiksh.DataSource = choices;
        }

        /// <summary>
        /// Βάζει τιμές στο combobox με τους τύπους αποδείξεων
        /// </summary>
        private void InitComboBoxInvoiceTypes()
        {
            ComboBoxInvoiceType.ValueMember = "Key";
            ComboBoxInvoiceType.DisplayMember = "Value";
            ComboBoxInvoiceType.DataSource = InvoiceDAL.getInvoiceTypes();
        }

        /// <summary>
        /// Διάφορες επιλογές ανάλογα με το ποιός τύπος απόδειξης επιλέχθηκε
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int key = ((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Key;

            TextBoxInvoiceNumber.Text = InvoiceDAL.getInvoiceNumber(key).ToString();

            GroupBoxDeltio.Enabled = (InvoiceDAL.getInvoiceTypeLabelById(key) == "deltio" || InvoiceDAL.getInvoiceTypeLabelById(key) == "timol_deltio");

            //if (key == 1 || key > 2)
            //{
            //    RadioButtonCash.Checked = true;
            //    RadioButtonCash.Enabled = false;
            //    RadioButtonCredit.Enabled = false;
            //}
            //else
            //{
            //    RadioButtonCash.Enabled = true;
            //    RadioButtonCredit.Enabled = true;
            //}
        }

        /// <summary>
        /// Ενεργοποιεί ή απενεργοποιεί την παρακράτηση
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxHold_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxHold.Checked)
            {
                TextBoxHold.Enabled = true;
            }
            else
            {
                TextBoxHold.Enabled = false;
            }
            ΤextBoxComments.Text = SetTextBoxCommentsTax();
        }

        private void InitTextBoxTaxOffices()
        {
            //TextBoxCustomerTaxOffice.AutoCompleteCustomSource = InvoiceDAL.getTaxOffices();
        }

        private void InitTextBoxTaxNumbers()
        {
            //TextBoxCustomerTaxNumber.AutoCompleteCustomSource = InvoiceDAL.getTaxNumbers();
        }

        private void InitComboCustomers()
        {
            cmbSavedClients.ValueMember = "Key";
            cmbSavedClients.DisplayMember = "Value";
            cmbSavedClients.DataSource = InvoiceDAL.getCustomerList().OrderBy(x => x.Value).ToList();
        }

        private void GetAfmDetailsFromGSIS()
        {
            u = new RgWsBasStoixNRtUser();
            BackgroundWorker workerLogin = new BackgroundWorker();
            workerLogin.WorkerReportsProgress = true;
            workerLogin.DoWork += new DoWorkEventHandler(doSearch);
            workerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeSearch);
            workerLogin.RunWorkerAsync();

            TextBoxCustomerTaxNumber.Enabled = false;
            //ButtonSearchCustomer.Enabled = false;
            TextBoxCustomerName.Enabled = false;
            TextBoxCustomerDescription.Enabled = false;
            TextBoxCustomerAddress.Enabled = false;
            TextBoxCustomerTaxOffice.Enabled = false;
            //PanelLoading.BringToFront();
            //PanelLoading.Visible = true;
        }

        public void completeSearch(object sender, RunWorkerCompletedEventArgs e)
        {
            if (u.onomasia != null)
            {
                TextBoxCustomerName.Text = u.onomasia != null ? u.onomasia.Trim() : string.Empty;
                TextBoxCustomerDescription.Text = u.actLongDescr != null ? u.actLongDescr.Trim() : string.Empty;
                TextBoxCustomerAddress.Text =
                    (u.postalAddress != null ? u.postalAddress.Trim() : string.Empty) + " " + (u.postalAddressNo != null ? u.postalAddressNo.Trim() : string.Empty) + ", " +
                    (u.postalZipCode != null ? u.postalZipCode.Trim() : string.Empty) + " " + (u.parDescription != null ? u.parDescription.Trim() : string.Empty);
                TextBoxCustomerTaxNumber.Text = u.afm != null ? u.afm.Trim() : string.Empty;
                TextBoxCustomerTaxOffice.Text = u.doyDescr != null ? u.doyDescr.Trim() : string.Empty;

                if (u.stopDate != null)
                {
                    General.ExclamationMessage("Το ΑΦΜ ανήκει σε απενεργοποιημένη εταιρεία");
                    throw new Exception("DeletedAFM");
                }
                _afmIsFound = true;
            }
            else
            {
                _afmIsFound = false;
            }

            //PanelLoading.Visible = false;
            TextBoxCustomerTaxNumber.Enabled = true;
            //ButtonSearchCustomer.Enabled = true;
            TextBoxCustomerName.Enabled = true;
            TextBoxCustomerDescription.Enabled = true;
            TextBoxCustomerAddress.Enabled = true;
            TextBoxCustomerTaxOffice.Enabled = true;
            TextBoxCustomerName.Focus();

            if (!_afmIsFound)
            {
                bool found = false;
                Dictionary<string, string> customer = InvoiceDAL.getCustomer(TextBoxCustomerTaxNumber.Text, out found);
                if (found)
                {
                    TextBoxCustomerName.Text = customer["customer_name"];
                    TextBoxCustomerAddress.Text = customer["customer_address"];
                    TextBoxCustomerDescription.Text = customer["customer_description"];
                    TextBoxCustomerTaxOffice.Text = customer["customer_taxoffice"];
                }
                else
                {
                    TextBoxCustomerName.Text = string.Empty;
                    TextBoxCustomerAddress.Text = string.Empty;
                    TextBoxCustomerDescription.Text = string.Empty;
                    TextBoxCustomerTaxOffice.Text = string.Empty;
                    General.InformationMessage("Δεν βρέθηκε πελάτης με αυτό το ΑΦΜ στην βάση δεδομένων της Διαχείρισης Παραστατικών");
                }
            }
        }

        public void doSearch(object sender, DoWorkEventArgs e)
        {
            RgWsBasStoixN c = new RgWsBasStoixN();
            GenWsErrorRtUser wse = new GenWsErrorRtUser();
            decimal d = 1;
            try
            {
                c.rgWsBasStoixN(TextBoxCustomerTaxNumber.Text, ref u, ref d, ref wse);
            }
            catch (Exception ex)
            {
            }
        }

        private void TextBoxVat_TextChanged(object sender, EventArgs e)//todo
        {
            decimal tax = 0;
            if (!TextBoxVat.Text.Equals(string.Empty))
            {
                decimal.TryParse(TextBoxVat.Text.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out tax);
            }
            _invoice.Tax = tax;

            if (_invoice.InvoiceItems != null && _invoice.InvoiceItems.Count > 0)
            {
                LabelVat.Text = General.formatAmount((_invoice.totalWithVat.Value - _invoice.Total.Value));
                LabelTotal.Text = General.formatAmount(_invoice.Total.Value);
                LabelTotalWithVat.Text = General.formatAmount(_invoice.totalWithVat.Value);
            }
        }

        private void GroupBoxExchanges_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridViewInvoiceItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewInvoiceItems.Columns[e.ColumnIndex].Name == "Delete")
            {
                try
                {
                    int invoiceId = (int)DataGridViewInvoiceItems.Rows[e.RowIndex].Cells["ID"].Value;
                    _itemsToDelete.Add(invoiceId);
                }
                catch
                {
                    //to kanoume gargara, giati an den exei id shmainei oti einai apo neo parastatiko, eno
                    //an exei shmainei oti anoikse apo palio parastatiko kai ara tha exei id gia na svhsei
                    //meta
                }
                ((List<InvoiceItem>)DataGridViewInvoiceItems.DataSource).RemoveAt(e.RowIndex);
            }
            refreshDataGridInvoiceItems(true);
        }

        private void ButtonReportSave_Click(object sender, EventArgs e)
        {
            List<InvoiceItem> toBePrinted = new List<InvoiceItem>();

            if (saveInvoiceGroup())
            {
                if (_invoice.Id != null)
                {
                    foreach (InvoiceItem item in _invoice.InvoiceItems)
                    {
                        if (item.is_deleted == 1 || item.is_deleted == null)
                        {
                            continue; // ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ ΚΑΙ ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                        }
                        toBePrinted.Add(item);
                    }
                }

                if (toBePrinted.Count == 0)
                {
                    General.ExclamationMessage("Δεν βρέθηκαν αιτιολογίες προς αποθήκευση");
                }
                else
                {
                    SealDetails seal = InvoiceDAL.getSeal();

                    if (seal.id.HasValue && _invoice.Id.HasValue)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        if (!string.IsNullOrEmpty(Settings.Default.initial_directory))
                        {
                            dialog.RestoreDirectory = true;
                            dialog.InitialDirectory = Settings.Default.initial_directory;
                        }
                        dialog.Filter = "Microsoft Word 97 - 2003 Document|*.doc"; //|Microsoft Word 2007 - 2013|*.docx
                        dialog.FileName = _invoice.Number + ". " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + _invoice.Customer_name;
                        dialog.ShowDialog();
                        string path = Path.GetDirectoryName(dialog.FileName);
                        if (!string.IsNullOrEmpty(path))
                        {
                            string fileName = dialog.FileName;
                            // prota elegxei ean mporei na sosei san WORD me ton kainourio tropo
                            if (Settings.Default.use_old_print) // bilias settings
                            {
                                FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, false);
                                frm.SaveToDoc(seal.id.Value, _invoice.Id.Value, fileName, true);
                                //labelPrintStatus.Text = frm.SavedLocation;
                                lblAnyMessage.Text = frm.SavedLocation;
                            }
                            else
                            {
                                if (!Report.SaveToWord(seal, _invoice, fileName))
                                {
                                    // kai ean den mporei tote proxoraei me ton palio tropo
                                    //FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, false);
                                    //frm.SaveToDoc(seal.id.Value, _invoice.Id.Value, fileName, true);
                                    //lblAnyMessage.Text = frm.SavedLocation;
                                    General.InformationMessage("Αυτή η λειτουργία δεν υποστηρίζεται.\n\nΕπικοινωνήστε με τον διαχειριστή στο 213-0267712");
                                }
                                else
                                {
                                    lblAnyMessage.Text = "Το αρχείο αποθηκεύτηκε στο: " + path + ".";
                                }
                            }
                        }
                        else
                        {
                            // min kaneis tipota efoson den epelekse kapou na sosei to arxeio
                            lblAnyMessage.Text = "Δεν επιλέξατε που θα αποθηκεύσετε το αρχείο.";
                        }
                    }
                }
                refreshDataGridInvoiceItems();
            }
        }

        private void ButtonPdfReportSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.foxit_path) && string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.adobe_path))
            {
                General.ErrorMessage("Για την εξαγωγή του τιμολογίου σε PDF πρέπει να ορίσετε τη θέση του προγράμματος Adobe Reader ή Foxit Reader");
                return;
            }

            List<InvoiceItem> toBePrinted = new List<InvoiceItem>();

            if (saveInvoiceGroup())
            {
                if (_invoice.Id != null)
                {
                    foreach (InvoiceItem item in _invoice.InvoiceItems)
                    {
                        if (item.is_deleted == 1 || item.is_deleted == null)
                        {
                            continue; // ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ ΚΑΙ ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                        }
                        toBePrinted.Add(item);
                    }
                }

                if (toBePrinted.Count == 0)
                {
                    General.ExclamationMessage("Δεν βρέθηκαν αιτιολογίες προς αποθήκευση");
                }
                else
                {
                    SealDetails seal = InvoiceDAL.getSeal();
                    if (seal.id.HasValue && _invoice.Id.HasValue)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        if (!string.IsNullOrEmpty(Settings.Default.initial_directory))
                        {
                            dialog.RestoreDirectory = true;
                            dialog.InitialDirectory = Settings.Default.initial_directory;
                        }
                        dialog.Filter = "Portable Document Format (PDF)|*.pdf";
                        dialog.FileName = _invoice.Number + ". " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + _invoice.Customer_name;
                        dialog.ShowDialog();
                        string path = Path.GetDirectoryName(dialog.FileName);
                        if (!string.IsNullOrEmpty(path))
                        {
                            string fileName = dialog.FileName;
                            if (!Report.SaveToWord(seal, _invoice, fileName, true))
                            {
                                // kai ean den mporei tote proxoraei me ton palio tropo
                                //FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, false);
                                //frm.SaveToDoc(seal.id.Value, _invoice.Id.Value, fileName, true, true);
                                //lblAnyMessage.Text = frm.SavedLocation;
                                General.InformationMessage("Αυτή η λειτουργία δεν υποστηρίζεται.\n\nΕπικοινωνήστε με τον διαχειριστή στο 213-0267712");
                                return;
                            }

                            Report.SaveToPdf(seal.id.Value, _invoice.Id.Value, fileName);
                            lblAnyMessage.Text = "Το αρχείο αποθηκεύτηκε στο: " + path + ".";
                        }
                        else
                        {
                            // min kaneis tipota efoson den epelekse kapou na sosei to arxeio
                            lblAnyMessage.Text = "Δεν επιλέξατε που θα αποθηκεύσετε το αρχείο.";
                        }
                    }
                }
                refreshDataGridInvoiceItems();
            }
        }

        private void TextBoxHold_TextChanged(object sender, EventArgs e)
        {
            ΤextBoxComments.Text = SetTextBoxCommentsTax(true);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.foxit_path) && string.IsNullOrEmpty(InvoicesGenerator.Properties.Settings.Default.adobe_path))
            {
                General.ErrorMessage("Για την εξαγωγή του τιμολογίου σε PDF πρέπει να ορίσετε τη θέση του προγράμματος Adobe Reader ή Foxit Reader");
                return;
            }

            List<InvoiceItem> toBePrinted = new List<InvoiceItem>();

            if (saveInvoiceGroup())
            {
                if (_invoice.Id != null)
                {
                    foreach (InvoiceItem item in _invoice.InvoiceItems)
                    {
                        if (item.is_deleted == 1 || item.is_deleted == null)
                        {
                            continue; // ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ ΚΑΙ ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                        }
                        toBePrinted.Add(item);
                    }
                }

                if (toBePrinted.Count == 0)
                {
                    General.ExclamationMessage("Δεν βρέθηκαν αιτιολογίες προς αποθήκευση");
                }
                else
                {
                    SealDetails seal = InvoiceDAL.getSeal();
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\InvoicesGenerator\\invoice_generator.doc";
                    if (seal.id.HasValue && _invoice.Id.HasValue)
                    {
                        if (Settings.Default.use_old_print)
                        {
                            FormWebViewPrint frm = new FormWebViewPrint(_invoice.Id.Value, false);
                            Application.DoEvents();
                            frm.Print();
                        }
                        else
                        {
                            // PROSOXH: afth h leitourgeia einai gia tis pollaples ektyposeis, kai den paizei me to palio systhma. ean xreiastei tha ftiaxtei!
                            for (var i = 1; i <= Settings.Default.times_to_print; i++)
                            {
                                string endeiksh = Settings.Default["print_desc_" + i].ToString();
                                if (!Report.SaveToWord(seal, _invoice, fileName, true, endeiksh))
                                {
                                    General.InformationMessage("Αυτή η λειτουργία δεν υποστηρίζεται.\n\nΕπικοινωνήστε με τον διαχειριστή στο 213-0267712");
                                    return;
                                }

                                Report.SaveToPdf(seal.id.Value, _invoice.Id.Value, fileName, true);
                            }
                        }
                        lblAnyMessage.Text = "Το αρχείο εκτυπώθηκε.";
                    }
                }
                refreshDataGridInvoiceItems();
            }
        }

        private void cmbSavedClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInInitialization) return;

            bool found = false;
            string selectedTax = cmbSavedClients.SelectedValue.ToString();
            Dictionary<string, string> customer = InvoiceDAL.getCustomer(selectedTax, out found);
            if (found)
            {
                TextBoxCustomerName.Text = customer["customer_name"];
                TextBoxCustomerAddress.Text = customer["customer_address"];
                TextBoxCustomerDescription.Text = customer["customer_description"];
                TextBoxCustomerTaxOffice.Text = customer["customer_taxoffice"];
                textBoxCustomerCity.Text = customer["customer_city"];
                textBoxCustomerPostalCode.Text = customer["customer_postalcode"];
                TextBoxCustomerPhone.Text = customer["customer_phone"];
                if (selectedTax.Split(' ').Count() > 1)
                {
                    int index = 0;
                    foreach (string item in cmbCountry.Items)
                    {
                        if (item.Contains(selectedTax.Split(' ')[0]))
                        {
                            cmbCountry.SelectedIndex = index;
                        }
                        index++;
                    }
                    selectedTax = selectedTax.Split(' ')[1];
                }
                TextBoxCustomerTaxNumber.Text = selectedTax;
            }
        }

        private void btnSearchVies_Click(object sender, EventArgs e)
        {
            using (CheckFirm frm = new CheckFirm(true))
            {
                frm.ShowDialog();
                TextBoxCustomerName.Text = frm.CustName;
                TextBoxCustomerAddress.Text = frm.CustAddress;
                TextBoxCustomerTaxNumber.Text = frm.CustAfm;
            }
        }
    }
}
