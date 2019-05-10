namespace Fitness.Model.DBContext
{
    using System.Data.Entity;
    using Fitness.Model;
    using System.Collections.Generic;

    public class FitnessDB : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessDB"/> class.
        /// </summary>
        public FitnessDB()
          : base("name=FitnessDB")
        {
        }

        public virtual DbSet<TicketType> TicketTypes{ get; set; }
        public virtual DbSet<Ticket> Tickets{ get; set; }
        public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<Entry> Entries{ get; set; }
        public virtual DbSet<Client> Clients{ get; set; }
        
    }
}
