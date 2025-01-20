using BeautySalon.Components.Themes;
using BeautySalon.DB;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormMain : Form, IThemable
    {
        public FormMain()
        {
            if (!Authorisation()) return;

            InitializeComponent();
            ChangePage(new PageMain());
            ReportPDFFactory.Init();
        }

        private bool Authorisation()
        {
            FormConnect formConnect = new FormConnect();
            formConnect.ShowDialog();
            if (AppDatabase.IsConnected())
            {
                formConnect.Dispose();
                return true;
            }
            else
            {
                Dispose();
                return false;
            }
        }

        public void ApplyTheme(ITheme theme)
        {
            BackColor = theme.ColorBackground;
            ForeColor = theme.ColorForeground;
            Font = theme.Font;
            buttonMain.BackColor = theme.ColorSecondary;
            buttonMain.ForeColor = theme.ColorForeground;
            buttonMain.Font = theme.Font;
            buttonNotes.BackColor = theme.ColorSecondary;
            buttonNotes.ForeColor = theme.ColorForeground;
            buttonNotes.Font = theme.Font;
            buttonMaterials.BackColor = theme.ColorSecondary;
            buttonMaterials.ForeColor = theme.ColorForeground;
            buttonMaterials.Font = theme.Font;
            buttonStorage.BackColor = theme.ColorSecondary;
            buttonStorage.ForeColor = theme.ColorForeground;
            buttonStorage.Font = theme.Font;
        }

        private void ChangePage(UserControl pageControl)
        {
            Control previous = null;
            if (pageContainer.Controls.Count > 0)
            {
                previous = pageContainer.Controls[0];
            }
            if (previous == null || !Equals(previous.GetType(), pageControl.GetType()))
            {
                pageControl.Dock = DockStyle.Fill;
                pageControl.Margin = new Padding(0);
                pageContainer.Controls.Clear();
                pageContainer.Controls.Add(pageControl);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppDatabase.Disconect();
        }

        private void OpenMain_Click(object sender, EventArgs e)
        {
            ChangePage(new PageMain());
        }

        private void OpenNotes_Click(object sender, EventArgs e)
        {
            ChangePage(new PageOrders());
        }

        private void OpenMaterials_Click(object sender, EventArgs e)
        {
            ChangePage(new PageSupplierContracts());
        }

        private void OpenStorage_Click(object sender, EventArgs e)
        {
            ChangePage(new PageStorage());
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ChangePage(new PageMaterials());
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            ChangePage(new PageStaff());
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            ChangePage(new PageServices());
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            ChangePage(new PageSuppliers());
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            ChangePage(new PageReportStaff());
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "help.chm";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            new FormProgrammInfo().ShowDialog();
        }
    }
}
