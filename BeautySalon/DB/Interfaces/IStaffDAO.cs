using BeautySalon.DB.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautySalon.DB.Interfaces
{
    internal interface IStaffDAO
    {
        Task<List<Staff>> GetAll();
        Task<Staff> GetById(int id);
        Task Insert(Staff staff);
        Task Insert(int id, string fullName, DateTime birthday, DateTime dateJoin, DateTime dateLeave, string position);
        Task Update(Staff staff);
        Task Update(int id, string fullName, DateTime birthday, DateTime dateJoin, DateTime dateLeave, string position);
        Task Delete(int id);
    }
}
