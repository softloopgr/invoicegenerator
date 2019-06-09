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
    public partial class FormRegister : Form
    {
        User user = new User();

        public FormRegister()
        {
            InitializeComponent();
        }

        private void TextBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.ButtonRegister_Click(null, null);
            }
        }

        private void MaskedTextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.ButtonRegister_Click(null, null);
            }
        }
        
        private void FormSecurity_Load(object sender, EventArgs e)
        {
            user.UserID = UserDAL.getCurrentUserID();
            if (user.UserID.HasValue)
            {
                TextBoxUsername.Text = UserDAL.getUsername(user.UserID.Value);
            }
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            LabelError.Text = string.Empty;

            if (TextBoxUsername.Text == string.Empty)
            {
                LabelError.Text="Δεν ορίσατε όνομα χρήστη";
                return;
            }

            if (MaskedTextBoxPassword.Text == string.Empty)
            {
                LabelError.Text = "Δεν ορίσατε κωδικό";
                return;
            }

            if (MaskedTextBoxPassword.Text != MaskedTextBoxPasswordRepeat.Text)
            {
                LabelError.Text = "Ο κωδικός επιβεβαίωσης δεν είναι σωστός";
                return;
            }

            User user = new User();
            user.UserID = UserDAL.getCurrentUserID();
            user.Name = "OnlyUser";
            user.IsActive = 1;
            user.UserHierarchy = 200;
            user.UserLevelId = 4;
            user.Username = TextBoxUsername.Text;
            user.Name = string.Empty;
            user.Surname = string.Empty;
            user.Password = General.CalculateHash(MaskedTextBoxPassword.Text, TextBoxUsername.Text);
            user.Save();

            this.Close();
        }

        private void LinkTos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }
}
