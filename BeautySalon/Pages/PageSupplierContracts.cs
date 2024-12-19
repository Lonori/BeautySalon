using BeautySalon.DB;
using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageSupplierContracts : UserControl
    {
        private readonly AppDatabase _DB;
        private List<SupplierContract> contracts;
        private List<Material> materials;
        private List<Supplier> suppliers;
        private readonly Dictionary<string, List<ComboBoxItem>> dictionary = new Dictionary<string, List<ComboBoxItem>>();

        public PageSupplierContracts()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            viewTableData.TableHeaders = new List<string> { "ID", "Дата", "Поставщик", "Материалы" };
            viewTableData.TableWeights = new int[] { 0, 1, 1, 2 };
            viewTableData.Clear();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0).AddMonths(1).AddMinutes(-1);
            dateTimePicker1.ValueChanged += period_ValueChanged;
            dateTimePicker2.ValueChanged += period_ValueChanged;

            InitTables();
        }

        private async void InitTables()
        {
            suppliers = await _DB.SupplierDAO.GetAll();
            List<ComboBoxItem> itemServices = new List<ComboBoxItem>();
            foreach (Supplier supplier in suppliers)
            {
                itemServices.Add(new ComboBoxItem(supplier.Name, supplier.Id.ToString()));
            }
            dictionary.Add("SupplierId", itemServices);

            materials = await _DB.MaterialDAO.GetAll();
            List<ComboBoxItem> itemMaterials = new List<ComboBoxItem>();
            foreach (Material material in materials)
            {
                itemMaterials.Add(new ComboBoxItem(material.Name + " - " + material.Price + " руб.", material.Id.ToString()));
            }
            dictionary.Add("MaterialId", itemMaterials);

            UpdateTable();
        }

        private async void UpdateTable()
        {
            viewTableData.Clear();
            contracts = await _DB.SupplierContractDAO.GetByPeriod(dateTimePicker1.Value, dateTimePicker2.Value);
            List<List<string>> tableData = new List<List<string>>();

            foreach (SupplierContract contract in contracts)
            {
                string materialsText = "";
                List<MaterialArrival> materialsOrder = await _DB.MaterialArrivalDAO.GetByContractId(contract.Id);
                for (int i = 0; i < materialsOrder.Count; i++)
                {
                    MaterialArrival ma = materialsOrder[i];
                    materialsText += (i + 1) + ". " + GetMaterialById(ma.MaterialId)?.Name + " - " + ma.Price + " руб. (" + ma.Amount + ")";

                    if (i < (materialsOrder.Count - 1))
                    {
                        materialsText += "\n";
                    }
                }

                tableData.Add(new List<string>{
                    contract.Id.ToString(),
                    contract.Time.ToShortDateString() + " " + contract.Time.ToShortTimeString(),
                    GetSupplierById(contract.SupplierId).Name,
                    materialsText
                });
            }

            viewTableData.TableData = tableData;
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

        private Supplier GetSupplierById(int id)
        {
            foreach (Supplier supplier in suppliers)
            {
                if (Equals(supplier.Id, id))
                {
                    return supplier;
                }
            }

            return null;
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            SupplierContractModel model = new SupplierContractModel()
            {
                Id = await _DB.SupplierContractDAO.GetNewId(),
                Time = DateTime.Now
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", model, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.SupplierContractDAO.Insert(
                        model.Id,
                        model.Time,
                        model.SupplierId
                    );

                    foreach (MaterialArrivalModel ma in model.Materials)
                    {
                        Material material = GetMaterialById(ma.MaterialId);

                        await _DB.MaterialArrivalDAO.Insert(
                            model.Id,
                            material.Id,
                            material.Price,
                            ma.Amount
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

            SupplierContract contract = contracts[viewTableData.SelectedRow];
            SupplierContractModel model = new SupplierContractModel()
            {
                Id = contract.Id,
                Time = contract.Time,
                SupplierId = contract.SupplierId,
                Materials = new List<MaterialArrivalModel>(),
            };

            List<MaterialArrival> materialsArrival = await _DB.MaterialArrivalDAO.GetByContractId(model.Id);
            foreach (MaterialArrival ma in materialsArrival)
            {
                model.Materials.Add(new MaterialArrivalModel { MaterialId = ma.MaterialId, Amount = ma.Amount });
            }

            FormEntityEditor editor = new FormEntityEditor("Изменить", model, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.MaterialArrivalDAO.DeleteByContractId(model.Id);
                    foreach (MaterialArrivalModel ma in model.Materials)
                    {
                        Material material = GetMaterialById(ma.MaterialId);

                        await _DB.MaterialArrivalDAO.Insert(
                            model.Id,
                            material.Id,
                            material.Price,
                            ma.Amount
                        );
                    }

                    await _DB.SupplierContractDAO.Update(
                        model.Id,
                        model.Time,
                        model.SupplierId
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

            SupplierContract contract = contracts[viewTableData.SelectedRow];
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить запись " + contract.Time.ToString() + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.MaterialArrivalDAO.DeleteByContractId(contract.Id);
                    await _DB.SupplierContractDAO.Delete(contract.Id);
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

    class SupplierContractModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Время поставки")]
        public DateTime Time { get; set; }

        [DisplayName("Поставщик")]
        public int SupplierId { get; set; }

        [DisplayName("Материалы")]
        public List<MaterialArrivalModel> Materials { get; set; } = new List<MaterialArrivalModel>();
    }

    class MaterialArrivalModel
    {
        [DisplayName("Material")]
        public int MaterialId { get; set; }

        [DisplayName("Кол-во")]
        public int Amount { get; set; }
    }
}
