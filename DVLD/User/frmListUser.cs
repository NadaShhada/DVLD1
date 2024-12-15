using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.User
{
    public partial class frmListUser : Form
    {
        private static DataTable _dtAllUser = clsUser.GetAllUsers();
        public frmListUser()
        {
            InitializeComponent();

            dgvUser.DataSource = _dtAllUser;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshPeoplList()
        {
            _dtAllUser = clsUser.GetAllUsers();


            dgvUser.DataSource = _dtAllUser;
            lblRecords.Text = dgvUser.Rows.Count.ToString();
        }

        private void frmListUser_Load(object sender, EventArgs e)
        {

            dgvUser.DataSource = _dtAllUser;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvUser.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;


            if (dgvUser.Rows.Count > 0)
            {

                dgvUser.Columns[0].HeaderText = "User ID";
                dgvUser.Columns[0].Width = 110;

                dgvUser.Columns[1].HeaderText = "Person ID";
                dgvUser.Columns[1].Width = 120;


                dgvUser.Columns[2].HeaderText = "Full Name";
                dgvUser.Columns[2].Width = 350;

                dgvUser.Columns[3].HeaderText = "User Name";
                dgvUser.Columns[3].Width = 140;


                dgvUser.Columns[4].HeaderText = "Is Active";
                dgvUser.Columns[4].Width = 120;


            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;

            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColum = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColum = "UserID";
                    break;
                case "User Name":
                    FilterColum = "UserName";
                    break;
                case "Person ID":
                    FilterColum = "PersonID";
                    break;
                case "Full Name":
                    FilterColum = "FullName";
                    break;
                default:
                    FilterColum = "None";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "" || FilterColum == "None")
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblRecords.Text = dgvUser.Rows.Count.ToString();
                return;

            }
            if (FilterColum != "FullName" && FilterColum != "UserName")
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColum, txtFilterValue.Text.Trim());
            else _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColum, txtFilterValue.Text.Trim());
            lblRecords.Text = _dtAllUser.Rows.Count.ToString();

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }
            if (FilterValue == "All")
                _dtAllUser.DefaultView.RowFilter = "";
            else _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            lblRecords.Text = _dtAllUser.Rows.Count.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUser_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser();
            frmAddUpdateUser.ShowDialog();
            frmListUser_Load(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUser_Load(null, null);

        }

        private void dgvUser_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo frm2 = new frmUserInfo((int)dgvUser.CurrentRow.Cells[0].Value);
            frm2.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvUser.CurrentRow.Cells[0].Value);
            frmUserInfo.ShowDialog();

        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUser.CurrentRow.Cells[0].Value;
            frmChangePassword frmChangePassword = new frmChangePassword(UserID);
            frmChangePassword.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUser.CurrentRow.Cells[0].Value;
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUser_Load(null, null);
            }
            else MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
