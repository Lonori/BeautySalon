using System.Drawing;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorFieldText : UserControl
    {
        private Size text_size;

        public string FieldName
        {
            get { return fieldName.Text; }
            set { fieldName.Text = value; }
        }
        public bool Multiline
        {
            get { return fieldData.Multiline; }
            set {
                if(value == true)
                {
                    fieldData.Size = new Size(text_size.Width, 60);
                }
                else
                {
                    fieldData.Size = text_size;
                }
                fieldData.Multiline = value;
            }
        }
        public string Value
        {
            get { return fieldData.Text; }
        }

        private EditorFieldText()
        {
            InitializeComponent();
            text_size = new Size(fieldData.Width, fieldData.Height);
        }

        public EditorFieldText(string field_name, string data, bool multiline) : this()
        {
            fieldName.Text = field_name;
            fieldData.Text = data;
            Multiline = multiline;
        }
    }
}
