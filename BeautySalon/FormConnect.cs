using BeautySalon.DB;
using System;
using System.IO;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class FormConnect : Form
    {
        public FormConnect()
        {
            InitializeComponent();
            try
            {
                using (StreamReader sr = new StreamReader("auth", System.Text.Encoding.UTF8))
                {
                    string line;
                    int coun = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        switch (coun)
                        {
                            case 1:
                                input_host.Text = line;
                                break;
                            case 2:
                                if (line != "3306") input_host.Text += ":" + line;
                                break;
                            case 3:
                                input_user.Text = line;
                                break;
                            case 4:
                                input_pass.Text = line;
                                break;
                        }
                        coun++;
                    }
                    save_login_check.Checked = true;
                    sr.Close();
                }
            }
            catch
            { }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            string host;
            string username;
            string password;
            int port;

            label_status.Text = "";
            buttonConnect.Enabled = false;
            Cursor = Cursors.WaitCursor;

            string[] host_arr = input_host.Text.Trim().Split(new char[] { ':' });
            switch (host_arr.Length)
            {
                case 1:
                    port = 3306;
                    break;
                case 2:
                    try
                    {
                        port = int.Parse(host_arr[1].Trim());
                    }
                    catch
                    {
                        label_status.Text = "Неверный формат порта сервера";
                        buttonConnect.Enabled = true;
                        Cursor = Cursors.Default;
                        return;
                    }
                    break;
                default:
                    label_status.Text = "Неверный формат адреса сервера";
                    buttonConnect.Enabled = true;
                    Cursor = Cursors.Default;
                    return;
            }
            host = host_arr[0].Trim();
            username = input_user.Text.Trim();
            password = input_pass.Text.Trim();

            if (save_login_check.Checked)
            {
                string auth_file_data = host + "\n" + port + "\n" + username + "\n" + password;
                try
                {
                    using (StreamWriter sw = new StreamWriter("auth", false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(auth_file_data);
                        sw.Close();
                    }
                }
                catch (Exception err)
                {
                    AlertBox.Error(err.Message);
                    buttonConnect.Enabled = true;
                    Cursor = Cursors.Default;
                    return;
                }
            }

            try
            {
                AppDatabase.Connect(host, username, password, port);
                Cursor = Cursors.Default;
                Close();
            }
            catch (Exception err)
            {
                label_status.Text = "Не удалось подключиться к базе данных:\n" + err.Message;
                buttonConnect.Enabled = true;
                Cursor = Cursors.Default;
                return;
            }
        }

        private void ButtonShow_MouseDown(object sender, MouseEventArgs e)
        {
            input_pass.UseSystemPasswordChar = false;
        }

        private void ButtonShow_MouseUp(object sender, MouseEventArgs e)
        {
            input_pass.UseSystemPasswordChar = true;
        }
    }
}
