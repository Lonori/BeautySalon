
namespace BeautySalon
{
    partial class PageStaff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageStaff));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDelete = new BeautySalon.Components.MaterialButton();
            this.buttonUpdate = new BeautySalon.Components.MaterialButton();
            this.buttonCreate = new BeautySalon.Components.MaterialButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.viewTableData = new BeautySalon.Components.MaterialTable();
            this.themeProvider1 = new BeautySalon.Components.Themes.ThemeProvider();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 429);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 40);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonDelete, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonUpdate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCreate, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.buttonDelete.CornerRadius = 6;
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(422, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(187, 30);
            this.buttonDelete.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Удалить";
            this.themeProvider1.SetUseTheme(this.buttonDelete, true);
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.buttonUpdate.CornerRadius = 6;
            this.buttonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonUpdate.ForeColor = System.Drawing.Color.White;
            this.buttonUpdate.Location = new System.Drawing.Point(216, 5);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(186, 30);
            this.buttonUpdate.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Изменить";
            this.themeProvider1.SetUseTheme(this.buttonUpdate, true);
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.buttonCreate.CornerRadius = 6;
            this.buttonCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCreate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreate.ForeColor = System.Drawing.Color.White;
            this.buttonCreate.Location = new System.Drawing.Point(10, 5);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(186, 30);
            this.buttonCreate.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Добавить";
            this.themeProvider1.SetUseTheme(this.buttonCreate, true);
            this.buttonCreate.Click += new System.EventHandler(this.ButtonInsert_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.viewTableData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 429);
            this.panel2.TabIndex = 2;
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
            this.viewTableData.ColumnWeights = new int[] {
        0};
            this.viewTableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTableData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.viewTableData.ForeColor = System.Drawing.Color.Black;
            this.viewTableData.Location = new System.Drawing.Point(0, 0);
            this.viewTableData.Name = "viewTableData";
            this.viewTableData.SelectedRow = -1;
            this.viewTableData.Size = new System.Drawing.Size(619, 111);
            this.viewTableData.Style = BeautySalon.Components.MaterialTable.TableStyle.Flat;
            this.viewTableData.TabIndex = 1;
            this.viewTableData.TableData = ((System.Collections.Generic.List<System.Collections.Generic.List<string>>)(resources.GetObject("viewTableData.TableData")));
            this.viewTableData.TableHeaders = ((System.Collections.Generic.List<string>)(resources.GetObject("viewTableData.TableHeaders")));
            this.viewTableData.Text = "materialTable1";
            this.themeProvider1.SetUseTheme(this.viewTableData, true);
            // 
            // PageStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "PageStaff";
            this.Size = new System.Drawing.Size(619, 469);
            this.themeProvider1.SetUseTheme(this, true);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private Components.MaterialTable viewTableData;
        private Components.MaterialButton buttonCreate;
        private Components.Themes.ThemeProvider themeProvider1;
        private Components.MaterialButton buttonUpdate;
        private Components.MaterialButton buttonDelete;
    }
}
