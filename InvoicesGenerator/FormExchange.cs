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
    public partial class FormExchange : Form
    {
        public Exchange localExchange { get; set; }

        public FormExchange(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }

        public FormExchange()
        {
            InitializeComponent();
            UserControl.InitControls(this);
        }

        private void FormExchange_Load(object sender, EventArgs e)
        {
            if (localExchange.date !=DateTime.MinValue)
            {
                DateTimePickerEndDate.Value = localExchange.date;
                TextBoxAmount.Text = localExchange.amount.ToString();
            }
            else
            {
                DateTimePickerEndDate.Value = DateTime.Now.AddDays(1);
                localExchange.status = 0;
            }

            switch (localExchange.status)
            {
                case 2: //ΕΚΤΥΠΩΜΕΝΕΣ
                    DateTimePickerEndDate.Enabled = false;
                    TextBoxAmount.Enabled = false;
                    break;
                case 3: //ΑΚΥΡΩΜΕΝΕΣ
                    DateTimePickerEndDate.Enabled = false;
                    TextBoxAmount.Enabled = false;
                    buttonCancel.Enabled = false;
                    break;
                case 0: //ΜΗ ΑΠΟΘΗΚΕΥΜΕΝΕΣ
                case 1: //ΜΗ ΕΚΤΥΠΩΜΕΝΕΣ
                default:
                    buttonCancel.Enabled = false;
                    buttonDelete.Enabled = false;
                    break;
            }

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DateTimePickerEndDate.Value < DateTime.Now)
            {
                General.ExclamationMessage("Επιλέξατε ημερομηνία παρελθόντος");
                return;
            }

            if (TextBoxAmount.Text == string.Empty)
            {
                General.ExclamationMessage("Δεν ορίσατε ποσό");
                return;
            }

            string Str = TextBoxAmount.Text.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);
            if (!isNum)
            {
                General.ExclamationMessage("Δεν πληκτρολογίσατε αριθμό στο συνολικό ποσό");
                return;
            }

            if (decimal.Parse(TextBoxAmount.Text) < 0)
            {
                General.ExclamationMessage("Αρνητικό ποσό");
                return;
            }

            localExchange.date = DateTimePickerEndDate.Value;
            localExchange.amount = decimal.Parse(TextBoxAmount.Text);

            this.Close();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (!ExchangeDAL.delete((int)localExchange.id))
            {
                General.ExclamationMessage("Η συναλλαγματική δεν διαγράφτηκε");
            }
            else
            {
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (!ExchangeDAL.cancel((int)localExchange.id))
            {
                General.ExclamationMessage("Η συναλλαγματική δεν ακυρώθηκε");
            }
            else
            {
                this.Close();
            }
        }
    }
}
