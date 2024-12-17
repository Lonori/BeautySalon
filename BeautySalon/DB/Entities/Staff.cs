using System;
using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class Staff
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("ФИО")]
        [TextMultiline]
        public string FullName { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

        [DisplayName("Дата рождения")]
        [DateWithoutTime]
        public DateTime Birthday { get; set; }

        [DisplayName("Пол")]
        public string Gender { get; set; }

        [DisplayName("Дата найма")]
        [DateWithoutTime]
        public DateTime DateJoin { get; set; }

        [DisplayName("Дата увольнения")]
        [DateWithoutTime]
        public DateTime DateLeave { get; set; }

        [DisplayName("Должность")]
        public string Position { get; set; }

        public Staff() { }

        public Staff(
            int id,
            string fullName,
            string phoneNumber,
            DateTime birthday,
            string gender,
            DateTime dateJoin,
            DateTime dateLeave,
            string position
        )
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
            Gender = gender;
            DateJoin = dateJoin;
            DateLeave = dateLeave;
            Position = position;
        }
    }
}
