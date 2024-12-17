using BeautySalon.DB.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace BeautySalon.DB
{
    internal class AppDatabase
    {
        private static AppDatabase _instance;
        private static string _connectionString;
        private readonly MySqlConnection _connection;
        public readonly OrderDAO AppointmentDAO;
        public readonly ServiceDAO ServiceDAO;
        public readonly StaffDAO StaffDAO;
        public readonly SupplierDAO SupplierDAO;

        public static AppDatabase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppDatabase();
            }
            return _instance;
        }

        private AppDatabase()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();

            InitializeDatabase();
            _connection.ChangeDatabase("salon");
            AppointmentDAO = new OrderDAO(_connection);
            ServiceDAO = new ServiceDAO(_connection);
            StaffDAO = new StaffDAO(_connection);
            SupplierDAO = new SupplierDAO(_connection);
        }

        public MySqlConnection Connection
        {
            get { return _connection; }
        }

        public ConnectionState ConnectionState
        {
            get
            {
                if (_connection == null)
                {
                    throw new FieldAccessException("Connection not initialised");
                }
                return _connection.State;
            }
        }

        public void Close()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public static bool IsConnected()
        {
            if (_instance == null) return false;
            if (_instance.ConnectionState != ConnectionState.Open) return false;
            return true;
        }

        public static void Connect(string connectionString)
        {
            _connectionString = connectionString;
            _instance = new AppDatabase();
        }

        public static void Connect(string host, string username, string passwd, int port = 3306)
        {
            Connect("Server=" + host + "; Port=" + port + "; Uid=" + username + "; Pwd=" + passwd + "; CharSet=utf8mb4;");
        }

        public static void Disconect()
        {
            if (_instance != null)
            {
                _instance.Close();
            }
        }

        private void InitializeDatabase()
        {
            const string query = "CREATE DATABASE IF NOT EXISTS `salon`;";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
