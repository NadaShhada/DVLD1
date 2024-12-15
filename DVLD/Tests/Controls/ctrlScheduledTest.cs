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
using static DVLD.Tests.Controls.ctrlScheduleTest;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        private clsTestTypes.enTestType _TestTypeID;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;
        private int _LocalDrivingLicenseApplicationsID;
        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }
        private ctrlScheduledTest()
        {
            InitializeComponent();
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
        public void LoadInfo(int TestAppointmentID)
        {
           _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID=_TestAppointment.TestID;
            _LocalDrivingLicenseApplicationsID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(_LocalDrivingLicenseApplicationsID);
            if (_LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationsID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDLAppID.Text = _LocalDrivingLicenseApplicationsID.ToString();

         
            lblDLAppID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicenseApplications.LicenseClassInfo.ClassName;
            lblName.Text = _LocalDrivingLicenseApplications.PersonFullName;

          
            //this will show the trials for this test before 
            lblTrails.Text = _LocalDrivingLicenseApplications.TotalTrialsPerTest(_TestTypeID).ToString();



            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID==-1)? "Not Taken Yet":_TestAppointment.TestID.ToString();





            }

        private void ctrlScheduledTest_Load(object sender, EventArgs e)
        {
            LoadInfo(_LocalDrivingLicenseApplicationsID);
        }
    }

    }

