using System.ComponentModel.DataAnnotations;

namespace Fitness.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Status { get; set; }
        public double Salary { get; set; }
    }
}
