using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BeautySalon.DB.DAO
{
    internal class MaterialConsumptionDAO : DAO
    {
        public MaterialConsumptionDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE `materials_consumption` (
                    `order_id` int(11) NOT NULL,
                    `material_id` int(11) NOT NULL,
                    `price` float NOT NULL,
                    `amount` int(11) NOT NULL,
                    PRIMARY KEY (`order_id`, `material_id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<MaterialConsumption>> GetByOrderId(int orderId)
        {
            List<MaterialConsumption> list = new List<MaterialConsumption>();
            const string query = @"
                SELECT
                    `material_id`, `price`, `amount`
                FROM `materials_consumption`
                WHERE `order_id` = @order_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new MaterialConsumption(
                            reader.GetInt32(0),
                            reader.GetFloat(1),
                            reader.GetInt32(2)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task Insert(
            int orderId,
            int materialId,
            float price,
            int amount
        )
        {
            const string query = @"
                INSERT INTO `materials_consumption` (
                    `order_id`, `material_id`, `price`, `amount`
                ) VALUES (
                    @order_id, @material_id, @price, @amount
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@service_id", materialId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@amount", amount);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(int orderId, MaterialConsumption m)
        {
            await Insert(
                orderId,
                m.MaterialId,
                m.Price,
                m.Amount
            );
        }

        public async Task Update(
            int orderId,
            int materialId,
            float price,
            int amount,
            int oldOrderId,
            int oldMaterialId
        )
        {
            const string query = @"
                UPDATE `materials_consumption` SET
                    `order_id` = @order_id,
                    `material_id` = @material_id,
                    `price` = @price,
                    `amount` = @amount
                WHERE `order_id` = @old_order_id AND `material_id` = @old_material_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@material_id", materialId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@old_order_id", oldOrderId);
                command.Parameters.AddWithValue("@old_material_id", oldMaterialId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int orderId,
            int materialId,
            float price,
            int amount
        )
        {
            await Update(orderId, materialId, price, amount, orderId, materialId);
        }

        public async Task Update(int orderId, MaterialConsumption m, int oldOrderId, int oldServiceId)
        {
            await Update(orderId, m.MaterialId, m.Price, m.Amount, oldOrderId, oldServiceId);
        }

        public async Task Update(int orderId, MaterialConsumption m)
        {
            await Update(orderId, m, orderId, m.MaterialId);
        }

        public async Task Delete(int orderId, int materialId)
        {
            const string query = "DELETE FROM `materials_consumption` WHERE `order_id` = @order_id AND `material_id` = @material_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@material_id", materialId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteByOrderId(int orderId)
        {
            const string query = "DELETE FROM `materials_consumption` WHERE `order_id` = @order_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@order_id", orderId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
