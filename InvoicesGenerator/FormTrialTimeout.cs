using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvoicesGenerator
{
    public enum RegistrationMethod
    {
        PHONE = 1,
        INTERNET = 2
    };

    public partial class FormTrialTimeout : Form
    {

        public RegistrationMethod selectedMethod;

        public FormTrialTimeout()
        {
            InitializeComponent();
        }

        private void ButtonRegisterPhone_Click(object sender, EventArgs e)
        {
            this.selectedMethod = RegistrationMethod.PHONE;
            this.Close();
        }
    }
}
