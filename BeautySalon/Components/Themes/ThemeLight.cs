using System.Drawing;

namespace BeautySalon.Components.Themes
{
    public class ThemeLight : ITheme
    {
        public string Name => "Light";
        public Color ColorPrimary => Color.FromArgb(111, 69, 136);
        public Color ColorSecondary => ColorUtils.Blend(ColorPrimary, Color.White, 0.6f);
        public Color ColorBackground => Color.FromArgb(255, 255, 255);
        public Color ColorBackgroungDark => Color.FromArgb(249, 249, 249);
        public Color ColorForeground => Color.Black;
        public Color ColorForegroundContrast => Color.White;
        public Color HoverColor => ColorUtils.Lighten(ColorPrimary, 0.8f);
        public Color SelectionColor => ColorUtils.Lighten(ColorPrimary, 0.5f);
        public Color BorderColor => Color.FromArgb(122, 122, 122);
        public Color DisabledColor => Color.FromArgb(189, 189, 189);
        public Color ErrorColor => Color.FromArgb(244, 67, 54);
        public Color WarningColor => Color.FromArgb(255, 152, 0);
        public Color SuccessColor => Color.FromArgb(76, 175, 80);
        public Color InfoColor => Color.FromArgb(33, 150, 243);
        public Font Font => new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public MaterialButton.ButtonStyle ButtonStyle => MaterialButton.ButtonStyle.Flat;
        public MaterialTable.TableStyle TableStyle => MaterialTable.TableStyle.Basic;
    }
}
