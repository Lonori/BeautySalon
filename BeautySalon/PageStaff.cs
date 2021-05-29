using System;
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

            TableColumn[] columns = new TableColumn[] {
                    new TableColumnText("ID", 10, ""),
                    new TableColumnText("ФИО", 30, "", true),
                    new TableColumnDate("Дата найма", 20),
                    new TableColumnDate("Дата рождения", 20),
                    new TableColumnText("Должность", 20, "")
            };
            Table = new TableObject(columns, new TableData());

            table1.TableInit(Table);
            table1.PreAddRow += FormatTableNotes;
            UpdateTable();
        }

        private string[] FormatTableNotes(object[] data)
        {
            string[] str_data = new string[]
            {
                 data[0].ToString(),
                 data[1].ToString(),
                 ((DateTime)data[2]).ToShortDateString(),
                 ((DateTime)data[3]).ToShortDateString(),
                 data[4].ToString()
            };
            return str_data;
        }

        private void UpdateTable()
        {
            table1.Clear();
            Table.Data.Clear();

            OleDbCommand command = new OleDbCommand("SELECT `id`, `full_name`, `hiring_date`, `birthday`, `position` FROM `staff` WHERE 1", DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object[] data_arr = new object[] {
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2],
                    reader[3],
                    reader[4].ToString(),
                };
                table1.AddRow(data_arr);
                Table.Data.Add(data_arr);
            }

            reader.Close();
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

            if(tableEditor.Confirmed == true)
            {
                new OleDbCommand("INSERT INTO `staff`(`id`, `full_name`, `hiring_date`, `birthday`, `position`) VALUES (" + tableEditor.DataRow[0].ToString() + ",'"+ tableEditor.DataRow[1].ToString() + "','"+ tableEditor.DataRow[2].ToString() + "','"+ tableEditor.DataRow[3].ToString() + "','" + tableEditor.DataRow[4].ToString() + "')", DbConnection).ExecuteNonQuery();
                UpdateTable();
            }

            tableEditor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if(table1.row_selected < 0)
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
            UpdateTable();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
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

            tableEditor.Dispose();
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            Control tmp = Parent;
            tmp.Controls.Clear();
            tmp.Controls.Add(new ReportStaff(DbConnection) { Dock = DockStyle.Fill, Margin = new Padding(0) });
        }
    }
}
