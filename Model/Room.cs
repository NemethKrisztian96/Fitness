using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
