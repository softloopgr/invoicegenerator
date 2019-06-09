using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using InvoicesGenerator.Properties;


namespace InvoicesGenerator
{
    public partial class FormActivation : Form
    {
        int dwKeySize = 3704;//keysize
        string xmlString = string.Empty;
        string _TOP_SECRET_PASSWORD = "5boobies5!";
        bool is_registered = false;

        public FormActivation()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBoxKey.Text;

                //StreamReader streamReader = new StreamReader("Resources/MSMS_PrivateKey.kez", true);
                xmlString = Trial.rsaKey;// streamReader.ReadToEnd();
                //streamReader.Close();

                // TODO: Add Proper Exception Handlers
                RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
                rsaCryptoServiceProvider.FromXmlString(xmlString);
                int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
                int iterations = text.Length / base64BlockSize;
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < iterations; i++)
                {
                    byte[] encryptedBytes = Convert.FromBase64String(text.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the EncryptString function.
                    Array.Reverse(encryptedBytes);
                    arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
                }

                string output = Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
                string[] parameters = output.Split('|');

                string TOP_SECRET_PASSWORD = parameters[0];
                string Product = parameters[1];
                string Version = parameters[2];
                string ClientName = parameters[3];
                string ClientID = parameters[4];

                if (TOP_SECRET_PASSWORD != _TOP_SECRET_PASSWORD ||
                    Product != Settings.Default.app_name ||
                    Version != Application.ProductVersion)
                {
                    MessageBox.Show("Λάθος κωδικός. Παρακαλώ δοκιμάστε ξανά", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Trial.updateLicense(ClientName, ClientID);
                    //MessageBox.Show("Το προϊόν κατοχυρώθηκε επιτυχώς στον/στην " + parameters[1], "Επιτυχής Δήλωση", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.DialogResult = DialogResult.OK;
                    is_registered = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Λάθος κωδικός. Παρακαλώ δοκιμάστε ξανά", "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonTrial_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (is_registered)
           {
               this.DialogResult = DialogResult.OK;
           }
           else
           {
               if (Trial.updateTrial() == Trial.TrialStatus.TRIAL)
               {
                   this.DialogResult = DialogResult.OK;
               }
               else
               {
                   this.DialogResult = DialogResult.Cancel;
               }
           }
        }
    }
}
