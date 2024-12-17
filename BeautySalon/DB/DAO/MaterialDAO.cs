using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class MaterialDAO : DAO, IDAO<int, Material>
    {
        public MaterialDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `materials` (
                    `id` int(11) NOT NULL,
                    `name` varchar(255) NOT NULL,
                    `price` float NOT NULL,
                    PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Material>> GetAll()
        {
            List<Material> list = new List<Material>();
            const string query = @"
                SELECT
                    `id`, `name`, `price`
                FROM `materials`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Material(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetFloat(2)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task<Material> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `name`, `price`
                FROM `materials`
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Material(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetFloat(2)
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<int> GetNewId()
        {
            const string query = "SELECT MAX(`id`) FROM `materials` WHERE 1";

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
            string name,
            float price
        )
        {
            const string query = @"
                INSERT INTO `materials` (
                    `id`, `name`, `price`
                ) VALUES (
                    @id, @name, @price
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(Material m)
        {
            await Insert(
                m.Id,
                m.Name,
                m.Price
            );
        }

        public async Task Update(
            int id,
            string name,
            float price,
            int oldId
        )
        {
            const string query = @"
                UPDATE `materials` SET
                    `id` = @id,
                    `name` = @name,
                    `price` = @price
                WHERE `id` = @old_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@old_id", oldId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int id,
            string name,
            float price
        )
        {
            await Update(id, name, price, id);
        }

        public async Task Update(Material m, int oldId)
        {
            await Update(m.Id, m.Name, m.Price, oldId);
        }

        public async Task Update(Material m)
        {
            await Update(m, m.Id);
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM `materials` WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
