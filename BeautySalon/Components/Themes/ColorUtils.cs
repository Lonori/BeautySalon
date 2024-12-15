using System.Drawing;

namespace BeautySalon.Components.Themes
{
    public static class ColorUtils
    {
        public static Color Lighten(Color color, float factor)
        {
            int r = (int)(color.R + (255 - color.R) * factor);
            int g = (int)(color.G + (255 - color.G) * factor);
            int b = (int)(color.B + (255 - color.B) * factor);
            return Color.FromArgb(color.A, r, g, b);
        }

        public static Color Darken(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));
            return Color.FromArgb(color.A, r, g, b);
        }

        public static Color Blend(Color color1, Color color2, float factor)
        {
            int r = (int)(color1.R * (1 - factor) + color2.R * factor);
            int g = (int)(color1.G * (1 - factor) + color2.G * factor);
            int b = (int)(color1.B * (1 - factor) + color2.B * factor);
            return Color.FromArgb(color1.A, r, g, b);
        }
    }
}
