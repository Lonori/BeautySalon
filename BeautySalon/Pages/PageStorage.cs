using BeautySalon.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BeautySalon
{
    public partial class PageStorage : UserControl
    {
        private readonly AppDatabase _DB;

        public PageStorage()
        {
            _DB = AppDatabase.GetInstance();
            InitializeComponent();

            viewTableData.TableHeaders = new List<string> { "ID", "Материал", "Цена", "Кол-во" };
            viewTableData.TableWeights = new int[] { 0, 2 };
            viewTableData.Clear();

            UpdateTable();
        }
        private async void UpdateTable()
        {
            viewTableData.Clear();

            List<List<string>> tableData = new List<List<string>>();
            const string query = @"
                SELECT
                    `materials`.`id`, `materials`.`name`, `materials`.`price`, IFNULL(`MA`.`amount`, 0), IFNULL(`MC`.`amount`, 0)
                FROM
                    `materials`
                LEFT JOIN (SELECT `material_id`, SUM(`amount`) AS `amount` FROM `materials_arrival` GROUP BY `material_id`) as `MA` ON `materials`.`id` = `MA`.`material_id`
                LEFT JOIN (SELECT `material_id`, SUM(`amount`) AS `amount` FROM `materials_consumption` GROUP BY `material_id`) as `MC` ON `materials`.`id` = `MC`.`material_id`
                ORDER BY `materials`.`id`";

            using (MySqlCommand command = new MySqlCommand(query, _DB.Connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int arrival = reader.GetInt32(3);
                        int consumption = reader.GetInt32(4);
                        int amount = arrival - consumption;

                        if (!checkAllMat.Checked && amount <= 0) continue;

                        tableData.Add(new List<string>
                        {
                            reader.GetInt32(0).ToString(),
                            reader.GetString(1),
                            reader.GetFloat(2).ToString(),
                            amount.ToString(),
                        });
                    }
                }
            }

            viewTableData.TableData = tableData;
        }

        private void checkAllMat_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }
    }
}
