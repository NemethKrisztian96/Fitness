using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Common.FitnessTabContents
{
    public interface IManageTicketTypeContent : IFitnessContent
    {
        int TicketTypeId { get; set; }
    }
}
