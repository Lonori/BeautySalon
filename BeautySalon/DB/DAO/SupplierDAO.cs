using BeautySalon.DB.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class SupplierDAO : DAO, IDAO<int, Supplier>
    {
        public SupplierDAO(MySqlConnection connection) : base(connection) { }

        protected override void InitializeTable()
        {
            const string query = @"
                CREATE TABLE IF NOT EXISTS `suppliers` (
                    `id` int(11) NOT NULL,
                    `name` varchar(255) NOT NULL,
                    `address` varchar(255) NOT NULL,
                    `inn` varchar(255) NOT NULL,
                    `kpp` varchar(255) NOT NULL,
                    PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Supplier>> GetAll()
        {
            List<Supplier> list = new List<Supplier>();
            const string query = @"
                SELECT
                    `id`, `name`, `address`, `inn`, `kpp`
                FROM `suppliers`
                ORDER BY `name`";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Supplier(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        ));
                    }

                    return list;
                }
            }
        }

        public async Task<Supplier> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `name`, `address`, `inn`, `kpp`
                FROM `suppliers`
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Supplier(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4)
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<int> GetNewId()
        {
            const string query = "SELECT MAX(`id`) FROM `suppliers` WHERE 1";

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
            string address,
            string inn,
            string kpp
        )
        {
            const string query = @"
                INSERT INTO `suppliers` (
                    `id`, `name`, `address`, `inn`, `kpp`
                ) VALUES (
                    @id, @name, @address, @inn, @kpp
                )";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@inn", inn);
                command.Parameters.AddWithValue("@kpp", kpp);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Insert(Supplier m)
        {
            await Insert(
                m.Id,
                m.Name,
                m.Address,
                m.INN,
                m.KPP
            );
        }

        public async Task Update(
            int id,
            string name,
            string address,
            string inn,
            string kpp,
            int oldId
        )
        {
            const string query = @"
                UPDATE `suppliers` SET
                    `id` = @id,
                    `name` = @name,
                    `address` = @address,
                    `inn` = @inn,
                    `kpp` = @kpp
                WHERE `id` = @old_id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@inn", inn);
                command.Parameters.AddWithValue("@kpp", kpp);
                command.Parameters.AddWithValue("@old_id", oldId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(
            int id,
            string name,
            string address,
            string inn,
            string kpp
        )
        {
            await Update(id, name, address, inn, kpp, id);
        }

        public async Task Update(Supplier m, int oldId)
        {
            await Update(m.Id, m.Name, m.Address, m.INN, m.KPP, oldId);
        }

        public async Task Update(Supplier m)
        {
            await Update(m, m.Id);
        }

        public async Task Delete(int id)
        {
            const string query = "DELETE FROM `suppliers` WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
