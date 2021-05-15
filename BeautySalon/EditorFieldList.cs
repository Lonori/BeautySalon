using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ListComboBoxItem Data
        {
            get { return item.CopyOnNewValue(((ComboBoxItem)fieldData.SelectedItem).Value); }
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
