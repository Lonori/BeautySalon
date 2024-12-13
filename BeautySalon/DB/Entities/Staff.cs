using System;

namespace BeautySalon.DB.Entities
{
    internal class Staff
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DateJoin { get; set; }
        public DateTime DateLeave { get; set; }
        public string Position { get; set; }

        public Staff() { }

        public Staff(int id, string fullName, DateTime birthday, DateTime dateJoin, DateTime dateLeave, string position)
        {
            Id = id;
            FullName = fullName;
            Birthday = birthday;
            DateJoin = dateJoin;
            DateLeave = dateLeave;
            Position = position;
        }
    }
}
