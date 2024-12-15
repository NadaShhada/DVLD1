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

namespace DVLD.TestTypes
{
    public partial class frmEditTestTypes : Form
    {
        private clsTestTypes _TestTypes;
              private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
 
        public frmEditTestTypes(clsTestTypes.enTestType TestTypeID)
        {
           
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
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
            }
            else if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestTypes.FindTestTypes(_TestTypeID);

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (this.ValidateChildren())
            {
                _TestTypes.TestTypeTitle = txtTitle.Text.Trim();

                _TestTypes.TestTypeDescription = txtDescription.Text.Trim();

                _TestTypes.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());

                if (_TestTypes.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            _TestTypes= clsTestTypes.FindTestTypes(_TestTypeID);
            txtTitle.Text = _TestTypes.TestTypeTitle;
            txtDescription.Text = _TestTypes.TestTypeDescription;

            txtFees.Text = _TestTypes.TestTypeFees.ToString();


        }

       

       
    }
            }
 
