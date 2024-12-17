using MySql.Data.MySqlClient;

namespace BeautySalon.DB.DAO
{
    internal abstract class DAO
    {
        protected readonly MySqlConnection _connection;

        public DAO(MySqlConnection connection)
        {
            _connection = connection;
            InitializeTable();
        }

        protected abstract void InitializeTable();
    }
}
