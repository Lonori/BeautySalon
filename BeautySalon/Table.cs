using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeautySalon
{
    public delegate string[] PreAddRowEvent(object[] data);

    class Table : Control
    {
        private Color _ColorHeader = Color.SteelBlue;
        private Color _ColorRowHover = Color.LightSteelBlue;
        private Color _ColorRowSelected = Color.DodgerBlue;
        private Color _ColorRowEven = Color.AliceBlue;
        private Color _ColorRowOdd = Color.Lavender;
        private int _HeaderHeight = 40;

        private Panel TableHeader;
        private Panel TableBody;
        private TableLayoutPanel ColumnHeader;
        private TableLayoutPanel ColumnRows;

        private int column_amount = 0;
        private int row_amount = 0;
        public int row_selected = -1;
        private float[] columns_size;
        private List<string[]> table_data = new List<string[]>();

        public delegate void ClickOnRowEvent(int row_num, string[] data);
        public event ClickOnRowEvent RowClick;
        public event ClickOnRowEvent RowDoubleClick;
        public event ClickOnRowEvent RowSelectChange;
        public event PreAddRowEvent PreAddRow;


        [Category("Color")]
        [Description("Цвет шапки таблицы")]
        [DefaultValue(typeof(Color), "SteelBlue")]
        public Color ColorHeader
        {
            get { return _ColorHeader; }
            set
            {
                _ColorHeader = value;
                Invalidate();
            }
        }

        [Category("Color")]
        [Description("Цвет строки при наведении")]
        [DefaultValue(typeof(Color), "LightSteelBlue")]
        public Color ColorRowHover
        {
            get { return _ColorRowHover; }
            set
            {
                _ColorRowHover = value;
                Invalidate();
            }
        }

        [Category("Color")]
        [Description("Цвет выбранной строки")]
        [DefaultValue(typeof(Color), "DodgerBlue")]
        public Color ColorRowSelected
        {
            get { return _ColorRowSelected; }
            set
            {
                _ColorRowSelected = value;
                Invalidate();
            }
        }

        [Category("Color")]
        [Description("Цвет четных строк")]
        [DefaultValue(typeof(Color), "AliceBlue")]
        public Color ColorRowEven
        {
            get { return _ColorRowEven; }
            set
            {
                _ColorRowEven = value;
                Invalidate();
            }
        }

        [Category("Color")]
        [Description("Цвет нечетных строк")]
        [DefaultValue(typeof(Color), "Lavender")]
        public Color ColorRowOdd
        {
            get { return _ColorRowOdd; }
            set
            {
                _ColorRowOdd = value;
                Invalidate();
            }
        }

        [Description("Высота заголовка таблицы")]
        [DefaultValue(40)]
        public int HeaderHeight
        {
            get { return _HeaderHeight; }
            set
            {
                if (value < 0)
                {
                    _HeaderHeight = 0;
                }
                else
                {
                    _HeaderHeight = value;
                }
                Invalidate();
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public override string Text { get; set; }

        private void InitializeComponent()
        {
            TableHeader = new Panel();
            TableBody = new Panel();
            ColumnHeader = new TableLayoutPanel();
            ColumnRows = new TableLayoutPanel();

            TableHeader.Controls.Add(ColumnHeader);
            TableHeader.Dock = DockStyle.Top;
            TableHeader.Margin = new Padding(0);
            TableHeader.TabIndex = 1;

            ColumnHeader.BackColor = Color.Transparent;
            ColumnHeader.ColumnCount = 1;
            ColumnHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ColumnHeader.Dock = DockStyle.Fill;
            ColumnHeader.Margin = new Padding(0);
            ColumnHeader.RowCount = 1;
            ColumnHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ColumnHeader.TabIndex = 0;

            TableBody.AutoScroll = true;
            TableBody.BackColor = Color.Transparent;
            TableBody.Controls.Add(ColumnRows);
            TableBody.Dock = DockStyle.Fill;
            TableBody.Margin = new Padding(0);
            TableBody.TabIndex = 2;

            ColumnRows.AutoSize = true;
            ColumnRows.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ColumnRows.BackColor = Color.Transparent;
            ColumnRows.ColumnCount = 1;
            ColumnRows.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ColumnRows.Dock = DockStyle.Top;
            ColumnRows.Margin = new Padding(0);
            ColumnRows.RowCount = 1;
            ColumnRows.RowStyles.Add(new RowStyle());
            ColumnRows.TabIndex = 0;

            Controls.Add(TableBody);
            Controls.Add(TableHeader);
            BackColor = Color.White;
            Margin = new Padding(0);
            Size = new Size(400, 200);
        }

        public Table()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            TableHeader.BackColor = _ColorHeader;
            TableHeader.Size = new Size(TableHeader.Width, _HeaderHeight);
        }

        public void TableInit(string[] column_name, float[] columns_size)
        {
            if (column_name.Length != columns_size.Length) throw new Exception("Количество колонок не совпадают");

            column_amount = column_name.Length;
            this.columns_size = columns_size;
            ColumnHeader.Controls.Clear();
            ColumnHeader.ColumnStyles.Clear();

            ColumnHeader.ColumnCount = column_name.Length;
            for (int i = 0; i < column_name.Length; i++)
            {
                ColumnHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columns_size[i]));
                ColumnHeader.Controls.Add(new Label()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Text = column_name[i],
                    TextAlign = ContentAlignment.MiddleCenter
                }, i, 0);
            }
        }

        public void TableInit(TableObject table)
        {
            column_amount = table.Columns.Length;
            columns_size = new float[table.Columns.Length];

            ColumnHeader.Controls.Clear();
            ColumnHeader.ColumnStyles.Clear();
            ColumnHeader.ColumnCount = table.Columns.Length;
            for (int i = 0; i < table.Columns.Length; i++)
            {
                columns_size[i] = table.Columns[i].Size;
                ColumnHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, table.Columns[i].Size));
                ColumnHeader.Controls.Add(new Label()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Text = table.Columns[i].Name,
                    TextAlign = ContentAlignment.MiddleCenter
                }, i, 0);
            }
        }

        public string[] ObjToStr(object[] row_data)
        {

            string[] row_data_string = new string[row_data.Length];
            for (int i = 0; i < row_data.Length; i++)
                row_data_string[i] = row_data[i].ToString();
            return row_data_string;
        }

        public void AddRow(object[] row_data_obj)
        {
            string[] row_data;
            if (PreAddRow != null)
                row_data = PreAddRow?.Invoke(row_data_obj);
            else
                row_data = ObjToStr(row_data_obj);

            if (column_amount == 0) throw new Exception("Таблица не инициализирована");
            if (column_amount != row_data.Length) throw new Exception("Количество колонок с данными не соответствует настройкам");

            if (row_amount == 0)
            {
                ColumnRows.Controls.Clear();
                ColumnRows.RowStyles.Clear();
            }

            table_data.Add(row_data);
            TableLayoutPanel table_row = new TableLayoutPanel()
            {
                AutoSize = true,
                BackColor = row_amount % 2 == 0 ? _ColorRowEven : _ColorRowOdd,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Top,
                Margin = new Padding(0),
                Padding = new Padding(0, 6, 0, 6)
            };
            table_row.Click += new EventHandler(TableRow_Click);
            table_row.DoubleClick += new EventHandler(TableRow_DoubleClick);
            table_row.MouseEnter += new EventHandler(TableBody_MouseEnter);
            table_row.MouseLeave += new EventHandler(TableBody_MouseLeave);
            table_row.RowCount = 1;
            table_row.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table_row.ColumnCount = column_amount;
            for (int i = 0; i < column_amount; i++)
            {
                table_row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columns_size[i]));
                table_row.Controls.Add(new NoEventLabel()
                {
                    AutoSize = true,
                    //BackColor = Color.Red,
                    Dock = DockStyle.Top,
                    Margin = new Padding(0),
                    Padding = new Padding(4, 0, 4, 0),
                    Text = row_data[i]
                }, i, 0);
            }
            ColumnRows.RowCount = row_amount + 1;
            ColumnRows.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            ColumnRows.Controls.Add(table_row, 0, row_amount);
            row_amount++;
        }

        public void Clear()
        {
            row_selected = -1;
            row_amount = 0;
            ColumnRows.Controls.Clear();
        }

        private void TableRow_Click(object sender, EventArgs e)
        {
            int row_num = ColumnRows.GetRow((TableLayoutPanel)sender);
            RowClick?.Invoke(row_num, table_data[row_num]);
            if (row_num != row_selected)
            {
                if (row_selected != -1) ColumnRows.Controls[row_selected].BackColor = row_selected % 2 == 0 ? _ColorRowEven : _ColorRowOdd;
                ((TableLayoutPanel)sender).BackColor = _ColorRowSelected;
                row_selected = row_num;
                RowSelectChange?.Invoke(row_num, table_data[row_num]);
            }
        }

        private void TableRow_DoubleClick(object sender, EventArgs e)
        {
            int row_num = ColumnRows.GetCellPosition((TableLayoutPanel)sender).Row;
            RowDoubleClick?.Invoke(row_num, table_data[row_num]);
        }

        private void TableBody_MouseEnter(object sender, EventArgs e)
        {
            TableLayoutPanel row = (TableLayoutPanel)sender;
            row.BackColor = _ColorRowHover;
        }

        private void TableBody_MouseLeave(object sender, EventArgs e)
        {
            TableLayoutPanel row = (TableLayoutPanel)sender;
            int row_num = ColumnRows.GetRow(row);
            if (row_num == row_selected)
            {
                row.BackColor = _ColorRowSelected;
            }
            else
            {
                row.BackColor = row_num % 2 == 0 ? _ColorRowEven : _ColorRowOdd;
            }
        }
    }
}
