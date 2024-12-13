using System;

namespace BeautySalon.DB.Entities
{
    internal class Appointment
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int StaffId { get; set; }
        public string Remark { get; set; }
        public AppointmentStatus Status { get; set; }

        public Appointment() { }

        public Appointment(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, AppointmentStatus status)
        {
            Id = id;
            Time = time;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            StaffId = staffId;
            Remark = remark;
            Status = status;
        }

        public enum AppointmentStatus
        {
            Unknown,
            Created,
            Processed,
            Complete,
            Cancel
        }
    }
}
