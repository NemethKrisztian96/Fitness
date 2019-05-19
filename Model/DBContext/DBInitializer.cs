namespace Fitness.Model.DBContext
{
    using System.Data.Entity;
    using System.Linq;
    using Fitness.Model;

    /// <summary>
    /// Database initialization
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{Fitness.Model.DBContext.FitnessDB}" />

    public class DBInitializer: DropCreateDatabaseIfModelChanges<FitnessDB>//CreateDatabaseIfNotExists<FitnessDB>//DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(FitnessDB context)
        {
            base.Seed(context);
            this.AddTicketTypes(context);
            this.AddUsers(context);
            this.AddClients(context);
            context.SaveChanges();
            this.AddTickets(context);
            context.SaveChanges();
            this.AddEntries(context);
        }

        private void AddTicketTypes(FitnessDB context)
        {
            context.TicketTypes.Add(new TicketType { Name = "Standard30Day", DayNumber = 30, MaximumUsagePerDay = 1, Price = 90, Description = "One training per day for 30 days.", Status = "Active"});
            context.TicketTypes.Add(new TicketType { Name = "Intensive", DayNumber = 30, MaximumUsagePerDay = 3, Price = 170, Description = "Three training per day for 30 days.", Status = "Active"});
            context.TicketTypes.Add(new TicketType { Name = "VIP", DayNumber = 30, StartHour = 0, EndHour = 24, Price = 250, Description = "Unlimited usage for 30 days.", Status = "Active" });
            context.TicketTypes.Add(new TicketType { Name = "Morning", DayNumber = 30, StartHour = 6, EndHour = 10, Price = 50, Description = "Ticket for early birds.", Status = "Active" });
            context.TicketTypes.Add(new TicketType { Name = "Evening", DayNumber = 30, StartHour = 18, EndHour = 22 ,Price = 50, Description = "Ticket for the night lovers.", Status = "Active" });
        }

        private void AddUsers(FitnessDB context)
        {
            context.Users.Add(new User { UserName = "admin", Password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", Role = "Admin", Salary = 10000, Status = "Admin"});
            context.Users.Add(new User { UserName = "nancross", FirstName = "Nancy", LastName = "Ross", Password = "CF43E029EFE6476E1F7F84691F89C876818610C2EAEAEB881103790A48745B82", Role = "Manager", Salary = 3000, Status = "Active" });
            context.Users.Add(new User { UserName = "briaedwa", FirstName = "Brian", LastName = "Edwards", Password = "2DED0942480E1BAB8F783C2CA16AFE0AD2B27567E831E629801EE09DC394283A", Role = "Manager", Salary = 3000, Status = "Active" });
            context.Users.Add(new User { UserName = "douglong", FirstName = "Douglas", LastName = "Long", Password = "B37B03A62CF77B2767CA4261A756FB3269FB00C1D0830581778EA07A175C42F3", Role = "Receptionist",Salary = 1700, Status = "Active" });
            context.Users.Add(new User { UserName = "jerehend", FirstName = "Jeremy", LastName = "Henderson", Password = "D41FE7DB4D92A1DA0048E2F48227EBE18583800BEDB07A0EE0E20F96AE3FE286", Role = "Receptionist", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { UserName = "dennrobe", FirstName = "Dennis", LastName = "Roberts", Password = "E09CCE5BF6CF28922F108F206F8DA1FBBD6142F5C2632F74187A75C53C5C7B72", Role = "Receptionist", Salary = 1650, Status = "Active" });
            context.Users.Add(new User { UserName = "kimbsand", FirstName = "Kimberly", LastName = "Sanders", Password = "1A5AFEDA973D776E31D1D7266F184468F84D99BED311D88D3DCB67015934F9F9", Role = "Trainer", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { UserName = "shardavi", FirstName = "Sharon", LastName = "Davis", Password = "31F6213EEF65D7FA751A998EADF2395CFAC387F7DB9D4BECD4BF828846C458F0", Role = "Trainer", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { UserName = "kennking", FirstName = "Kenneth", LastName = "King", Password = "869E0257E722E690A9944C85FE8F723503A95159BEFC54C07E1784D60307A6C6", Role = "Trainer", Salary = 1900, Status = "Active" });
            context.Users.Add(new User { UserName = "jerrbake", FirstName = "Jerry", LastName = "Baker", Password = "668C222E4A5933F8616B6DC985D5EE015FA13ABE1F1D052A4EEAAE4170077011", Role = "Trainer", Salary = 1600, Status = "Active" });
            context.Users.Add(new User { UserName = "eugecoop", FirstName = "Eugene", LastName = "Cooper", Password = "BAD3CE77334DC4DD8BBFBF07674283ADCDD1DA9A6385909B24F6FBCE36EC64D9", Role = "Trainer", Salary = 1700, Status = "Active" });
            context.Users.Add(new User { UserName = "victjohn", FirstName = "Victor", LastName = "Johnson", Password = "F55FE41B6952437C99F4541D5762AC0C5ECCD504ACA6D0B7347C50FDA92EB7B7", Role = "Janitor", Salary = 1500, Status = "Active" });
            context.Users.Add(new User { UserName = "dianedwa", FirstName = "Diane", LastName = "Edwards", Password = "9C073343CC87A58F5C59450F99295C03A6C013F7DF9A3AB107AF2096A71A20DB", Role = "Janitor", Salary = 1400, Status = "Active" });
            context.Users.Add(new User { UserName = "alicross", FirstName = "Alice", LastName = "Ross", Password = "C0C5F4315B926D28BC0490DBC01B1DCEF499BD9A897D1DA45EACB8200A55D421", Role = "Janitor", Salary = 1450, Status = "Active" });
        }

        private void AddClients(FitnessDB context)
        {
            context.Clients.Add(new Client { BarCode = "123456789123", FirstName = "Guest", LastName = "Guest", Sex = "O", InsertDate = new System.DateTime(2019, 1, 1), InserterId = 0 });
            context.Clients.Add(new Client { BarCode = "614704024060", FirstName = "Alison", LastName = "Ramirez", PhoneNumber = "0757446374", Sex="F", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "594483537929", FirstName = "Tracy", LastName = "Berry", PhoneNumber = "0729085485", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "760763000069", FirstName = "Toni", LastName = "Sharp", PhoneNumber = "0702794292", Sex = "M", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "698497100539", FirstName = "Darrel", LastName = "Hammond", PhoneNumber = "0720057803", Sex = "M", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "375514164514", FirstName = "Ruby", LastName = "Brooks", PhoneNumber = "0754253354", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "618252276755", FirstName = "Ashley", LastName = "Cook", PhoneNumber = "0777278351", Sex = "F", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "242243662974", FirstName = "Julia", LastName = "Murphy", PhoneNumber = "0798824569", Sex = "F", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "275326489211", FirstName = "Lois", LastName = "Allen", PhoneNumber = "0747489794", Sex = "M", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "148890289309", FirstName = "Christopher", LastName = "Taylor", PhoneNumber = "0733675341", Sex = "M", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "682675671728", FirstName = "Mark", LastName = "Gonzales", PhoneNumber = "0711059158", Sex = "M", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "136751805332", FirstName = "Diana", LastName = "Richardson", PhoneNumber = "0733019204", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "348882585558", FirstName = "Donald", LastName = "Lewis", PhoneNumber = "0776043954", Sex = "M", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "382597335798", FirstName = "Nicholas", LastName = "Watson", PhoneNumber = "0735739765", Sex = "M", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "553671675421", FirstName = "Heather", LastName = "Jackson", PhoneNumber = "0759244536", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "661996133847", FirstName = "Fred", LastName = "Clark", PhoneNumber = "0738130360", Sex = "M", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "213346907014", FirstName = "Anna", LastName = "Martinez", PhoneNumber = "0721604475", Sex = "F", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "169076375984", FirstName = "Stephanie", LastName = "Flores", PhoneNumber = "0774711310", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "379914837074", FirstName = "Kathryn", LastName = "Price", PhoneNumber = "0701463487", Sex = "F", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "957018351346", FirstName = "Sandra", LastName = "Perry", PhoneNumber = "0748528709", Sex = "F", InsertDate = new System.DateTime(2019, 3, 24), InserterId = 3, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "730081343503", FirstName = "Denise", LastName = "Scott", PhoneNumber = "0729541904", Sex = "F", InsertDate = new System.DateTime(2019, 3, 25), InserterId = 4, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "781475044160", FirstName = "Angela", LastName = "Russell", PhoneNumber = "0733976716", Sex = "F", InsertDate = new System.DateTime(2019, 3, 26), InserterId = 5, IsDeleted = false });
            context.Clients.Add(new Client { BarCode = "781475044161", FirstName = "Angela", LastName = "Murphy", PhoneNumber = "0733976717", Sex = "F", InsertDate = new System.DateTime(2019, 3, 28), InserterId = 5, IsDeleted = false });
        }

        private void AddTickets(FitnessDB context)
        {
            Client client = context.Clients.Where(c => c.Id == 1).FirstOrDefault();
            Client client2 = context.Clients.Where(c => c.Id == 2).FirstOrDefault();

            TicketType type = context.TicketTypes.Where(tt => tt.Id == 1).FirstOrDefault();
            User seller = context.Users.Where(u => u.Id == 1).FirstOrDefault();

            if (client != null && client2 != null && type != null && seller != null)
            {
                context.Tickets.Add(new Ticket { Owner = client, OwnerId = client.Id, Type = type, TicketTypeId = type.Id, BuyingDate = new System.DateTime(2019, 4, 1), FirstUsingDate = new System.DateTime(2019, 4, 1), ExpirationDate = new System.DateTime(2019, 4, 1).AddDays(30), MaxLoginNumber = 30, Price = 90, Seller = seller, SellerId = seller.Id, Status = "Active" });
                context.Tickets.Add(new Ticket { Owner = client2, OwnerId = client2.Id, Type = type, TicketTypeId = type.Id, BuyingDate = new System.DateTime(2019, 4, 1), FirstUsingDate = new System.DateTime(2019, 4, 1), ExpirationDate = new System.DateTime(2019, 4, 1).AddDays(30), MaxLoginNumber = 30, Price = 90, Seller = seller, SellerId = seller.Id, Status = "Active" });
            }
        }

        private void AddEntries(FitnessDB context)
        {
            Ticket ticket = context.Tickets.Where(t => t.Id == 1).FirstOrDefault();
            Client client = context.Clients.Where(c => c.Id == 1).FirstOrDefault();
            User inserter = context.Users.Where(u => u.Id == 1).FirstOrDefault();
            User inserter2 = context.Users.Where(u => u.Id == 2).FirstOrDefault();

            //Ticket ticket = context.Tickets.Local[0];
            //Client client = context.Clients.Local[1];
            //User inserter = context.Users.Local[0];
            //User inserter2 = context.Users.Local[1];

            if (ticket != null && client != null && inserter != null && inserter2 != null)
            {
                context.Entries.Add(new Entry { UserTicket = ticket, UserTicketId = ticket.Id, BarCode = client.BarCode, LoginTime = new System.DateTime(2019, 4, 1, 18, 5, 32), Inserter = inserter, InserterId = inserter.Id, TrainingType = "Fittness" });
                context.Entries.Add(new Entry { UserTicket = ticket, UserTicketId = ticket.Id, BarCode = client.BarCode, LoginTime = new System.DateTime(2019, 4, 2, 18, 4, 11), Inserter = inserter2, InserterId = inserter2.Id, TrainingType = "Fittness" });
                
            }
        }

        public override void InitializeDatabase(FitnessDB context)
        {
            base.InitializeDatabase(context);
            if (context.Users.Count() < 5)
            {
                this.Seed(context);
            }
            context.SaveChanges();
            if (context.Users.Count() < 5)
            {
                this.AddUsers(context);
            }
            context.SaveChanges();
            if (context.TicketTypes.Count() < 2)
            {
                this.AddTicketTypes(context);
            }
            context.SaveChanges();
            if (context.Clients.Count() < 2)
            {
                this.AddClients(context);
            }
            context.SaveChanges();
            if (context.Tickets.Count() < 2)
            {
                this.AddTickets(context);
            }
            context.SaveChanges();
            if (context.Entries.Count() < 2)
            {
                this.AddEntries(context);
            }
            context.SaveChanges();
        }
    }
}
