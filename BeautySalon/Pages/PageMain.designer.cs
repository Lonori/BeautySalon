
namespace BeautySalon
{
    partial class PageMain
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageMain));
            this.panelNotes = new System.Windows.Forms.Panel();
            this.viewTableData = new BeautySalon.Components.MaterialTable();
            this.curentTime = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonBody = new System.Windows.Forms.TableLayoutPanel();
            this.materialButton1 = new BeautySalon.Components.MaterialButton();
            this.materialButton2 = new BeautySalon.Components.MaterialButton();
            this.materialButton3 = new BeautySalon.Components.MaterialButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.themeProvider1 = new BeautySalon.Components.Themes.ThemeProvider();
            this.panelNotes.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.buttonBody.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNotes
            // 
            this.panelNotes.Controls.Add(this.viewTableData);
            this.panelNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNotes.Location = new System.Drawing.Point(300, 0);
            this.panelNotes.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.panelNotes.Name = "panelNotes";
            this.panelNotes.Size = new System.Drawing.Size(600, 330);
            this.panelNotes.TabIndex = 0;
            // 
            // viewTableData
            // 
            this.viewTableData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewTableData.CellMaximumSize = new System.Drawing.Size(0, 0);
            this.viewTableData.CellMinimumSize = new System.Drawing.Size(0, 0);
            this.viewTableData.CellPadding = new System.Windows.Forms.Padding(8);
            this.viewTableData.ColorBorder = System.Drawing.Color.Gray;
            this.viewTableData.ColorHeader = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.viewTableData.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(217)))), ((int)(((byte)(231)))));
            this.viewTableData.ColorSelect = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(162)))), ((int)(((byte)(195)))));
            this.viewTableData.ColorTextHeader = System.Drawing.Color.White;
            this.viewTableData.TableWeights = new int[] {
        0};
            this.viewTableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTableData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.viewTableData.ForeColor = System.Drawing.Color.Black;
            this.viewTableData.Location = new System.Drawing.Point(0, 0);
            this.viewTableData.Margin = new System.Windows.Forms.Padding(0);
            this.viewTableData.Name = "viewTableData";
            this.viewTableData.SelectedRow = -1;
            this.viewTableData.Size = new System.Drawing.Size(600, 112);
            this.viewTableData.TabIndex = 2;
            this.viewTableData.TableData = ((System.Collections.Generic.List<System.Collections.Generic.List<string>>)(resources.GetObject("viewTableData.TableData")));
            this.viewTableData.TableHeaders = ((System.Collections.Generic.List<string>)(resources.GetObject("viewTableData.TableHeaders")));
            this.viewTableData.Text = "materialTable1";
            this.themeProvider1.SetUseTheme(this.viewTableData, true);
            // 
            // curentTime
            // 
            this.curentTime.AutoSize = true;
            this.curentTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.curentTime.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.curentTime.Location = new System.Drawing.Point(0, 286);
            this.curentTime.Margin = new System.Windows.Forms.Padding(0);
            this.curentTime.Name = "curentTime";
            this.curentTime.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.curentTime.Size = new System.Drawing.Size(300, 44);
            this.curentTime.TabIndex = 1;
            this.curentTime.Text = "00:00";
            this.curentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelButtons.Controls.Add(this.buttonBody);
            this.panelButtons.Controls.Add(this.tableLayoutPanel1);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.ForeColor = System.Drawing.Color.Black;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(300, 500);
            this.panelButtons.TabIndex = 1;
            this.themeProvider1.SetUseTheme(this.panelButtons, true);
            // 
            // buttonBody
            // 
            this.buttonBody.ColumnCount = 1;
            this.buttonBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonBody.Controls.Add(this.materialButton1, 0, 0);
            this.buttonBody.Controls.Add(this.curentTime, 0, 3);
            this.buttonBody.Controls.Add(this.materialButton2, 0, 1);
            this.buttonBody.Controls.Add(this.materialButton3, 0, 2);
            this.buttonBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBody.Location = new System.Drawing.Point(0, 0);
            this.buttonBody.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBody.Name = "buttonBody";
            this.buttonBody.RowCount = 4;
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.buttonBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.buttonBody.Size = new System.Drawing.Size(300, 330);
            this.buttonBody.TabIndex = 1;
            // 
            // materialButton1
            // 
            this.materialButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.materialButton1.CornerRadius = 6;
            this.materialButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.materialButton1.ForeColor = System.Drawing.Color.White;
            this.materialButton1.Location = new System.Drawing.Point(5, 6);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Size = new System.Drawing.Size(290, 48);
            this.materialButton1.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.materialButton1.TabIndex = 3;
            this.materialButton1.Text = "Добавить запись";
            this.themeProvider1.SetUseTheme(this.materialButton1, true);
            this.materialButton1.Click += new System.EventHandler(this.ButtonInsert_Click);
            // 
            // materialButton2
            // 
            this.materialButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.materialButton2.CornerRadius = 6;
            this.materialButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.materialButton2.ForeColor = System.Drawing.Color.White;
            this.materialButton2.Location = new System.Drawing.Point(5, 66);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.Size = new System.Drawing.Size(290, 48);
            this.materialButton2.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.materialButton2.TabIndex = 4;
            this.materialButton2.Text = "Выполнено";
            this.themeProvider1.SetUseTheme(this.materialButton2, true);
            this.materialButton2.Click += new System.EventHandler(this.ButtonComplete_Click);
            // 
            // materialButton3
            // 
            this.materialButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.materialButton3.CornerRadius = 6;
            this.materialButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.materialButton3.ForeColor = System.Drawing.Color.White;
            this.materialButton3.Location = new System.Drawing.Point(5, 126);
            this.materialButton3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton3.Name = "materialButton3";
            this.materialButton3.Size = new System.Drawing.Size(290, 48);
            this.materialButton3.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.materialButton3.TabIndex = 5;
            this.materialButton3.Text = "Отменить";
            this.themeProvider1.SetUseTheme(this.materialButton3, true);
            this.materialButton3.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.monthCalendar1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 330);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 170);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(68, 4);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(4);
            this.monthCalendar1.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(300, 330);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 170);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label10, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(600, 170);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(230, 124);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(366, 22);
            this.label10.TabIndex = 9;
            this.label10.Text = "0";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(4, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(218, 22);
            this.label9.TabIndex = 8;
            this.label9.Text = "Доход за месяц:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(230, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(366, 22);
            this.label8.TabIndex = 7;
            this.label8.Text = "0";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 94);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 22);
            this.label7.TabIndex = 6;
            this.label7.Text = "Расход материалов за месяц:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(230, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(366, 22);
            this.label6.TabIndex = 5;
            this.label6.Text = "0";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Выручка за месяц:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(230, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(366, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Посетителей за месяц:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(230, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Записей сегодня:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PageMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.panelNotes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PageMain";
            this.Size = new System.Drawing.Size(900, 500);
            this.themeProvider1.SetUseTheme(this, true);
            this.panelNotes.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.buttonBody.ResumeLayout(false);
            this.buttonBody.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNotes;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label curentTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel buttonBody;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private Components.MaterialButton materialButton1;
        private Components.MaterialButton materialButton2;
        private Components.MaterialButton materialButton3;
        private Components.MaterialTable viewTableData;
        private Components.Themes.ThemeProvider themeProvider1;
    }
}
