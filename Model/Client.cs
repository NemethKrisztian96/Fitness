using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BarCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        [Required]
        public string Sex { get; set; }
        public DateTime InsertDate { get; set; }
        [NotMapped]
        public User Inserter { get; set; }
        public int InserterId { get; set; }
    }
}
