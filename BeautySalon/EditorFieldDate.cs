using System;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorFieldDate : UserControl
    {
        private TableLayoutPanel dateTimeBody;
        private DateTimePicker fieldDataDate;
        private ComboBox fieldDataHour;
        private ComboBox fieldDataMinute;
        private DateTimePicker fieldData;

        private bool TimeChecked = false;

        public string FieldName
        {
            get { return fieldName.Text; }
            set { fieldName.Text = value; }
        }

        public DateTime Data
        {
            get
            {
                if (TimeChecked == true)
                {
                    DateTime tmp = fieldDataDate.Value;
                    return new DateTime(tmp.Year, tmp.Month, tmp.Day, (int)fieldDataHour.SelectedItem, (int)fieldDataMinute.SelectedItem, 0);
                }
                else
                {
                    return fieldData.Value;
                }
            }
        }

        public EditorFieldDate(string field_name, DateTime datatime) : this(field_name, datatime, false) { }

        public EditorFieldDate(string field_name, DateTime datatime, bool time_checked)
        {
            InitializeComponent();
            fieldName.Text = field_name;
            TimeChecked = time_checked;

            if (time_checked == true)
            {
                InitializeDataTime();
                fieldDataDate.Value = new DateTime(datatime.Year, datatime.Month, datatime.Day, 0, 0, 0);
                fieldDataHour.SelectedItem = datatime.Hour;
                fieldDataMinute.SelectedItem = datatime.Minute;
            }
            else
            {
                InitializeData();
                fieldData.Value = datatime;
            }
        }

        private void InitializeData()
        {
            fieldBody.Controls.Remove(dateTimeBody);

            fieldData = new DateTimePicker();

            fieldData.Dock = DockStyle.Fill;
            fieldData.Format = DateTimePickerFormat.Short;
            fieldData.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            fieldData.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            fieldData.Value = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            fieldBody.Controls.Add(fieldData, 1, 0);
        }

        private void InitializeDataTime()
        {
            fieldBody.Controls.Remove(fieldData);

            dateTimeBody = new TableLayoutPanel();
            fieldDataDate = new DateTimePicker();
            fieldDataHour = new ComboBox();
            fieldDataMinute = new ComboBox();

            dateTimeBody.AutoSize = true;
            dateTimeBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dateTimeBody.ColumnCount = 3;
            dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            dateTimeBody.Controls.Add(fieldDataDate, 0, 0);
            dateTimeBody.Controls.Add(fieldDataHour, 1, 0);
            dateTimeBody.Controls.Add(fieldDataMinute, 2, 0);
            dateTimeBody.Dock = DockStyle.Fill;
            dateTimeBody.Margin = new Padding(0);
            dateTimeBody.RowCount = 1;
            dateTimeBody.RowStyles.Add(new RowStyle());

            fieldDataDate.Dock = DockStyle.Fill;
            fieldDataDate.Format = DateTimePickerFormat.Short;
            fieldDataDate.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            fieldDataDate.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            fieldDataHour.Dock = DockStyle.Top;
            fieldDataHour.DropDownHeight = 200;
            fieldDataHour.FormattingEnabled = true;
            for (int i = 0; i < 24; i++) fieldDataHour.Items.Add(i);

            fieldDataMinute.Dock = DockStyle.Top;
            fieldDataMinute.DropDownHeight = 200;
            fieldDataMinute.FormattingEnabled = true;
            for (int i = 0; i < 60; i++) fieldDataMinute.Items.Add(i);

            fieldBody.Controls.Add(dateTimeBody, 1, 0);
        }
    }
}
