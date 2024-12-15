namespace BeautySalon.Components
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public enum TextBoxType
    {
        Default,
        Password,
        Time
    }

    public class MaterialTextBox : Control
    {
        private string text = string.Empty;
        private string hintText = "Hint text";
        private int borderRadius = 6;
        private Color borderColor = Color.Gray;
        private Color focusedBorderColor = Color.Blue;
        private Color hintTextColor = Color.Gray;
        private Color textColor = Color.Black;
        private Padding textPadding = new Padding(12, 6, 12, 6);
        private bool isFocused = false;
        private TextBoxType textBoxType = TextBoxType.Text;
        private int caretPosition = 0;
        private bool showCaret = false;
        private System.Timers.Timer caretTimer;

        public enum TextBoxType
        {
            Text,
            Password,
            Time,
            Numeric
        }

        public MaterialTextBox()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.IBeam;
            this.Font = new Font("Segoe UI", 10);

            caretTimer = new System.Timers.Timer(500);
            caretTimer.Elapsed += (s, e) =>
            {
                showCaret = !showCaret;
                Invalidate();
            };
        }

        [Category("Material Settings")]
        public string HintText
        {
            get => hintText;
            set { hintText = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Material Settings")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public Color FocusedBorderColor
        {
            get => focusedBorderColor;
            set { focusedBorderColor = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public Color HintTextColor
        {
            get => hintTextColor;
            set { hintTextColor = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public Color TextColor
        {
            get => textColor;
            set { textColor = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public Padding TextPadding
        {
            get => textPadding;
            set { textPadding = value; Invalidate(); }
        }

        [Category("Material Settings")]
        public TextBoxType Type
        {
            get => textBoxType;
            set { textBoxType = value; Invalidate(); }
        }

        public override string Text
        {
            get => text;
            set { text = value; caretPosition = text.Length; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw border
            var borderRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var path = RoundedRectangle(borderRectangle, borderRadius);
            using (var pen = new Pen(isFocused ? focusedBorderColor : borderColor, 1))
            {
                graphics.DrawPath(pen, path);
            }

            // Draw text or hint
            var textRectangle = new Rectangle(
                textPadding.Left,
                textPadding.Top,
                this.Width - textPadding.Horizontal,
                this.Height - textPadding.Vertical
            );

            if (string.IsNullOrEmpty(this.Text) && !isFocused)
            {
                TextRenderer.DrawText(
                    graphics,
                    hintText,
                    this.Font,
                    textRectangle,
                    hintTextColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
            else
            {
                string displayText = textBoxType == TextBoxType.Password ? new string('●', text.Length) : text;
                TextRenderer.DrawText(
                    graphics,
                    displayText,
                    this.Font,
                    textRectangle,
                    textColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                // Draw caret
                if (isFocused && showCaret)
                {
                    var textSize = TextRenderer.MeasureText(displayText.Substring(0, caretPosition), this.Font);
                    var caretX = textPadding.Left + textSize.Width;
                    var caretY = textPadding.Top;
                    graphics.DrawLine(new Pen(textColor, 1), caretX, caretY, caretX, caretY + this.Font.Height);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            isFocused = true;
            caretTimer.Start();
            caretPosition = text.Length; // Move caret to end of text
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isFocused = false;
            caretTimer.Stop();
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (textBoxType == TextBoxType.Numeric && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (textBoxType == TextBoxType.Time && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':')
            {
                e.Handled = true;
            }
            else
            {
                text = text.Insert(caretPosition, e.KeyChar.ToString());
                caretPosition++;
                Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Back && caretPosition > 0)
            {
                text = text.Remove(caretPosition - 1, 1);
                caretPosition--;
                Invalidate();
            }
            else if (e.KeyCode == Keys.Left && caretPosition > 0)
            {
                caretPosition--;
                Invalidate();
            }
            else if (e.KeyCode == Keys.Right && caretPosition < text.Length)
            {
                caretPosition++;
                Invalidate();
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            float r = radius * 2;
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, r, r, 180, 90);
            path.AddArc(bounds.Right - r, bounds.Y, r, r, 270, 90);
            path.AddArc(bounds.Right - r, bounds.Bottom - r, r, r, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
