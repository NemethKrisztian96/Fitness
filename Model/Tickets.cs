using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public Client Owner { get; set; }
        public int OwnerId { get; set; }
        [NotMapped]
        public TicketType Type { get; set; }
        [Required]
        public int TicketTypeId { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime FirstUsingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MaxLoginNumber { get; set; }
        public int? LoginNumber { get; set; }
        public double Price { get; set; }
        [NotMapped]
        public User Seller { get; set; }
        public int SellerId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
