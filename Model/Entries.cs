using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Model
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public Ticket UserTicket { get; set; }
        public int UserTicketId { get; set; }
        [Required]
        public string BarCode { get; set; }
        public DateTime LoginTime { get; set; }
        [NotMapped]
        public User Inserter { get; set; }
        [Required]
        public int InserterId { get; set; }
        public string TrainingType { get; set; }
    }
}
