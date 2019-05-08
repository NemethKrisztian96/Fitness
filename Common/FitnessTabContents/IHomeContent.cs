namespace Fitness.Common.FitnessTabContents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHomeContent : IFitnessContent
    {
        bool IsAdmin { get; }
        int UserId { get; }
    }
}
