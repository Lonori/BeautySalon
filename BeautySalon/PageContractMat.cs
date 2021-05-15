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
    public partial class PageContractMat : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table;
        private TableColumn[] ColumnsServices;
        private ListComboBoxItem list_suppliers = new ListComboBoxItem();
        private ListComboBoxItem list_materials = new ListComboBoxItem();
        private Dictionary<string, float> material_prise = new Dictionary<string, float>();

        public PageContractMat(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);
            dateTimePicker1.ValueChanged += period_ValueChanged;
            dateTimePicker2.ValueChanged += period_ValueChanged;

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`supplier` FROM `suppliers` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list_suppliers.Add(new ComboBoxItem(reader[1].ToString(), reader[0].ToString()));
                reader.Close();
            }

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`material`,`prise` FROM `materials` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_materials.Add(new ComboBoxItem(reader[1].ToString(), reader[0].ToString()));
                    material_prise.Add(reader[0].ToString(), float.Parse(reader[2].ToString()));
                }
                reader.Close();
            }

            ColumnsServices = new TableColumn[] {
                new TableColumnList("Материал", 40, list_materials),
                new TableColumn("Цена", 20),
                new TableColumnText("Кол-во", 20, "0"),
                new TableColumn("Стоимость", 20)
            };

            TableObject services = new TableObject(
                ColumnsServices,
                new TableData()
            ) { PreAddRow = FormatTableServices };

            Table = new TableObject(
                new TableColumn[] {
                    new TableColumnText("ID", 10, ""),
                    new TableColumnDate("Дата", 20),
                    new TableColumnList("Поставщик", 40, list_suppliers),
                    new TableColumnTable("Материалы", 30, services)
                }, 
                new TableData()
            );

            table1.TableInit(Table);
            UpdateTable();
        }

        private string[] FormatTableServices(object[] data)
        {
            string[] str_data = new string[]
            {
                 data[0].ToString(),
                 material_prise[((ListComboBoxItem)data[0]).Value].ToString(),
                 data[2].ToString(),
                 (material_prise[((ListComboBoxItem)data[0]).Value] * int.Parse(data[2].ToString())).ToString()
            };
            return str_data;
        }

        private void UpdateTable()
        {
            table1.Clear();
            Table.Data.Clear();

            using (OleDbCommand command = new OleDbCommand("SELECT `id`, `time`, `suppliers_id` FROM `purchase_contracts` WHERE `time`>=CDate('" + dateTimePicker1.Value.ToString()+ "') AND `time`<=CDate('" + dateTimePicker2.Value.ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object[] data_arr = new object[] {
                        reader[0].ToString(),
                        reader[1],
                        list_suppliers.CopyOnNewValue(reader[2].ToString()),
                        null
                    };
                    Table.Data.Add(data_arr);
                }
                reader.Close();
            }

            for (int i = 0; i < Table.Data.Length; i++)
            {
                TableObject services = new TableObject(
                    ColumnsServices,
                    new TableData()
                ) { PreAddRow = FormatTableServices };

                using (OleDbCommand command = new OleDbCommand("SELECT `material_id`,`amount` FROM `materials_arrival` WHERE `contract_id`=" + Table.Data[i][0].ToString(), DbConnection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        services.Data.Add(new object[] { list_materials.CopyOnNewValue(reader[0].ToString()), "", reader[1].ToString(), "" });
                    }
                    reader.Close();
                }

                Table.Data[i][3] = services;
                table1.AddRow(Table.Data[i]);
            }
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            object[] data = Table.GetDefault();
            using (OleDbCommand command = new OleDbCommand("SELECT MAX(`id`)+1 FROM `purchase_contracts` WHERE 1", DbConnection))
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
                new OleDbCommand("INSERT INTO `purchase_contracts`(`id`,`time`,`suppliers_id`) VALUES (" + tableEditor.DataRow[0].ToString() + ",'" + tableEditor.DataRow[1].ToString() + "'," + ((ListComboBoxItem)tableEditor.DataRow[2]).Value + ")", DbConnection).ExecuteNonQuery();
                TableObject materials = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < materials.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_arrival`(`contract_id`,`material_id`,`amount`) VALUES (" + tableEditor.DataRow[0].ToString() + "," + ((ListComboBoxItem)materials.Data[i][0]).Value + ","+ materials.Data[i][2].ToString() + ")", DbConnection).ExecuteNonQuery();
                }
                Crutches.UpdateMaterialRegister(DbConnection);
                UpdateTable();
            }

            tableEditor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if(table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            new OleDbCommand("DELETE FROM `materials_arrival` WHERE `contract_id`=" + Table.Data[table1.row_selected][0].ToString(), DbConnection).ExecuteNonQuery();
            new OleDbCommand("DELETE FROM `purchase_contracts` WHERE `id`=" + Table.Data[table1.row_selected][0].ToString(), DbConnection).ExecuteNonQuery();
            Crutches.UpdateMaterialRegister(DbConnection);
            table1.row_selected = -1;
            UpdateTable();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            string id = Table.Data[table1.row_selected][0].ToString();
            FormTableEditor tableEditor = new FormTableEditor("Изменить", Table.Columns, Table.Data[table1.row_selected]);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("DELETE FROM `materials_arrival` WHERE `contract_id`=" + id, DbConnection).ExecuteNonQuery();
                new OleDbCommand("UPDATE `purchase_contracts` SET `id`=" + tableEditor.DataRow[0].ToString() + ",`time`='" + tableEditor.DataRow[1].ToString() + "',`suppliers_id`=" + ((ListComboBoxItem)tableEditor.DataRow[2]).Value + " WHERE `id`=" + id, DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_arrival`(`contract_id`,`material_id`,`amount`) VALUES ('" + tableEditor.DataRow[0].ToString() + "'," + ((ListComboBoxItem)services.Data[i][0]).Value + "," + services.Data[i][2].ToString() + ")", DbConnection).ExecuteNonQuery();
                }
                Crutches.UpdateMaterialRegister(DbConnection);
                UpdateTable();
            }

            tableEditor.Dispose();
        }

        private void period_ValueChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }
    }
}
