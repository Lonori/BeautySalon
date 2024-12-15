using System.Drawing;

namespace BeautySalon.Components.Themes
{
    public interface ITheme
    {
        string Name { get; }
        Color ColorPrimary { get; }
        Color ColorSecondary { get; }
        Color ColorBackground { get; }
        Color ColorBackgroungDark { get; }
        Color ColorForeground { get; }
        Color ColorForegroundContrast { get; }
        Color HoverColor { get; }
        Color SelectionColor { get; }
        Color BorderColor { get; }
        Color DisabledColor { get; }
        Color ErrorColor { get; }
        Color WarningColor { get; }
        Color SuccessColor { get; }
        Color InfoColor { get; }
        Font Font { get; }
        MaterialButton.ButtonStyle ButtonStyle { get; }
        MaterialTable.TableStyle TableStyle { get; }
    }

    public interface IThemable
    {
        void ApplyTheme(ITheme theme);
    }
}
