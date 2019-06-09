using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExchangesGenerator
{
    public partial class FormExchangeGroup : Form
    {
        ExchangeGroup _exchangeGroup = new ExchangeGroup();
        int? _exchangeGroupID = null;
        List<KeyValuePair<int, string>> banks = General.getBanks();
        List<KeyValuePair<int, string>> branches = new List<KeyValuePair<int, string>>();

        public FormExchangeGroup(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }
        public FormExchangeGroup()
        {
            InitializeComponent();
            UserControl.InitControls(this);
        }
        public FormExchangeGroup(int exchangeGroupID)
        {
            InitializeComponent();
            UserControl.InitControls(this);
            _exchangeGroupID = exchangeGroupID;
        }

        // Init Functions
        private void FormExchangeGroup_Load(object sender, EventArgs e)
        {
            initBanks();
            DataGridViewExchanges.AutoGenerateColumns = false;

            TextBoxBankOrder.Text = "KONICA MINOLTA BGR AE - ΑΝΕΞΟΔΟΣ ΕΠΙΣΤΡΟΦΗ";
            TextBoxAcceptedDate.Text = DateTime.Now.ToString("ddMMyy");
            TextBoxAcceptedLocation.Text = "Αθήνα";
            TextBoxPublishedDate.Text = DateTime.Now.ToString("ddMMyy");
            TextBoxPublishedLocation.Text = "Αθήνα";

            if (_exchangeGroupID != null)
            {
                _exchangeGroup = ExchangeGroupDAL.get((int)_exchangeGroupID);

                ComboBoxPron.SelectedIndex = _exchangeGroup.customer_pron;
                TextBoxCustomerName.Text = _exchangeGroup.customer_name;
                TextBoxCustomerCity.Text = _exchangeGroup.customer_city;
                TextBoxCustomerAddress.Text = _exchangeGroup.customer_address;
                TextBoxCustomerAddressNumber.Text = _exchangeGroup.customer_addressnumber;
                TextBoxCustomerFatherName.Text = _exchangeGroup.customer_father;
                TextBoxCustomerIDNumber.Text = _exchangeGroup.customer_idnumber;
                TextBoxCustomerPostNumber.Text = _exchangeGroup.customer_postnumber;
                TextBoxCustomerTaxNumber.Text = _exchangeGroup.customer_taxnumber;
                TextBoxCustomerERPCode.Text = _exchangeGroup.customer_erpcode;

                ComboBoxBankName.Text = _exchangeGroup.bank_name;
                ComboBoxBankBranch.Text = _exchangeGroup.bank_branch;
                TextBoxBankOrder.Text = _exchangeGroup.bank_order;

                TextBoxAcceptedDate.Text = _exchangeGroup.accepted_date.ToString("ddMMyy");
                TextBoxAcceptedLocation.Text = _exchangeGroup.accepted_location;
                TextBoxPublishedDate.Text = _exchangeGroup.published_date.ToString("ddMMyy");
                TextBoxPublishedLocation.Text = _exchangeGroup.published_location;

                TextBoxThirdName.Text = _exchangeGroup.third_name;
                TextBoxThirdNumber.Text = _exchangeGroup.third_number;

                this.Text += " - Αριθμός Πινακίου: " + _exchangeGroup.id.ToString();

                groupBoxExchangesInfo.Visible = true;
                labelCreator.Text = UserDAL.getUsername((int)_exchangeGroup.user_id);
                labelDateCreated.Text = _exchangeGroup.date_created.ToShortDateString();

                refreshDataGridExchanges(false);
            }
        }
        private void initBanks()
        {
            ComboBoxBankName.DisplayMember = "value";
            ComboBoxBankName.ValueMember = "key";
            ComboBoxBankName.DataSource = banks;
            ComboBoxBankName.SelectedIndex = -1;
        }
        private void initBranches(int bank_id)
        {
            branches = General.getBranches(bank_id);
            ComboBoxBankBranch.DisplayMember = "value";
            ComboBoxBankBranch.ValueMember = "key";
            ComboBoxBankBranch.DataSource = branches;
            ComboBoxBankBranch.SelectedIndex = -1;
        }

        // Form Functions
        private void ComboBoxBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxBankName.SelectedValue != null)
            {
                initBranches((int)ComboBoxBankName.SelectedValue);
            }
        }
        private void ButtonSearchCustomer_Click(object sender, EventArgs e)
        {
            bool found = false;
            Dictionary<string, string> customer = ExchangeGroupDAL.getCustomer(TextBoxCustomerERPCode.Text, out found);
            if (found)
            {
                TextBoxCustomerName.Text = customer["customer_name"];
                TextBoxCustomerCity.Text = customer["customer_city"];
                TextBoxCustomerAddress.Text = customer["customer_address"];
                TextBoxCustomerAddressNumber.Text = customer["customer_addressnumber"];
                TextBoxCustomerFatherName.Text = customer["customer_father"];
                TextBoxCustomerIDNumber.Text = customer["customer_idnumber"];
                TextBoxCustomerPostNumber.Text = customer["customer_postnumber"];
                TextBoxCustomerTaxNumber.Text = customer["customer_taxnumber"];
                TextBoxCustomerERPCode.Text = customer["customer_erpcode"];
            }
            else
            {
                General.InformationMessage("Δεν βρέθηκε πελάτης με αυτό το ERP κωδικο στην βάση δεδομένων της 'Διαχείρισης Συναλλαγματικών");
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            saveExchangeGroup();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (_exchangeGroup.id == null)
            {
                General.InformationMessage("Δεν έχει ακόμα αποθηκευτεί η ομάδα των συναλλαγματικών. Δεν υπαρχει λόγος ακύρωσης");
                return;
            }

            if (General.QuestionMessage("Είστε σίγουρος ότι θέλετε να ακυρώσετε το πακέτο. Η ενέργεια δεν μπορέι να αναιρεθεί.") == DialogResult.Yes)
            {
                if (ExchangeGroupDAL.cancel((int)_exchangeGroup.id))
                {
                    LabelStatus.Text = "Όλες οι συναλλαγματικές ακυρώθηκαν";
                    refreshDataGridExchanges(true);
                }
                else
                {
                    LabelStatus.Text = "Αποτυχία Ακυρώσης";
                }
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (_exchangeGroup.id == null)
            {
                General.InformationMessage("Δεν έχει ακόμα αποθηκευτεί η ομάδα των συναλλαγματικών. Δεν υπαρχει λόγος διαγραφής");
                return;
            }
            if (General.QuestionMessage("Είστε σίγουρος ότι θέλετε να διαγράψετε το πακέτο. Η ενέργεια δεν μπορέι να αναιρεθεί.") == DialogResult.Yes)
            {
                if (ExchangeGroupDAL.delete((int)_exchangeGroup.id))
                {
                    this.Close();
                }
            }
        }
        private void ButtonNewExchange_Click(object sender, EventArgs e)
        {
            using (FormExchange exchange = new FormExchange())
            {
                exchange.localExchange = new Exchange();
                exchange.ShowDialog(this);
                if (exchange.localExchange.date > DateTime.Now & exchange.localExchange.amount > 0)
                {
                    Exchange tempExchange = exchange.localExchange;
                    if (tempExchange != null)
                    {
                        if (_exchangeGroup.exchanges == null)
                        {
                            _exchangeGroup.exchanges = new List<Exchange>();
                        }
                        _exchangeGroup.exchanges.Add(tempExchange);

                        refreshDataGridExchanges(false);
                    }
                }
            }
        }
        private void DataGridViewExchanges_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = DataGridViewExchanges.CurrentRow.Index;
            using (FormExchange exchange = new FormExchange())
            {
                exchange.localExchange = _exchangeGroup.exchanges[index];
                exchange.ShowDialog();
                _exchangeGroup.exchanges[index].date = exchange.localExchange.date;
                _exchangeGroup.exchanges[index].amount = exchange.localExchange.amount;

                refreshDataGridExchanges(false);
            }
        }
        private void ButtonGenerateExchanges_Click(object sender, EventArgs e)
        {
            using (FormGenerator formGenerator = new FormGenerator())
            {
                formGenerator.ShowDialog(this);
                List<Exchange> tempExchanges = formGenerator.generatedExchanges;
                if (tempExchanges.Count != 0)
                {
                    if (_exchangeGroup.exchanges == null)
                    {
                        _exchangeGroup.exchanges = new List<Exchange>();
                    }
                    _exchangeGroup.exchanges.AddRange(tempExchanges);
                    refreshDataGridExchanges(false);
                }
            }
        }
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            if (_exchangeGroup.exchanges != null)
            {
                DataGridViewExchanges.DataSource = null;
                _exchangeGroup.exchanges.RemoveAll(delegate(Exchange exc) { return exc.id == null; });
                DataGridViewExchanges.DataSource = _exchangeGroup.exchanges;
            }
        }
        private void ButtonReceipt_Click(object sender, EventArgs e)
        {

            List<Exchange> toBeExported = new List<Exchange>();

            saveExchangeGroup();
            foreach (var exchange in _exchangeGroup.exchanges)
            {
                if (exchange.status == 2)//ΕΚΤΥΠΩΜΕΝΕΣ
                {
                    toBeExported.Add(exchange);
                }
            }

            if (toBeExported.Count == 0)
            {
                General.ExclamationMessage("Δεν βρέθηκαν εκτυπωμένες συναλλαγματικές. Μονο οι ΕΚΤΥΠΩΜΕΝΕΣ συναλλαγματικες μπορουν να εξαχθούν");
            }
            else
            {
                using (FormPrint frm = new FormPrint(_exchangeGroup, toBeExported, "generateReportReceipt"))
                {
                    frm.ShowDialog();
                }
            }
        }
        private void ButtonDelivered_Click(object sender, EventArgs e)
        {

            List<Exchange> toBeExported = new List<Exchange>();

            saveExchangeGroup();
            foreach (var exchange in _exchangeGroup.exchanges)
            {
                if (exchange.status == 2)//ΕΚΤΥΠΩΜΕΝΕΣ
                {
                    toBeExported.Add(exchange);
                }
            }

            if (toBeExported.Count == 0)
            {
                General.ExclamationMessage("Δεν βρέθηκαν εκτυπωμένες συναλλαγματικές. Μονο οι ΕΚΤΥΠΩΜΕΝΕΣ συναλλαγματικες μπορουν να εξαχθούν");
            }
            else
            {
                using (FormPrint frm = new FormPrint(_exchangeGroup, toBeExported, "generateReportDelivered"))
                {
                    frm.ShowDialog();
                }
            }
        }
        private void buttonPreview_Click(object sender, EventArgs e)
        {
            List<Exchange> toBePrinted = new List<Exchange>();
            saveExchangeGroup();

            foreach (var exchange in _exchangeGroup.exchanges)
            {
                if (exchange.status < 3) // ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ ΚΑΙ ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                {
                    toBePrinted.Add(exchange);
                }
            }

            if (toBePrinted.Count == 0)
            {
                General.ExclamationMessage("Δεν βρέθηκαν συναλλαγματικές προς εκτύπωση. Μονο οι ΑΠΟΘΗΚΕΥΜΕΝΕΣ και ΕΚΤΥΠΩΜΕΝΕΣ συναλλαγματικες μπορουν να εκτυπωθούν");
            }
            else
            {
                preview(toBePrinted);
            }
            refreshDataGridExchanges(true);
        }

        // General Functions
        private void saveExchangeGroup()
        {
            if (_exchangeGroup.exchanges == null)
            {
                General.ExclamationMessage("Δεν έχετε εισάγει συναλλαγματικές");
                return;
            }

            _exchangeGroup.customer_pron = ComboBoxPron.SelectedIndex;
            _exchangeGroup.customer_name = TextBoxCustomerName.Text;
            _exchangeGroup.customer_father = TextBoxCustomerFatherName.Text;
            _exchangeGroup.customer_erpcode = TextBoxCustomerERPCode.Text;
            _exchangeGroup.customer_address = TextBoxCustomerAddress.Text;
            _exchangeGroup.customer_city = TextBoxCustomerCity.Text;
            _exchangeGroup.customer_addressnumber = TextBoxCustomerAddressNumber.Text;
            _exchangeGroup.customer_idnumber = TextBoxCustomerIDNumber.Text;
            _exchangeGroup.customer_postnumber = TextBoxCustomerPostNumber.Text;
            _exchangeGroup.customer_taxnumber = TextBoxCustomerTaxNumber.Text;

            _exchangeGroup.bank_name = ComboBoxBankName.Text;
            _exchangeGroup.bank_branch = ComboBoxBankBranch.Text;
            _exchangeGroup.bank_order = TextBoxBankOrder.Text;
            
            _exchangeGroup.accepted_date = DateTime.Parse(TextBoxAcceptedDate.Text);
            _exchangeGroup.accepted_location=TextBoxAcceptedLocation.Text;
            _exchangeGroup.published_date = DateTime.Parse(TextBoxPublishedDate.Text);
            _exchangeGroup.published_location=TextBoxPublishedLocation.Text;

            _exchangeGroup.third_name = TextBoxThirdName.Text;
            _exchangeGroup.third_number = TextBoxThirdNumber.Text;

            if (_exchangeGroup.save())
            {
                LabelStatus.Text = "Επιτυχής αποθήκευση";
            }
            else
            {
                LabelStatus.Text = "Η αποθήκευση απέτυχε";
            }

            _exchangeGroup = ExchangeGroupDAL.get((int)_exchangeGroup.id);
            refreshDataGridExchanges(true);

            groupBoxExchangesInfo.Visible = true;
            labelCreator.Text = UserDAL.getUsername((int)_exchangeGroup.user_id);
            labelDateCreated.Text = _exchangeGroup.date_created.ToShortDateString();
        }
        private void refreshDataGridExchanges(bool reload)
        {
            if (reload)
            {
                _exchangeGroup.exchanges = ExchangeDAL.getExchanges((int)_exchangeGroup.id);
            }

            _exchangeGroup.exchanges.Sort(
                    delegate(Exchange p1, Exchange p2)
                    {
                        return p1.date.CompareTo(p2.date);
                    }
                );

            DataGridViewExchanges.DataSource = null;
            DataGridViewExchanges.DataSource = _exchangeGroup.exchanges;


            groupBoxExchangesInfo.Visible = true;

            labelDates.Text = _exchangeGroup.exchanges.First().date.ToString("dd/MM/yyyy") + " - " +
                 _exchangeGroup.exchanges.Last().date.ToString("dd/MM/yyyy");

            decimal total = 0;
            foreach (var exchange in _exchangeGroup.exchanges)
            {
                total += exchange.amount;
            }
            labelTotalAmount.Text = General.formatAmount(total).ToString();
        }
        private void preview(List<Exchange> exchanges)
        {
            using (FormPrint frm = new FormPrint(_exchangeGroup, exchanges, "generateExchanges"))
            {
                frm.ShowDialog();
            }
        }
    }
}
