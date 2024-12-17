using System;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class EditorFieldTable : UserControl
    {
        private TableObject _table;

        public TableObject DataTable
        {
            get { return _table; }
        }

        public PreAddRowEvent PreAddRow
        {
            set { table1.PreAddRow += value; UpdateTable(); }
        }

        public EditorFieldTable(TableObject table)
        {
            InitializeComponent();

            _table = table;
            table1.TableInit(_table);
            UpdateTable();
        }

        private void UpdateTable()
        {
            table1.Clear();

            for (int i = 0; i < _table.Data.Length; i++)
            {
                table1.AddRow(_table.Data[i]);
            }
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            FormTableEditor tableEditor = new FormTableEditor("Добавить", _table.Columns, _table.GetDefault());
            tableEditor.ShowDialog();
            if (tableEditor.Confirmed == true)
            {
                _table.Data.Add(tableEditor.DataRow);
                UpdateTable();
            }
            tableEditor.Dispose();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            FormTableEditor tableEditor = new FormTableEditor("Изменить", _table.Columns, _table.Data[table1.row_selected]);
            tableEditor.ShowDialog();
            if (tableEditor.Confirmed == true)
            {
                UpdateTable();
            }
            tableEditor.Dispose();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (table1.row_selected < 0)
            {
                AlertBox.Warning("Не выбрано ни одной записи");
                return;
            }
            _table.Data.RemoveAt(table1.row_selected);
            table1.row_selected = -1;
            UpdateTable();
        }
    }
}
