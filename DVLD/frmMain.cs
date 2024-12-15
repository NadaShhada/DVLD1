using System;
using System.Windows.Forms;
using DVLD.ApplcationsTypes;
using DVLD.Classes;
using DVLD.Login_Screen;
using DVLD.People;
using DVLD.TestTypes;
using DVLD.User;


namespace DVLD
{

    public partial class frmMain : Form
    {
        frmLoginScreen _frmLogin;
        public frmMain(frmLoginScreen frm)
        {
            InitializeComponent();
            _frmLogin = frm;

        }
        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }



       











        private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUser();
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListApplcationsTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTestTypes();
            frm.ShowDialog();
        }
    }
}
