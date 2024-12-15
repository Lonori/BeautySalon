using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BeautySalon.Components.Themes
{
    public static class ThemeManager
    {
        private static readonly Dictionary<string, ITheme> Themes = new Dictionary<string, ITheme>();
        private static ITheme _currentTheme;
        public static event Action ThemeChanged;

        static ThemeManager()
        {
            // Register default themes
            RegisterTheme(new ThemeLight());
            RegisterTheme(new ThemeDark());

            _currentTheme = Themes["Light"];
        }

        public static ITheme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ThemeChanged?.Invoke();
                }
            }
        }

        public static void SwitchTheme(string themeName)
        {
            if (Themes.ContainsKey(themeName))
            {
                CurrentTheme = Themes[themeName];
            }
        }

        public static void RegisterTheme(ITheme theme)
        {
            if (!Themes.ContainsKey(theme.Name))
            {
                Themes[theme.Name] = theme;
            }
        }

        public static void ControlTheming(Control control)
        {
            ITheme theme = CurrentTheme;
            if (control is IThemable tControl)
            {
                tControl.ApplyTheme(theme);
            }
            else if(control is Form form)
            {
                form.BackColor = theme.ColorBackground;
                form.ForeColor = theme.ColorForeground;
                form.Font = theme.Font;
            }
            else if (control is MenuStrip menuStrip)
            {
                menuStrip.BackColor = theme.ColorSecondary;
                menuStrip.ForeColor = theme.ColorForeground;
                menuStrip.Font = new Font(theme.Font.FontFamily, 9, theme.Font.Style, theme.Font.Unit, theme.Font.GdiCharSet);
            }
            else if(control is MaterialButton matButton)
            {
                matButton.BackColor = theme.ColorPrimary;
                matButton.ForeColor = theme.ColorForegroundContrast;
                matButton.Font = theme.Font;
                matButton.Style = theme.ButtonStyle;
            }
            else if (control is MaterialTable matTable)
            {
                matTable.BackColor = theme.ColorBackground;
                matTable.ForeColor = theme.ColorForeground;
                matTable.Font = theme.Font;
                matTable.ColorHeader = theme.ColorPrimary;
                matTable.ColorTextHeader = theme.ColorForegroundContrast;
                matTable.ColorHover = theme.HoverColor;
                matTable.ColorSelect = theme.SelectionColor;
                matTable.Style = theme.TableStyle;
                matTable.StripedRows = true;
            }
            else if (control is Button button)
            {
                button.BackColor = theme.ColorPrimary;
                button.ForeColor = theme.ColorForegroundContrast;
                button.Font = theme.Font;
            }
            else
            {
                control.BackColor = theme.ColorBackground;
                control.ForeColor = theme.ColorForeground;
            }
        }
    }
}
