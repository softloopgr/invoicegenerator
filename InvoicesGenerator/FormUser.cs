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
    public partial class FormUser : Form
    {
        User user = new User();
        List<KeyValuePair<int, string>> levels = new List<KeyValuePair<int, string>>();
        string error = string.Empty;
        public FormUser(UserControl.dummyType DummyParameter)
        {
            InitializeComponent();
        }

        public FormUser(int UserID)
        {
            InitializeComponent();
            UserControl.InitControls(this);
            user.UserID  = UserID;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            user.setHierarchy((int)comboBoxLevel.SelectedValue);
            if (user.UserHierarchy < 0)
            {
                throw new Exception("Internal error -- Hierarchy not Found");
            }
            user.UserLevelId = (int)comboBoxLevel.SelectedValue; 
            user.Username = textBoxUsername.Text;
            user.Name = textBoxName.Text;
            user.Surname = textBoxSurname.Text;
            if (!user.UserID.HasValue || !textBoxPassword.Text.Equals("ΚΩΔΙΚΟΣ"))
            {
                user.Password = General.CalculateHash(textBoxPassword.Text, user.Username);
            }
            user.IsActive = radioButtonActive.Checked ? 1 : 0;
            if (user.Save())
            {
                this.Close();
            }
            else
            {
                string error = user.GetError();
                MessageBox.Show("Υπήρξε πρόβλημα με την αποθήκευση του προϊόντος\r\n Πηροφορίες Σφάλματος\n" + error, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            comboBoxLevel.DisplayMember = "Value";
            comboBoxLevel.ValueMember = "Key";
            levels = UserDAL.getLevelCombo();
            comboBoxLevel.DataSource = levels;
            comboBoxLevel.SelectedIndex = -1;

            initForm();
        }

        private void loadUser()
        {
            if (user.UserID.Value > 0)
            {
                user = new User(user.UserID.Value, out error);
            }
            else
            {
                user = new User();
            }
        }

        private void loadUserInformation()
        {
            textBoxName.Text = user.Name;
            comboBoxLevel.SelectedItem = levels.Find(delegate(KeyValuePair<int, string> item) { return item.Key == user.UserLevelId; });
            textBoxPassword.Text = "ΚΩΔΙΚΟΣ";
            textBoxSurname.Text = user.Surname;
            textBoxUsername.Text = user.Username;
            textBoxID.Text = user.UserID.Value.ToString();
            if(user.IsActive ==1)
            {
                radioButtonActive.Checked = true;
            }
            else
            {
                radioButtonInactive.Checked=true;
            }
        }

        private void initForm()
        {
            if (!user.UserID.HasValue || user.UserID.Value<0)
            {
                this.Text += " - Νέος Χρήστης";
                editMenu.Visible = false;
                radioButtonActive.Checked = true;
            }
            else
            {
                loadUser();
                if (error != string.Empty)
                {
                    MessageBox.Show("Υπήρξε πρόβλημα με την ανάκτηση του χρήστη." + error, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                this.Text += " - " + user.Username + "/" + user.UserID;
                buttonDelete.Visible = true;

                loadUserInformation();
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            textBoxPassword.Text = String.Empty;
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxPassword.Text.TrimEnd().TrimStart()))
            {
                textBoxPassword.Text = "ΚΩΔΙΚΟΣ";
            }
        }
    }
}
