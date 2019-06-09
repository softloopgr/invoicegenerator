using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace InvoicesGenerator
{
    partial class FormAbout : Form
    {

        public FormAbout(UserControl.dummyType dummyObject)
        {
            InitializeComponent();
        }
        public FormAbout()
        {
            InitializeComponent();
            LabelVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (Trial.isTrial())
            {
                GroupBoxLicense.Text = "Δοκιμαστική Χρήση";
                LabelLicense.Text = "Απομένουν " + Trial.getTrialDays().ToString() + " μέρες χρήσης";
            }
            else
            {
                GroupBoxLicense.Text = "Κατωχειρωμένο: "+Trial.getSetting("owner").ToString();
                LabelLicense.Text = Trial.getSetting("ClientID").ToString();
            }
        }
    }
}
