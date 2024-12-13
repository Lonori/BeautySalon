namespace BeautySalon.Components
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="MaterialButton" />
    /// </summary>
    public class MaterialButton : Control
    {
        private bool _hover;
        private bool _active;
        private int _cornerRadius = 10;
        private ButtonStyle _style = ButtonStyle.Basic;

        public MaterialButton()
        {
            DoubleBuffered = true;
            ColourDistribution();
        }

        [Browsable(true)]
        [DefaultValue(10)]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                if (value < 0)
                {
                    _cornerRadius = -1;
                }
                else
                {
                    _cornerRadius = value;
                }
                Invalidate();
            }
        }

        [Browsable(true)]
        [DefaultValue(ButtonStyle.Basic)]
        public ButtonStyle Style
        {
            get { return _style; }
            set
            {
                _style = value;
                ColourDistribution();
                Invalidate();
            }
        }

        [Browsable(false)]
        public bool Hovered
        {
            get { return _hover; }
        }

        [Browsable(false)]
        public bool Activated
        {
            get { return _active; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent.BackColor);

            Rectangle buttonRect = new Rectangle(ClientRectangle.Location, ClientRectangle.Size);

            switch (_style)
            {
                case ButtonStyle.Basic:
                    DrawButton(g, buttonRect, BackColor, ForeColor, false, false);
                    break;
                case ButtonStyle.Raised:
                    DrawButton(g, buttonRect, BackColor, ForeColor, false, true);
                    break;
                case ButtonStyle.Stroked:
                    DrawButton(g, buttonRect, BackColor, ForeColor, true, false);
                    break;
                case ButtonStyle.Flat:
                    DrawButton(g, buttonRect, BackColor, ForeColor, false, false);
                    break;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            MouseEnter += (sender, args) =>
            {
                _hover = true;
                Invalidate();
            };

            MouseLeave += (sender, args) =>
            {
                _hover = false;
                Invalidate();
            };

            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    _active = true;
                    Invalidate();
                }
            };

            MouseUp += (sender, args) =>
            {
                _active = false;
                Invalidate();
            };

        }

        private void ColourDistribution()
        {
            switch (_style)
            {
                case ButtonStyle.Basic:
                    BackColor = MaterialColors.SecondaryLight;
                    ForeColor = MaterialColors.PrimaryDark;
                    break;
                case ButtonStyle.Raised:
                    BackColor = MaterialColors.PrimaryDark;
                    ForeColor = MaterialColors.SecondaryLight;
                    break;
                case ButtonStyle.Stroked:
                    BackColor = MaterialColors.SecondaryLight;
                    ForeColor = MaterialColors.PrimaryDark;
                    break;
                case ButtonStyle.Flat:
                    BackColor = MaterialColors.PrimaryDark;
                    ForeColor = MaterialColors.SecondaryLight;
                    break;
                default:
                    BackColor = MaterialColors.PrimaryDark;
                    ForeColor = MaterialColors.SecondaryLight;
                    break;
            }
        }

        private void DrawButton(Graphics g, Rectangle clientRect, Color primary, Color secondary, bool stroked, bool shadow)
        {
            if (shadow)
            {
                clientRect.X += 3;
                clientRect.Y += 1;
                clientRect.Width -= 6;
                clientRect.Height -= 6;
            }

            RectangleF rect = new RectangleF(clientRect.Location, clientRect.Size);
            rect.X -= 0.5f;
            rect.Y -= 0.5f;
            GraphicsPath rectPath = DrawHelper.CreateRoundRect(rect, _cornerRadius);

            if (shadow) DrawHelper.DrawSquareShadow(g, rect, _cornerRadius);
            using (SolidBrush brush = new SolidBrush(primary))
            {
                g.FillPath(brush, rectPath);
            }
            if (stroked)
            {
                using (Pen pen = new Pen(Color.FromArgb(100, 122, 122, 122), 1))
                {
                    g.DrawPath(pen, rectPath);
                }
            }
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(60, secondary)))
            {
                if (Hovered) g.FillPath(brush, rectPath);
                if (Activated) g.FillPath(brush, rectPath);
            }

            TextFormatFlags alignment = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(g, Text, Font, clientRect, secondary, alignment);
        }

        public enum ButtonStyle
        {
            Basic,
            Raised,
            Stroked,
            Flat,
            Icon
        }
    }
}