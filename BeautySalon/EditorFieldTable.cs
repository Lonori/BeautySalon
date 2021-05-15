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
    public partial class EditorFieldTable : UserControl
    {
        private TableObject _Table;

        public TableObject DataTable
        {
            get { return _Table; }
        }

        public PreAddRowEvent PreAddRow
        {
            set { table1.PreAddRow += value; UpdateTable(); }
        }

        public EditorFieldTable(TableObject table)
        {
            InitializeComponent();

            _Table = table;
            table1.TableInit(_Table);
            UpdateTable();
        }

        private void UpdateTable()
        {
            table1.Clear();

            for(int i = 0; i < _Table.Data.Length; i++)
            {
                table1.AddRow(_Table.Data[i]);
            }
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            FormTableEditor tableEditor = new FormTableEditor("Добавить", _Table.Columns, _Table.GetDefault());
            tableEditor.ShowDialog();
            if (tableEditor.Confirmed == true)
            {
                _Table.Data.Add(tableEditor.DataRow);
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
            FormTableEditor tableEditor = new FormTableEditor("Изменить", _Table.Columns, _Table.Data[table1.row_selected]);
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
            _Table.Data.RemoveAt(table1.row_selected);
            table1.row_selected = -1;
            UpdateTable();
        }
    }
}
