using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Model
{
    [Table("Contacts")]
    public class Contact
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Ignore]
        public string FullName => $"{FirstName} {LastName}";

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
