using BeautySalon.DB.Entities;
using BeautySalon.DB.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class StaffDAO : DAO, IStaffDAO
    {
        public StaffDAO(MySqlConnection connection) : base(connection)
        {
        }

        protected override void InitializeTable()
        {
            const string query = @"
            CREATE TABLE IF NOT EXISTS staff (
                id INT AUTO_INCREMENT PRIMARY KEY,
                full_name VARCHAR(255) NOT NULL,
                birthday DATE NOT NULL,
                date_join DATE NULL,
                date_leave DATE NULL,
                position VARCHAR(100) NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Staff>> GetAll()
        {
            List<Staff> staffs = new List<Staff>();
            const string query = @"
                SELECT
                    `id`, `full_name`, `birthday`, `date_join`, `date_leave`, `position`
                FROM `staff`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        staffs.Add(new Staff(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDateTime(2),
                            reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                            reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                            reader.GetString(5)
                        ));
                    }

                    return staffs;
                }
            }
        }

        public async Task<Staff> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `full_name`, `birthday`, `date_join`, `date_leave`, `position`
                FROM `staff
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Staff(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDateTime(2),
                            reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                            reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                            reader.GetString(5)
                        );
                    }

                    return null;
                }
            }
        }

        public async Task Insert(int id, string fullName, DateTime birthday, DateTime dateJoin, DateTime dateLeave, string position)
        {
            const string query = @"
                INSERT INTO `staff` (
                    `id`, `full_name`, `birthday`, `date_join`, `date_leave`, `position`
                ) VALUES (
                    @id, @full_name, @birthday, @date_join, @date_leave, @position
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@birthday", birthday);
                command.Parameters.AddWithValue("@date_join", dateJoin);
                command.Parameters.AddWithValue("@date_leave", dateLeave);
                command.Parameters.AddWithValue("@position", position);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(Staff staff)
        {
            await Insert(staff.Id, staff.FullName, staff.Birthday, staff.DateJoin, staff.DateLeave, staff.Position);
        }

        public async Task Update(int id, string fullName, DateTime birthday, DateTime dateJoin, DateTime dateLeave, string position)
        {
            const string query = @"
                UPDATE `staff` SET
                    `full_name` = @full_name,
                    `birthday` = @birthday,
                    `date_join` = @date_join,
                    `date_leave` = @date_leave,
                    `position` = @position
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@birthday", birthday);
                command.Parameters.AddWithValue("@date_join", dateJoin);
                command.Parameters.AddWithValue("@date_leave", dateLeave);
                command.Parameters.AddWithValue("@position", position);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(Staff staff)
        {
            await Update(staff.Id, staff.FullName, staff.Birthday, staff.DateJoin, staff.DateLeave, staff.Position);
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM `staff` WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
