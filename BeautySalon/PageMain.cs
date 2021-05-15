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
    public partial class PageMain : UserControl
    {
        private OleDbConnection DbConnection;
        private TableObject Table;
        private ListComboBoxItem list_staff = new ListComboBoxItem();
        private ListComboBoxItem list_services = new ListComboBoxItem();
        private ListComboBoxItem list_materials = new ListComboBoxItem();

        public PageMain(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();
            table1.PreAddRow += FormatTableNotes;
            curentTime.Text = FormatTodayDate();

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

            Table = new TableObject(
                new TableColumn[] {
                        new TableColumnDate("Время", 10, true),
                        new TableColumnText("ФИО", 20, "", true),
                        new TableColumnText("Номер телефона", 10, "+7"),
                        new TableColumnTable("Услуги", 15, services),
                        new TableColumnList("Сотрудник", 20, list_staff),
                        new TableColumnText("Примечание", 25, "", true)
                },
                new TableData()
            );

            table1.TableInit(Table);
            UpdateTable();
        }

        private string FormatTodayDate()
        {
            DateTime now = DateTime.Now;
            string dayweek = "";
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday: dayweek = "Понедельник"; break;
                case DayOfWeek.Tuesday: dayweek = "Вторник"; break;
                case DayOfWeek.Wednesday: dayweek = "Среда"; break;
                case DayOfWeek.Thursday: dayweek = "Четверг"; break;
                case DayOfWeek.Friday: dayweek = "Пятница"; break;
                case DayOfWeek.Saturday: dayweek = "Суббота"; break;
                case DayOfWeek.Sunday: dayweek = "Воскресенье"; break;
            }
            return "Сегодня: " + now.Day + " " + dayweek + " - " + now.ToShortTimeString();
        }

        private string[] FormatTableNotes(object[] data)
        {
            string[] str_data = new string[]
            {
                 ((DateTime)data[0]).ToShortTimeString(),
                 data[1].ToString(),
                 data[2].ToString(),
                 data[3].ToString(),
                 data[4].ToString(),
                 data[5].ToString(),
            };
            return str_data;
        }

        private void UpdateStatistic()
        {
            float services_sum = 0;
            float material_sum = 0;
            using (OleDbCommand command = new OleDbCommand("SELECT COUNT(`time`) FROM `notes` WHERE `time`>=CDate('" + DateTime.Today.ToString() + "') AND `time`<CDate('" + DateTime.Today.AddDays(1).ToString() + "') AND `completed`=0", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                label2.Text = reader[0].ToString();
                reader.Close();
            }
            using (OleDbCommand command = new OleDbCommand("SELECT COUNT(`time`) FROM `notes` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "') AND `completed`=1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                label4.Text = reader[0].ToString();
                reader.Close();
            }
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`) FROM `services_rendered` LEFT JOIN `services` ON `services`.`id`=`services_rendered`.`services_id` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                services_sum = (reader[0].GetType() == typeof(DBNull) ? 0 : float.Parse(reader[0].ToString()));
                label6.Text = services_sum + " руб.";
                reader.Close();
            }
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`*`materials_consumption`.`amount`) FROM `materials_consumption` LEFT JOIN `materials` ON `materials`.`id`=`materials_consumption`.`material_id` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                material_sum = (reader[0].GetType() == typeof(DBNull) ? 0 : float.Parse(reader[0].ToString()));
                label8.Text = material_sum + " руб.";
                reader.Close();
            }
            label10.Text = (services_sum - material_sum) + " руб.";
        }

        private void UpdateTable()
        {
            UpdateStatistic();
            table1.Clear();
            Table.Data.Clear();

            using (OleDbCommand command = new OleDbCommand("SELECT `time`,`full_name`,`phone_number`,`staff`,`remark` FROM `notes` WHERE `time`>=CDate('"+DateTime.Today.ToString()+ "') AND `time`<CDate('" + DateTime.Today.AddDays(1).ToString() + "') AND `completed`=0", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object[] data_arr = new object[] {
                        reader[0],
                        reader[1].ToString(),
                        reader[2].ToString(),
                        null,
                        list_staff.CopyOnNewValue(reader[3].ToString()),
                        reader[4].ToString()
                    };
                    Table.Data.Add(data_arr);
                }
                reader.Close();
            }

            for(int i = 0; i < Table.Data.Length; i++)
            {
                TableObject services = new TableObject(
                    new TableColumn[] {
                        new TableColumnList("Услуги", 100, list_services)
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

                Table.Data[i][3] = services;
                table1.AddRow(Table.Data[i]);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            curentTime.Text = FormatTodayDate();
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            FormTableEditor tableEditor = new FormTableEditor("Добавить", Table.Columns, Table.GetDefault());
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("INSERT INTO `notes`(`time`,`full_name`,`phone_number`,`staff`,`remark`,`completed`) VALUES ('" + tableEditor.DataRow[0].ToString() + "','" + tableEditor.DataRow[1].ToString() + "','" + tableEditor.DataRow[2].ToString() + "'," + ((ListComboBoxItem)tableEditor.DataRow[4]).Value + ",'" + tableEditor.DataRow[5].ToString() + "',0)", DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES ('"+ tableEditor.DataRow[0].ToString() + "',"+ ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                UpdateTable();
            }

            tableEditor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            string time = Table.Data[table1.row_selected][0].ToString();
            TableObject materials = new TableObject(
                new TableColumn[] {
                    new TableColumnList("Материал", 80, list_materials),
                    new TableColumnText("Кол-во", 20, "0")
                },
                new TableData()
            );
            TableColumn[] columns = new TableColumn[] {
                new TableColumnDate("Время", 10, true),
                new TableColumnText("ФИО", 20, "", true),
                new TableColumnText("Номер телефона", 10, "+7"),
                new TableColumnTable("Услуги", 10, new TableObject(new TableColumn[] { }, new TableData())),
                new TableColumnTable("Материалы", 10, materials),
                new TableColumnList("Сотрудник", 20, list_staff),
                new TableColumnText("Примечание", 20, "", true),
            };
            object[] table_row = Table.Data[table1.row_selected];
            object[] tmp_row = new object[]
            {
                table_row[0],
                table_row[1],
                table_row[2],
                table_row[3],
                materials,
                table_row[4],
                table_row[5]
            };
            FormTableEditor tableEditor = new FormTableEditor("Изменить", columns, tmp_row);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("DELETE FROM `services_rendered` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("DELETE FROM `materials_consumption` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("UPDATE `notes` SET `time`='" + tableEditor.DataRow[0].ToString() + "',`full_name`='" + tableEditor.DataRow[1].ToString() + "',`phone_number`='" + tableEditor.DataRow[2].ToString() + "',`staff`=" + ((ListComboBoxItem)tableEditor.DataRow[5]).Value + ",`remark`='" + tableEditor.DataRow[6].ToString() + "',`completed`=1 WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES (CDate('" + tableEditor.DataRow[0].ToString() + "')," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                TableObject tmp_materials = (TableObject)tableEditor.DataRow[4];
                for (int i = 0; i < tmp_materials.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_consumption`(`time`,`material_id`,`amount`) VALUES ('" + tableEditor.DataRow[0].ToString() + "'," + ((ListComboBoxItem)tmp_materials.Data[i][0]).Value + "," + tmp_materials.Data[i][1] + ")", DbConnection).ExecuteNonQuery();
                }
                UpdateTable();
            }

            tableEditor.Dispose();
        }
    }
}
