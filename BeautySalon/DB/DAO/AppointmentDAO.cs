using BeautySalon.DB.Entities;
using BeautySalon.DB.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.DAO
{
    internal class AppointmentDAO : DAO, IAppointmentDAO
    {
        public AppointmentDAO(MySqlConnection connection) : base(connection)
        {
        }

        protected override void InitializeTable()
        {
            const string query = @"
            CREATE TABLE IF NOT EXISTS appointment (
                id INT AUTO_INCREMENT PRIMARY KEY,
                time DATETIME NOT NULL,
                full_name VARCHAR(255) NOT NULL,
                phone_number VARCHAR(12) NOT NULL,
                staff_id INT NOT NULL,
                remark VARCHAR(255) NULL,
                status INT NOT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Appointment>> GetAll()
        {
            List<Appointment> appointments = new List<Appointment>();
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `appointment`
                WHERE 1";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appointments.Add(new Appointment(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            Enum.IsDefined(typeof(Appointment.AppointmentStatus), reader.GetInt32(6)) ? (Appointment.AppointmentStatus)reader.GetInt32(6) : Appointment.AppointmentStatus.Unknown
                        ));
                    }

                    return appointments;
                }
            }
        }

        public async Task<Appointment> GetById(int id)
        {
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `appointment`
                WHERE `id` = @id";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        return new Appointment(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            Enum.IsDefined(typeof(Appointment.AppointmentStatus), reader.GetInt32(6)) ? (Appointment.AppointmentStatus)reader.GetInt32(6) : Appointment.AppointmentStatus.Unknown
                        );
                    }

                    return null;
                }
            }
        }

        public async Task<List<Appointment>> GetByPeriod(DateTime timeStart, DateTime timeEnd)
        {
            List<Appointment> appointments = new List<Appointment>();
            const string query = @"
                SELECT
                    `id`, `time`, `full_name`, `phone_number`, `staff_id`, `remark`, `status`
                FROM `appointment`
                WHERE `time` >= @timeStart AND `time` < @timeEnd";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@timeStart", timeStart);
                command.Parameters.AddWithValue("@timeEnd", timeEnd);

                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appointments.Add(new Appointment(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Enum.IsDefined(typeof(Appointment.AppointmentStatus), reader.GetInt32(6)) ? (Appointment.AppointmentStatus)reader.GetInt32(6) : Appointment.AppointmentStatus.Unknown
                        ));
                    }

                    return appointments;
                }
            }
        }

        public async Task Insert(Appointment staff)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Appointment.AppointmentStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Appointment staff)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Appointment.AppointmentStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
