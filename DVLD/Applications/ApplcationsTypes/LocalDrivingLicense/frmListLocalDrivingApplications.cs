using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bussiness_Layer;
using DVLD.User;
using DVLD_Buisness;

namespace DVLD.ApplcationsTypes.LocalDrivingLicense
{
    public partial class frmListLocalDrivingApplications : Form
    {
        private static DataTable _dtAllLocalDrivingApplicationsLicense = clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();

        public frmListLocalDrivingApplications()
        {
            InitializeComponent();
            dgvLocalDrivingLicenseApplication.DataSource= _dtAllLocalDrivingApplicationsLicense;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshLocalDrivingApplicationsList()
        {
            _dtAllLocalDrivingApplicationsLicense = clsUser.GetAllUsers();


            dgvLocalDrivingLicenseApplication.DataSource = _dtAllLocalDrivingApplicationsLicense;
            lblRecords.Text = dgvLocalDrivingLicenseApplication.Rows.Count.ToString();
        }

        private void frmListLocalDrivingApplications_Load(object sender, EventArgs e)
        {

            dgvLocalDrivingLicenseApplication.DataSource = _dtAllLocalDrivingApplicationsLicense;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvLocalDrivingLicenseApplication.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;


            if (dgvLocalDrivingLicenseApplication.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplication.Columns[0].HeaderText = "L D App ID";
                dgvLocalDrivingLicenseApplication.Columns[0].Width = 110;

                dgvLocalDrivingLicenseApplication.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplication.Columns[1].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[2].HeaderText = "National No";
                dgvLocalDrivingLicenseApplication.Columns[2].Width = 140;

                dgvLocalDrivingLicenseApplication.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplication.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplication.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplication.Columns[4].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplication.Columns[5].Width = 120;

                dgvLocalDrivingLicenseApplication.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplication.Columns[6].Width = 120;


            }
            cbFilterBy.SelectedIndex = 0;


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                
                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            _dtAllLocalDrivingApplicationsLicense.DefaultView.RowFilter = "";
            lblRecords.Text=dgvLocalDrivingLicenseApplication.Rows.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColum = "";
            switch (cbFilterBy.Text)
            {
                case "L D.L AppID":
                    FilterColum = "LocalDrivingLicenseApplicationID";
                    break;
               
                case "National No":
                    FilterColum = "NationalNo";
                    break;
                case "Full Name":
                    FilterColum = "FullName";
                    break;
                case "Status":
                    FilterColum = "Status";
                    break;
                
                    
                default:
                    FilterColum = "None";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "" || FilterColum == "None")
            {
                _dtAllLocalDrivingApplicationsLicense.DefaultView.RowFilter = "";
                lblRecords.Text = dgvLocalDrivingLicenseApplication.Rows.Count.ToString();
                return;

            }
            if (FilterColum == "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingApplicationsLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColum, txtFilterValue.Text.Trim());
            else _dtAllLocalDrivingApplicationsLicense.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColum, txtFilterValue.Text.Trim());
            lblRecords.Text = _dtAllLocalDrivingApplicationsLicense.Rows.Count.ToString();
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateNewLocalDrivingLicenseApplication frm = new frmAddUpdateNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingApplications_Load(null, null);
        }

      

        private void dgvLocalDrivingLicenseApplication_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L D.L AppID" )
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm=new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingApplications_Load(null,null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingApplications_Load(null, null);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications =  clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID);
            int TotalPassedTests=(int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value;
            bool LicenseExists = localDrivingLicenseApplications.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=LicenseExists;
            editToolStripMenuItem.Enabled=! LicenseExists&&(localDrivingLicenseApplications.ApplicationStatus==clsApplications.enApplicationStatus.New);

            ScheduleTestsMenue.Enabled = !LicenseExists;


            CancelApplicaitonToolStripMenuItem.Enabled= (localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New); ;

             DeleteApplicationToolStripMenuItem.Enabled=(localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New);

      
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            int LicenseID=clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID).GetActiveLicenseID();


        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
