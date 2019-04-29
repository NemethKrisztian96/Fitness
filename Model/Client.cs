using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public DateTime InsertDate { get; set; }
        public User Inserter { get; set; }
    }
}
