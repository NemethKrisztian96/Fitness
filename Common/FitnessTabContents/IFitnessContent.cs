using Fitness.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Common.FitnessTabContents
{
    public interface IFitnessContent
    {
        string Header { get; }

        ///22
        RelayCommand CloseTabItemCommand { get; set; }
        bool ShowCloseButton { get; }

    }
}
