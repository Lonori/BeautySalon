using System;

namespace BeautySalon
{
    public class ComboBoxItem
    {
        public string Text { get; set; } = "";
        public string Value { get; set; } = "";

        public ComboBoxItem(string value) : this(value, value) { }

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public ComboBoxItem(ComboBoxItem item)
        {
            Text = item.Text;
            Value = item.Value;
        }

        public bool Equals(ComboBoxItem obj)
        {
            if (Object.Equals(obj.Value, Value))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
