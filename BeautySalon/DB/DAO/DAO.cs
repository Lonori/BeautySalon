using MySql.Data.MySqlClient;
using System.Threading.Tasks;

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
