using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class StaffDAO : DAO, IDAO<int, Staff>
    {
        public StaffDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `staff` (
                    `id` int(11) NOT NULL,
                    `full_name` varchar(255) NOT NULL,
                    `phone_number` varchar(12) NOT NULL,
                    `birthday` date NOT NULL,
                    `gender` varchar(1) NOT NULL,
                    `date_join` date DEFAULT NULL,
                    `date_leave` date DEFAULT NULL,
                    `position` varchar(100) NOT NULL,
                    PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Staff>> GetAll()
        {
            List<Staff> list = new List<Staff>();
            const string query = @"
                SELECT
                    `id`, `full_name`, `phone_number`, `birthday`, `gender`, `date_join`, `date_leave`, `position`
                FROM `staff`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Staff(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                            reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                            reader.GetString(7)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task<Staff> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `full_name`, `phone_number`, `birthday`, `gender`, `date_join`, `date_leave`, `position`
                FROM `staff`
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
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                            reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                            reader.GetString(7)
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<int> GetNewId()
        {
            const string query = "SELECT MAX(`id`) FROM `staff` WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return reader.IsDBNull(0) ? 1 : reader.GetInt32(0) + 1;
                    }

                    return 1;
                }
            }
        }

        public async Task Insert(
            int id,
            string fullName,
            string phoneNumber,
            DateTime birthday,
            string gender,
            DateTime dateJoin,
            DateTime dateLeave,
            string position
        )
        {
            const string query = @"
                INSERT INTO `staff` (
                    `id`, `full_name`, `phone_number`, `birthday`, `gender`, `date_join`, `date_leave`, `position`
                ) VALUES (
                    @id, @full_name, @phone_number, @birthday, @gender, @date_join, @date_leave, @position
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@position", position);
                if (birthday == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@birthday", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@birthday", birthday);
                }
                if (dateJoin == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@date_join", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_join", dateJoin);
                }
                if (dateLeave == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@date_leave", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_leave", dateLeave);
                }

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(Staff m)
        {
            await Insert(
                m.Id,
                m.FullName,
                m.PhoneNumber,
                m.Birthday,
                m.Gender,
                m.DateJoin,
                m.DateLeave,
                m.Position
            );
        }

        public async Task Update(
            int id,
            string fullName,
            string phoneNumber,
            DateTime birthday,
            string gender,
            DateTime dateJoin,
            DateTime dateLeave,
            string position,
            int oldId
        )
        {
            const string query = @"
                UPDATE `staff` SET
                    `id` = @id,
                    `full_name` = @full_name,
                    `phone_number` = @phone_number,
                    `birthday` = @birthday,
                    `gender` = @gender,
                    `date_join` = @date_join,
                    `date_leave` = @date_leave,
                    `position` = @position
                WHERE `id` = @old_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@old_id", oldId);
                if (birthday == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@birthday", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@birthday", birthday);
                }
                if (dateJoin == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@date_join", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_join", dateJoin);
                }
                if (dateLeave == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@date_leave", null);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_leave", dateLeave);
                }

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int id,
            string fullName,
            string phoneNumber,
            DateTime birthday,
            string gender,
            DateTime dateJoin,
            DateTime dateLeave,
            string position
        )
        {
            await Update(id, fullName, phoneNumber, birthday, gender, dateJoin, dateLeave, position, id);
        }

        public async Task Update(Staff m, int oldId)
        {
            await Update(m.Id, m.FullName, m.PhoneNumber, m.Birthday, m.Gender, m.DateJoin, m.DateLeave, m.Position, oldId);
        }

        public async Task Update(Staff m)
        {
            await Update(m, m.Id);
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
