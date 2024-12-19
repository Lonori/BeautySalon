using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorFieldDate : UserControl
    {
        private TableLayoutPanel dateTimeBody;
        private CheckBox fieldCheckBox;
        private Label fieldLabelEmpty;
        private DateTimePicker fieldDataDate;
        private ComboBox fieldDataHour;
        private ComboBox fieldDataMinute;

        private DateTime _getValue = DateTime.MinValue;
        private DateTime _value = DateTime.MinValue;
        private bool _timeChecked = false;

        public string FieldName
        {
            get { return fieldName.Text; }
            set { fieldName.Text = value; }
        }

        public DateTime Value
        {
            get { return _value; }
        }

        public EditorFieldDate(string fieldName, DateTime value) : this(fieldName, value, false) { }

        public EditorFieldDate(string fieldName, DateTime value, bool timeChecked)
        {
            InitializeComponent();
            this.fieldName.Text = fieldName;
            _getValue = value;
            _value = value;
            _timeChecked = timeChecked;

            dateTimeBody = new TableLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            dateTimeBody.RowCount = 1;
            dateTimeBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fieldBody.Controls.Add(dateTimeBody, 1, 0);

            fieldCheckBox = new CheckBox()
            {
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "",
                Margin = new Padding(0)
            };
            fieldCheckBox.Checked = !_value.Equals(DateTime.MinValue);
            fieldCheckBox.CheckedChanged += new EventHandler(FieldCheckBox_CheckedChanged);

            fieldLabelEmpty = new Label()
            {
                Dock = DockStyle.Top,
                ForeColor = Color.FromArgb(122, 122, 122),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Нет даты"
            };

            fieldDataDate = new DateTimePicker()
            {
                Dock = DockStyle.Top,
                Format = DateTimePickerFormat.Short
            };
            fieldDataDate.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            fieldDataDate.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (_value.Equals(DateTime.MinValue))
            {
                fieldDataDate.Value = fieldDataDate.MinDate;
            }
            else if (_value <= fieldDataDate.MinDate)
            {
                fieldDataDate.Value = fieldDataDate.MinDate;
            }
            else if (_value >= fieldDataDate.MaxDate)
            {
                fieldDataDate.Value = fieldDataDate.MaxDate;
            }
            else
            {
                fieldDataDate.Value = new DateTime(_value.Year, _value.Month, _value.Day, 0, 0, 0, 0);
            }
            fieldDataDate.ValueChanged += new EventHandler(FieldsDateTime_Changed);

            fieldDataHour = new ComboBox()
            {
                Dock = DockStyle.Top,
                DropDownHeight = 200,
                FormattingEnabled = true
            };
            for (int i = 0; i < 24; i++)
            {
                fieldDataHour.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            fieldDataHour.SelectedIndex = _value.Hour;
            fieldDataHour.SelectedIndexChanged += new EventHandler(FieldsDateTime_Changed);

            fieldDataMinute = new ComboBox()
            {
                Dock = DockStyle.Top,
                DropDownHeight = 200,
                FormattingEnabled = true
            };
            for (int i = 0; i < 60; i++)
            {
                fieldDataMinute.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            fieldDataMinute.SelectedIndex = _value.Minute;
            fieldDataMinute.SelectedIndexChanged += new EventHandler(FieldsDateTime_Changed);

            FillField();
        }

        private void FillField()
        {
            dateTimeBody.SuspendLayout();
            dateTimeBody.Controls.Clear();
            dateTimeBody.ColumnStyles.Clear();

            dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            dateTimeBody.Controls.Add(fieldCheckBox, 0, 0);

            if (_value.Equals(DateTime.MinValue))
            {
                dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
                dateTimeBody.Controls.Add(fieldLabelEmpty, 1, 0);
                dateTimeBody.ColumnCount = 2;
            }
            else
            {
                if (_timeChecked)
                {
                    dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    dateTimeBody.Controls.Add(fieldDataDate, 1, 0);
                    dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    dateTimeBody.Controls.Add(fieldDataHour, 2, 0);
                    dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    dateTimeBody.Controls.Add(fieldDataMinute, 3, 0);
                    dateTimeBody.ColumnCount = 4;
                }
                else
                {
                    dateTimeBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
                    dateTimeBody.Controls.Add(fieldDataDate, 1, 0);
                    dateTimeBody.ColumnCount = 2;
                }
            }
            dateTimeBody.ResumeLayout();
        }

        private void FieldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldCheckBox.Checked)
            {
                if (_getValue.Equals(DateTime.MinValue))
                {
                    _value = DateTime.Now;
                }
                else
                {
                    _value = _getValue;
                }
                fieldDataDate.Value = new DateTime(_value.Year, _value.Month, _value.Day, 0, 0, 0, 0);
                fieldDataHour.SelectedIndex = _value.Hour;
                fieldDataMinute.SelectedIndex = _value.Minute;
            }
            else
            {
                _value = DateTime.MinValue;
                fieldDataDate.Value = fieldDataDate.MinDate;
                fieldDataHour.SelectedIndex = 0;
                fieldDataMinute.SelectedIndex = 0;
            }

            FillField();
        }

        private void FieldsDateTime_Changed(object sender, EventArgs e)
        {
            if (fieldCheckBox.Checked)
            {
                DateTime fieldDate = fieldDataDate.Value;
                int fieldHour = fieldDataHour.SelectedIndex;
                int fieldMinute = fieldDataMinute.SelectedIndex;

                _value = new DateTime(fieldDate.Year, fieldDate.Month, fieldDate.Day, fieldHour, fieldMinute, 0);
            }
            else
            {
                _value = DateTime.MinValue;
            }
        }
    }
}
