using Lab07.Forms;
using System;
using System.Windows.Forms;

namespace Lab07
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin loginForm = new FrmLogin();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FrmMain(loginForm.AuthenticatedUser));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}