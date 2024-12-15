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
using DVLD.Properties;
using DVLD_Buisness;
using static Bussiness_Layer.clsTestTypes;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;

        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationsID = -1;
        public frmListTestAppointments(int LocalDrivingLicenseApplicationsID ,clsTestTypes.enTestType enTestType)
        {
            InitializeComponent();
            _TestType= enTestType; 
            _LocalDrivingLicenseApplicationsID= LocalDrivingLicenseApplicationsID;
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestTypes.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox10.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestTypes.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox10.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox10.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationsID);
            DataTable dt = clsTestAppointment.GetAllAppointmentsByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationsID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationsID, _TestType);
            lblCountRecords.Text = dt.Rows.Count.ToString();
            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 150;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 200;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 150;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 100;
            }

           
        }

        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplications(_LocalDrivingLicenseApplicationsID);
            if (localDrivingLicenseApplications.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsTests LastTest = localDrivingLicenseApplications.GetLastTestPerTestType(_TestType);
            if(LastTest == null)
            {
                frmScheduleTest fr = new frmScheduleTest(_LocalDrivingLicenseApplicationsID, _TestType);
                fr.ShowDialog();
                frmListTestAppointments_Load(null,null);
                return;
            }
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        
            frmScheduleTest form = new frmScheduleTest(_LocalDrivingLicenseApplicationsID, _TestType);
            form.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID=(int)dgvAppointments.CurrentRow.Cells[0].Value;
            frmScheduleTest frm =new frmScheduleTest(_LocalDrivingLicenseApplicationsID,_TestType,TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null,null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;
            

        }
    }
}
