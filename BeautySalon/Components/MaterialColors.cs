namespace BeautySalon.Components
{
    using System;
    using System.Drawing;

    internal static class MaterialColors
    {
        // Основные цвета
        public static readonly Color PrimaryLight = Color.FromArgb(237, 227, 243);
        public static readonly Color Primary = Color.FromArgb(185, 155, 203);
        public static readonly Color PrimaryDark = Color.FromArgb(111, 69, 136);

        // Контрастные цвета для доступности
        public static readonly Color ContrastLight = Color.White;
        public static readonly Color ContrastDark = Color.Black;

        // Вторичные цвета
        public static readonly Color SecondaryLight = Blend(PrimaryLight, ContrastLight, 0.8f);
        public static readonly Color Secondary = Blend(Primary, ContrastLight, 0.6f);
        public static readonly Color SecondaryDark = Blend(PrimaryDark, ContrastDark, 0.4f);

        // Производные цвета для пользовательских взаимодействий
        public static readonly Color HoverLight = Lighten(Primary, 0.5f);
        public static readonly Color Hover = Lighten(PrimaryDark, 0.1f);
        public static readonly Color HoverDark = Darken(PrimaryDark, 0.1f);

        public static readonly Color SelectLight = Lighten(Primary, 0.3f);
        public static readonly Color Select = Lighten(PrimaryDark, 0.2f);
        public static readonly Color SelectDark = Darken(PrimaryDark, 0.2f);

        public static readonly Color FocusLight = Lighten(Primary, 0.3f);
        public static readonly Color Focus = Lighten(PrimaryDark, 0.2f);
        public static readonly Color FocusDark = Darken(PrimaryDark, 0.2f);

        // Цвета статусов
        public static readonly Color Error = Color.FromArgb(244, 67, 54);
        public static readonly Color ErrorLight = Lighten(Error, 0.4f);
        public static readonly Color ErrorDark = Darken(Error, 0.4f);

        public static readonly Color Success = Color.FromArgb(76, 175, 80);
        public static readonly Color SuccessLight = Lighten(Success, 0.4f);
        public static readonly Color SuccessDark = Darken(Success, 0.4f);

        public static readonly Color Warning = Color.FromArgb(255, 152, 0);
        public static readonly Color WarningLight = Lighten(Warning, 0.4f);
        public static readonly Color WarningDark = Darken(Warning, 0.4f);

        public static readonly Color Info = Color.FromArgb(33, 150, 243);
        public static readonly Color InfoLight = Lighten(Info, 0.4f);
        public static readonly Color InfoDark = Darken(Info, 0.4f);

        // Нейтральные цвета для фонов и границ
        public static readonly Color Neutral = Color.FromArgb(158, 158, 158);
        public static readonly Color NeutralLight = Lighten(Neutral, 0.5f);
        public static readonly Color NeutralDark = Darken(Neutral, 0.5f);

        // Цвета для отключенных элементов
        public static readonly Color Disabled = Color.FromArgb(189, 189, 189);
        public static readonly Color DisabledLight = Lighten(Disabled, 0.2f);
        public static readonly Color DisabledDark = Darken(Disabled, 0.2f);

        // Утилиты для работы с цветами

        /// <summary>
        /// Смешивает два цвета с заданной долей второго цвета.
        /// </summary>
        public static Color Blend(Color color1, Color color2, float amount)
        {
            byte r = (byte)(color1.R * (1 - amount) + color2.R * amount);
            byte g = (byte)(color1.G * (1 - amount) + color2.G * amount);
            byte b = (byte)(color1.B * (1 - amount) + color2.B * amount);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Осветляет заданный цвет на указанный процент.
        /// </summary>
        public static Color Lighten(Color color, float amount)
        {
            byte r = (byte)Math.Min(color.R + color.R * amount, 255);
            byte g = (byte)Math.Min(color.G + color.G * amount, 255);
            byte b = (byte)Math.Min(color.B + color.B * amount, 255);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Затемняет заданный цвет на указанный процент.
        /// </summary>
        public static Color Darken(Color color, float amount)
        {
            byte r = (byte)Math.Max(color.R - color.R * amount, 0);
            byte g = (byte)Math.Max(color.G - color.G * amount, 0);
            byte b = (byte)Math.Max(color.B - color.B * amount, 0);
            return Color.FromArgb(r, g, b);
        }
    }
}
