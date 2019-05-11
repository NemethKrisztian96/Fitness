using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Model.DBContext;

namespace Fitness.Logic
{
    public class FitnessController
    {
        private FitnessDB fitnessDatabase;

        public FitnessController()
        {
            this.fitnessDatabase = new FitnessDB();
        }

        public string GetPassword(string username)
        {
            Console.WriteLine(this.fitnessDatabase.Users.Where(u => u.UserName == username).ToList().FirstOrDefault());
            return this.fitnessDatabase.Users.Where(u => u.UserName == username)?.First().Password ?? "";
        }

        public int GetUserId(string username)
        {
            return this.fitnessDatabase.Users.Where(u => u.UserName == username)?.First().Id ?? -1;
        }

        public string GetRole(string username)
        {
            return this.fitnessDatabase.Users.Where(u => u.UserName == username)?.First().Role ?? "";
        }
    }
}
