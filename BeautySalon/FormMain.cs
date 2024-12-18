using BeautySalon.Components.Themes;
using BeautySalon.DB;
using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormMain : Form, IThemable
    {
        private OleDbConnection DbConnection;

        public FormMain()
        {
            if (!Authorisation()) return;

            InitializeComponent();

            DbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=main.mdb;");
            DbConnection.Open();

            ChangePage(new PageMain());
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
            DbConnection.Close();
            AppDatabase.Disconect();
        }

        private void OpenMain_Click(object sender, EventArgs e)
        {
            ChangePage(new PageMain());
        }

        private void OpenNotes_Click(object sender, EventArgs e)
        {
            ChangePage(new PageNotes(DbConnection));
        }

        private void OpenMaterials_Click(object sender, EventArgs e)
        {
            ChangePage(new PageContractMat(DbConnection));
        }

        private void OpenStorage_Click(object sender, EventArgs e)
        {
            ChangePage(new PageStorage(DbConnection));
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
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "help.chm";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            new FormProgrammInfo().ShowDialog();
        }
    }
}
