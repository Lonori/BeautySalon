using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageSuppliers : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table; 

        public PageSuppliers(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();

            TableColumn[] columns = new TableColumn[] {
                    new TableColumnText("ID", 10, ""),
                    new TableColumnText("Поставщик", 90, ""),
            };
            Table = new TableObject(columns, new TableData());

            table1.TableInit(Table);
            UpdateTable();
        }

        private void UpdateTable()
        {
            table1.Clear();
            Table.Data.Clear();

            OleDbCommand command = new OleDbCommand("SELECT `id`,`supplier` FROM `suppliers` WHERE 1", DbConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object[] data_arr = new object[] {
                    reader[0].ToString(),
                    reader[1].ToString()
                };
                table1.AddRow(data_arr);
                Table.Data.Add(data_arr);
            }

            reader.Close();
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            object[] data = Table.GetDefault();
            using (OleDbCommand command = new OleDbCommand("SELECT MAX(`id`)+1 FROM `suppliers` WHERE 1", DbConnection))
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
                new OleDbCommand("INSERT INTO `suppliers`(`id`,`supplier`) VALUES (" + tableEditor.DataRow[0].ToString() + ",'" + tableEditor.DataRow[1].ToString() + "')", DbConnection).ExecuteNonQuery();
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
            new OleDbCommand("DELETE FROM `suppliers` WHERE `id`=" + Table.Data[table1.row_selected][0], DbConnection).ExecuteNonQuery();
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
                new OleDbCommand("UPDATE `suppliers` SET `supplier`='" + tableEditor.DataRow[1].ToString() + "' WHERE `id`=" + id, DbConnection).ExecuteNonQuery();
                UpdateTable();
            }

            tableEditor.Dispose();
        }
    }
}
