using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator;
using System.Globalization;
using InvoicesGenerator.Properties;

namespace InvoicesGenerator
{
    public partial class FormInvoice : Form
    {
        private InvoiceItem _localInvoiceItem;
        private decimal _vat;
        private decimal _quantity;
        private decimal _finalPrice;
        private decimal _finalUnitPrice;
        private decimal _vatPrice;
        private List<ExistingDescriptions> _existingInvoiceDescriptions;

        public bool isSaved { get; set; }
        private bool isNew;

        public InvoiceItem localInvoiceItem
        {
            get { return _localInvoiceItem; }
        }

        //Παίρνει το status απο το τιμολόγιο
        public int status { get; set; }

        public FormInvoice(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }

        public FormInvoice(InvoiceItem localInvoiceItem, decimal vat)
        {
            InitializeComponent();
            UserControl.InitControls(this);

            if (localInvoiceItem == null)
            {
                _localInvoiceItem = new InvoiceItem();
            }
            else
            {
                _localInvoiceItem = localInvoiceItem;
            }
            _vat = vat;
            decimal total = _localInvoiceItem.total.HasValue ? _localInvoiceItem.total.Value : 0;
            _vatPrice = ((total * _vat) - total);
            txtVat.Text = _vatPrice.ToString();
            txtFinalPrice.Text = General.formatAmount(total * _vat);
        }

        private void FormInvoice_Load(object sender, EventArgs e)
        {
            comboBoxMetricUnit.ValueMember = "Key";
            comboBoxMetricUnit.DisplayMember = "Value";
            comboBoxMetricUnit.DataSource = InvoiceDAL.getMetricTypes();
            if (Settings.Default.show_metric_unit || !string.IsNullOrEmpty(_localInvoiceItem.MetricUnit))
            {
                labelMetricUnit.Visible = true;
                comboBoxMetricUnit.Visible = true;
            }

            if (_localInvoiceItem != null)
            {
                txtQuantity.Text = _localInvoiceItem.amount.ToString();
                txtUnitPrice.Text = String.Format("{0:N}", _localInvoiceItem.unit);

                TextBoxDescription.Text = !string.IsNullOrEmpty(_localInvoiceItem.description) ? _localInvoiceItem.description : Settings.Default.default_invoice_description;

                if (!string.IsNullOrEmpty(_localInvoiceItem.MetricUnit))
                {
                    comboBoxMetricUnit.SelectedValue = _localInvoiceItem.MetricUnit;
                }

                if (txtQuantity.Text == string.Empty)
                {
                    isNew = true;
                    txtQuantity.Text = "1,00";
                }
                else if (txtUnitPrice.Text == string.Empty)
                {
                    isNew = true;
                    txtUnitPrice.Text = "0,00";
                }
            }

            isSaved = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            switch (status)
            {
                case 2: //ΕΚΤΥΠΩΜΕΝΕΣ
                    txtUnitPrice.Enabled = false;
                    TextBoxDescription.Enabled = false;
                    txtQuantity.Enabled = false;
                    break;
                case 3: //ΑΚΥΡΩΜΕΝΕΣ
                    txtUnitPrice.Enabled = false;
                    TextBoxDescription.Enabled = false;
                    txtQuantity.Enabled = false;
                    break;
                case 0: //ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ
                case 1: //ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                default:
                    //buttonCancel.Enabled = false;
                    //buttonDelete.Enabled = false;
                    break;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _existingInvoiceDescriptions = InvoiceDAL.getExistingInvoiceDescriptions();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_existingInvoiceDescriptions.Count > 0)
            {
                DataGridViewExistingDescription.AutoGenerateColumns = false;
                DataGridViewExistingDescription.DataSource = _existingInvoiceDescriptions;
            }
            else
            {
                groupBoxExistingDescriptions.Enabled = false;
            }
        }

        private void DataGridViewExistingDescription_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = DataGridViewExistingDescription.CurrentRow.Index;
            TextBoxDescription.Text = DataGridViewExistingDescription.Rows[index].Cells["Description"].Value.ToString();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxDescription.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ονομασία αιτιολογίας");
                return;
            }

            if (txtUnitPrice.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ποσό");
                return;
            }

            if (txtQuantity.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ποσότητα");
                return;
            }

            string Str = txtUnitPrice.Text.Trim();
            decimal Num;
            bool isNum = decimal.TryParse(Str, out Num);
            if (!isNum)
            {
                General.ExclamationMessage("Δεν πληκτρολογίσατε σωστό αριθμό στο συνολικό ποσό");
                return;
            }

            decimal inNum;
            isNum = decimal.TryParse(txtQuantity.Text, out inNum);
            if (!isNum)
            {
                General.ExclamationMessage("Δεν πληκτρολογίσατε σωστό αριθμό στην ποσότητα");
                return;
            }

            if (decimal.Parse(txtQuantity.Text) == 0)
            {
                General.ExclamationMessage("Η ποσότητα πρέπει να είναι τουλάχιστον 1");
                txtQuantity.Text = "1,00";
                return;
            }

            _localInvoiceItem.amount = inNum;
            _localInvoiceItem.description = TextBoxDescription.Text;
            _localInvoiceItem.unit = _finalUnitPrice;
            _localInvoiceItem.total = _finalPrice - _vatPrice;
            _localInvoiceItem.vatPrice = _vatPrice;
            _localInvoiceItem.MetricUnit = Settings.Default.show_metric_unit ? ((KeyValuePair<string, string>)comboBoxMetricUnit.SelectedItem).Key : "";
            isSaved = true;

            this.Close();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (!InvoiceDAL.delete(_localInvoiceItem.id.HasValue ? _localInvoiceItem.id.Value : -1))
            {
                General.ExclamationMessage("Η αιτιολογία δεν διαγράφτηκε");
            }
            else
            {
                _localInvoiceItem.is_deleted = 1;
                this.Close();
            }
        }

        private void TextBoxDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            txtFinalPrice.TextChanged -= new EventHandler(txtFinalPrice_TextChanged);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            String qnt = txtQuantity.Text.Replace(".", nfi.NumberDecimalSeparator).Replace(",", nfi.NumberDecimalSeparator);
            if (!decimal.TryParse(qnt, NumberStyles.Any, nfi, out _quantity))
            {
                _quantity = 0;
            }
            _finalPrice = _finalUnitPrice * (1 + (_vat / 100)) * _quantity;

            // banker
            //_finalPrice = General.BankersRound.RoundAwayFromZero(_finalPrice, 2);

            txtFinalPrice.Text = General.BankersRound.RoundAwayFromZero(_finalPrice, 2).ToString("0.00");
            txtFinalPrice.TextChanged += new EventHandler(txtFinalPrice_TextChanged);
            txtFinalPrice_TextChanged("Quantity", null);
        }

        private void txtFinalPrice_TextChanged(object sender, EventArgs e)
        {
            bool unitPriceChanges = false;
            if (sender.Equals("UnitPrice"))
            {
                unitPriceChanges = true;
            }
            if (sender.Equals("Quantity"))
            {
                unitPriceChanges = true;
            }

            txtUnitPrice.TextChanged -= new EventHandler(txtUnitPrice_TextChanged);
            txtQuantity.TextChanged -= new EventHandler(txtQuantity_TextChanged);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            String fprice = txtFinalPrice.Text.Replace(".", nfi.NumberDecimalSeparator).Replace(",", nfi.NumberDecimalSeparator);
            if (!decimal.TryParse(fprice, NumberStyles.Any, nfi, out _finalPrice))
            {
                _finalPrice = 0;
            }

            if (_quantity > 0)
            {
                _finalUnitPrice = unitPriceChanges ? _finalUnitPrice : (_finalPrice / (1 + (_vat / 100))) / _quantity;

                // banker

            }

            if (!unitPriceChanges)
            {
                txtUnitPrice.Text = _finalUnitPrice.ToString("0.00");
            }

            // banker
            _vatPrice = General.BankersRound.RoundAwayFromZero((_finalPrice - (_finalUnitPrice * _quantity)), 2);
            _finalUnitPrice = General.BankersRound.RoundAwayFromZero(_finalUnitPrice, 2);

            txtVat.Text = _vatPrice.ToString("0.00");

            txtUnitPrice.TextChanged += new EventHandler(txtUnitPrice_TextChanged);
            txtQuantity.TextChanged += new EventHandler(txtQuantity_TextChanged);
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            txtFinalPrice.TextChanged -= new EventHandler(txtFinalPrice_TextChanged);
            txtQuantity.TextChanged -= new EventHandler(txtQuantity_TextChanged);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";

            String unitprice = txtUnitPrice.Text;//.Replace(".", ",");
            if (!decimal.TryParse(unitprice, NumberStyles.Any, nfi, out _finalUnitPrice))
            {
                _finalUnitPrice = 0;
            }
            //_finalPrice = _finalUnitPrice * _quantity;
            _finalPrice = _finalUnitPrice * (1 + (_vat / 100)) * _quantity;

            // banker
            _finalPrice = General.BankersRound.RoundAwayFromZero(_finalPrice, 2);

            txtFinalPrice.Text = _finalPrice.ToString("0.00");
            txtFinalPrice.TextChanged += new EventHandler(txtFinalPrice_TextChanged);
            txtQuantity.TextChanged += new EventHandler(txtQuantity_TextChanged);
            txtFinalPrice_TextChanged("UnitPrice", null);
        }

        private void TextBoxDescription_Click(object sender, EventArgs e)
        {
            //TextBoxDescription.SelectAll();
        }

        private void TextBoxAmount_Click(object sender, EventArgs e)
        {
            txtUnitPrice.SelectAll();
        }

        private void TextBoxInputAmount_Click(object sender, EventArgs e)
        {
            txtQuantity.SelectAll();
        }

        private void TextBoxAmount_Leave(object sender, EventArgs e)
        {
            //NumberFormatInfo nfi = new NumberFormatInfo();
            //nfi.NumberDecimalSeparator = ",";
            //txtUnitPrice.Text = String.Format(nfi,"{0:N}", decimal.Parse(txtUnitPrice.Text.Equals("") ? "0" : txtUnitPrice.Text));
        }

        private void GroupBoxPricing_Enter(object sender, EventArgs e)
        {

        }
    }
}
