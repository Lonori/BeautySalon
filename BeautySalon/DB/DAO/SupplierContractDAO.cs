using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class SupplierContractDAO : DAO
    {
        public SupplierContractDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `supplier_contracts` (
                    `id` int(11) NOT NULL,
                    `time` DATETIME NOT NULL,
                    `supplier_id` int(11) NOT NULL,
                    PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<SupplierContract>> GetAll()
        {
            List<SupplierContract> list = new List<SupplierContract>();
            const string query = @"
                SELECT
                    `id`, `time`, `supplier_id`
                FROM `supplier_contracts`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new SupplierContract(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetInt32(2)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task<SupplierContract> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `time`, `supplier_id`
                FROM `supplier_contracts`
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new SupplierContract(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetInt32(2)
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<int> GetNewId()
        {
            const string query = "SELECT MAX(`id`) FROM `supplier_contracts` WHERE 1";

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

        public async Task<List<SupplierContract>> GetByPeriod(DateTime timeStart, DateTime timeEnd)
        {
            List<SupplierContract> list = new List<SupplierContract>();
            const string query = @"
                SELECT
                    `id`, `time`, `supplier_id`
                FROM `supplier_contracts`
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
                        list.Add(new SupplierContract(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetInt32(2)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task Insert(
            int id,
            DateTime time,
            int supplierId
        )
        {
            const string query = @"
                INSERT INTO `supplier_contracts` (
                    `id`, `time`, `supplier_id`
                ) VALUES (
                    @id, @time, @supplier_id
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@supplier_id", supplierId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(SupplierContract m)
        {
            await Insert(
                m.Id,
                m.Time,
                m.SupplierId
            );
        }

        public async Task Update(
            int id,
            DateTime time,
            int supplierId,
            int oldId
        )
        {
            const string query = @"
                UPDATE `supplier_contracts` SET
                    `id` = @id,
                    `time` = @time,
                    `supplier_id` = @supplier_id
                WHERE `id` = @old_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@supplier_id", supplierId);
                command.Parameters.AddWithValue("@old_id", oldId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int id,
            DateTime time,
            int supplierId
        )
        {
            await Update(id, time, supplierId, id);
        }

        public async Task Update(SupplierContract m, int oldId)
        {
            await Update(m.Id, m.Time, m.SupplierId, oldId);
        }

        public async Task Update(SupplierContract m)
        {
            await Update(m, m.Id);
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM `supplier_contracts` WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
