using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class OrderDAO : DAO, IDAO<int, Order>
    {
        public OrderDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `orders` (
                    id INT NOT NULL,
                    time DATETIME NOT NULL,
                    full_name VARCHAR(255) NOT NULL,
                    phone_number VARCHAR(12) NOT NULL,
                    staff_id INT NOT NULL,
                    remark VARCHAR(255) NULL,
                    status INT NOT NULL,
                    PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Order>> GetAll()
        {
            List<Order> list = new List<Order>();
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `orders`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Order(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            Enum.IsDefined(typeof(Order.OrderStatus), reader.GetInt32(6)) ? (Order.OrderStatus)reader.GetInt32(6) : Order.OrderStatus.Unknown
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task<Order> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `orders`
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Order(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            Enum.IsDefined(typeof(Order.OrderStatus), reader.GetInt32(6)) ? (Order.OrderStatus)reader.GetInt32(6) : Order.OrderStatus.Unknown
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<int> GetNewId()
        {
            const string query = "SELECT MAX(`id`) FROM `orders` WHERE 1";

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

        public async Task<List<Order>> GetByPeriod(DateTime timeStart, DateTime timeEnd)
        {
            List<Order> appointments = new List<Order>();
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `orders`
                WHERE `time` >= @timeStart AND `time` < @timeEnd
                ORDER BY `time`";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@timeStart", timeStart);
                command.Parameters.AddWithValue("@timeEnd", timeEnd);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appointments.Add(new Order(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Enum.IsDefined(typeof(Order.OrderStatus), reader.GetInt32(6)) ? (Order.OrderStatus)reader.GetInt32(6) : Order.OrderStatus.Unknown
                        ));
                    }

                    return appointments;
                }
            }
        }

        public async Task<List<Order>> GetByPeriodAndStatus(DateTime timeStart, DateTime timeEnd, Order.OrderStatus status)
        {
            List<Order> appointments = new List<Order>();
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `orders`
                WHERE `time` >= @timeStart AND `time` < @timeEnd AND `status` = @status
                ORDER BY `time`";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@timeStart", timeStart);
                command.Parameters.AddWithValue("@timeEnd", timeEnd);
                command.Parameters.AddWithValue("@status", (int)status);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appointments.Add(new Order(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Enum.IsDefined(typeof(Order.OrderStatus), reader.GetInt32(6)) ? (Order.OrderStatus)reader.GetInt32(6) : Order.OrderStatus.Unknown
                        ));
                    }

                    return appointments;
                }
            }
        }

        public async Task Insert(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Order.OrderStatus status)
        {
            const string query = @"
                INSERT INTO `orders` (
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                ) VALUES (
                    @id, @time, @full_name, @phone_number, @staff_id, @remark, @status
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@staff_id", staffId);
                command.Parameters.AddWithValue("@remark", remark);
                command.Parameters.AddWithValue("@status", (int)status);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(Order m)
        {
            await Insert(
                m.Id,
                m.Time,
                m.FullName,
                m.PhoneNumber,
                m.StaffId,
                m.Remark,
                m.Status
            );
        }

        public async Task Update(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Order.OrderStatus status)
        {
            const string query = @"
                UPDATE `orders` SET
                    `time` = @time,
                    `full_name` = @full_name,
                    `phone_number` = @phone_number,
                    `staff_id` = @staff_id,
                    `remark` = @remark,
                    `status` = @status
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@full_name", fullName);
                command.Parameters.AddWithValue("@phone_number", phoneNumber);
                command.Parameters.AddWithValue("@staff_id", staffId);
                command.Parameters.AddWithValue("@remark", remark);
                command.Parameters.AddWithValue("@status", (int)status);

                await command.ExecuteNonQueryAsync();
            }
        }

        public Task Update(Order m, int oldId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Order m)
        {
            await Update(
                m.Id,
                m.Time,
                m.FullName,
                m.PhoneNumber,
                m.StaffId,
                m.Remark,
                m.Status
            );
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM `orders` WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
