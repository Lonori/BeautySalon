using BeautySalon.DB;
using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageOrders : UserControl
    {
        private readonly AppDatabase _DB;
        private List<Order> orders;
        private List<Staff> staffs;
        private List<Service> services;
        private List<Material> materials;
        private readonly Dictionary<string, List<ComboBoxItem>> dictionary = new Dictionary<string, List<ComboBoxItem>>
        {
            {
                "OrderStatus", new List<ComboBoxItem> {
                    new ComboBoxItem("Создан", "1"),
                    new ComboBoxItem("Завершен", "3"),
                    new ComboBoxItem("Отменен", "4")
                }
            }
        };

        public PageOrders()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            viewTableData.TableHeaders = new List<string> { "Дата", "ФИО", "Номер телефона", "Услуги", "Материалы", "Сотрудник", "Примечание", "Статус" };
            viewTableData.ColumnWeights = new int[] { 0, 1, 0, 1, 1, 1, 1, 0 };
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);
            dateTimePicker1.ValueChanged += period_ValueChanged;
            dateTimePicker2.ValueChanged += period_ValueChanged;

            InitTables();
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

        private async void UpdateTable()
        {
            orders = await _DB.OrderDAO.GetByPeriod(dateTimePicker1.Value, dateTimePicker2.Value);
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

                string materialsText = "";
                List<MaterialConsumption> materialsOrder = await _DB.MaterialConsumptionDAO.GetByOrderId(order.Id);
                for (int i = 0; i < materialsOrder.Count; i++)
                {
                    MaterialConsumption mc = materialsOrder[i];
                    materialsText += (i + 1) + ". " + GetMaterialById(mc.MaterialId)?.Name + " - " + mc.Price + " руб.";

                    if (i < (materialsOrder.Count - 1))
                    {
                        materialsText += "\n";
                    }
                }

                string orderStatusText = "Неизвестен";
                foreach (ComboBoxItem item in dictionary["OrderStatus"])
                {
                    if (Equals(item.Value, ((int)order.Status).ToString()))
                    {
                        orderStatusText = item.Text;
                        break;
                    }
                }

                tableData.Add(new List<string>{
                    order.Time.ToShortDateString() + " " + order.Time.ToShortTimeString(),
                    order.FullName,
                    order.PhoneNumber,
                    servicesText,
                    materialsText,
                    GetStaffById(order.StaffId)?.FullName,
                    order.Remark,
                    orderStatusText
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

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            OrderModel model = new OrderModel()
            {
                Time = DateTime.Now,
                PhoneNumber = "+7",
                OrderStatus = 1
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

                    foreach (MaterialModel mc in model.Materials)
                    {
                        Material material = GetMaterialById(mc.MaterialId);

                        await _DB.MaterialConsumptionDAO.Insert(
                            orderId,
                            material.Id,
                            material.Price,
                            mc.Amount
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

        private async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (viewTableData.SelectedRow < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }

            Order order = orders[viewTableData.SelectedRow];
            OrderModel model = new OrderModel()
            {
                Time = order.Time,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                Services = new List<ServiceModel>(),
                Materials = new List<MaterialModel>(),
                StaffId = order.StaffId,
                Remark = order.Remark,
                OrderStatus = (int)order.Status
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

            FormEntityEditor editor = new FormEntityEditor("Изменить", model, dictionary);
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
                        (Order.OrderStatus)model.OrderStatus
                    );
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при создании записи:\n" + ex.Message);
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
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить запись " + order.Time.ToString() + " " + order.FullName + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.ServiceFulfilledDAO.DeleteByOrderId(order.Id);
                    await _DB.MaterialConsumptionDAO.DeleteByOrderId(order.Id);
                    await _DB.OrderDAO.Delete(order.Id);
                    viewTableData.SelectedRow = -1;

                    UpdateTable();
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при удалении записи:\n" + ex.Message);
                }
            }
        }

        private void period_ValueChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }
    }

    public class OrderModel
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

        [DisplayName("Статус")]
        public int OrderStatus { get; set; }
    }

    public class ServiceModel
    {
        [DisplayName("Услуга")]
        public int ServiceId { get; set; }
    }

    public class MaterialModel
    {
        [DisplayName("Материал")]
        public int MaterialId { get; set; }

        [DisplayName("Кол-во")]
        public int Amount { get; set; }
    }
}
