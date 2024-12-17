using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormTableEditor : Form
    {
        private object[] _DataRow = null;
        private bool _Confirmed = false;
        private Control[] DataInput = null;
        private string[] table_data = new string[0];

        public string[] TableData
        {
            get { return table_data; }
            set
            {
                table_data = value;
            }
        }
        public object[] DataRow
        {
            get { return _DataRow; }
            set
            {
                _DataRow = value;
            }
        }
        public bool Confirmed
        {
            get { return _Confirmed; }
        }

        private FormTableEditor()
        {
            InitializeComponent();

            TableFieldsBody.Controls.Clear();
            TableFieldsBody.RowStyles.Clear();
            TableFieldsBody.RowCount = 0;
        }

        private FormTableEditor(string formname) : this()
        {
            Text = formname;
        }

        public FormTableEditor(string formname, TableColumn[] columns, object[] data) : this(formname)
        {
            object[] row_data = data;

            AddEditorFieldAll(columns, row_data);
            _DataRow = row_data;
        }

        public void AddEditorFieldAll(TableColumn[] columns, object[] data)
        {
            DataInput = new Control[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                Type data_type = columns[i].GetType();
                if (data_type == typeof(TableColumnTable))
                {
                    EditorFieldTable RowObj = new EditorFieldTable((TableObject)data[i]);
                    RowObj.PreAddRow = ((TableObject)data[i]).PreAddRow;
                    RowObj.Dock = DockStyle.Top;
                    RowObj.Margin = new Padding(0);
                    RowObj.Size = new Size(TableFieldsBody.Width, 200);
                    DataInput[i] = RowObj;
                }
                else if (data_type == typeof(TableColumnList))
                {
                    EditorFieldList RowObj = new EditorFieldList(columns[i].Name, (ListComboBoxItem)data[i]);
                    RowObj.AutoSize = true;
                    RowObj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    RowObj.Dock = DockStyle.Top;
                    RowObj.Margin = new Padding(0);
                    DataInput[i] = RowObj;
                }
                else if (data_type == typeof(TableColumnDate))
                {
                    EditorFieldDate RowObj = new EditorFieldDate(columns[i].Name, (DateTime)data[i], ((TableColumnDate)columns[i]).TimeChecked);
                    RowObj.AutoSize = true;
                    RowObj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    RowObj.Dock = DockStyle.Top;
                    RowObj.Margin = new Padding(0);
                    DataInput[i] = RowObj;
                }
                else if (data_type == typeof(TableColumnText))
                {
                    EditorFieldText RowObj = new EditorFieldText(columns[i].Name, (string)data[i], ((TableColumnText)columns[i]).Multiline);
                    RowObj.AutoSize = true;
                    RowObj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    RowObj.Dock = DockStyle.Top;
                    RowObj.Margin = new Padding(0);
                    DataInput[i] = RowObj;
                }
                else if (data_type == typeof(TableColumn))
                {
                    continue;
                }
                else
                {
                    throw new Exception("Неизвестный тип параметра - " + data_type.Name);
                }
                TableFieldsBody.RowCount++;
                TableFieldsBody.Controls.Add(DataInput[i], 0, TableFieldsBody.RowCount - 1);
                TableFieldsBody.RowStyles.Add(new RowStyle());
            }
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DataInput.Length; i++)
            {
                if (DataInput[i] == null)
                {
                    _DataRow[i] = "";
                    continue;
                }
                Type data_type = DataInput[i].GetType();
                if (data_type == typeof(EditorFieldText))
                {
                    _DataRow[i] = ((EditorFieldText)DataInput[i]).Data;
                }
                else if (data_type == typeof(EditorFieldDate))
                {
                    _DataRow[i] = ((EditorFieldDate)DataInput[i]).Data;
                }
                else if (data_type == typeof(EditorFieldList))
                {
                    _DataRow[i] = ((EditorFieldList)DataInput[i]).Data;
                }
                else if (data_type == typeof(EditorFieldTable))
                { }
                else
                {
                    throw new Exception("Неизвестный тип параметра - " + data_type.Name);
                }
            }

            _Confirmed = true;
            Close();
        }

        public string GetString(int index)
        {
            if (0 > index || index >= _DataRow.Length) throw new Exception("Индекс находится вне границ массива");
            if (_DataRow[index].GetType() == typeof(string) || _DataRow[index].GetType() == typeof(DateTime))
            {
                return _DataRow[index].ToString();
            }
            else if (_DataRow[index].GetType() == typeof(ListComboBoxItem))
            {
                return ((ListComboBoxItem)_DataRow[index]).Value;
            }
            else
            {
                throw new Exception("Невозможно выполнить преобразование типов");
            }
        }
    }
}
