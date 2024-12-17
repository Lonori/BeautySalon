using System;
using System.Collections.Generic;

namespace BeautySalon
{
    public class TableModel
    {
        public string TableName;
        public List<TableProperty<object>> list;
    }

    public class TableProperty<T>
    {
        public string PropertyName { get; set; }
        public T ProdertyData { get; set; }

        public TableProperty(string propertyName, T prodertyData)
        {
            PropertyName = propertyName;
            ProdertyData = prodertyData;
        }
    }

    public class TablePropertyInt : TableProperty<int>
    {
        public TablePropertyInt(string propertyName, int prodertyData) : base(propertyName, prodertyData) { }
    }

    public class TablePropertyDouble : TableProperty<double>
    {
        public TablePropertyDouble(string propertyName, double prodertyData) : base(propertyName, prodertyData) { }
    }

    public class TablePropertyString : TableProperty<string>
    {
        public bool Multiline { get; set; } = true;

        public TablePropertyString(string propertyName, string prodertyData, bool multiline = false) : base(propertyName, prodertyData) {
            Multiline = multiline;
        }
    }

    public class TablePropertyDateTime : TableProperty<DateTime>
    {
        public bool DateWithTime { get; set; } = true;

        public TablePropertyDateTime(string propertyName, DateTime prodertyData, bool dateWithTime = true) : base(propertyName, prodertyData)
        {
            DateWithTime = dateWithTime;
        }
    }

    public class TablePropertyList : TableProperty<string>
    {
        public List<ComboBoxItem> ItemList { get; set; } = new List<ComboBoxItem>();

        public TablePropertyList(string propertyName, string prodertyData, List<ComboBoxItem> itemList) : base(propertyName, prodertyData)
        {
            ItemList = itemList;
        }
    }

    public class TableLinked : TableProperty<TableModel>
    {
        public TableLinked(string propertyName, TableModel prodertyData) : base(propertyName, prodertyData) { }
    }
}
