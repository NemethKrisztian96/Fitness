using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Common.FitnessTabContents
{
    public interface IClientTickets: IFitnessContent
    {
        int ClientId { get; set; }
    }
}
