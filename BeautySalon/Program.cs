using System;
using System.Windows.Forms;

namespace BeautySalon
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain formMain = new FormMain();
            if (formMain != null && !formMain.IsDisposed)
            {
                Application.Run(formMain);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
