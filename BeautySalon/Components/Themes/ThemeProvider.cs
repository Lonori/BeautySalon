using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeautySalon.Components.Themes
{
    [ProvideProperty("UseTheme", typeof(Control))]
    public class ThemeProvider : Component, IExtenderProvider
    {

        private readonly Dictionary<Control, bool> _useThemeMap = new Dictionary<Control, bool>();

        public ThemeProvider()
        {
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        public bool CanExtend(object extendee)
        {
            return extendee is Control;
        }

        [DefaultValue(false)]
        [Description("Indicates whether the control should use the current theme.")]
        [DisplayName("UseTheme")]
        public bool GetUseTheme(Control control)
        {
            return _useThemeMap.ContainsKey(control) && _useThemeMap[control];
        }


        public void SetUseTheme(Control control, bool useTheme)
        {
            if (useTheme)
            {
                _useThemeMap[control] = true;
                ApplyTheme(control);
            }
            else
            {
                _useThemeMap.Remove(control);
            }
        }

        private void ApplyTheme(Control control)
        {
            if (!GetUseTheme(control)) return;

            ThemeManager.ControlTheming(control);
        }

        private void OnThemeChanged()
        {
            foreach (Control component in _useThemeMap.Keys)
            {
                ApplyTheme(component);
            }
        }
    }
}
