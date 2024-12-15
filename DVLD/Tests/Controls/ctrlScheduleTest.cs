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
using DVLD.Properties;
using DVLD_Buisness;
using static Bussiness_Layer.clsTestTypes;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };

        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        private int _LocalDrivingLicenseApplicationsID = -1;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications

        {
            get { return _LocalDrivingLicenseApplications; }
        }
        public clsTestTypes.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pictureBox1.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pictureBox1.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pictureBox1.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();

        }


        public ctrlScheduleTest(int LocalDrivingLicenseApplicationsID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;

        }

        public void LoadInfo(int LocalDrivingApplicationID, int AppointmentID = -1)
        { if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;
            _TestAppointmentID = AppointmentID;

            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(_LocalDrivingLicenseApplicationsID);
            if (_LocalDrivingLicenseApplications != null) {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationsID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicenseApplications.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule) {
                lblRetakeTestFees.Text = clsApplicationTypes.FindApplicationTypes((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";

            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";

            }

            lblDLAppID.Text = _LocalDrivingLicenseApplicationsID.ToString();

            lblDClass.Text = clsLicenseClasses.Find(LocalDrivingLicenseApplications.LicenseClassID).ClassName.ToString();

            lblName.Text = LocalDrivingLicenseApplications.PersonFullName;

            lblTrails.Text = "3";

            lblFees.Text = "4";
            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.FindTestTypes(_TestTypeID).TestTypeFees.ToString();
                dateTimePicker1.MinDate = DateTime.Now;
            }
            else
            {
                if (!_LoadTestAppointmentData())
                {
                    return;
                }
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();


                if (!_HandleActiveTestAppointmentConstraint())
                    return;

                if (!_HandleAppointmentLockedConstraint())
                    return;

                if (!_HandlePreviousTestConstraint())
                    return;



            }

        }
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            { dateTimePicker1.MinDate = DateTime.Now;

            } else dateTimePicker1.MinDate = _TestAppointment.AppointmentDate;
            dateTimePicker1.Value = _TestAppointment.AppointmentDate;
            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeTestFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeTestFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            } return true;




        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplications.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationsID, _TestTypeID))
            {
                lblUserMessage.Text = "person already have an active appointment for this tes";
                btnSave.Enabled = false;
                dateTimePicker1.Enabled = false;
                return false;
            }
            return true;

        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                return false;
            } else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePreviousTestConstraint() {
            switch (TestTypeID) {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestTypes.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplications.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    } else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }


                    return true;
                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LocalDrivingLicenseApplications.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }


                    return true;

            }
            return true;
        }

        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplications.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.FindApplicationTypes((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
