﻿using BeautySalon.Components.Themes;
using BeautySalon.DB;
using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageMain : UserControl, IThemable
    {
        private readonly AppDatabase _DB;
        private List<Order> orders;
        private List<Staff> staffs;
        private List<Service> services;
        private List<Material> materials;
        private readonly Dictionary<string, List<ComboBoxItem>> dictionary = new Dictionary<string, List<ComboBoxItem>>();

        public PageMain()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            viewTableData.TableHeaders = new List<string> { "Время", "ФИО", "Номер телефона", "Услуги", "Сотрудник", "Примечание" };
            viewTableData.TableWeights = new int[] { 0, 1, 0 };
            viewTableData.Clear();
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
                itemServices.Add(new ComboBoxItem(service.Name + " - " + service.Price + " руб.", service.Id.ToString()));
            }
            dictionary.Add("ServiceId", itemServices);

            materials = await _DB.MaterialDAO.GetAll();
            List<ComboBoxItem> itemMaterials = new List<ComboBoxItem>();
            foreach (Material material in materials)
            {
                itemMaterials.Add(new ComboBoxItem(material.Name, material.Id.ToString()));
            }
            dictionary.Add("MaterialId", itemMaterials);

            UpdateTable();
        }

        private async Task UpdateStatistic()
        {
            try
            {
                float servicesSum = 0;
                float materialSum = 0;

                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(`id`) FROM `orders` WHERE `time` >= @time_start AND `time` <= @time_end AND `status` = @status", _DB.Connection))
                {
                    command.Parameters.AddWithValue("@time_start", DateTime.Today);
                    command.Parameters.AddWithValue("@time_end", DateTime.Today.AddDays(1));
                    command.Parameters.AddWithValue("@status", Order.OrderStatus.Created);

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            label2.Text = reader.IsDBNull(0) ? "0" : reader.GetInt32(0).ToString();
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand("SELECT COUNT(`id`) FROM `orders` WHERE `time` >= @time_start AND `time` < @time_end", _DB.Connection))
                {
                    DateTime today = DateTime.Today;
                    command.Parameters.AddWithValue("@time_start", new DateTime(today.Year, today.Month, 1));
                    command.Parameters.AddWithValue("@time_end", new DateTime(today.Year, today.Month, 1).AddMonths(1));

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            label4.Text = reader.IsDBNull(0) ? "0" : reader.GetInt32(0).ToString();
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand("SELECT SUM(`services_fulfilled`.`price`) FROM `orders` INNER JOIN `services_fulfilled` ON `orders`.`id`=`services_fulfilled`.`order_id` WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end", _DB.Connection))
                {
                    DateTime today = DateTime.Today;
                    command.Parameters.AddWithValue("@time_start", new DateTime(today.Year, today.Month, 1));
                    command.Parameters.AddWithValue("@time_end", new DateTime(today.Year, today.Month, 1).AddMonths(1));

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            servicesSum = reader.IsDBNull(0) ? 0 : reader.GetFloat(0);
                            label6.Text = servicesSum + " руб.";
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand("SELECT SUM(`materials_consumption`.`price` * `materials_consumption`.`amount`) FROM `orders` INNER JOIN `materials_consumption` ON `orders`.`id`=`materials_consumption`.`order_id` WHERE `orders`.`time` >= @time_start AND `orders`.`time` <= @time_end", _DB.Connection))
                {
                    DateTime today = DateTime.Today;
                    command.Parameters.AddWithValue("@time_start", new DateTime(today.Year, today.Month, 1));
                    command.Parameters.AddWithValue("@time_end", new DateTime(today.Year, today.Month, 1).AddMonths(1));

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            materialSum = reader.IsDBNull(0) ? 0 : reader.GetFloat(0);
                            label8.Text = materialSum + " руб.";
                        }
                    }
                }

                label10.Text = (servicesSum - materialSum) + " руб.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка вывода статистики:" + ex.Message);
            }
        }

        private async void UpdateTable()
        {
            await UpdateStatistic();

            viewTableData.Clear();
            orders = await _DB.OrderDAO.GetByPeriodAndStatus(DateTime.Today, DateTime.Today.AddDays(1), Order.OrderStatus.Created);
            List<List<string>> tableData = new List<List<string>>();

            foreach (Order order in orders)
            {
                string servicesText = "";
                List<ServiceFulfilled> servicesOrder = await _DB.ServiceFulfilledDAO.GetByOrderId(order.Id);
                for (int i = 0; i < servicesOrder.Count; i++)
                {
                    ServiceFulfilled sf = servicesOrder[i];
                    servicesText += (i + 1) + ". " + GetServiceById(sf.ServiceId)?.Name + " - " + sf.Price + " руб.";

                    if (i < (servicesOrder.Count - 1))
                    {
                        servicesText += "\n";
                    }
                }

                tableData.Add(new List<string>{
                    order.Time.ToShortTimeString(),
                    order.FullName,
                    order.PhoneNumber,
                    servicesText,
                    GetStaffById(order.StaffId)?.FullName,
                    order.Remark
                });
            }

            viewTableData.TableData = tableData;
        }

        private Staff GetStaffById(int id)
        {
            foreach (Staff staff in staffs)
            {
                if (Equals(staff.Id, id))
                {
                    return staff;
                }
            }

            return null;
        }

        private Service GetServiceById(int id)
        {
            foreach (Service service in services)
            {
                if (Equals(service.Id, id))
                {
                    return service;
                }
            }

            return null;
        }

        private Material GetMaterialById(int id)
        {
            foreach (Material material in materials)
            {
                if (Equals(material.Id, id))
                {
                    return material;
                }
            }

            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            curentTime.Text = FormatTodayDate();
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            OrderCreateModel model = new OrderCreateModel()
            {
                Time = DateTime.Now,
                PhoneNumber = "+7"
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", model, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                int orderId = await _DB.OrderDAO.GetNewId();

                try
                {
                    await _DB.OrderDAO.Insert(
                        orderId,
                        model.Time,
                        model.FullName,
                        model.PhoneNumber,
                        model.StaffId,
                        model.Remark,
                        Order.OrderStatus.Created
                    );

                    foreach (ServiceModel sm in model.Services)
                    {
                        Service service = GetServiceById(sm.ServiceId);

                        await _DB.ServiceFulfilledDAO.Insert(
                            orderId,
                            service.Id,
                            service.Price
                        );
                    }
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при создании записи:\n" + ex.Message);
                }

                UpdateTable();
            }

            editor.Dispose();
        }

        private async void ButtonComplete_Click(object sender, EventArgs e)
        {
            if (viewTableData.SelectedRow < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }

            Order order = orders[viewTableData.SelectedRow];
            OrderCompleteModel model = new OrderCompleteModel()
            {
                Time = order.Time,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                Services = new List<ServiceModel>(),
                Materials = new List<MaterialModel>(),
                StaffId = order.StaffId,
                Remark = order.Remark
            };

            List<ServiceFulfilled> servicesOrder = await _DB.ServiceFulfilledDAO.GetByOrderId(order.Id);
            foreach (ServiceFulfilled sf in servicesOrder)
            {
                model.Services.Add(new ServiceModel { ServiceId = sf.ServiceId });
            }
            List<MaterialConsumption> materialsOrder = await _DB.MaterialConsumptionDAO.GetByOrderId(order.Id);
            foreach (MaterialConsumption mc in materialsOrder)
            {
                model.Materials.Add(new MaterialModel { MaterialId = mc.MaterialId, Amount = mc.Amount });
            }

            FormEntityEditor editor = new FormEntityEditor("Выполнить", model, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.ServiceFulfilledDAO.DeleteByOrderId(order.Id);
                    foreach (ServiceModel sm in model.Services)
                    {
                        Service service = GetServiceById(sm.ServiceId);

                        await _DB.ServiceFulfilledDAO.Insert(
                            order.Id,
                            service.Id,
                            service.Price
                        );
                    }

                    await _DB.MaterialConsumptionDAO.DeleteByOrderId(order.Id);
                    foreach (MaterialModel mc in model.Materials)
                    {
                        Material material = GetMaterialById(mc.MaterialId);

                        await _DB.MaterialConsumptionDAO.Insert(
                            order.Id,
                            material.Id,
                            material.Price,
                            mc.Amount
                        );
                    }

                    await _DB.OrderDAO.Update(
                        order.Id,
                        model.Time,
                        model.FullName,
                        model.PhoneNumber,
                        model.StaffId,
                        model.Remark,
                        Order.OrderStatus.Complete
                    );
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при изменении записи:\n" + ex.Message);
                }

                UpdateTable();
            }

            editor.Dispose();
        }

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (viewTableData.SelectedRow < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }

            Order order = orders[viewTableData.SelectedRow];
            order.Status = Order.OrderStatus.Cancel;

            try
            {
                await _DB.OrderDAO.Update(order);

                UpdateTable();
            }
            catch (Exception ex)
            {
                AlertBox.Error("Ошибка при обновлении записи:\n" + ex.Message);
            }
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

    public class OrderCompleteModel
    {
        [DisplayName("Время")]
        public DateTime Time { get; set; }

        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [DisplayName("Номер")]
        public string PhoneNumber { get; set; }

        [DisplayName("Услуги")]
        public List<ServiceModel> Services { get; set; } = new List<ServiceModel>();

        [DisplayName("Материалы")]
        public List<MaterialModel> Materials { get; set; } = new List<MaterialModel>();

        [DisplayName("Сотрудник")]
        public int StaffId { get; set; }

        [DisplayName("Примечание")]
        [TextMultiline]
        public string Remark { get; set; }
    }
}
