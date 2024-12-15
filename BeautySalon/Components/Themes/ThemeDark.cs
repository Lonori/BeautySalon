using System.Drawing;

namespace BeautySalon.Components.Themes
{
    public class ThemeDark : ITheme
    {
        public string Name => "Dark";
        public Color ColorPrimary => Color.FromArgb(111, 69, 136);
        public Color ColorSecondary => ColorUtils.Blend(ColorPrimary, Color.Black, 0.5f);
        public Color ColorBackground => Color.FromArgb(25, 27, 28);
        public Color ColorBackgroungDark => Color.FromArgb(22, 24, 25);
        public Color ColorForeground => Color.White;
        public Color ColorForegroundContrast => Color.White;
        public Color HoverColor => ColorUtils.Darken(ColorPrimary, 0.6f);
        public Color SelectionColor => ColorUtils.Darken(ColorPrimary, 0.4f);
        public Color BorderColor => Color.FromArgb(122, 122, 122);
        public Color DisabledColor => Color.FromArgb(189, 189, 189);
        public Color ErrorColor => Color.FromArgb(244, 67, 54);
        public Color WarningColor => Color.FromArgb(255, 152, 0);
        public Color SuccessColor => Color.FromArgb(76, 175, 80);
        public Color InfoColor => Color.FromArgb(33, 150, 243);
        public Font Font => new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public MaterialButton.ButtonStyle ButtonStyle => MaterialButton.ButtonStyle.Flat;
        public MaterialTable.TableStyle TableStyle => MaterialTable.TableStyle.Flat;
    }
}
