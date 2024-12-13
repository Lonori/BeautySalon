using BeautySalon.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class MaterialColorsForm : Form
    {
        public MaterialColorsForm()
        {
            InitializeComponent();
            // Настройка формы
            Text = "Material Colors Viewer";
            Size = new Size(600, 800);
            AutoScroll = true;

            // Создание панели для размещения цветов
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
            };

            // Добавление цветов в форму
            AddColorBlock(panel, "PrimaryLight", MaterialColors.PrimaryLight);
            AddColorBlock(panel, "Primary", MaterialColors.Primary);
            AddColorBlock(panel, "PrimaryDark", MaterialColors.PrimaryDark);

            AddColorBlock(panel, "ContrastLight", MaterialColors.ContrastLight);
            AddColorBlock(panel, "ContrastDark", MaterialColors.ContrastDark);

            AddColorBlock(panel, "SecondaryLight", MaterialColors.SecondaryLight);
            AddColorBlock(panel, "Secondary", MaterialColors.Secondary);
            AddColorBlock(panel, "SecondaryDark", MaterialColors.SecondaryDark);

            AddColorBlock(panel, "HoverLight", MaterialColors.HoverLight);
            AddColorBlock(panel, "Hover", MaterialColors.Hover);
            AddColorBlock(panel, "HoverDark", MaterialColors.HoverDark);

            AddColorBlock(panel, "FocusLight", MaterialColors.FocusLight);
            AddColorBlock(panel, "Focus", MaterialColors.Focus);
            AddColorBlock(panel, "FocusDark", MaterialColors.FocusDark);

            AddColorBlock(panel, "SelectLight", MaterialColors.SelectLight);
            AddColorBlock(panel, "Select", MaterialColors.Select);
            AddColorBlock(panel, "SelectDark", MaterialColors.SelectDark);

            AddColorBlock(panel, "SuccessLight", MaterialColors.SuccessLight);
            AddColorBlock(panel, "Success", MaterialColors.Success);
            AddColorBlock(panel, "SuccessDark", MaterialColors.SuccessDark);

            AddColorBlock(panel, "ErrorLight", MaterialColors.ErrorLight);
            AddColorBlock(panel, "Error", MaterialColors.Error);
            AddColorBlock(panel, "ErrorDark", MaterialColors.ErrorDark);

            AddColorBlock(panel, "WarningLight", MaterialColors.WarningLight);
            AddColorBlock(panel, "Warning", MaterialColors.Warning);
            AddColorBlock(panel, "WarningDark", MaterialColors.WarningDark);

            AddColorBlock(panel, "InfoLight", MaterialColors.InfoLight);
            AddColorBlock(panel, "Info", MaterialColors.Info);
            AddColorBlock(panel, "InfoDark", MaterialColors.InfoDark);

            AddColorBlock(panel, "NeutralLight", MaterialColors.NeutralLight);
            AddColorBlock(panel, "Neutral", MaterialColors.Neutral);
            AddColorBlock(panel, "NeutralDark", MaterialColors.NeutralDark);

            AddColorBlock(panel, "DisabledLight", MaterialColors.DisabledLight);
            AddColorBlock(panel, "Disabled", MaterialColors.Disabled);
            AddColorBlock(panel, "DisabledDark", MaterialColors.DisabledDark);

            // Добавление панели в форму
            Controls.Add(panel);
        }

        private void AddColorBlock(FlowLayoutPanel panel, string name, Color color)
        {
            // Контейнер для отображения цвета и названия
            Panel colorPanel = new Panel
            {
                Size = new Size(550, 40),
                Margin = new Padding(5),
            };

            // Блок цвета
            Panel colorBlock = new Panel
            {
                BackColor = color,
                Size = new Size(40, 40),
                Margin = new Padding(0),
            };

            // Метка с названием цвета
            Label colorLabel = new Label
            {
                Text = name,
                AutoSize = true,
                Location = new Point(50, 10),
                Font = new Font("Arial", 10, FontStyle.Regular),
            };

            // Добавление в контейнер
            colorPanel.Controls.Add(colorBlock);
            colorPanel.Controls.Add(colorLabel);

            // Добавление в основную панель
            panel.Controls.Add(colorPanel);
        }
    }
}
