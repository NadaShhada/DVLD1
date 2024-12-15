using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {
        private clsUser _User;
        private int _UserID;
        public frmChangePassword(int UserID)
        {
            _UserID = UserID;

            InitializeComponent();
        }
        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmedPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {  //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                    "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if ((string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()) && (_User.Password == txtCurrentPassword.Text.Trim())))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This field is required!");
                return;
            }
            else if ((_User.Password != txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "the password is not correct!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field is required!");
                return;
            }
            else if (txtNewPassword.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Password must be at least 4 charecters!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

        }

        private void txtConfirmedPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field is required!");
                return;
            }
            else if (txtNewPassword.Text != txtConfirmedPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Passwords is not the same");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            ctrlUserControl1.LoadUserInfo(_UserID);
        }
    }
}
