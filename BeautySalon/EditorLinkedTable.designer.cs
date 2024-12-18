
namespace BeautySalon
{
    partial class EditorLinkedTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorLinkedTable));
            this.TableName = new System.Windows.Forms.Label();
            this.ButtonContainer = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonCreate = new BeautySalon.Components.MaterialButton();
            this.ButtonUpdate = new BeautySalon.Components.MaterialButton();
            this.ButtonDelete = new BeautySalon.Components.MaterialButton();
            this.table = new BeautySalon.Components.MaterialTable();
            this.themeProvider1 = new BeautySalon.Components.Themes.ThemeProvider();
            this.ButtonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableName
            // 
            this.TableName.AutoSize = true;
            this.TableName.BackColor = System.Drawing.Color.Transparent;
            this.TableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableName.Location = new System.Drawing.Point(0, 0);
            this.TableName.Margin = new System.Windows.Forms.Padding(0);
            this.TableName.Name = "TableName";
            this.TableName.Padding = new System.Windows.Forms.Padding(6);
            this.TableName.Size = new System.Drawing.Size(65, 25);
            this.TableName.TabIndex = 0;
            this.TableName.Text = "Таблица:";
            this.TableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonContainer
            // 
            this.ButtonContainer.BackColor = System.Drawing.Color.Transparent;
            this.ButtonContainer.ColumnCount = 3;
            this.ButtonContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ButtonContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ButtonContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ButtonContainer.Controls.Add(this.ButtonCreate, 0, 0);
            this.ButtonContainer.Controls.Add(this.ButtonUpdate, 1, 0);
            this.ButtonContainer.Controls.Add(this.ButtonDelete, 2, 0);
            this.ButtonContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonContainer.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonContainer.Location = new System.Drawing.Point(0, 25);
            this.ButtonContainer.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonContainer.Name = "ButtonContainer";
            this.ButtonContainer.RowCount = 1;
            this.ButtonContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.ButtonContainer.Size = new System.Drawing.Size(400, 36);
            this.ButtonContainer.TabIndex = 1;
            // 
            // ButtonCreate
            // 
            this.ButtonCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.ButtonCreate.CornerRadius = 0;
            this.ButtonCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonCreate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonCreate.ForeColor = System.Drawing.Color.White;
            this.ButtonCreate.Location = new System.Drawing.Point(3, 3);
            this.ButtonCreate.Name = "ButtonCreate";
            this.ButtonCreate.Size = new System.Drawing.Size(127, 30);
            this.ButtonCreate.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.ButtonCreate.TabIndex = 3;
            this.ButtonCreate.Text = "Добавить";
            this.themeProvider1.SetUseTheme(this.ButtonCreate, true);
            this.ButtonCreate.Click += new System.EventHandler(this.ButtonInsert_Click);
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.ButtonUpdate.CornerRadius = 0;
            this.ButtonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonUpdate.ForeColor = System.Drawing.Color.White;
            this.ButtonUpdate.Location = new System.Drawing.Point(136, 3);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(127, 30);
            this.ButtonUpdate.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.ButtonUpdate.TabIndex = 1;
            this.ButtonUpdate.Text = "Изменить";
            this.themeProvider1.SetUseTheme(this.ButtonUpdate, true);
            this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.ButtonDelete.CornerRadius = 0;
            this.ButtonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonDelete.ForeColor = System.Drawing.Color.White;
            this.ButtonDelete.Location = new System.Drawing.Point(269, 3);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(128, 30);
            this.ButtonDelete.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.ButtonDelete.TabIndex = 2;
            this.ButtonDelete.Text = "Удалить";
            this.themeProvider1.SetUseTheme(this.ButtonDelete, true);
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // table
            // 
            this.table.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.table.CellMaximumSize = new System.Drawing.Size(0, 0);
            this.table.CellMinimumSize = new System.Drawing.Size(0, 0);
            this.table.CellPadding = new System.Windows.Forms.Padding(8);
            this.table.ColorBorder = System.Drawing.Color.Gray;
            this.table.ColorHeader = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.table.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(217)))), ((int)(((byte)(231)))));
            this.table.ColorSelect = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(162)))), ((int)(((byte)(195)))));
            this.table.ColorTextHeader = System.Drawing.Color.White;
            this.table.ColumnWeights = new int[] {
        0};
            this.table.Dock = System.Windows.Forms.DockStyle.Top;
            this.table.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.table.ForeColor = System.Drawing.Color.Black;
            this.table.Location = new System.Drawing.Point(0, 61);
            this.table.Margin = new System.Windows.Forms.Padding(0);
            this.table.Name = "table";
            this.table.SelectedRow = -1;
            this.table.Size = new System.Drawing.Size(400, 111);
            this.table.TabIndex = 2;
            this.table.TableData = ((System.Collections.Generic.List<System.Collections.Generic.List<string>>)(resources.GetObject("table.TableData")));
            this.table.TableHeaders = ((System.Collections.Generic.List<string>)(resources.GetObject("table.TableHeaders")));
            this.table.Text = "materialTable1";
            this.themeProvider1.SetUseTheme(this.table, true);
            // 
            // EditorLinkedTable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.table);
            this.Controls.Add(this.ButtonContainer);
            this.Controls.Add(this.TableName);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(0, 172);
            this.Name = "EditorLinkedTable";
            this.Size = new System.Drawing.Size(400, 172);
            this.themeProvider1.SetUseTheme(this, true);
            this.ButtonContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TableName;
        private System.Windows.Forms.TableLayoutPanel ButtonContainer;
        private Components.MaterialButton ButtonUpdate;
        private Components.MaterialButton ButtonDelete;
        private Components.MaterialButton ButtonCreate;
        private Components.Themes.ThemeProvider themeProvider1;
        private Components.MaterialTable table;
    }
}
