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

        public static DialogResult Confirm(string text)
        {
            return ConstructConfirmBox(text, "Подтверждение", MessageBoxIcon.Information);
        }

        public static DialogResult ConfirmWarn(string text)
        {
            return ConstructConfirmBox(text, "Подтверждение", MessageBoxIcon.Warning);
        }

        private static DialogResult ConstructConfirmBox(string text, string name, MessageBoxIcon typeicon)
        {
            return MessageBox.Show(
                 text,
                 name,
                 MessageBoxButtons.OKCancel,
                 typeicon,
                 MessageBoxDefaultButton.Button1
            );
        }

        private static DialogResult ConstructMesBox(string text, string name, MessageBoxIcon typeicon)
        {
            return MessageBox.Show(
                 text,
                 name,
                 MessageBoxButtons.OK,
                 typeicon,
                 MessageBoxDefaultButton.Button1
            );
        }
    }
}
