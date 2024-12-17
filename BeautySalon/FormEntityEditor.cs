using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormEntityEditor : Form
    {
        private Dictionary<string, List<ComboBoxItem>> _lookupData = new Dictionary<string, List<ComboBoxItem>>();
        private readonly object _model;
        private readonly Type _modelType;
        private bool _confirmed = false;

        public bool Confirmed
        {
            get { return _confirmed; }
        }

        public FormEntityEditor(string formName, object model)
        {
            _model = model ?? throw new ArgumentNullException("model");
            _modelType = model.GetType();

            InitializeComponent();
            Text = formName;

            InitializeForm();
        }

        public FormEntityEditor(string formName, object model, Dictionary<string, List<ComboBoxItem>> lookupData)
        {
            _model = model ?? throw new ArgumentNullException("model");
            _modelType = model.GetType();
            _lookupData = lookupData;

            InitializeComponent();
            Text = formName;

            InitializeForm();
        }

        private void InitializeForm()
        {
            TableFieldsBody.Controls.Clear();
            TableFieldsBody.RowStyles.Clear();
            TableFieldsBody.RowCount = 0;

            foreach (var property in _modelType.GetProperties())
            {
                CreateControlForProperty(property, _model);
            }
        }

        private void CreateControlForProperty(PropertyInfo property, object model)
        {
            string fieldName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;
            Control control = null;

            if (_lookupData.ContainsKey(property.Name))
            {
                int selectedIndex = -1;
                for (int i = 0; i < _lookupData[property.Name].Count; i++)
                {
                    if (Equals(_lookupData[property.Name][i].Value, property.GetValue(model)))
                    {
                        selectedIndex = i;
                        break;
                    }
                }

                control = new EditorFieldList(fieldName, _lookupData[property.Name], selectedIndex)
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Name = property.Name
                };
            }
            else if (typeof(IEnumerable<object>).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                IEnumerable<object> collection = property.GetValue(model) as IEnumerable<object>;

                control = new EditorLinkedTable(fieldName, property.PropertyType.GetGenericArguments()[0], collection.ToList(), _lookupData)
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Name = property.Name
                };
            }
            else if (property.PropertyType == typeof(bool))
            {
                List<ComboBoxItem> items = new List<ComboBoxItem> { new ComboBoxItem("Да", "true"), new ComboBoxItem("Нет", "false") };

                control = new EditorFieldList(fieldName, items, Convert.ToInt32(!(bool)(property.GetValue(model) ?? false)))
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Name = property.Name
                };
            }
            else if (
                property.PropertyType == typeof(int) ||
                property.PropertyType == typeof(double) ||
                property.PropertyType == typeof(string) ||
                property.PropertyType.IsEnum
            )
            {
                bool textMultiline = Attribute.IsDefined(property, typeof(TextMultilineAttribute));

                control = new EditorFieldText(fieldName, property.GetValue(model)?.ToString(), textMultiline)
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Name = property.Name
                };
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                bool timeChecked = !Attribute.IsDefined(property, typeof(DateWithoutTimeAttribute));

                control = new EditorFieldDate(fieldName, (DateTime)property.GetValue(model), timeChecked)
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Name = property.Name
                };
            }

            if (control != null)
            {
                TableFieldsBody.RowCount++;
                TableFieldsBody.Controls.Add(control, 0, TableFieldsBody.RowCount - 1);
                TableFieldsBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in TableFieldsBody.Controls)
                {
                    if (control is EditorFieldList controlList)
                    {
                        PropertyInfo property = _modelType.GetProperty(controlList.Name);
                        if (property.GetType() == typeof(bool))
                        {
                            property.SetValue(_model, Equals(controlList.Value, "true"));
                        }
                        else
                        {
                            property.SetValue(_model, Convert.ChangeType(controlList.Value, property.PropertyType));
                        }
                    }
                    else if (control is EditorFieldText controlText)
                    {
                        PropertyInfo property = _modelType.GetProperty(controlText.Name);
                        property.SetValue(_model, Convert.ChangeType(controlText.Value, property.PropertyType));
                    }
                    else if (control is EditorFieldDate controlDate)
                    {
                        PropertyInfo property = _modelType.GetProperty(controlDate.Name);
                        property.SetValue(_model, Convert.ChangeType(controlDate.Value, property.PropertyType));
                    }
                    else if (control is EditorLinkedTable controlLinkedTable)
                    {
                        PropertyInfo property = _modelType.GetProperty(controlLinkedTable.Name);

                        if (
                            property != null &&
                            property.PropertyType.IsGenericType &&
                            property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                        )
                        {
                            Type listElementType = property.PropertyType.GetGenericArguments()[0];
                            IList convertedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listElementType));

                            foreach (object sourceItem in controlLinkedTable.Value)
                            {
                                object destinationItem = Convert.ChangeType(sourceItem, listElementType);
                                convertedList.Add(destinationItem);
                            }

                            property.SetValue(_model, convertedList);
                        }
                    }
                }

                _confirmed = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обработки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
