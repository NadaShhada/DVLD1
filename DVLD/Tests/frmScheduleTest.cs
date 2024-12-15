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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _AppointmentID = -1;
        private clsTestTypes.enTestType _enTestType=clsTestTypes.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationsID;
        public frmScheduleTest(int LocalDrivingLicenseApplicationsID,clsTestTypes.enTestType enTestType, int appointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
            _AppointmentID = appointmentID;
            enTestType =_enTestType;

        }

        private void btnClosew_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _enTestType;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationsID);
        }
    }
}
