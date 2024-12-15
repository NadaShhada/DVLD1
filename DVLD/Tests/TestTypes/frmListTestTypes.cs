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

namespace DVLD.TestTypes
{
    public partial class frmListTestTypes : Form
    { private DataTable _dtAllTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }
      private void _RefreshTestTypesList()
        {
             _dtAllTestTypes =clsTestTypes.GetAllTestTypes();

            lblRecords.Text = _dtAllTestTypes.Rows.Count.ToString();
            dgvTestTypes.DataSource = _dtAllTestTypes;  
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
            if (_dtAllTestTypes.Rows.Count > 0)
            {


                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 110;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 150;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width =400;

                dgvTestTypes.Columns[3].HeaderText = "Test Type fees";
                dgvTestTypes.Columns[3].Width = 110;

            }
        }

      

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditTestTypes((clsTestTypes.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}
