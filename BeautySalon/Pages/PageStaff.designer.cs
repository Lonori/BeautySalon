
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
            this.ButtonInsert = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.ButtonUpdate = new System.Windows.Forms.Button();
            this.ButtonReport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.materialTable1 = new BeautySalon.Components.MaterialTable();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 100);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ButtonInsert, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonDelete, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonUpdate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ButtonReport, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ButtonInsert
            // 
            this.ButtonInsert.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonInsert.FlatAppearance.BorderSize = 0;
            this.ButtonInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonInsert.Location = new System.Drawing.Point(20, 10);
            this.ButtonInsert.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.ButtonInsert.Name = "ButtonInsert";
            this.ButtonInsert.Size = new System.Drawing.Size(269, 30);
            this.ButtonInsert.TabIndex = 0;
            this.ButtonInsert.Text = "Добавить";
            this.ButtonInsert.UseVisualStyleBackColor = false;
            this.ButtonInsert.Click += new System.EventHandler(this.ButtonInsert_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonDelete.FlatAppearance.BorderSize = 0;
            this.ButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDelete.Location = new System.Drawing.Point(329, 10);
            this.ButtonDelete.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(270, 30);
            this.ButtonDelete.TabIndex = 1;
            this.ButtonDelete.Text = "Удалить";
            this.ButtonDelete.UseVisualStyleBackColor = false;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonUpdate.FlatAppearance.BorderSize = 0;
            this.ButtonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUpdate.Location = new System.Drawing.Point(20, 60);
            this.ButtonUpdate.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(269, 30);
            this.ButtonUpdate.TabIndex = 2;
            this.ButtonUpdate.Text = "Изменить";
            this.ButtonUpdate.UseVisualStyleBackColor = false;
            this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // ButtonReport
            // 
            this.ButtonReport.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonReport.FlatAppearance.BorderSize = 0;
            this.ButtonReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonReport.Location = new System.Drawing.Point(329, 60);
            this.ButtonReport.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.ButtonReport.Name = "ButtonReport";
            this.ButtonReport.Size = new System.Drawing.Size(270, 30);
            this.ButtonReport.TabIndex = 4;
            this.ButtonReport.Text = "Отчетность";
            this.ButtonReport.UseVisualStyleBackColor = false;
            this.ButtonReport.Click += new System.EventHandler(this.ButtonReport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.materialTable1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 369);
            this.panel2.TabIndex = 2;
            // 
            // materialTable1
            // 
            this.materialTable1.BackColor = System.Drawing.Color.White;
            this.materialTable1.CellMaximumSize = new System.Drawing.Size(0, 0);
            this.materialTable1.CellMinimumSize = new System.Drawing.Size(0, 0);
            this.materialTable1.CellPadding = new System.Windows.Forms.Padding(8);
            this.materialTable1.ColorBorder = System.Drawing.Color.Gray;
            this.materialTable1.ColorHeader = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.materialTable1.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.materialTable1.ColorSelect = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(201)))), ((int)(((byte)(255)))));
            this.materialTable1.ColorTextHeader = System.Drawing.Color.White;
            this.materialTable1.ColumnWeights = new int[] {
        0};
            this.materialTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTable1.ForeColor = System.Drawing.Color.Black;
            this.materialTable1.Location = new System.Drawing.Point(0, 0);
            this.materialTable1.Name = "materialTable1";
            this.materialTable1.Size = new System.Drawing.Size(619, 87);
            this.materialTable1.Style = BeautySalon.Components.MaterialTable.TableStyle.Flat;
            this.materialTable1.TabIndex = 1;
            this.materialTable1.TableData = ((System.Collections.Generic.List<System.Collections.Generic.List<string>>)(resources.GetObject("materialTable1.TableData")));
            this.materialTable1.TableHeaders = ((System.Collections.Generic.List<string>)(resources.GetObject("materialTable1.TableHeaders")));
            this.materialTable1.Text = "materialTable1";
            // 
            // PageStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PageStaff";
            this.Size = new System.Drawing.Size(619, 469);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ButtonInsert;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.Button ButtonUpdate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ButtonReport;
        private Components.MaterialTable materialTable1;
    }
}
