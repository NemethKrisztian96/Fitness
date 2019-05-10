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
        private FitnessDB fitessDatabase;

        public FitnessController()
        {
            this.fitessDatabase = new FitnessDB();
        }
    }
}
