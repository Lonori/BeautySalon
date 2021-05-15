
namespace BeautySalon
{
    partial class FormTableEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTableEditor));
            this.TableFieldsBody = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonConfirm = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableFieldsBody
            // 
            this.TableFieldsBody.AutoSize = true;
            this.TableFieldsBody.ColumnCount = 1;
            this.TableFieldsBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableFieldsBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableFieldsBody.Location = new System.Drawing.Point(0, 0);
            this.TableFieldsBody.Margin = new System.Windows.Forms.Padding(0);
            this.TableFieldsBody.Name = "TableFieldsBody";
            this.TableFieldsBody.RowCount = 2;
            this.TableFieldsBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableFieldsBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableFieldsBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableFieldsBody.Size = new System.Drawing.Size(684, 0);
            this.TableFieldsBody.TabIndex = 0;
            // 
            // ButtonConfirm
            // 
            this.ButtonConfirm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonConfirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonConfirm.FlatAppearance.BorderSize = 0;
            this.ButtonConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonConfirm.Location = new System.Drawing.Point(0, 421);
            this.ButtonConfirm.Name = "ButtonConfirm";
            this.ButtonConfirm.Size = new System.Drawing.Size(684, 40);
            this.ButtonConfirm.TabIndex = 1;
            this.ButtonConfirm.Text = "Подтвердить";
            this.ButtonConfirm.UseVisualStyleBackColor = false;
            this.ButtonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.Controls.Add(this.TableFieldsBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(684, 421);
            this.panelBody.TabIndex = 2;
            // 
            // FormTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.ButtonConfirm);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormTableEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор записей";
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableFieldsBody;
        private System.Windows.Forms.Button ButtonConfirm;
        private System.Windows.Forms.Panel panelBody;
    }
}