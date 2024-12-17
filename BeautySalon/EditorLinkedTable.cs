using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorLinkedTable : UserControl
    {
        private readonly Dictionary<string, List<ComboBoxItem>> _lookupData;
        private readonly List<object> _tableData;
        private readonly Type _entityType;

        public List<object> Value
        {
            get { return _tableData; }
        }

        public EditorLinkedTable(string tableName, Type modelType, List<object> tableData, Dictionary<string, List<ComboBoxItem>> lookupData)
        {
            _tableData = tableData ?? throw new ArgumentNullException("tableData");
            _entityType = modelType;
            _lookupData = lookupData;

            InitializeComponent();

            TableName.Text = tableName + ":";
            List<string> headers = new List<string>();
            foreach (PropertyInfo property in _entityType.GetProperties())
            {
                string fieldName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;
                headers.Add(fieldName);
            }
            table.TableHeaders = headers;

            UpdateTable();
        }

        private void UpdateTable()
        {
            List<List<string>> tableData = new List<List<string>>();

            foreach (object model in _tableData)
            {
                List<string> rowData = new List<string>();

                foreach (PropertyInfo property in _entityType.GetProperties())
                {
                    if (_lookupData.ContainsKey(property.Name))
                    {
                        List<ComboBoxItem> items = _lookupData[property.Name];
                        string value = property.GetValue(model)?.ToString();

                        foreach (ComboBoxItem item in items)
                        {
                            if (Equals(value, item.Value))
                            {
                                value = item.ToString();
                                break;
                            }
                        }

                        rowData.Add(value);
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        rowData.Add((bool)(property.GetValue(model) ?? false) ? "Да" : "Нет");
                    }
                    else if (
                        property.PropertyType == typeof(int) ||
                        property.PropertyType == typeof(double) ||
                        property.PropertyType == typeof(string) ||
                        property.PropertyType.IsEnum
                    )
                    {
                        rowData.Add(property.GetValue(model)?.ToString());
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        DateTime value = (DateTime)(property.GetValue(model) ?? DateTime.MinValue);
                        rowData.Add(value.Equals(DateTime.MinValue) ? "---" : value.ToShortDateString());
                    }
                }

                tableData.Add(rowData);
            }

            table.TableData = tableData;
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            object entity = Activator.CreateInstance(_entityType);
            FormEntityEditor editor = new FormEntityEditor("Добавить", entity, _lookupData);
            editor.ShowDialog();
            if (editor.Confirmed)
            {
                _tableData.Add(entity);
                UpdateTable();
            }
            editor.Dispose();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (table.SelectedRow < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            object entity = _tableData[table.SelectedRow];
            FormEntityEditor editor = new FormEntityEditor("Изменить", entity, _lookupData);
            editor.ShowDialog();
            if (editor.Confirmed)
            {
                UpdateTable();
            }
            editor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (table.SelectedRow < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            _tableData.RemoveAt(table.SelectedRow);
            table.SelectedRow = -1;
            UpdateTable();
        }
    }
}
