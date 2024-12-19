using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using BeautySalon.DB;
using BeautySalon.DB.Entities;

namespace BeautySalon
{
    public partial class PageMaterials : UserControl
    {
        private readonly AppDatabase _DB;
        private List<Material> materials;

        public PageMaterials()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            FillTableHeader();
            UpdateTable();
        }

        private void FillTableHeader()
        {
            List<string> headers = new List<string>();

            foreach (PropertyInfo property in typeof(Material).GetProperties())
            {
                headers.Add(property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name);
            }

            viewTableData.TableHeaders = headers;
            viewTableData.TableWeights = new int[] { 0, 4, 1 };
        }

        private async void UpdateTable()
        {
            materials = await _DB.MaterialDAO.GetAll();
            List<List<string>> tableData = new List<List<string>>();

            foreach (Material material in materials)
            {
                tableData.Add(new List<string>{
                    material.Id.ToString(),
                    material.Name,
                    material.Price.ToString()
                });
            }

            viewTableData.TableData = tableData;
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            Material material = new Material()
            {
                Id = await _DB.MaterialDAO.GetNewId()
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", material);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.MaterialDAO.Insert(material);

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

            Material material = materials[viewTableData.SelectedRow];
            int oldId = material.Id;

            FormEntityEditor editor = new FormEntityEditor("Изменить", material);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.MaterialDAO.Update(material);

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

            Material material = materials[viewTableData.SelectedRow];
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить материал " + material.Name + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.MaterialDAO.Delete(material.Id);
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
