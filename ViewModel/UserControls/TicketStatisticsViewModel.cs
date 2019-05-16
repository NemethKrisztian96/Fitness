using Fitness.Common.FitnessTabContents;
using Fitness.Common.MVVM;
using Fitness.Logic;
using Fitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel.UserControls
{
    public class TicketStatisticsViewModel : ViewModelBase, ITicketStatistics
    {
        private List<Ticket> tickets;
        public RelayCommand CloseTabItemCommand { get; set; }

        public TicketStatisticsViewModel()
        {
            InitializeTicketList();

            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public string Header { get; private set; }

        public bool ShowCloseButton => true;

        public List<Ticket> Tickets { get; set; }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
        private void InitializeTicketList()
        {
            this.Tickets = Data.Fitness.GetAllTickets();
        }
    }
}
