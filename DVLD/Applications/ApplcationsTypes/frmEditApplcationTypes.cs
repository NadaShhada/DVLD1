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
using DVLD.Classes;

namespace DVLD.ApplcationsTypes
{
    public partial class frmEditApplcationTypes : Form
    {
        private int _ApplcationTypeID;
        private clsApplicationTypes ApplcationType ;
        public frmEditApplcationTypes(int ApplcationTypeID )
        {
            InitializeComponent();

            _ApplcationTypeID = ApplcationTypeID;

          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
               
            }
            ApplcationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            ApplcationType.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());

            if (ApplcationType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim())){
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }else if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };

        }

        private void frmEditApplcationTypes_Load(object sender, EventArgs e)
        {
            lblApplcationTypesID.Text = _ApplcationTypeID.ToString();
            ApplcationType=clsApplicationTypes.FindApplicationTypes(_ApplcationTypeID);
            if (ApplcationType != null)
            {
                lblApplcationTypesID.Text = ((int)_ApplcationTypeID).ToString();
                txtTitle.Text = ApplcationType.ApplicationTypeTitle;
                txtFees.Text = ApplcationType.ApplicationFees.ToString();
            }
            else

            {
                MessageBox.Show("Could not find Test Type with id = " + _ApplcationTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                this.Close();

            }
        }
    }
}
