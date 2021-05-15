using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormMain : Form
    {
        private OleDbConnection DbConnection;

        public FormMain()
        {
            InitializeComponent();

            DbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=main.mdb;");
            DbConnection.Open();

            pageContainer.Controls.Add(new PageMain(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DbConnection.Close();
        }

        private void OpenMain_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageMain(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void OpenNotes_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageNotes(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void OpenMaterials_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageContractMat(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void OpenStorage_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageStorage(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageMaterials(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageStaff(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageServices(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            pageContainer.Controls.Clear();
            pageContainer.Controls.Add(new PageSuppliers(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            var proc = new System.Diagnostics.Process();
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
