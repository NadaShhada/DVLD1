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

namespace DVLD.ApplcationsTypes
{
    public partial class frmListApplcationsTypes : Form
    { public DataTable _dtAllApplacationsTypes =clsApplicationTypes.GetAllApplicationTypes();
        public frmListApplcationsTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshApplicationTypesList()
        {
           _dtAllApplacationsTypes=clsApplicationTypes.GetAllApplicationTypes();    
            dgvApllicationsTypes.DataSource = _dtAllApplacationsTypes; 
            lblRecords1.Text = dgvApllicationsTypes.Rows.Count.ToString();
        }
        private void frmListApplcationsTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
            if (dgvApllicationsTypes.Rows.Count > 0)
            {

                dgvApllicationsTypes.Columns[0].HeaderText = "ID";
                dgvApllicationsTypes.Columns[0].Width = 110;

                dgvApllicationsTypes.Columns[1].HeaderText = "Title";
                dgvApllicationsTypes.Columns[1].Width =400;


                dgvApllicationsTypes.Columns[2].HeaderText = "fees";
                dgvApllicationsTypes.Columns[2].Width = 110;



            }

        }

       

        private void editApplcationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditApplcationTypes((int)(dgvApllicationsTypes.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmListApplcationsTypes_Load(null, null);
        }
    }
}
