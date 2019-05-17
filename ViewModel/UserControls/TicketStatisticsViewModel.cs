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
        public RelayCommand CloseTabItemCommand { get; set; }

        public TicketStatisticsViewModel()
        {
            this.Header = "Ticket Statistics";
            InitializeTicketList();

            this.ListTicketStatisticsCommand = new RelayCommand(this.ListTicketStatisticsExecute);
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public string Header { get; private set; }

        public bool ActiveChecked { get; set; }
        public bool InactiveChecked { get; set; }

        public bool ShowCloseButton => true;

        private List<Ticket> tickets;
        public List<Ticket> Tickets {
            get
            {
                return this.tickets;
            }
            set
            {
                this.tickets = value;
                this.RaisePropertyChanged();
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
        private void InitializeTicketList()
        {
            this.Tickets = Data.Fitness.GetAllTickets();
        }

        public RelayCommand ListTicketStatisticsCommand { get; set; }
        public void ListTicketStatisticsExecute()
        {
            this.Tickets = Data.Fitness.GetAllTickets();
            List<Ticket> filtered = this.Tickets;

            if (ActiveChecked && !InactiveChecked)
            {
                filtered = filtered.Where(t => t.Status == "Active").ToList();
            }
            if(!ActiveChecked && InactiveChecked)
            {
                filtered = filtered.Where(t => t.Status == "Disable" || t.Status == "Deleted").ToList();
            }

            Tickets = filtered;

            //TODO finishing...
        }
    }
}
