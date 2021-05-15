using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautySalon
{
    static class AlertBox
    {
        public static DialogResult Info(string text)
        {
            return ConstructMesBox(text, "Информация", MessageBoxIcon.Information);
        }

        public static DialogResult Warning(string text)
        {
            return ConstructMesBox(text, "Предупреждение", MessageBoxIcon.Warning);
        }

        public static DialogResult Error(string text)
        {
            return ConstructMesBox(text, "Ошибка", MessageBoxIcon.Error);
        }

        private static DialogResult ConstructMesBox(string text, string name, MessageBoxIcon typeicon)
        {
            return MessageBox.Show(
                 text,
                 name,
                 MessageBoxButtons.OK,
                 typeicon,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly
            );
        }
    }
}
