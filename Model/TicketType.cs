using System.ComponentModel.DataAnnotations;

namespace Fitness.Model
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayNumber { get; set; }
        public int OccasionNumber { get; set; }
        public int MaximumUsagePerDay { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
