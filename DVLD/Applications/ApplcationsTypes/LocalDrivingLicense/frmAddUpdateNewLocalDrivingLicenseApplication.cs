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
using DVLD_Buisness;
using static Bussiness_Layer.clsApplications;

namespace DVLD.ApplcationsTypes.LocalDrivingLicense
{
    public partial class frmAddUpdateNewLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        public int LicenseClassID { set; get; }

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID=-1;
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;
        private clsPerson PersonInfo ;
        public frmAddUpdateNewLocalDrivingLicenseApplication()
        {
            _Mode = enMode.AddNew;
            InitializeComponent();
        }
        public frmAddUpdateNewLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
           
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID; 
        }

        private void _ResetDefaulteValues() {
            if (_Mode == enMode.AddNew) { 
            lblTitle.Text = "New Local Driving License Application"; 
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypes((int)clsApplications.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToString();
                lblCreatedBy.Text=clsGlobal.CurrentUser.UserName;

            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }


        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplications.ApplicantPersonID);
            lblDLApplicationID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplications.ApplicationDate);
            lblApplicationFees.Text = _LocalDrivingLicenseApplications.PaidFees.ToString();
            lblCreatedBy.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplications.CreatedByUserID).UserName;
        }

        private void DataBackEvent(object sender ,int PersonID)
        {
            _SelectedPersonID = PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);


        }



        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }


        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        { PersonInfo = clsPerson.Find(ctrlPersonCardWithFilter1.PersonID);

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplcationInfo.SelectedTab = tcApplcationInfo.TabPages["tpApplicationInfo"];
                return;
            }
            if (ctrlPersonCardWithFilter1.PersonID!=-1) {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplcationInfo.SelectedTab = tcApplcationInfo.TabPages["tpApplicationInfo"];

            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            int ActiveApplicationID = clsApplications.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplications.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }


            //check if user already have issued license of the same driving  class.
            if (clsLicenseClasses.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplications.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID; ;
            _LocalDrivingLicenseApplications.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplications.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplications.ApplicationStatus = clsApplications.enApplicationStatus.New;
            _LocalDrivingLicenseApplications.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplications.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseApplications.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplications.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplications.Save())
            {
                lblDLApplicationID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);




        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;


        }

        private void frmAddUpdateNewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void frmAddUpdateNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaulteValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }
    }
}
