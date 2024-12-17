
namespace BeautySalon
{
    partial class FormEntityEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntityEditor));
            this.TableFieldsBody = new System.Windows.Forms.TableLayoutPanel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.themeProvider1 = new BeautySalon.Components.Themes.ThemeProvider();
            this.ButtonConfirm = new BeautySalon.Components.MaterialButton();
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
            this.TableFieldsBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.TableFieldsBody.Size = new System.Drawing.Size(684, 0);
            this.TableFieldsBody.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.Controls.Add(this.TableFieldsBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(684, 425);
            this.panelBody.TabIndex = 2;
            // 
            // ButtonConfirm
            // 
            this.ButtonConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.ButtonConfirm.CornerRadius = 0;
            this.ButtonConfirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonConfirm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonConfirm.ForeColor = System.Drawing.Color.White;
            this.ButtonConfirm.Location = new System.Drawing.Point(0, 425);
            this.ButtonConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonConfirm.Name = "ButtonConfirm";
            this.ButtonConfirm.Size = new System.Drawing.Size(684, 36);
            this.ButtonConfirm.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.ButtonConfirm.TabIndex = 1;
            this.ButtonConfirm.Text = "Подтвердить";
            this.themeProvider1.SetUseTheme(this.ButtonConfirm, true);
            this.ButtonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // FormEntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.ButtonConfirm);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormEntityEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор записей";
            this.themeProvider1.SetUseTheme(this, true);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableFieldsBody;
        private System.Windows.Forms.Panel panelBody;
        private Components.Themes.ThemeProvider themeProvider1;
        private Components.MaterialButton ButtonConfirm;
    }
}