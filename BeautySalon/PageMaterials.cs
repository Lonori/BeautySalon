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
    public partial class PageMaterials : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table; 

        public PageMaterials(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();

            TableColumn[] columns = new TableColumn[] {
                new TableColumnText("ID", 10, ""),
                new TableColumnText("Материал", 70, ""),
                new TableColumnText("Цена", 20, "0")
            };
            Table = new TableObject(columns, new TableData());

            table1.TableInit(Table);
            UpdateTable();
        }

        private void UpdateTable()
        {
            table1.Clear();
            Table.Data.Clear();

            OleDbCommand command = new OleDbCommand("SELECT `id`,`material`,`prise` FROM `materials` WHERE 1", DbConnection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                object[] data = new object[] {
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString()
                };
                Table.Data.Add(data);
                table1.AddRow(data);
            }
            reader.Close();
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            object[] data = Table.GetDefault();
            using (OleDbCommand command = new OleDbCommand("SELECT MAX(`id`)+1 FROM `materials` WHERE 1", DbConnection))
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
                new OleDbCommand("INSERT INTO `materials`(`id`,`material`,`prise`,`amount`) VALUES (" + tableEditor.DataRow[0].ToString() + ",'" + tableEditor.DataRow[1].ToString() + "'," + tableEditor.DataRow[2].ToString() + ",0)", DbConnection).ExecuteNonQuery();
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
            new OleDbCommand("DELETE FROM `materials` WHERE `id`=" + Table.Data[table1.row_selected][0], DbConnection).ExecuteNonQuery();
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
                new OleDbCommand("UPDATE `materials` SET `material`='" + tableEditor.DataRow[1].ToString() + "',`prise`=" + tableEditor.DataRow[2].ToString() + " WHERE `id`=" + id, DbConnection).ExecuteNonQuery();
                UpdateTable();
            }

            tableEditor.Dispose();
        }
    }
}
