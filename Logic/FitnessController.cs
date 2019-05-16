using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Model;
using Fitness.Model.DBContext;

namespace Fitness.Logic
{
    public class FitnessController
    {
        public FitnessDB FitnessDatabase { get; }

        public Client LastModified { get; set; }

        public FitnessController()
        {
            this.FitnessDatabase = new FitnessDB();
        }

        public string GetPassword(string username)
        {
            //Console.WriteLine(this.fitnessDatabase.Users.Where(u => u.UserName == username).ToList().FirstOrDefault().Password);
            return this.FitnessDatabase.Users.Where(u => u.UserName == username && u.Role.ToLower() != "janitor").FirstOrDefault()?.Password ?? "";
        }

        public User GetUserById(int id)
        {
            return this.FitnessDatabase.Users.Where(u => u.Id == id)?.FirstOrDefault();
        }

        public int GetUserId(string username)
        {
            return this.FitnessDatabase.Users.Where(u => u.UserName == username).FirstOrDefault()?.Id ?? -1;
        }

        public string GetRole(string username)
        {
            return this.FitnessDatabase.Users.Where(u => u.UserName == username).FirstOrDefault()?.Role ?? "";
        }

        public Client GetClientByBarcode(string barcode)
        {
            return this.FitnessDatabase.Clients.Where(c => c.BarCode == barcode)?.FirstOrDefault() ?? null;
        }

        public List<Client> GetClientsByName(string name)
        {
            List<string> nameList = name.Split(' ').ToList();
            return this.FitnessDatabase.Clients.Where(c => nameList.Contains(c.FirstName) || nameList.Contains(c.LastName))?.ToList() ?? new List<Client>();
        }

        public int GetNextClientId()
        {
            return this.FitnessDatabase.Clients.Max(c => c.Id) + 1;
        }

        public List<Client> GetClients()
        {
            return this.FitnessDatabase.Clients.Where(c => c?.IsDeleted!=true).ToList();
        }

        public void AddClient(Client client)
        {
            this.FitnessDatabase.Clients.Add(client);
            this.FitnessDatabase.SaveChanges();
        }

        public List<TicketType> GetActiveTicketTypes()
        {
            return this.FitnessDatabase.TicketTypes.Where(tt => tt.Status.ToLower() == "active").ToList();
        }

        public List<TicketType> GetTicketTypes()
        {
            return this.FitnessDatabase.TicketTypes.ToList();
        }

        public void AddTicketType(TicketType ticketType)
        {
            this.FitnessDatabase.TicketTypes.Add(ticketType);
            this.FitnessDatabase.SaveChanges();
        }

        public int GetNextTicketTypeId()
        {
            return this.FitnessDatabase.TicketTypes.Max(c => c.Id) + 1;
        }

        public List<Ticket> GetAllTicketOfAClient(Client client)
        {
            return this.FitnessDatabase.Tickets.Where(t => t.OwnerId == client.Id).ToList();
        }

        public void SaveAllChanges()
        {
            this.FitnessDatabase.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return this.FitnessDatabase.Users.Where(u => u.Status.ToLower() != "admin").ToList();
        }

        public List<User> GetActiveUsers()
        {
            return this.FitnessDatabase.Users.Where(u => u.Status.ToLower() == "active" && u.Status.ToLower() != "admin").ToList();
        }

        public void AddUser(User user)
        {
            this.FitnessDatabase.Users.Add(user);
            this.FitnessDatabase.SaveChanges();
        }

        public int GetNextUserId()
        {
            return this.FitnessDatabase.Users.Max(c => c.Id) + 1;
        }

        public bool IsUserUsernameUnique(string username)
        {
            int count = this.FitnessDatabase.Users.Where(u => u.UserName == username).Count();
            if(count == 0)
            {
                return true;
            }
            return false;
        }

        public int GetNextTicketId()
        {
            return this.FitnessDatabase.Tickets.Max(t => t.Id) + 1;
        }

        public void AddTicket(Ticket ticket)
        {
            this.FitnessDatabase.Tickets.Add(ticket);
            this.FitnessDatabase.SaveChanges();
        }

        public void DisableTicket(int ticketId)
        {
            Ticket ticket = this.FitnessDatabase.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
            ticket.Status = "Disable";

            this.FitnessDatabase.SaveChanges();
        }

        public Ticket GetTicketById(int ticketId)
        {
            return this.FitnessDatabase.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
        }
    }
}
