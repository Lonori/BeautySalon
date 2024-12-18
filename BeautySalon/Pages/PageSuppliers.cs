using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using BeautySalon.DB;
using BeautySalon.DB.Entities;

namespace BeautySalon
{
    public partial class PageSuppliers : UserControl
    {
        private readonly AppDatabase _DB;
        private List<Supplier> suppliers;


        public PageSuppliers()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            FillTableHeader();
            UpdateTable();
        }

        private void FillTableHeader()
        {
            List<string> headers = new List<string>();

            foreach (PropertyInfo property in typeof(Supplier).GetProperties())
            {
                headers.Add(property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name);
            }

            viewTableData.TableHeaders = headers;
            viewTableData.ColumnWeights = new int[] { 0, 1, 2 };
        }

        private async void UpdateTable()
        {
            suppliers = await _DB.SupplierDAO.GetAll();
            List<List<string>> tableData = new List<List<string>>();

            foreach (Supplier supplier in suppliers)
            {
                tableData.Add(new List<string>{
                    supplier.Id.ToString(),
                    supplier.Name,
                    supplier.Address,
                    supplier.INN,
                    supplier.KPP.ToString()
                });
            }

            viewTableData.TableData = tableData;
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier()
            {
                Id = await _DB.SupplierDAO.GetNewId()
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", supplier);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.SupplierDAO.Insert(supplier);

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

            Supplier supplier = suppliers[viewTableData.SelectedRow];
            int oldId = supplier.Id;

            FormEntityEditor editor = new FormEntityEditor("Изменить", supplier);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.SupplierDAO.Update(supplier, oldId);

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

            Supplier supplier = suppliers[viewTableData.SelectedRow];
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить поставщика " + supplier.Name + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.SupplierDAO.Delete(supplier.Id);
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
