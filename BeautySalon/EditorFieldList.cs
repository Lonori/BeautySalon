using System.Collections.Generic;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorFieldList : UserControl
    {
        ListComboBoxItem item;

        public string FieldName
        {
            get { return fieldName.Text; }
            set { fieldName.Text = value; }
        }

        public int SelectedIndex
        {
            get { return fieldData.SelectedIndex; }
            set { fieldData.SelectedIndex = value; }
        }

        public string Value
        {
            get
            {
                ComboBoxItem item = (ComboBoxItem)fieldData.SelectedItem;
                return item == null ? null : item.Value;
            }
        }

        public ListComboBoxItem Data
        {
            get { return item.CopyOnNewValue(((ComboBoxItem)fieldData.SelectedItem).Value); }
        }

        public EditorFieldList(string fieldName, List<ComboBoxItem> sourceList)
        {
            InitializeComponent();

            this.fieldName.Text = fieldName;
            for (int i = 0; i < sourceList.Count; i++)
            {
                fieldData.Items.Add(sourceList[i]);
            }
        }

        public EditorFieldList(string fieldName, List<ComboBoxItem> sourceList, ComboBoxItem selectedItem) : this(fieldName, sourceList)
        {
            fieldData.SelectedItem = selectedItem;
        }

        public EditorFieldList(string fieldName, List<ComboBoxItem> sourceList, int selectedIndex) : this(fieldName, sourceList)
        {
            fieldData.SelectedIndex = selectedIndex;
        }

        public EditorFieldList(string field_name, ListComboBoxItem field_list)
        {
            InitializeComponent();

            fieldName.Text = field_name;
            item = field_list;
            for (int i = 0; i < field_list.Length; i++) fieldData.Items.Add(field_list[i]);
            fieldData.SelectedItem = field_list.SelectedItem;
        }
    }
}
