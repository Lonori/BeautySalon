using BeautySalon.DB;
using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageStaff : UserControl
    {
        private readonly AppDatabase _DB;
        private List<Staff> staffs;
        private readonly Dictionary<string, List<ComboBoxItem>> dictionary = new Dictionary<string, List<ComboBoxItem>>
        {
            {
                "Gender", new List<ComboBoxItem> {
                    new ComboBoxItem("Женский", "Ж"),
                    new ComboBoxItem("Мужской", "М")
                }
            }
        };

        public PageStaff()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            FillTableHeader();
            UpdateTable();
        }

        private void FillTableHeader()
        {
            List<string> headers = new List<string>();

            foreach (PropertyInfo property in typeof(Staff).GetProperties())
            {
                headers.Add(property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name);
            }

            viewTableData.TableHeaders = headers;
            viewTableData.TableWeights = new int[] { 0, 2, 1, 1, 0 };
        }

        private async void UpdateTable()
        {
            staffs = await _DB.StaffDAO.GetAll();
            List<List<string>> tableData = new List<List<string>>();

            foreach (Staff staff in staffs)
            {
                tableData.Add(new List<string>{
                    staff.Id.ToString(),
                    staff.FullName,
                    staff.PhoneNumber,
                    staff.Birthday.ToShortDateString(),
                    staff.Gender,
                    staff.DateJoin == DateTime.MinValue ? "---" : staff.DateJoin.ToShortDateString(),
                    staff.DateLeave == DateTime.MinValue ? "---" : staff.DateLeave.ToShortDateString(),
                    staff.Position
                });
            }

            viewTableData.TableData = tableData;
        }

        private async void ButtonInsert_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff()
            {
                Id = await _DB.StaffDAO.GetNewId(),
                PhoneNumber = "+7",
                Birthday = DateTime.Today,
                Gender = "Ж"
            };

            FormEntityEditor editor = new FormEntityEditor("Добавить", staff, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.StaffDAO.Insert(staff);

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

            Staff staff = staffs[viewTableData.SelectedRow];
            int oldId = staff.Id;

            FormEntityEditor editor = new FormEntityEditor("Изменить", staff, dictionary);
            editor.ShowDialog();

            if (editor.Confirmed)
            {
                try
                {
                    await _DB.StaffDAO.Update(staff, oldId);

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

            Staff staff = staffs[viewTableData.SelectedRow];
            if (AlertBox.ConfirmWarn("Вы действительно хотите удалить сотрудника " + staff.FullName + "?") == DialogResult.OK)
            {
                try
                {
                    await _DB.StaffDAO.Delete(staff.Id);
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
