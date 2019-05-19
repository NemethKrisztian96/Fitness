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
        private List<string> ticketTypeNameList;
        private string selectedTicketTypeName;
        private DateTime? selectedSellingDate;
        private DateTime? selectedExpirationDate;


        public RelayCommand CloseTabItemCommand { get; set; }

        public TicketStatisticsViewModel()
        {
            this.Header = "Ticket Statistics";
            Initialize();

            this.ListTicketStatisticsCommand = new RelayCommand(this.ListTicketStatisticsExecute);
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public string Header { get; private set; }

        public bool ActiveChecked { get; set; }
        public bool InactiveChecked { get; set; }

        
        public bool ShowCloseButton => true;

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

        public List<string> TicketTypeNameList
        {
            get
            {
                return this.ticketTypeNameList;
            }
            set
            {
                this.ticketTypeNameList = value;
                this.RaisePropertyChanged();
            }
        }
        public string SelectedTicketTypeName
        {
            get
            {
                return this.selectedTicketTypeName;
            }
            set
            {
                this.selectedTicketTypeName = value;
                this.RaisePropertyChanged();
            }
        }
        public DateTime? SelectedSellingDate
        {
            get
            {
                return this.selectedSellingDate;
            }
            set
            {
                this.selectedSellingDate = value;
                this.RaisePropertyChanged();
            }
        }
        public DateTime? SelectedExpirationDate
        {
            get
            {
                return this.selectedExpirationDate;
            }
            set
            {
                this.selectedExpirationDate = value;
                this.RaisePropertyChanged();
            }
        }


        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
        private void Initialize()
        {
            this.Tickets = Data.Fitness.GetAllTickets();
            this.TicketTypeNameList = Data.Fitness.GetTicketTypeNames();
        }

        public RelayCommand ListTicketStatisticsCommand { get; set; }
        public void ListTicketStatisticsExecute()
        {
            if (!string.IsNullOrEmpty(this.SelectedTicketTypeName))
            {
                this.Tickets = Data.Fitness.GetTicketsByTypeName(SelectedTicketTypeName);
            }
            else
            {
                this.Tickets = Data.Fitness.GetAllTickets();
            }
            
            List<Ticket> filtered = this.Tickets;

            if (ActiveChecked && !InactiveChecked)
            {
                filtered = filtered.Where(t => t.Status == "Active").ToList();
            }
            if(!ActiveChecked && InactiveChecked)
            {
                filtered = filtered.Where(t => t.Status == "Expired" || t.Status == "Disable" || t.Status == "Deleted").ToList();
            }

            if (this.SelectedSellingDate?.Date.CompareTo(DateTime.Today.Date) <= 0)
            {
                if(this.SelectedExpirationDate?.Date.CompareTo(DateTime.Today.Date) <= 0)
                {
                    // both date setted
                    filtered = filtered.Where(t => t.BuyingDate.CompareTo(SelectedSellingDate) >= 0).Where(t => t.ExpirationDate.CompareTo(SelectedExpirationDate) <=0).ToList();
                }
                else
                {
                    //only selling date
                    filtered = filtered.Where(t => t.BuyingDate.CompareTo(SelectedSellingDate) >= 0).ToList();
                }
            }
            else
            {
                if (this.SelectedExpirationDate?.Date.CompareTo(DateTime.Today.Date) <= 0)
                {
                    //only expiration date
                    filtered = filtered.Where(t => t.ExpirationDate.CompareTo(SelectedExpirationDate) <= 0).ToList();
                }
            }

            Tickets = filtered;
        }
    }
}
