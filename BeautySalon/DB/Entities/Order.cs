using System;

namespace BeautySalon.DB.Entities
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int StaffId { get; set; }
        public string Remark { get; set; }
        public OrderStatus Status { get; set; }

        public Order() { }

        public Order(int id, DateTime time, string fullName, string phoneNumber, int staffId, string remark, OrderStatus status)
        {
            Id = id;
            Time = time;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            StaffId = staffId;
            Remark = remark;
            Status = status;
        }

        public enum OrderStatus
        {
            Unknown,
            Created,
            Processed,
            Complete,
            Cancel
        }
    }
}
