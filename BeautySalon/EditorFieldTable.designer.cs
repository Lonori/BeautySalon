
namespace BeautySalon
{
    partial class EditorFieldTable
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
            this.TableName = new System.Windows.Forms.Label();
            this.ButtonContainer = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonInsert = new System.Windows.Forms.Button();
            this.ButtonUpdate = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.table1 = new BeautySalon.Table();
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
            this.ButtonContainer.Controls.Add(this.ButtonInsert, 0, 0);
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
            // ButtonInsert
            // 
            this.ButtonInsert.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonInsert.FlatAppearance.BorderSize = 0;
            this.ButtonInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonInsert.Location = new System.Drawing.Point(3, 3);
            this.ButtonInsert.Name = "ButtonInsert";
            this.ButtonInsert.Size = new System.Drawing.Size(127, 30);
            this.ButtonInsert.TabIndex = 0;
            this.ButtonInsert.Text = "Добавить";
            this.ButtonInsert.UseVisualStyleBackColor = false;
            this.ButtonInsert.Click += new System.EventHandler(this.ButtonInsert_Click);
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonUpdate.FlatAppearance.BorderSize = 0;
            this.ButtonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUpdate.Location = new System.Drawing.Point(136, 3);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(127, 30);
            this.ButtonUpdate.TabIndex = 1;
            this.ButtonUpdate.Text = "Изменить";
            this.ButtonUpdate.UseVisualStyleBackColor = false;
            this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonDelete.FlatAppearance.BorderSize = 0;
            this.ButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDelete.Location = new System.Drawing.Point(269, 3);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(128, 30);
            this.ButtonDelete.TabIndex = 2;
            this.ButtonDelete.Text = "Удалить";
            this.ButtonDelete.UseVisualStyleBackColor = false;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // table1
            // 
            this.table1.BackColor = System.Drawing.Color.White;
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.table1.HeaderHeight = 30;
            this.table1.Location = new System.Drawing.Point(0, 61);
            this.table1.Margin = new System.Windows.Forms.Padding(0);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(400, 89);
            this.table1.TabIndex = 2;
            // 
            // EditorFieldTable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.ButtonContainer);
            this.Controls.Add(this.TableName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "EditorFieldTable";
            this.Size = new System.Drawing.Size(400, 150);
            this.ButtonContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TableName;
        private System.Windows.Forms.TableLayoutPanel ButtonContainer;
        private Table table1;
        private System.Windows.Forms.Button ButtonInsert;
        private System.Windows.Forms.Button ButtonUpdate;
        private System.Windows.Forms.Button ButtonDelete;
    }
}
