using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Reflection;
using System.Windows.Forms;
using BeautySalon.DB;
using BeautySalon.DB.Entities;

namespace BeautySalon
{
    public partial class PageServices : UserControl
    {
        private readonly AppDatabase _DB;
        private List<Service> services;

        public PageServices()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            FillTableHeader();
            UpdateTable();
        }

        private void FillTableHeader()
        {
            List<string> headers = new List<string>();

            foreach (PropertyInfo property in typeof(Service).GetProperties())
            {
                headers.Add(property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name);
            }

            viewTableData.TableHeaders = headers;
            viewTableData.ColumnWeights = new int[] { 0, 4, 1 };
        }

        private async void UpdateTable()
        {
            services = await _DB.ServiceDAO.GetAll();
            List<List<string>> tableData = new List<List<string>>();

            foreach (Service service in services)
            {
                tableData.Add(new List<string>{
                    service.Id.ToString(),
                    service.Name,
                    service.Price.ToString()
                });
            }

            viewTableData.TableData = tableData;
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            Service service = new Service()
            {
                Id = await _DB.ServiceDAO.GetNewId()
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", service);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.ServiceDAO.Insert(service);

                    UpdateTable();
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при создании записи:\n" + ex.Message);
                }
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

            Service service = services[viewTableData.SelectedRow];
            int oldId = service.Id;

            FormEntityEditor editor = new FormEntityEditor("Изменить", service);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.ServiceDAO.Update(service, oldId);

                    UpdateTable();
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при обновлении записи:\n" + ex.Message);
                }
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

            Service service = services[viewTableData.SelectedRow];
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить услугу " + service.Name + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.ServiceDAO.Delete(service.Id);
                    viewTableData.SelectedRow = -1;

                    UpdateTable();
                }
                catch (Exception ex)
                {
                    AlertBox.Error("Ошибка при удалении записи:\n" + ex.Message);
                }
            }
        }
    }
}
