using BeautySalon.Components.Themes;
using BeautySalon.DB;
using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageMain : UserControl, IThemable
    {
        private readonly AppDatabase _DB;
        private List<Staff> staffs;
        private List<Service> services;
        private List<Material> materials;
        private readonly Dictionary<string, List<ComboBoxItem>> dictionary = new Dictionary<string, List<ComboBoxItem>>();

        public PageMain()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            materialTable1.TableHeaders = new List<string> { "Время", "ФИО", "Номер телефона", "Услуги", "Сотрудник", "Примечание" };
            curentTime.Text = FormatTodayDate();

            InitTables();
        }

        public void ApplyTheme(ITheme theme)
        {
            BackColor = theme.ColorBackground;
            ForeColor = theme.ColorForeground;
            Font = theme.Font;
            panelButtons.BackColor = theme.ColorBackgroungDark;
        }

        private string FormatTodayDate()
        {
            DateTime now = DateTime.Now;
            string dayweek = "";
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday: dayweek = "Пн"; break;
                case DayOfWeek.Tuesday: dayweek = "Вт"; break;
                case DayOfWeek.Wednesday: dayweek = "Ср"; break;
                case DayOfWeek.Thursday: dayweek = "Чт"; break;
                case DayOfWeek.Friday: dayweek = "Пт"; break;
                case DayOfWeek.Saturday: dayweek = "Сб"; break;
                case DayOfWeek.Sunday: dayweek = "Вс"; break;
            }
            return dayweek + " " + now.Day + "/" + now.Month + "/" + now.Year + " " + now.Hour.ToString().PadLeft(2, '0') + ':' + now.Minute.ToString().PadLeft(2, '0') + ':' + now.Second.ToString().PadLeft(2, '0');
        }

        private async void InitTables()
        {
            staffs = await _DB.StaffDAO.GetAll();
            List<ComboBoxItem> itemStaffs = new List<ComboBoxItem>();
            foreach (Staff staff in staffs)
            {
                itemStaffs.Add(new ComboBoxItem(staff.FullName, staff.Id.ToString()));
            }
            dictionary.Add("StaffId", itemStaffs);

            services = await _DB.ServiceDAO.GetAll();
            List<ComboBoxItem> itemServices = new List<ComboBoxItem>();
            foreach (Service service in services)
            {
                itemServices.Add(new ComboBoxItem(service.Name, service.Id.ToString()));
            }
            dictionary.Add("ServiceId", itemServices);

            materials = await _DB.MaterialDAO.GetAll();
            List<ComboBoxItem> itemMaterials = new List<ComboBoxItem>();
            foreach (Material material in materials)
            {
                itemMaterials.Add(new ComboBoxItem(material.Name, material.Id.ToString()));
            }
            dictionary.Add("MaterialId", itemServices);

            UpdateTable();
        }

        private void UpdateStatistic()
        {
            /*double services_sum = 0;
            double material_sum = 0;
            using (OleDbCommand command = new OleDbCommand("SELECT COUNT(`time`) FROM `notes` WHERE `time`>=CDate('" + DateTime.Today.ToString() + "') AND `time`<CDate('" + DateTime.Today.AddDays(1).ToString() + "') AND `completed`=false", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                label2.Text = reader[0].ToString();
            }
            using (OleDbCommand command = new OleDbCommand("SELECT COUNT(`time`) FROM `notes` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "') AND `completed`=true", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                label4.Text = reader[0].ToString();
            }
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`*`materials_consumption`.`amount`) FROM `materials_consumption` LEFT JOIN `materials` ON `materials`.`id`=`materials_consumption`.`material_id` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                material_sum = (reader[0].GetType() == typeof(DBNull) ? 0 : double.Parse(reader[0].ToString()));
                label8.Text = material_sum + " руб.";
            }
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`) FROM `services_rendered` LEFT JOIN `services` ON `services`.`id`=`services_rendered`.`services_id` WHERE `time`>=CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).ToString() + "') AND `time`<CDate('" + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).ToString() + "')", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                services_sum = (reader[0].GetType() == typeof(DBNull) ? 0 : double.Parse(reader[0].ToString())) + material_sum;
                label6.Text = services_sum + " руб.";
            }
            label10.Text = (services_sum - material_sum) + " руб.";*/
        }

        private async void UpdateTable()
        {
            UpdateStatistic();

            List<Order> appointments = await _DB.OrderDAO.GetByPeriod(DateTime.Today, DateTime.Today.AddDays(1));
            List<List<string>> tableData = new List<List<string>>();

            foreach (Order appointment in appointments)
            {
                tableData.Add(new List<string>{
                    appointment.Time.ToShortTimeString(),
                    appointment.FullName,
                    appointment.PhoneNumber,
                    "",
                    appointment.StaffId.ToString(),
                    appointment.Remark
                });
            }

            materialTable1.TableData = tableData;

            /*using (OleDbCommand command = new OleDbCommand("SELECT `time`,`full_name`,`phone_number`,`staff`,`remark` FROM `notes` WHERE `time`>=CDate('" + DateTime.Today.ToString() + "') AND `time`<CDate('" + DateTime.Today.AddDays(1).ToString() + "') AND `completed`=false", DbConnection))
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

            for (int i = 0; i < Table.Data.Length; i++)
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
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            curentTime.Text = FormatTodayDate();
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            OrderCreateModel model = new OrderCreateModel()
            {
                Time = DateTime.Now
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", model, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                /*new OleDbCommand("INSERT INTO `notes`(`time`,`full_name`,`phone_number`,`staff`,`remark`,`completed`) VALUES ('" + tableEditor.GetString(0) + "','" + tableEditor.GetString(1) + "','" + tableEditor.GetString(2) + "'," + tableEditor.GetString(4) + ",'" + tableEditor.GetString(5) + "',false)", DbConnection).ExecuteNonQuery();

                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES ('" + tableEditor.GetString(0) + "'," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                UpdateTable();*/
            }

            editor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            /*if (table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            new OleDbCommand("DELETE FROM `services_rendered` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
            new OleDbCommand("DELETE FROM `materials_consumption` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
            new OleDbCommand("DELETE FROM `notes` WHERE `time`=CDate('" + Table.Data[table1.row_selected][0] + "')", DbConnection).ExecuteNonQuery();
            table1.row_selected = -1;
            UpdateTable();*/
        }

        private void DoubleClickOnTable(int row, string[] data)
        {
            /*string time = Table.Data[row][0].ToString();
            FormTableEditor tableEditor = new FormTableEditor("Изменить", Table.Columns, Table.Data[row]);
            tableEditor.ShowDialog();

            if (tableEditor.Confirmed == true)
            {
                new OleDbCommand("DELETE FROM `services_rendered` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("DELETE FROM `materials_consumption` WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                new OleDbCommand("UPDATE `notes` SET `time`='" + tableEditor.GetString(0) + "',`full_name`='" + tableEditor.GetString(1) + "',`phone_number`='" + tableEditor.GetString(2) + "',`staff`=" + tableEditor.GetString(4) + ",`remark`='" + tableEditor.GetString(5) + "',`completed`=false WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();

                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES (CDate('" + tableEditor.GetString(0) + "')," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                UpdateTable();
            }

            tableEditor.Dispose();*/
        }

        private void ButtonComplete_Click(object sender, EventArgs e)
        {
            /*if (table1.row_selected < 0)
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
                new OleDbCommand("UPDATE `notes` SET `time`='" + tableEditor.GetString(0) + "',`full_name`='" + tableEditor.GetString(1) + "',`phone_number`='" + tableEditor.GetString(2) + "',`staff`=" + tableEditor.GetString(5) + ",`remark`='" + tableEditor.GetString(6) + "',`completed`=true WHERE `time`=CDate('" + time + "')", DbConnection).ExecuteNonQuery();
                TableObject services = (TableObject)tableEditor.DataRow[3];
                for (int i = 0; i < services.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `services_rendered`(`time`, `services_id`) VALUES (CDate('" + tableEditor.GetString(0) + "')," + ((ListComboBoxItem)services.Data[i][0]).Value + ")", DbConnection).ExecuteNonQuery();
                }
                TableObject tmp_materials = (TableObject)tableEditor.DataRow[4];
                for (int i = 0; i < tmp_materials.Data.Length; i++)
                {
                    new OleDbCommand("INSERT INTO `materials_consumption`(`time`,`material_id`,`amount`) VALUES ('" + tableEditor.GetString(0) + "'," + ((ListComboBoxItem)tmp_materials.Data[i][0]).Value + "," + tmp_materials.Data[i][1] + ")", DbConnection).ExecuteNonQuery();
                }
                UpdateTable();
            }

            tableEditor.Dispose();*/
        }
    }

    public class OrderCreateModel
    {
        [DisplayName("Время")]
        public DateTime Time { get; set; }
        [DisplayName("ФИО")]
        public string FullName { get; set; }
        [DisplayName("Номер")]
        public string PhoneNumber { get; set; }
        [DisplayName("Услуги")]
        public List<ServiceModel> Services { get; set; } = new List<ServiceModel>();
        [DisplayName("Сотрудник")]
        public int StaffId { get; set; }
        [DisplayName("Примечание")]
        [TextMultiline]
        public string Remark { get; set; }
    }

    public class ServiceModel
    {
        [DisplayName("Услуга")]
        public string ServiceId { get; set; }
    }
}
