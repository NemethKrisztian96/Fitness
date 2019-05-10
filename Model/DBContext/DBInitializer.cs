namespace Fitness.Model.DBContext
{
    using System.Data.Entity;
    using Fitness.Model;

    /// <summary>
    /// Database initialization
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{Fitness.Model.DBContext.FitnessDB}" />

    public class DBInitializer: CreateDatabaseIfNotExists<FitnessDB>
    {
        protected override void Seed(FitnessDB context)
        {
            this.AddTicketTypes(context);
            this.AddUsers(context);
            //base.Seed(context);
        }

        private void AddTicketTypes(FitnessDB context)
        {
            context.TicketTypes.Add(new TicketType { Id = 1, Name = "Standard30Day", DayNumber = 30, MaximumUsagePerDay = 1, Price = 90, Description = "One training per day for 30 days.", Status = "Active"});
            context.TicketTypes.Add(new TicketType { Id = 2, Name = "Intensive", DayNumber = 30, MaximumUsagePerDay = 3, Price = 170, Description = "Three training per day for 30 days.", Status = "Active"});
            context.TicketTypes.Add(new TicketType { Id = 3, Name = "VIP", DayNumber = 30, StartHour = 0, EndHour = 24, Price = 250, Description = "Unlimited usage for 30 days.", Status = "Active" });
            context.TicketTypes.Add(new TicketType { Id = 4, Name = "Morning", DayNumber = 30, StartHour = 6, EndHour = 10, Price = 50, Description = "Ticket for early birds.", Status = "Active" });
            context.TicketTypes.Add(new TicketType { Id = 5, Name = "Evening", DayNumber = 30, StartHour = 18, EndHour = 22 ,Price = 50, Description = "Ticket for the night lovers.", Status = "Active" });
        }

        private void AddUsers(FitnessDB context)
        {
            context.Users.Add(new User { Id = 0, UserName = "Admin", Password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", Role = "Admin", Salary = 10000, Status = "Admin"});
            context.Users.Add(new User { Id = 1, UserName="nancross", FirstName = "Nancy", LastName = "Ross", Password = "CF43E029EFE6476E1F7F84691F89C876818610C2EAEAEB881103790A48745B82", Role = "Manager", Salary = 3000, Status = "Active" });
            context.Users.Add(new User { Id = 2, UserName = "briaedwa", FirstName = "Brian", LastName = "Edwards", Password = "2DED0942480E1BAB8F783C2CA16AFE0AD2B27567E831E629801EE09DC394283A", Role = "Manager", Salary = 3000, Status = "Active" });
            context.Users.Add(new User { Id = 3, UserName = "douglong", FirstName = "Douglas", LastName = "Long", Password = "B37B03A62CF77B2767CA4261A756FB3269FB00C1D0830581778EA07A175C42F3", Role = "Receptionist",Salary = 1700, Status = "Active" });
            context.Users.Add(new User { Id = 4, UserName = "jerehend", FirstName = "Jeremy", LastName = "Henderson", Password = "D41FE7DB4D92A1DA0048E2F48227EBE18583800BEDB07A0EE0E20F96AE3FE286", Role = "Receptionist", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { Id = 5, UserName = "dennrobe", FirstName = "Dennis", LastName = "Roberts", Password = "E09CCE5BF6CF28922F108F206F8DA1FBBD6142F5C2632F74187A75C53C5C7B72", Role = "Receptionist", Salary = 1650, Status = "Active" });
            context.Users.Add(new User { Id = 6, UserName = "kimbsand", FirstName = "Kimberly", LastName = "Sanders", Password = "1A5AFEDA973D776E31D1D7266F184468F84D99BED311D88D3DCB67015934F9F9", Role = "Trainer", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { Id = 7, UserName = "shardavi", FirstName = "Sharon", LastName = "Davis", Password = "31F6213EEF65D7FA751A998EADF2395CFAC387F7DB9D4BECD4BF828846C458F0", Role = "Trainer", Salary = 1800, Status = "Active" });
            context.Users.Add(new User { Id = 8, UserName = "kennking", FirstName = "Kenneth", LastName = "King", Password = "869E0257E722E690A9944C85FE8F723503A95159BEFC54C07E1784D60307A6C6", Role = "Trainer", Salary = 1900, Status = "Active" });
            context.Users.Add(new User { Id = 9, UserName = "jerrbake", FirstName = "Jerry", LastName = "Baker", Password = "668C222E4A5933F8616B6DC985D5EE015FA13ABE1F1D052A4EEAAE4170077011", Role = "Trainer", Salary = 1600, Status = "Active" });
            context.Users.Add(new User { Id = 10, UserName = "eugecoop", FirstName = "Eugene", LastName = "Cooper", Password = "BAD3CE77334DC4DD8BBFBF07674283ADCDD1DA9A6385909B24F6FBCE36EC64D9", Role = "Trainer", Salary = 1700, Status = "Active" });
            context.Users.Add(new User { Id = 11, UserName = "victjohn", FirstName = "Victor", LastName = "Johnson", Password = "F55FE41B6952437C99F4541D5762AC0C5ECCD504ACA6D0B7347C50FDA92EB7B7", Role = "Janitor", Salary = 1500, Status = "Active" });
            context.Users.Add(new User { Id = 12, UserName = "dianedwa", FirstName = "Diane", LastName = "Edwards", Password = "9C073343CC87A58F5C59450F99295C03A6C013F7DF9A3AB107AF2096A71A20DB", Role = "Janitor", Salary = 1400, Status = "Active" });
            context.Users.Add(new User { Id = 13, UserName = "alicross", FirstName = "Alice", LastName = "Ross", Password = "C0C5F4315B926D28BC0490DBC01B1DCEF499BD9A897D1DA45EACB8200A55D421", Role = "Janitor", Salary = 1450, Status = "Active" });
        }


    }
}
