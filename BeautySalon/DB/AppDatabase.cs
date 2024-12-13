﻿using BeautySalon.DB.DAO;
using BeautySalon.DB.Interfaces;
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
        public readonly IStaffDAO StaffDAO;
        public readonly IAppointmentDAO AppointmentDAO;

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
            StaffDAO = new StaffDAO(_connection);
            AppointmentDAO = new AppointmentDAO(_connection);
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

        public static bool IsConnected()
        {
            if (_instance == null) return false;
            if (_instance.ConnectionState != ConnectionState.Open) return false;
            return true;
        }

        public static void Initialisation(string connectionString)
        {
            _connectionString = connectionString;
            _instance = new AppDatabase();
        }

        public static void Initialisation(string host, string username, string passwd, int port = 3306)
        {
            Initialisation("Server=" + host + "; Port=" + port + "; Uid=" + username + "; Pwd=" + passwd + "; CharSet=utf8mb4;");
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
