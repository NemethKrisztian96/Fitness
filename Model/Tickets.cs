using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public Client Owner { get; set; }
        public TicketType Type { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime FirstUsingDate { get; set; }
        public DateTime LastUsingDate { get; set; }
        public int LoginNumber { get; set; }
        public double Price { get; set; }
        public User Seller { get; set; }
        public string Status { get; set; }
    }
}
