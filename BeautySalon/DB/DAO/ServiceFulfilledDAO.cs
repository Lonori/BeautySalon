using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class ServiceFulfilledDAO : DAO
    {
        public ServiceFulfilledDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE `services_fulfilled` (
                    `order_id` int(11) NOT NULL,
                    `service_id` int(11) NOT NULL,
                    `price` float NOT NULL,
                    PRIMARY KEY (`order_id`, `service_id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<ServiceFulfilled>> GetByOrderId(int orderId)
        {
            List<ServiceFulfilled> list = new List<ServiceFulfilled>();
            const string query = @"
                SELECT
                    `service_id`, `price`
                FROM `services_fulfilled`
                WHERE `order_id` = @order_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ServiceFulfilled(
                            reader.GetInt32(0),
                            reader.GetFloat(1)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task Insert(
            int orderId,
            int serviceId,
            float price
        )
        {
            const string query = @"
                INSERT INTO `services_fulfilled` (
                    `order_id`, `service_id`, `price`
                ) VALUES (
                    @order_id, @service_id, @price
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@service_id", serviceId);
                command.Parameters.AddWithValue("@price", price);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(int orderId, ServiceFulfilled m)
        {
            await Insert(
                orderId,
                m.ServiceId,
                m.Price
            );
        }

        public async Task Update(
            int orderId,
            int serviceId,
            float price,
            int oldOrderId,
            int oldServiceId
        )
        {
            const string query = @"
                UPDATE `services_fulfilled` SET
                    `order_id` = @order_id,
                    `service_id` = @service_id,
                    `price` = @price
                WHERE `order_id` = @old_order_id AND `service_id` = @old_service_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@service_id", serviceId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@old_order_id", oldOrderId);
                command.Parameters.AddWithValue("@old_service_id", oldServiceId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int orderId,
            int serviceId,
            float price
        )
        {
            await Update(orderId, serviceId, price, orderId, serviceId);
        }

        public async Task Update(int orderId, ServiceFulfilled m, int oldOrderId, int oldServiceId)
        {
            await Update(orderId, m.ServiceId, m.Price, oldOrderId, oldServiceId);
        }

        public async Task Update(int orderId, ServiceFulfilled m)
        {
            await Update(orderId, m, orderId, m.ServiceId);
        }

        public async Task Delete(int orderId, int serviceId)
        {
            const string query = "DELETE FROM `services_fulfilled` WHERE `order_id` = @order_id AND `service_id` = @service_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@service_id", serviceId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteByOrderId(int orderId)
        {
            const string query = "DELETE FROM `services_fulfilled` WHERE `order_id` = @order_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
