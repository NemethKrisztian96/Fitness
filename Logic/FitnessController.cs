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
            //return this.FitnessDatabase.Clients.Include().Where(c => c.IsDeleted != true).ToList();
            List<Client> list = this.FitnessDatabase.Clients.Where(c => c.IsDeleted != true).ToList();
            foreach (Client item in list)
            {
                if (item?.Inserter == null)
                {
                    item.Inserter = this.GetUserById(item.InserterId);
                }
            }
            return list;
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
            List<Ticket> list = this.FitnessDatabase.Tickets.Where(t => t.OwnerId == client.Id).ToList();
            foreach (Ticket item in list)
            {
                if (item?.Type == null)
                {
                    item.Type = this.GetTicketTypeById(item.TicketTypeId);
                }
            }
            return list;
        }

        public TicketType GetTicketTypeById(int ticketTypeId)
        {
            return this.FitnessDatabase.TicketTypes.Where(tt => tt.Id == ticketTypeId).FirstOrDefault();
        }

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> list = this.FitnessDatabase.Tickets.Select(t => t).ToList();
            foreach (Ticket item in list)
            {
                if (item?.Type == null)
                {
                    item.Type = this.GetTicketTypeById(item.TicketTypeId);
                }
            }
            return list;
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
            if (count == 0)
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

        public void DeleteClient(Client client)
        {
            client.IsDeleted = true;
            this.FitnessDatabase.SaveChanges();
        }

        public List<string> GetClientEmails()
        {
            return this.FitnessDatabase.Clients.Where(c => c.IsDeleted ?? false != true && !string.IsNullOrEmpty(c.Email)).Select(c => c.Email)?.ToList() ?? null;
        }

        public List<Entry> GetEntries()
        {
            //return this.FitnessDatabase.Entries.Include("UserTicket").Include("Inserter").Include("UserTicket.Type").Include("UserTicket.Owner").ToList();
            //return this.FitnessDatabase.Entries.ToList();
            List<Entry> list = this.FitnessDatabase.Entries.ToList();
            foreach (Entry item in list)
            {
                item.Inserter = this.GetUserById(item.InserterId);
                item.UserTicket = this.GetTicketById(item.UserTicketId);
                if (item.UserTicket == null)
                {
                    item.UserTicket = new Ticket() { Owner = this.GetClientByBarcode(item.BarCode) };
                    continue;
                }
                if (item.UserTicket?.Type == null)
                {
                    item.UserTicket.Type = this.FitnessDatabase.TicketTypes.Where(tt => tt.Id != 0 && tt.Id == item.UserTicket.TicketTypeId).First() ?? null;
                }
                if (item?.UserTicket?.Owner == null)
                {
                    item.UserTicket.Owner = this.FitnessDatabase.Clients.Where(c => c.Id == item.UserTicket.OwnerId).First() ?? null;
                }
            }
            return list;
        }

        public List<string> GetTicketTypeNames()
        {
            //List<string> list = new List<string>();
            //foreach(TicketType item in this.FitnessDatabase.TicketTypes.ToList())
            //{
            //    list.Add(item.Name);
            //}
            //return list;
            return this.FitnessDatabase.TicketTypes.Select(tt => tt.Name).ToList();
        }

        public int GetTincketTypeIdByName(string name)
        {
            return this.FitnessDatabase.TicketTypes.Where(tt => tt.Name == name).Select(t => t.Id).FirstOrDefault();
        }
        public List<Ticket> GetTicketsByTypeName(string selectedTicketTypeName)
        {
            int typeId = GetTincketTypeIdByName(selectedTicketTypeName);
            var temp = this.FitnessDatabase.Tickets.Where(t => t.TicketTypeId == typeId).ToList();
            return temp;
        }

        public void RefreshTicketsStatus()
        {
            List<Ticket> list = this.FitnessDatabase.Tickets.Where(t => t.Status.ToLower() == "active").ToList();
            foreach (Ticket item in list)
            {
                if (item.ExpirationDate.Date.CompareTo(DateTime.Today.Date) < 0)
                {
                    item.Status = "Expired";
                }
            }
            this.SaveAllChanges();
        }

        public void AddEntry(string barcode, int inserterId, int ticketId, string trainigType)
        {
            Entry entry = new Entry()
            {
                BarCode = barcode,
                InserterId = inserterId,
                LoginTime = DateTime.Today,
                TrainingType = trainigType,
                UserTicketId = ticketId
            };
            this.FitnessDatabase.Entries.Add(entry);
            this.SaveAllChanges();
        }

        public int GetLoginCount(int ticketId)
        {
            return this.FitnessDatabase.Entries.Where(e => e.UserTicketId == ticketId)?.Count() ?? 0;
        }
    }
}
