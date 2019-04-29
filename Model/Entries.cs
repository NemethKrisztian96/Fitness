using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Entries
    {
        [Key]
        public int Id { get; set; }
        public Ticket UserTicket { get; set; }
        public string BarCode { get; set; }
        public DateTime LoginTime { get; set; }
        public User Inserter { get; set; }
    }
}
