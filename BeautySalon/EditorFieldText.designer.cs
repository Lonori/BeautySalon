﻿
namespace BeautySalon
{
    partial class EditorFieldText
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
            this.fieldBody = new System.Windows.Forms.TableLayoutPanel();
            this.fieldName = new System.Windows.Forms.Label();
            this.fieldData = new System.Windows.Forms.TextBox();
            this.fieldBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // fieldBody
            // 
            this.fieldBody.AutoSize = true;
            this.fieldBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fieldBody.BackColor = System.Drawing.Color.Transparent;
            this.fieldBody.ColumnCount = 2;
            this.fieldBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.fieldBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.fieldBody.Controls.Add(this.fieldName, 0, 0);
            this.fieldBody.Controls.Add(this.fieldData, 1, 0);
            this.fieldBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.fieldBody.Location = new System.Drawing.Point(0, 0);
            this.fieldBody.Margin = new System.Windows.Forms.Padding(0);
            this.fieldBody.Name = "fieldBody";
            this.fieldBody.RowCount = 1;
            this.fieldBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fieldBody.Size = new System.Drawing.Size(151, 26);
            this.fieldBody.TabIndex = 0;
            // 
            // fieldName
            // 
            this.fieldName.AutoSize = true;
            this.fieldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldName.Location = new System.Drawing.Point(0, 0);
            this.fieldName.Margin = new System.Windows.Forms.Padding(0);
            this.fieldName.Name = "fieldName";
            this.fieldName.Padding = new System.Windows.Forms.Padding(6);
            this.fieldName.Size = new System.Drawing.Size(45, 25);
            this.fieldName.TabIndex = 0;
            this.fieldName.Text = "Поле";
            this.fieldName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldData
            // 
            this.fieldData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fieldData.Dock = System.Windows.Forms.DockStyle.Top;
            this.fieldData.Location = new System.Drawing.Point(48, 3);
            this.fieldData.Name = "fieldData";
            this.fieldData.Size = new System.Drawing.Size(100, 20);
            this.fieldData.TabIndex = 1;
            // 
            // EditorFieldText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fieldBody);
            this.Name = "EditorFieldText";
            this.Size = new System.Drawing.Size(151, 26);
            this.fieldBody.ResumeLayout(false);
            this.fieldBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel fieldBody;
        private System.Windows.Forms.Label fieldName;
        private System.Windows.Forms.TextBox fieldData;
    }
}
