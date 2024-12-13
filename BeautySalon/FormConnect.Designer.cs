namespace BeautySalon
{
    partial class FormConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnect));
            this.label_host = new System.Windows.Forms.Label();
            this.label_user = new System.Windows.Forms.Label();
            this.label_pass = new System.Windows.Forms.Label();
            this.input_host = new System.Windows.Forms.TextBox();
            this.input_user = new System.Windows.Forms.TextBox();
            this.input_pass = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.save_login_check = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonConnect = new BeautySalon.Components.MaterialButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_host
            // 
            this.label_host.AutoSize = true;
            this.label_host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_host.Location = new System.Drawing.Point(0, 0);
            this.label_host.Margin = new System.Windows.Forms.Padding(0);
            this.label_host.Name = "label_host";
            this.label_host.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label_host.Size = new System.Drawing.Size(134, 36);
            this.label_host.TabIndex = 0;
            this.label_host.Text = "Хост:";
            this.label_host.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_user.Location = new System.Drawing.Point(0, 36);
            this.label_user.Margin = new System.Windows.Forms.Padding(0);
            this.label_user.Name = "label_user";
            this.label_user.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label_user.Size = new System.Drawing.Size(134, 36);
            this.label_user.TabIndex = 0;
            this.label_user.Text = "Пользователь:";
            this.label_user.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_pass
            // 
            this.label_pass.AutoSize = true;
            this.label_pass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_pass.Location = new System.Drawing.Point(0, 72);
            this.label_pass.Margin = new System.Windows.Forms.Padding(0);
            this.label_pass.Name = "label_pass";
            this.label_pass.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label_pass.Size = new System.Drawing.Size(134, 36);
            this.label_pass.TabIndex = 1;
            this.label_pass.Text = "Пароль:";
            this.label_pass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // input_host
            // 
            this.input_host.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.input_host.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_host.Location = new System.Drawing.Point(137, 4);
            this.input_host.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.input_host.MaxLength = 100;
            this.input_host.Name = "input_host";
            this.input_host.Size = new System.Drawing.Size(244, 29);
            this.input_host.TabIndex = 2;
            // 
            // input_user
            // 
            this.input_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.input_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_user.Location = new System.Drawing.Point(137, 40);
            this.input_user.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.input_user.MaxLength = 100;
            this.input_user.Name = "input_user";
            this.input_user.Size = new System.Drawing.Size(244, 29);
            this.input_user.TabIndex = 2;
            // 
            // input_pass
            // 
            this.input_pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input_pass.Location = new System.Drawing.Point(0, 1);
            this.input_pass.Margin = new System.Windows.Forms.Padding(0);
            this.input_pass.MaxLength = 100;
            this.input_pass.Name = "input_pass";
            this.input_pass.Size = new System.Drawing.Size(200, 22);
            this.input_pass.TabIndex = 2;
            this.input_pass.UseSystemPasswordChar = true;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label_status, 2);
            this.label_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_status.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(0, 136);
            this.label_status.Margin = new System.Windows.Forms.Padding(0);
            this.label_status.MaximumSize = new System.Drawing.Size(383, 0);
            this.label_status.Name = "label_status";
            this.label_status.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label_status.Size = new System.Drawing.Size(383, 0);
            this.label_status.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.label_host, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_user, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_pass, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.input_user, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.input_host, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_status, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.save_login_check, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonConnect, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 261);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // save_login_check
            // 
            this.save_login_check.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.save_login_check, 2);
            this.save_login_check.Dock = System.Windows.Forms.DockStyle.Right;
            this.save_login_check.Location = new System.Drawing.Point(212, 108);
            this.save_login_check.Margin = new System.Windows.Forms.Padding(0);
            this.save_login_check.Name = "save_login_check";
            this.save_login_check.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.save_login_check.Size = new System.Drawing.Size(172, 28);
            this.save_login_check.TabIndex = 5;
            this.save_login_check.Text = "Запомнить данные";
            this.save_login_check.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.input_pass);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(139, 75);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 29);
            this.panel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(214, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 27);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(69)))), ((int)(((byte)(136)))));
            this.tableLayoutPanel1.SetColumnSpan(this.buttonConnect, 2);
            this.buttonConnect.CornerRadius = 8;
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.buttonConnect.Location = new System.Drawing.Point(140, 217);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(6);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(238, 38);
            this.buttonConnect.Style = BeautySalon.Components.MaterialButton.ButtonStyle.Flat;
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Подключиться";
            this.buttonConnect.Click += new System.EventHandler(this.connect_Click);
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FormConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB Viewer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_host;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.Label label_pass;
        private System.Windows.Forms.TextBox input_host;
        private System.Windows.Forms.TextBox input_user;
        private System.Windows.Forms.TextBox input_pass;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox save_login_check;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private Components.MaterialButton buttonConnect;
    }
}