using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InvoicesGenerator.Classes;

namespace InvoicesGenerator
{
    public partial class PrePrint : Form
    {
        private int type = 1;
        private int starting_page;
        private int pages_to_print;

        public PrePrint()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (TextBoxFirstPage.Text == string.Empty)
            {
                General.ErrorMessage("Παρακαλώ δώστε αρχικό αριθμό");
                return;
            }

            if (TextBoxPageNumber.Text == string.Empty)
            {
                General.ErrorMessage("Παρακαλώ δώστε αριθμό σελίδων προς εκτύπωση");
                return;
            }

            int.TryParse(TextBoxFirstPage.Text, out starting_page);
            int.TryParse(TextBoxPageNumber.Text, out pages_to_print);

            string invoiceTypeName = ((KeyValuePair<int, string>)ComboBoxInvoiceType.SelectedItem).Value;
            using (FormWebViewPrint frm = new FormWebViewPrint(invoiceTypeName, starting_page, type, pages_to_print))
            {
                frm.ShowDialog();
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radio = ((RadioButton)sender).Name;
            if (radio == RadioButtonOne.Name)
            {
                type = 1;
            }
            else if (radio == RadioButtonTwo.Name)
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                tsButtonPrint.PerformClick();
            }
        }

        private void InitComboBoxInvoiceTypes()
        {
            ComboBoxInvoiceType.ValueMember = "Key";
            ComboBoxInvoiceType.DisplayMember = "Value";
            ComboBoxInvoiceType.DataSource = InvoiceDAL.getInvoiceTypes();
        }

        private void PrePrint_Load(object sender, EventArgs e)
        {
            InitComboBoxInvoiceTypes();
        }
    }
}
