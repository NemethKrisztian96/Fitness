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

        public FitnessController()
        {
            this.FitnessDatabase = new FitnessDB();
        }

        public string GetPassword(string username)
        {
            //Console.WriteLine(this.fitnessDatabase.Users.Where(u => u.UserName == username).ToList().FirstOrDefault().Password);
            return this.FitnessDatabase.Users.Where(u => u.UserName == username).FirstOrDefault()?.Password ?? "";
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
            return this.FitnessDatabase.Clients.Where(c => c.BarCode == barcode).First() ?? null;
        }

        public List<Client> GetClientsByName(string name)
        {
            List<string> nameList = name.Split(' ').ToList();
            return this.FitnessDatabase.Clients.Where(c => nameList.Contains(c.FirstName) || nameList.Contains(c.LastName))?.ToList() ?? new List<Client>();
        }
    }
}
