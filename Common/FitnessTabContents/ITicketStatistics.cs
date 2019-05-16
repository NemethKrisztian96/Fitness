using Fitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Common.FitnessTabContents
{
    public interface ITicketStatistics: IFitnessContent
    {
        List<Ticket> Tickets { get; set; }
    }
}
