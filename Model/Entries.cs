using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Model
{
    public class Entries
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public Ticket UserTicket { get; set; }
        public int UserTicketId { get; set; }
        public string BarCode { get; set; }
        public DateTime LoginTime { get; set; }
        [NotMapped]
        public User Inserter { get; set; }
        public int InserterId { get; set; }
    }
}
