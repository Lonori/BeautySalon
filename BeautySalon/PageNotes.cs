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
    public partial class PageNotes : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table;
        private ListComboBoxItem list_staff = new ListComboBoxItem();
        private ListComboBoxItem list_services = new ListComboBoxItem();
        private ListComboBoxItem list_materials = new ListComboBoxItem();

        public PageNotes(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);
            dateTimePicker1.ValueChanged += period_ValueChanged;
            dateTimePicker2.ValueChanged += period_ValueChanged;

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`full_name` FROM `staff` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list_staff.Add(new ComboBoxItem(reader[1].ToString(), reader[0].ToString()));
                reader.Close();
            }

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`service_name` FROM `services` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list_services.Add(new ComboBoxItem(reader[1].ToString(), reader[0].ToString()));
                reader.Close();
            }

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`material` FROM `materials` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    list_materials.Add(new ComboBoxItem(reader[1].ToString(), reader[0].ToString()));
                reader.Close();
            }

            TableObject services = new TableObject(
                new TableColumn[] {
                    new TableColumnList("Услуги", 100, list_services)
                },
                new TableData()
            );
            TableObject materials = new TableObject(
                new TableColumn[] {
                    new TableColumnList("Материал", 80, list_materials),
                    new TableColumnText("Кол-во", 20, "0")
                },
                new TableData()
            );
            TableColumn[] columns = new TableColumn[] {
                new TableColumnDate("Время", 10, true),
                new TableColumnText("ФИО", 15, "", true),
                new TableColumnText("Номер телефона", 10, "+7"),
                new TableColumnTable("Услуги", 10, services),
                new TableColumnTable("Материалы", 10, materials),
                new TableColumnList("Сотрудник", 15, list_staff),
                new TableColumnText("Примечание", 22, "", true),
                new TableColumnText("Завершен", 8, "0")
            };
            Table = new TableObject(columns, new TableData());

            table1.TableInit(Table);
            UpdateTable();
        }

        private void UpdateTable()
        {
            table1.Clear();
            Table.Data.Clear();

            using (OleDbCommand command = new OleDbCommand("SELECT `time`,`full_name`,`phone_number`,`staff`,`remark`,`completed` FROM `notes` WHERE `time`>=CDate('" + dateTimePicker1.Value.ToString()+ "') AND `time`<=CDate('" + dateTimePicker2.Value.ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object[] data_arr = new object[] {
                        reader[0],
                        reader[1].ToString(),
                        reader[2].ToString(),
                        null,
                        null,
                        list_staff.CopyOnNewValue(reader[3].ToString()),
                        reader[4].ToString(),
                        (((bool)reader[5]) == true ? 1 : 0).ToString()
                    };
                    Table.Data.Add(data_arr);
                }
                reader.Close();
            }

            for (int i = 0; i < Table.Data.Length; i++)
            {
                TableObject services = new TableObject(
                    new TableColumn[] {
                        new TableColumnList("Услуги", 100, list_services)
                    },
                    new TableData()
                );
                TableObject materials = new TableObject(
                    new TableColumn[] {
                        new TableColumnList("Материал", 80, list_materials),
                        new TableColumnText("Кол-во", 20, "0")
                    },
                    new TableData()
                );

                using (OleDbCommand command = new OleDbCommand("SELECT `services_id` FROM `services_rendered` WHERE `time`=CDate('" + ((DateTime)Table.Data[i][0]).ToString() + "')", DbConnection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        services.Data.Add(new object[] { list_services.CopyOnNewValue(reader[0].ToString()) });
                    }
                    reader.Close();
                }
                using (OleDbCommand command = new OleDbCommand("SELECT `material_id`,`amount` FROM `materials_consumption` WHERE `time`=CDate('" + ((DateTime)Table.Data[i][0]).ToString() + "')", DbConnection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        materials.Data.Add(new object[] { list_materials.CopyOnNewValue(reader[0].ToString()), reader[1].ToString() });
                    }
                    reader.Close();
                }

                Table.Data[i][3] = services;
                Table.Data[i][4] = materials;
                table1.AddRow(Table.Data[i]);
            }
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            FormTableEditor tableEditor = new FormTableEditor("Добавить", Table.Columns, Table.GetDefault());
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("INSERT INTO `notes`(`time`,`full_name`,`phone_number`,`staff`,`remark`,`completed`) VALUES ('" + tableEditor.DataRow[0].ToString() + "','" + tableEditor.DataRow[1].ToString() + "','" + tableEditor.DataRow[2].ToString() + "'," + ((ListComboBoxItem)tableEditor.DataRow[5]).Value + ",'" + tableEditor.DataRow[6].ToString() + "','" + tableEditor.DataRow[7].ToString() + "')", DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`,`services_id`) VALUES ('" + tableEditor.DataRow[0].ToString() + "'," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                TableObject materials = (TableObject)tableEditor.DataRow[4];
                for (int i = 0; i < materials.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_consumption`(`time`,`material_id`,`amount`) VALUES ('" + tableEditor.DataRow[0].ToString() + "'," + ((ListComboBoxItem)materials.Data[i][0]).Value + ","+ materials.Data[i][1] + ")", DbConnection).ExecuteNonQuery();
                }
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
            new OleDbCommand("DELETE FROM `services_rendered` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
            new OleDbCommand("DELETE FROM `materials_consumption` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
            new OleDbCommand("DELETE FROM `notes` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
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
            string time = Table.Data[table1.row_selected][0].ToString();
            FormTableEditor tableEditor = new FormTableEditor("Изменить", Table.Columns, Table.Data[table1.row_selected]);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("DELETE FROM `services_rendered` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("DELETE FROM `materials_consumption` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("UPDATE `notes` SET `time`='"+ tableEditor.DataRow[0].ToString() + "',`full_name`='" + tableEditor.DataRow[1].ToString() + "',`phone_number`='" + tableEditor.DataRow[2].ToString() + "',`staff`=" + ((ListComboBoxItem)tableEditor.DataRow[5]).Value + ",`remark`='" + tableEditor.DataRow[6].ToString() + "',`completed`='" + tableEditor.DataRow[7].ToString() + "' WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES (CDate('" + tableEditor.DataRow[0].ToString() + "')," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                TableObject materials = (TableObject)tableEditor.DataRow[4];
                for (int i = 0; i < materials.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_consumption`(`time`,`material_id`,`amount`) VALUES ('" + tableEditor.DataRow[0].ToString() + "'," + ((ListComboBoxItem)materials.Data[i][0]).Value + "," + materials.Data[i][1] + ")", DbConnection).ExecuteNonQuery();
                }
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
