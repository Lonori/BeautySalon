
namespace BeautySalon
{
    partial class PageStorage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageStorage));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkAllMat = new System.Windows.Forms.CheckBox();
            this.viewTableData = new BeautySalon.Components.MaterialTable();
            this.themeProvider1 = new BeautySalon.Components.Themes.ThemeProvider();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.viewTableData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 436);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.checkAllMat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 33);
            this.panel1.TabIndex = 2;
            // 
            // checkAllMat
            // 
            this.checkAllMat.AutoSize = true;
            this.checkAllMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkAllMat.Location = new System.Drawing.Point(0, 0);
            this.checkAllMat.Margin = new System.Windows.Forms.Padding(0);
            this.checkAllMat.Name = "checkAllMat";
            this.checkAllMat.Padding = new System.Windows.Forms.Padding(8);
            this.checkAllMat.Size = new System.Drawing.Size(619, 33);
            this.checkAllMat.TabIndex = 1;
            this.checkAllMat.Text = "Отображать все материалы";
            this.checkAllMat.UseVisualStyleBackColor = true;
            this.checkAllMat.CheckedChanged += new System.EventHandler(this.checkAllMat_CheckedChanged);
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
            this.viewTableData.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewTableData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.viewTableData.ForeColor = System.Drawing.Color.Black;
            this.viewTableData.Location = new System.Drawing.Point(0, 0);
            this.viewTableData.Margin = new System.Windows.Forms.Padding(0);
            this.viewTableData.Name = "viewTableData";
            this.viewTableData.SelectedRow = -1;
            this.viewTableData.Size = new System.Drawing.Size(619, 88);
            this.viewTableData.TabIndex = 0;
            this.viewTableData.TableData = ((System.Collections.Generic.List<System.Collections.Generic.List<string>>)(resources.GetObject("viewTableData.TableData")));
            this.viewTableData.TableHeaders = ((System.Collections.Generic.List<string>)(resources.GetObject("viewTableData.TableHeaders")));
            this.viewTableData.Text = "materialTable1";
            this.themeProvider1.SetUseTheme(this.viewTableData, true);
            // 
            // PageStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PageStorage";
            this.Size = new System.Drawing.Size(619, 469);
            this.themeProvider1.SetUseTheme(this, true);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkAllMat;
        private Components.MaterialTable viewTableData;
        private Components.Themes.ThemeProvider themeProvider1;
    }
}
