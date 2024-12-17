using System;
using System.Collections.Generic;

namespace BeautySalon
{
    public class ListComboBoxItem
    {
        private List<ComboBoxItem> _List;
        private ComboBoxItem _SelectedItem = null;

        public ComboBoxItem this[int index]
        {
            get
            {
                if (0 > index || index >= _List.Count) throw new Exception("Индекс(" + index + ") находится вне границ массива");
                return _List[index];
            }
            set
            {
                if (0 > index || index >= _List.Count) throw new Exception("Индекс(" + index + ") находится вне границ массива");
                _List[index] = value;
            }
        }
        public int Length
        {
            get { return _List.Count; }
        }
        public string Value
        {
            get { return _SelectedItem.Value; }
            set { _SelectedItem.Value = value; }
        }
        public ComboBoxItem SelectedItem
        {
            get { return _SelectedItem; }
        }

        public ListComboBoxItem()
        {
            _List = new List<ComboBoxItem>();
        }

        public ListComboBoxItem(List<ComboBoxItem> list)
        {
            if (list.Count > 0) _SelectedItem = list[0];
            _List = list;
        }

        public ListComboBoxItem(ComboBoxItem selected_item, List<ComboBoxItem> list)
        {
            _SelectedItem = selected_item;
            _List = list;
        }

        public void Add(ComboBoxItem item)
        {
            if (_List.Count == 0) _SelectedItem = item;
            _List.Add(item);
        }

        public void Clear() { _List.Clear(); }

        public void RemoveAt(int index) { _List.RemoveAt(index); }

        public ListComboBoxItem CopyOnNewValue(string value)
        {
            ComboBoxItem item = null;
            for (int i = 0; i < _List.Count; i++)
                if (_List[i].Value == value)
                {
                    item = _List[i];
                    break;
                }
            if (item == null) throw new Exception("Не найдено ни одного совпадения");
            return new ListComboBoxItem(item, _List);
        }

        public override string ToString()
        {
            return _SelectedItem.Text;
        }
    }

    public class TableObject
    {
        private TableColumn[] _Columns = null;
        private TableData _Data = null;

        public PreAddRowEvent PreAddRow;

        public TableColumn[] Columns
        {
            get { return _Columns; }
        }
        public TableData Data
        {
            get
            {
                if (_Data == null) throw new Exception("Данные не определены");
                return _Data;
            }
        }

        public TableObject(TableColumn[] columns, TableData data)
        {
            _Columns = columns;
            _Data = data;
        }

        public object[] GetDefault()
        {
            object[] settings = new object[_Columns.Length];
            for (int i = 0; i < _Columns.Length; i++)
            {
                Type col_type = _Columns[i].GetType();
                if (col_type == typeof(TableColumnText))
                {
                    settings[i] = ((TableColumnText)_Columns[i]).DefaultData;
                }
                else if (col_type == typeof(TableColumnDate))
                {
                    DateTime tmp = ((TableColumnDate)_Columns[i]).DefaultData;
                    settings[i] = new DateTime(tmp.Ticks);
                }
                else if (col_type == typeof(TableColumnList))
                {
                    ListComboBoxItem tmp = ((TableColumnList)_Columns[i]).DefaultData;
                    settings[i] = tmp.CopyOnNewValue(tmp.Value);
                }
                else if (col_type == typeof(TableColumnTable))
                {
                    settings[i] = ((TableColumnTable)_Columns[i]).DefaultData;
                    ((TableObject)settings[i]).Data.Clear();
                }
                else
                {
                    settings[i] = "";
                }
            }
            return settings;
        }

        public override string ToString()
        {
            if (_Data == null) return "";
            string str = "";
            for (int i = 0; i < _Data.Length; i++)
            {
                for (int j = 0; j < _Data[i].Length; j++)
                {
                    str += (j == 0 ? "" : "   ") + _Data[i][j].ToString();
                }
                str += "\n";
            }
            return str;
        }
    }

    public class TableData
    {
        private List<object[]> _Data = null;

        public object[] this[int index]
        {
            get
            {
                if (0 > index || index >= _Data.Count) throw new Exception("Индекс(" + index + ") находится вне границ массива");
                return _Data[index];
            }
            set
            {
                if (0 > index || index >= _Data.Count) throw new Exception("Индекс(" + index + ") находится вне границ массива");
                _Data[index] = value;
            }
        }
        public int Length
        {
            get { return _Data.Count; }
        }

        public TableData()
        {
            _Data = new List<object[]>();
        }

        public TableData(List<object[]> data)
        {
            _Data = data;
        }

        public void Add(object[] data)
        {
            _Data.Add(data);
        }

        public void Clear()
        {
            _Data.Clear();
        }

        public void RemoveAt(int index)
        {
            _Data.RemoveAt(index);
        }
    }

    public class TableColumn
    {
        protected string _Name = "";
        protected int _Size = 100;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public TableColumn(string name, int size)
        {
            _Name = name;
            _Size = size;
        }
    }

    public class TableColumnText : TableColumn
    {
        protected string _DefaultData = "";
        protected bool _Multiline = false;

        public string DefaultData
        {
            get { return _DefaultData; }
            set { _DefaultData = value; }
        }
        public bool Multiline
        {
            get { return _Multiline; }
            set { _Multiline = value; }
        }

        public TableColumnText(string name, int size, string default_data) : base(name, size)
        {
            _DefaultData = default_data;
        }

        public TableColumnText(string name, int size, string default_data, bool multiline) : this(name, size, default_data)
        {
            _Multiline = multiline;
        }
    }

    public class TableColumnDate : TableColumn
    {
        protected DateTime _DefaultData = DateTime.Today;
        protected bool _TimeChecked = false;

        public DateTime DefaultData
        {
            get { return _DefaultData; }
            set { _DefaultData = value; }
        }
        public bool TimeChecked
        {
            get { return _TimeChecked; }
            set { _TimeChecked = value; }
        }

        public TableColumnDate(string name, int size) : base(name, size) { }

        public TableColumnDate(string name, int size, bool time_checked) : this(name, size)
        {
            _TimeChecked = time_checked;
        }

        public TableColumnDate(string name, int size, DateTime default_data) : this(name, size)
        {
            _DefaultData = default_data;
        }

        public TableColumnDate(string name, int size, DateTime default_data, bool time_checked) : this(name, size, default_data)
        {
            _TimeChecked = time_checked;
        }
    }

    public class TableColumnList : TableColumn
    {
        protected ListComboBoxItem _DefaultData = new ListComboBoxItem();

        public ListComboBoxItem DefaultData
        {
            get { return _DefaultData; }
            set { _DefaultData = value; }
        }

        public TableColumnList(string name, int size, ListComboBoxItem default_data) : base(name, size)
        {
            _DefaultData = default_data;
        }
    }

    public class TableColumnTable : TableColumn
    {
        protected TableObject _DefaultData = null;

        public TableObject DefaultData
        {
            get { return _DefaultData; }
        }

        public TableColumnTable(string name, int size, TableObject default_data) : base(name, size)
        {
            _DefaultData = default_data;
        }
    }
}
