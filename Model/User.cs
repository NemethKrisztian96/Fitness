using System.ComponentModel.DataAnnotations;

namespace Fitness.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public double Salary { get; set; }
    }
}
