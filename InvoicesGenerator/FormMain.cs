using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator.Classes;
using InvoicesGenerator;
using InvoicesGenerator.gr.gsis.www1;

namespace InvoicesGenerator
{
    public partial class FormMain : Form
    {
        //CheckFirm formCheckFirm = new CheckFirm();

        public FormMain(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }

        public FormMain()
        {
            InitializeComponent();
            UserControl.InitControls(this);
        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            DataGridViewInvoices.AutoGenerateColumns = false;
            InitComboBoxInvoiceTypes();

            DateTimePickerFrom.Value = DateTime.Today.AddMonths(-1);
            DateTimePickerTo.Value = DateTime.Today;

            var seal = InvoiceDAL.getSeal();
            if (seal == null || seal.id == null || seal.id < 0)
            {
                using (FormSeal frm = new FormSeal())
                {
                    DialogResult result = frm.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }

            Program.seal = seal;

            ButtonSearch.PerformClick();
        }

        private void InitComboBoxInvoiceTypes()
        {
            ComboBoxInvoiceType.DataSource = InvoiceDAL.getInvoiceTypes(true);
            ComboBoxInvoiceType.ValueMember = "Key";
            ComboBoxInvoiceType.DisplayMember = "Value";
        }

        private void ButtonNewInvoiceGroup_Click(object sender, EventArgs e)
        {
            using (FormInvoiceGroup invoiceGroup = new FormInvoiceGroup())
            {
                invoiceGroup.ShowDialog();
                if (!invoiceGroup.blank)
                {
                    ButtonSearch.PerformClick();
                }
            }
        }

        private void DataGridViewInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewInvoices.CurrentRow != null && DataGridViewInvoices.CurrentRow.Index > -1)
            {
                //try
                //{
                    int index = DataGridViewInvoices.CurrentRow.Index;
                    int exchangeGroupID = (int) DataGridViewInvoices.Rows[index].Cells["ID"].Value;
                    using (FormInvoiceGroup invoiceGroup = new FormInvoiceGroup(exchangeGroupID))
                    {
                        invoiceGroup.ShowDialog();
                        generateDataGrid(
                            InvoiceDAL.search(
                                DateTimePickerFrom.Value,
                                DateTimePickerTo.Value,
                                TextBoxCompany.Text,
                                TextBoxTaxNumber.Text,
                                ((KeyValuePair<int,string>)ComboBoxInvoiceType.SelectedItem).Key));
                    }
                //}
                //catch
                //{
                //    General.ErrorMessage("Μόλις προσπάθησες να πατήσεις ένα invoice απο μια λίστα που πιθανώς να μην μπορεί να βρεί ποιό invoice_group είναι.");
                //}
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            generateDataGrid(
                InvoiceDAL.search(
                    DateTimePickerFrom.Value,
                    DateTimePickerTo.Value,
                    TextBoxCompany.Text,
                    TextBoxTaxNumber.Text,
                    ((KeyValuePair<int,string>)ComboBoxInvoiceType.SelectedItem).Key));
        }

        private void ButtonDatesSearch_Click(object sender, EventArgs e)
        {

            generateDataGrid(
                InvoiceDAL.search(
                    DateTimePickerFrom.Value,
                    DateTimePickerTo.Value,
                    TextBoxCompany.Text,
                    TextBoxTaxNumber.Text,
                    ((KeyValuePair<int,string>)ComboBoxInvoiceType.SelectedItem).Key));
        }

        private void generateDataGrid(List<Invoice> invoices)
        {
            SortableBindingList<Invoice> invoiceBinding = new SortableBindingList<Invoice>();
            int deleted = 0;
            foreach (var items in invoices)
            {
                if (items.Is_deleted == 1)
                {
                    deleted += 1;
                    continue; 
                }

                Invoice invoice = new Invoice();
                invoice.Id = items.Id;
                invoice.Customer_name = items.Customer_name;
                invoice.Customer_address = items.Customer_address;
                invoice.TotalVF = decimal.Parse(String.Format("{0:N}", items.TotalVF));
                invoice.Status = items.Status;
                invoice.Date = items.Date;
                invoice.Number = items.Number;
                invoice.Subtotal = decimal.Parse(String.Format("{0:N}", items.Subtotal));
                invoice.Tax = decimal.Parse(String.Format("{0:N}", items.TaxNumeric));
                invoice.Customer_taxnumber = items.Customer_taxnumber;

                invoiceBinding.Add(invoice);
            }
            DataGridViewInvoices.DataSource = invoiceBinding;

            int count = invoices.Count - deleted;
            LabelStatus.Text = (count == 1) ? "Bρέθηκε 1 παραστατικό" : "Bρέθηκαν " + count + " παραστατικά";
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            using (FormSeal frmSeal = new FormSeal())
            {
                frmSeal.ShowDialog();
            }
        }

        private void ButtonHelp_Click(object sender, EventArgs e)
        {
            using (FormAbout frmSettings = new FormAbout())
            {
                frmSettings.ShowDialog();
            }
        }

        private void tsBtnCentralized_Click(object sender, EventArgs e)
        {
            using(FormWebViewPrint frm =new FormWebViewPrint())
            {
                frm.ShowDialog();
            }
        }

        private void TextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ButtonSearch.PerformClick();
            }
        }

        private void ButtonTaxNumberCheck_Click(object sender, EventArgs e)
        {
            //formCheckFirm.Show(this);
            using (CheckFirm frm = new CheckFirm())
            {
                frm.ShowDialog();
            }
        }

        private void ButtonPrePrint_Click(object sender, EventArgs e)
        {
            using (PrePrint frm = new PrePrint())
            {
                frm.ShowDialog();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            using (FormRegister frmSecurity = new FormRegister())
            {
                frmSecurity.ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormSettings frmSecurity = new FormSettings(true))
            {
                frmSecurity.ShowDialog();
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
