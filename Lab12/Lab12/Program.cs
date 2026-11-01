using Lab12.Forms;
using System;
using System.Windows.Forms;

namespace Lab12
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmNhapDiemTheoLop());
        }
    }
}