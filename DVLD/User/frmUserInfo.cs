using System;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmUserInfo : Form
    {

        public int UserID = -1;

        public frmUserInfo(int UserID)
        {
            this.UserID = UserID;

            InitializeComponent();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserControl1.LoadUserInfo(UserID);

        }
    }
}
