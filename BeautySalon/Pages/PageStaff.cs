using BeautySalon.DB;
using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageStaff : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table;

        public PageStaff(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();

            materialTable1.TableHeaders = new List<string> {
                "ID",
                "ФИО",
                "Дата рождения",
                "Дата найма",
                "Дата увольнения",
                "Должность"
            };

            TableColumn[] columns = new TableColumn[] {
                    new TableColumnText("ID", 10, ""),
                    new TableColumnText("ФИО", 30, "", true),
                    new TableColumnDate("Дата найма", 20),
                    new TableColumnDate("Дата рождения", 20),
                    new TableColumnText("Должность", 20, "")
            };
            Table = new TableObject(columns, new TableData());

            UpdateTable();
        }

        private async void UpdateTable()
        {
            List<Staff> staffs = await AppDatabase.GetInstance().StaffDAO.GetAll();
            List<List<string>> tableData = new List<List<string>>();

            foreach (Staff staff in staffs)
            {
                tableData.Add(new List<string>{
                    staff.Id.ToString(),
                    staff.FullName,
                    staff.Birthday.ToShortDateString(),
                    staff.DateJoin == DateTime.MinValue ? "---" : staff.DateJoin.ToShortDateString(),
                    staff.DateLeave == DateTime.MinValue ? "---" : staff.DateLeave.ToShortDateString(),
                    staff.Position
                });
            }

            materialTable1.TableData = tableData;
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            object[] data = Table.GetDefault();
            using (OleDbCommand command = new OleDbCommand("SELECT MAX(`id`)+1 FROM `staff` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                data[0] = reader[0].GetType() == typeof(DBNull) ? "1" : reader[0].ToString();
                reader.Close();
            }

            FormTableEditor tableEditor = new FormTableEditor("Добавить", Table.Columns, data);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("INSERT INTO `staff`(`id`, `full_name`, `hiring_date`, `birthday`, `position`) VALUES (" + tableEditor.DataRow[0].ToString() + ",'" + tableEditor.DataRow[1].ToString() + "','" + tableEditor.DataRow[2].ToString() + "','" + tableEditor.DataRow[3].ToString() + "','" + tableEditor.DataRow[4].ToString() + "')", DbConnection).ExecuteNonQuery();
                UpdateTable();
            }

            tableEditor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            /*if (table1.row_selected < 0)
            {
                MessageBox.Show(
                   "Не выбрано ни одной записи",
                   "Предупреждение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly
                );
                return;
            }
            new OleDbCommand("DELETE FROM `staff` WHERE `id`=" + Table.Data[table1.row_selected][0], DbConnection).ExecuteNonQuery();
            table1.row_selected = -1;
            UpdateTable();*/
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            /*if (table1.row_selected < 0)
            {
                MessageBox.Show(
                   "Не выбрано ни одной записи",
                   "Предупреждение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly
                );
                return;
            }
            string id = (string)Table.Data[table1.row_selected][0];
            FormTableEditor tableEditor = new FormTableEditor("Изменить", Table.Columns, Table.Data[table1.row_selected]);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("UPDATE `staff` SET `full_name`='" + tableEditor.DataRow[1].ToString() + "',`hiring_date`='" + tableEditor.DataRow[2].ToString() + "',`birthday`='" + tableEditor.DataRow[3].ToString() + "',`position`='" + tableEditor.DataRow[4].ToString() + "' WHERE `id`=" + id, DbConnection).ExecuteNonQuery();
                UpdateTable();
            }

            tableEditor.Dispose();*/
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            Control tmp = Parent;
            tmp.Controls.Clear();
            tmp.Controls.Add(new ReportStaff(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }
    }
}
