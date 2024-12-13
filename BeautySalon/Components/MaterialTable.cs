namespace BeautySalon.Components
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class MaterialTable : Control
    {
        protected List<string> _tableDataHeaders = null;
        protected List<List<string>> _tableData = null;
        protected int[] _columnWeights = new int[] { 0 };
        protected Size _cellMinSize = new Size(0, 0);
        protected Size _cellMaxSize = new Size(0, 0);
        protected Padding _cellPadding = new Padding(8);
        protected Color _colorBorder = Color.Gray;
        protected Color _colorHeader = Color.CornflowerBlue;
        protected Color _colorTextHeader = Color.White;
        protected Color _colorRowPrimary = Color.White;
        protected Color _colorRowSecondary = Color.WhiteSmoke;
        protected Color _colorHover = Color.LightBlue;
        protected Color _colorSelect = Color.Blue;
        protected TableStyle _style = TableStyle.Basic;

        private Rectangle _tableHeader = new Rectangle();
        private Rectangle[] _tableRows = null;
        private int[] _tableColumns = null;
        private int _hoveredRow = -1;
        private int _selectedRow = -1;

        public MaterialTable()
        {
            DoubleBuffered = true;
            BackColor = MaterialColors.ContrastLight;
            ForeColor = MaterialColors.ContrastDark;
            _colorHeader = MaterialColors.PrimaryDark;
            _colorTextHeader = MaterialColors.ContrastLight;
            _colorRowPrimary = MaterialColors.ContrastLight;
            _colorRowSecondary = MaterialColors.SecondaryLight;
            _colorHover = MaterialColors.HoverLight;
            _colorSelect = MaterialColors.SelectLight;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                _tableDataHeaders = new List<string> { "col1", "col2", "col3" };
                _tableData = new List<List<string>> {
                    new List<string> { "row1", "cell12", "col13" },
                    new List<string> { "row2", "cell22", "col23" }
                };
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public Size CellMinimumSize
        {
            get { return _cellMinSize; }
            set
            {
                _cellMinSize = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public Size CellMaximumSize
        {
            get { return _cellMaxSize; }
            set
            {
                _cellMaxSize = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public Padding CellPadding
        {
            get { return _cellPadding; }
            set
            {
                _cellPadding = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public List<string> TableHeaders
        {
            get { return _tableDataHeaders; }
            set
            {
                _tableDataHeaders = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public List<List<string>> TableData
        {
            get { return _tableData; }
            set
            {
                _tableData = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        [DefaultValue(new int[] { 0 })]
        public int[] ColumnWeights
        {
            get { return _columnWeights; }
            set
            {
                _columnWeights = value;
                RecalculateLayout();
            }
        }

        [Browsable(true)]
        public Color ColorBorder
        {
            get { return _colorBorder; }
            set
            {
                _colorBorder = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color ColorHeader
        {
            get { return _colorHeader; }
            set
            {
                _colorHeader = value;
                if (_tableHeader != null)
                {
                    Invalidate(_tableHeader);
                }
            }
        }

        [Browsable(true)]
        public Color ColorTextHeader
        {
            get { return _colorTextHeader; }
            set
            {
                _colorTextHeader = value;
                if (_tableHeader != null)
                {
                    Invalidate(_tableHeader);
                }
            }
        }

        [Browsable(true)]
        public Color ColorRowPrimary
        {
            get { return _colorRowPrimary; }
            set
            {
                _colorRowPrimary = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color ColorRowSecondary
        {
            get { return _colorRowSecondary; }
            set
            {
                _colorRowSecondary = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color ColorHover
        {
            get { return _colorHover; }
            set
            {
                _colorHover = value;
            }
        }

        [Browsable(true)]
        public Color ColorSelect
        {
            get { return _colorSelect; }
            set
            {
                _colorSelect = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DefaultValue(TableStyle.Basic)]
        public TableStyle Style
        {
            get { return _style; }
            set
            {
                _style = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public int SelectedRow
        {
            get { return _selectedRow; }
        }

        protected int GetColumnWeight(int index)
        {
            if (ColumnWeights != null && index < _columnWeights.Length)
            {
                return ColumnWeights[index];
            }
            return 1;
        }

        public void RecalculateLayout()
        {
            _tableHeader = new Rectangle();
            _tableColumns = null;
            _tableRows = null;

            int columnsHeader = 0;
            int columnsData = 0;
            int rowsTotal = 0;

            if (TableHeaders != null)
            {
                columnsHeader = TableHeaders.Count;
                rowsTotal += 1;
            }

            if (TableData != null)
            {
                foreach (List<string> row in TableData)
                {
                    if (columnsData < row.Count)
                    {
                        columnsData = row.Count;
                    }
                }
                rowsTotal += TableData.Count;
            }

            int columnsTotal = Math.Max(columnsHeader, columnsData);

            if (columnsTotal == 0) return;

            Rectangle[] rows = new Rectangle[rowsTotal];
            int[] columns = new int[columnsTotal];
            int totalWidth = Width;
            int totalWeight = 0;
            int totalWidthFixed = 0;
            int availableWidth = totalWidth;

            for (int i = 0; i < columnsTotal; i++)
            {
                int columnWeight = GetColumnWeight(i);
                totalWeight += columnWeight;

                if (columnWeight == 0)
                {
                    int maxTextWidth = 0;
                    if (TableHeaders != null && i < TableHeaders.Count)
                    {
                        maxTextWidth = TextRenderer.MeasureText(TableHeaders[i], Font).Width;
                    }

                    foreach (List<string> row in TableData)
                    {
                        if (i < row.Count)
                        {
                            int textWidth = TextRenderer.MeasureText(row[i], Font).Width;
                            maxTextWidth = Math.Max(maxTextWidth, textWidth);
                        }
                    }

                    columns[i] = MathExt.Clamp(
                        maxTextWidth + _cellPadding.Horizontal,
                        _cellMinSize.Width,
                        _cellMaxSize.Width == 0 ? int.MaxValue : _cellMaxSize.Width
                    );
                    totalWidthFixed += columns[i];
                }
            }

            availableWidth -= totalWidthFixed;

            for (int i = 0; i < columnsTotal; i++)
            {
                int columnWeight = GetColumnWeight(i);
                if (columnWeight > 0)
                {
                    int growWidth = totalWeight > 0 ? MathExt.Floor((availableWidth * columnWeight) / totalWeight) : 0;
                    columns[i] = MathExt.Clamp(
                        growWidth,
                        _cellMinSize.Width,
                        _cellMaxSize.Width == 0 ? int.MaxValue : _cellMaxSize.Width
                    );
                }
            }

            int extraWidth = (availableWidth + totalWidthFixed - 1) - columns.Sum();
            columns[columns.Length - 1] += extraWidth;

            int yOffset = 0;
            if (TableHeaders != null)
            {
                int maxHeight = _cellMinSize.Height;

                for (int i = 0; i < TableHeaders.Count; i++)
                {
                    Size textSize = TextRenderer.MeasureText(
                        TableHeaders[i],
                        Font,
                        new Size(columns[i], int.MaxValue),
                        TextFormatFlags.WordBreak
                    );

                    maxHeight = Math.Max(maxHeight, textSize.Height + _cellPadding.Vertical);
                }

                _tableHeader = new Rectangle(0, yOffset, totalWidth, maxHeight);
                yOffset += maxHeight;
            }

            for (int row = 0; row < TableData.Count; row++)
            {
                int maxHeight = _cellMinSize.Height;
                for (int i = 0; i < TableData[row].Count; i++)
                {
                    Size textSize = TextRenderer.MeasureText(
                        TableData[row][i],
                        Font,
                        new Size(columns[i], int.MaxValue),
                        TextFormatFlags.WordBreak
                    );

                    maxHeight = Math.Max(maxHeight, textSize.Height + _cellPadding.Vertical);
                }

                rows[row] = new Rectangle(0, yOffset, totalWidth, maxHeight);
                yOffset += maxHeight;
            }

            if (_style.Equals(TableStyle.Flat))
            {
                Height = yOffset;
            }
            else
            {
                Height = yOffset + 1;
            }

            _tableColumns = columns;
            _tableRows = rows;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_tableColumns == null || _tableRows == null) return;

            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            if (TableHeaders != null)
            {
                using (SolidBrush brushBg = new SolidBrush(_colorHeader))
                using (Pen pen = new Pen(_colorBorder))
                {
                    g.FillRectangle(brushBg, _tableHeader);

                    int xOffset = 0;
                    for (int i = 0; i < _tableColumns.Length; i++)
                    {
                        Rectangle cellRect = new Rectangle(xOffset, _tableHeader.Y, _tableColumns[i], _tableHeader.Height);

                        if (i < TableHeaders.Count)
                        {
                            Rectangle textRect = new Rectangle(
                                cellRect.X + _cellPadding.Left,
                                cellRect.Y + _cellPadding.Top,
                                cellRect.Width - _cellPadding.Horizontal,
                                cellRect.Height - _cellPadding.Vertical
                            );

                            TextRenderer.DrawText(
                                g,
                                TableHeaders[i],
                                Font,
                                textRect,
                                _colorTextHeader,
                                TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter
                            );
                        }

                        if (_style.Equals(TableStyle.Basic))
                        {
                            g.DrawRectangle(pen, cellRect);
                        }

                        xOffset += _tableColumns[i];
                    }
                }
            }

            using (SolidBrush brushHover = new SolidBrush(_colorHover))
            using (SolidBrush brushSelect = new SolidBrush(_colorSelect))
            using (SolidBrush brushBgPrimary = new SolidBrush(_colorRowPrimary))
            using (SolidBrush brushBgSecondary = new SolidBrush(_colorRowSecondary))
            using (Pen pen = new Pen(_colorBorder))
            {
                for (int row = 0; row < TableData.Count; row++)
                {
                    Rectangle rowRect = _tableRows[row];

                    if (row == _hoveredRow && row != _selectedRow)
                    {
                        g.FillRectangle(brushHover, rowRect);
                    }
                    else if (row == _selectedRow)
                    {
                        g.FillRectangle(brushSelect, rowRect);
                    }
                    else if (row % 2 == 0)
                    {
                        g.FillRectangle(brushBgPrimary, rowRect);
                    }
                    else
                    {
                        g.FillRectangle(brushBgSecondary, rowRect);
                    }

                    int xOffset = 0;
                    for (int i = 0; i < _tableColumns.Length; i++)
                    {
                        Rectangle cellRect = new Rectangle(xOffset, rowRect.Y, _tableColumns[i], rowRect.Height);

                        if (i < TableData[row].Count)
                        {
                            Rectangle textRect = new Rectangle(
                                cellRect.X + _cellPadding.Left,
                                cellRect.Y + _cellPadding.Top,
                                cellRect.Width - _cellPadding.Horizontal,
                                cellRect.Height - _cellPadding.Vertical
                            );

                            TextRenderer.DrawText(
                                g,
                                TableData[row][i],
                                Font,
                                textRect,
                                ForeColor,
                                TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter
                            );
                        }

                        switch (_style)
                        {
                            case TableStyle.Basic:
                                g.DrawRectangle(pen, cellRect);
                                break;
                            case TableStyle.BorderLine:
                                g.DrawLine(
                                    pen,
                                    new Point(cellRect.X, cellRect.Y),
                                    new Point(cellRect.X + cellRect.Width, cellRect.Y)
                                );
                                if (row < TableData.Count - 1) break;
                                g.DrawLine(
                                    pen,
                                    new Point(cellRect.X, cellRect.Y + cellRect.Height),
                                    new Point(cellRect.X + cellRect.Width, cellRect.Y + cellRect.Height)
                                );
                                break;
                        }

                        xOffset += _tableColumns[i];
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RecalculateLayout();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_tableRows == null) return;
            int previousHoveredRow = _hoveredRow;
            _hoveredRow = -1;

            for (int i = 0; i < _tableRows.Length; i++)
            {
                if (_tableRows[i].Contains(e.Location))
                {
                    _hoveredRow = i;
                    break;
                }
            }

            if (_hoveredRow != previousHoveredRow)
            {
                if (previousHoveredRow >= 0)
                {
                    Invalidate(_tableRows[previousHoveredRow]);
                }

                if (_hoveredRow >= 0)
                {
                    Invalidate(_tableRows[_hoveredRow]);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_tableRows == null) return;
            int previousHoveredRow = _hoveredRow;
            _hoveredRow = -1;

            if (_hoveredRow != previousHoveredRow)
            {
                if (previousHoveredRow >= 0)
                {
                    Invalidate(_tableRows[previousHoveredRow]);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_tableRows == null) return;
            int previousSelectedRow = _selectedRow;

            for (int i = 0; i < _tableRows.Length; i++)
            {
                if (_tableRows[i].Contains(e.Location))
                {
                    _selectedRow = i;
                    break;
                }
            }

            if (_selectedRow != previousSelectedRow)
            {
                if (previousSelectedRow >= 0)
                {
                    Invalidate(_tableRows[previousSelectedRow]);
                }

                if (_selectedRow >= 0)
                {
                    Invalidate(_tableRows[_selectedRow]);
                }
            }
        }

        public enum TableStyle
        {
            Basic,
            BorderLine,
            Flat
        }
    }
}
