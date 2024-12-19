using BeautySalon.DB;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageReportStaff : UserControl
    {
        private readonly AppDatabase _DB;

        public PageReportStaff()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);

            InitStaffs();
        }

        private async void InitStaffs()
        {
            using (MySqlCommand command = new MySqlCommand("SELECT `id`, `full_name` FROM `staffs`", _DB.Connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        comboBox1.Items.Add(new ComboBoxItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                    if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
                }
            }
        }

        private async void ButtonReport_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            try
            {
                textBox1.AppendText("//========================================================================\\\\\r\n");
                textBox1.AppendText("Работник:\r\n");
                textBox1.AppendText("   " + ((ComboBoxItem)comboBox1.SelectedItem).Text + "\r\n");
                textBox1.AppendText("Период:\r\n");
                textBox1.AppendText("   " + dateTimePicker1.Value.ToShortDateString() + "\r\n");
                textBox1.AppendText("   " + dateTimePicker2.Value.ToShortDateString() + "\r\n");
                textBox1.AppendText("//========================================================================\\\\\r\n");
                textBox1.AppendText("//========================================================================\\\\\r\n");
                textBox1.AppendText("//========================================================================\\\\\r\n");
                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(`id`) FROM `orders` WHERE `time` >= @time_start AND `time` <= @time_end AND `staff_id` = @staff_id", _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            textBox1.AppendText("Обслужено посетителей за период: " + reader.GetInt32(0) + "\r\n");
                        }
                    }
                }
                textBox1.AppendText("//========================================================================\\\\\r\n");
                using (MySqlCommand command = new MySqlCommand("SELECT SUM(`services_fulfilled`.`price`) FROM `orders` INNER JOIN `services_fulfilled` ON `orders`.`id`=`services_fulfilled`.`order_id` WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end AND `staff_id` = @staff_id", _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            textBox1.AppendText("Оказано услуг на: " + (reader.IsDBNull(0) ? 0 : reader.GetFloat(0)) + " руб.\r\n");
                        }
                    }
                }
                textBox1.AppendText("//========================================================================\\\\\r\n");
                using (MySqlCommand command = new MySqlCommand("SELECT SUM(`materials_consumption`.`price`) FROM `orders` INNER JOIN `materials_consumption` ON `orders`.`id`=`materials_consumption`.`order_id` WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end AND `staff_id` = @staff_id", _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@time_end", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@staff_id", ((ComboBoxItem)comboBox1.SelectedItem).Value);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            textBox1.AppendText("Израсходованно материалов на: " + (reader.IsDBNull(0) ? 0 : reader.GetFloat(0)) + " руб.\r\n");
                        }
                    }
                }
                textBox1.AppendText("//========================================================================\\\\\r\n");
            }
            catch (Exception ex)
            {
                textBox1.Clear();
                AlertBox.Error("Ошибка формирования отчета:\n" + ex.Message);
            }
        }
    }
}
