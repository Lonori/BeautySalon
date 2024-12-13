using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.Interfaces
{
    internal interface IAppointmentDAO
    {
        Task<List<Appointment>> GetAll();
        Task<Appointment> GetById(int id);
        Task<List<Appointment>> GetByPeriod(DateTime timeStart, DateTime timeEnd);
        Task Insert(Appointment staff);
        Task Insert(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Appointment.AppointmentStatus status);
        Task Update(Appointment staff);
        Task Update(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, Appointment.AppointmentStatus status);
        Task Delete(int id);
    }
}
