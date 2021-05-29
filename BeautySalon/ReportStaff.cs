using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class ReportStaff : UserControl
    {
        private OleDbConnection DbConnection;

        public ReportStaff(OleDbConnection DbConnection)
        {
            this.DbConnection = DbConnection;
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);

            using (OleDbCommand command = new OleDbCommand("SELECT `id`,`full_name` FROM `staff` WHERE 1", DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read()) comboBox1.Items.Add(new ComboBoxItem(reader.GetString(1), reader[0].ToString()));
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            }
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText("//========================================================================\\\\\r\n");
            textBox1.AppendText("Работник:\r\n");
            textBox1.AppendText("   " + ((ComboBoxItem)comboBox1.SelectedItem).Text + "\r\n");
            textBox1.AppendText("Период:\r\n");
            textBox1.AppendText("   "+dateTimePicker1.Value.ToShortDateString()+"\r\n");
            textBox1.AppendText("   "+dateTimePicker2.Value.ToShortDateString()+"\r\n");
            textBox1.AppendText("//========================================================================\\\\\r\n");
            textBox1.AppendText("//========================================================================\\\\\r\n");
            textBox1.AppendText("//========================================================================\\\\\r\n");
            using (OleDbCommand command = new OleDbCommand("SELECT COUNT(`time`) FROM `notes` WHERE `time`>=CDate('" + dateTimePicker1.Value.ToString() + "') AND `time`<=CDate('" + dateTimePicker2.Value.ToString() + "') AND `staff`="+ ((ComboBoxItem)comboBox1.SelectedItem).Value, DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                textBox1.AppendText("Обслужено посетителей за период: " + reader.GetInt32(0) + "\r\n");
            }
            textBox1.AppendText("//========================================================================\\\\\r\n");
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`) FROM `notes` LEFT JOIN (`services_rendered` LEFT JOIN `services` ON `services`.`id`=`services_rendered`.`services_id`) ON `notes`.`time`=`services_rendered`.`time` WHERE `notes`.`time`>=CDate('" + dateTimePicker1.Value.ToString() + "') AND `notes`.`time`<=CDate('" + dateTimePicker2.Value.ToString() + "') AND `staff`=" + ((ComboBoxItem)comboBox1.SelectedItem).Value, DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                double sum = 0;
                if(reader[0].GetType() != typeof(DBNull)) sum = reader.GetDouble(0);
                textBox1.AppendText("Оказано услуг на: " + sum + " руб.\r\n");
            }
            textBox1.AppendText("//========================================================================\\\\\r\n");
            using (OleDbCommand command = new OleDbCommand("SELECT SUM(`prise`) FROM `notes` LEFT JOIN (`materials_consumption` LEFT JOIN `materials` ON `materials`.`id`=`materials_consumption`.`material_id`) ON `notes`.`time`=`materials_consumption`.`time` WHERE `notes`.`time`>=CDate('" + dateTimePicker1.Value.ToString() + "') AND `notes`.`time`<=CDate('" + dateTimePicker2.Value.ToString() + "') AND `staff`=" + ((ComboBoxItem)comboBox1.SelectedItem).Value, DbConnection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                double sum = 0;
                if (reader[0].GetType() != typeof(DBNull)) sum = reader.GetDouble(0);
                textBox1.AppendText("Израсходованно материалов на: " + sum + " руб.\r\n");
            }
            textBox1.AppendText("//========================================================================\\\\\r\n");
        }
    }
}
