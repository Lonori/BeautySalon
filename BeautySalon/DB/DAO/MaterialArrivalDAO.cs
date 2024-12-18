using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class MaterialArrivalDAO : DAO
    {
        public MaterialArrivalDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `materials_arrival` (
                    `contract_id` int(11) NOT NULL,
                    `material_id` int(11) NOT NULL,
                    `price` float NOT NULL,
                    `amount` int(11) NOT NULL,
                    PRIMARY KEY (`contract_id`, `material_id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<MaterialArrival>> GetByContractId(int contractId)
        {
            List<MaterialArrival> list = new List<MaterialArrival>();
            const string query = @"
                SELECT
                    `contract_id`, `material_id`, `price`, `amount`
                FROM `materials_arrival`
                WHERE `contract_id` = @contract_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@contract_id", contractId);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new MaterialArrival(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetFloat(2),
                            reader.GetInt32(3)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task Insert(
            int contractId,
            int materialId,
            float price,
            int amount
        )
        {
            const string query = @"
                INSERT INTO `materials_arrival` (
                    `contract_id`, `material_id`, `price`, `amount`
                ) VALUES (
                    @contract_id, @material_id, @price, @amount
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@contract_id", contractId);
                command.Parameters.AddWithValue("@material_id", materialId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@amount", amount);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(MaterialArrival m)
        {
            await Insert(
                m.ContractId,
                m.MaterialId,
                m.Price,
                m.Amount
            );
        }

        public async Task Update(
            int contractId,
            int materialId,
            float price,
            int amount,
            int oldContractId,
            int oldMaterialId
        )
        {
            const string query = @"
                UPDATE `materials_arrival` SET
                    `contract_id` = @contract_id,
                    `material_id` = @material_id,
                    `price` = @price,
                    `amount` = @amount
                WHERE `contract_id` = @old_contract_id AND `material_id` = @old_material_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@contract_id", contractId);
                command.Parameters.AddWithValue("@material_id", materialId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@old_contract_id", oldContractId);
                command.Parameters.AddWithValue("@old_material_id", oldMaterialId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int contractId,
            int materialId,
            float price,
            int amount
        )
        {
            await Update(contractId, materialId, price, amount, contractId, materialId);
        }

        public async Task Update(MaterialArrival m, int oldContractId, int oldServiceId)
        {
            await Update(m.ContractId, m.MaterialId, m.Price, m.Amount, oldContractId, oldServiceId);
        }

        public async Task Update(MaterialArrival m)
        {
            await Update(m, m.ContractId, m.MaterialId);
        }

        public async Task Delete(int contractId, int materialId)
        {
            const string query = "DELETE FROM `materials_arrival` WHERE `contract_id` = @contract_id AND `material_id` = @material_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@contract_id", contractId);
                command.Parameters.AddWithValue("@material_id", materialId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteByContractId(int contractId)
        {
            const string query = "DELETE FROM `materials_arrival` WHERE `contract_id` = @contract_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@contract_id", contractId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
