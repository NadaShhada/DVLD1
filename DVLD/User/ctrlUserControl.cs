using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.User
{
    public partial class ctrlUserControl : UserControl
    {
        private clsUser _User;

        private int _UserID = -1;
        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserControl()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillUserInfo();


        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblIsActive.Text = (_User.IsActive) ? "true" : "false";
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
        }
        private void _ResetPersonInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

    }
}
