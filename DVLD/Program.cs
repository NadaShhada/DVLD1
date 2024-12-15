using System.Windows.Forms;
using DVLD.Login_Screen;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       // [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new frmMain());
            // Application.Run(new frmTest2());
            Application.Run(new frmLoginScreen());



        }
    }
}
