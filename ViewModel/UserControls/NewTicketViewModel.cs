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
    public class NewTicketViewModel : ViewModelBase, INewTicketContent
    {
        private int clientId;

        private List<string> ticketTypeNameList;
        private string selectedTicketTypeName;
        private DateTime selectedValidityDate;
        private double ticketPrice;
        private User seller;
        private Client customer;

        public RelayCommand BuyingTicketCommand { get; set; }

        public NewTicketViewModel(int sellerId,int customerId)
        {
            Header = "Buy new ticket";
            Initialize();

            seller = Data.Fitness.GetUserById(sellerId);
            customer = Data.Fitness.GetClientById(customerId);

            this.BuyingTicketCommand = new RelayCommand(this.BuyingTicketExecute);
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public RelayCommand CloseTabItemCommand { get; set; }

        public string Header { get; private set; }

        public bool ShowCloseButton => true;

        public int ClientId
        {
            get
            {
                return this.clientId;
            }

            set
            {
                this.clientId = value;
                this.RaisePropertyChanged();
            }
        }

        private void Initialize()
        {
            this.TicketTypeNameList = Data.Fitness.GetTicketTypeNames();
            this.SelectedValidityDate = DateTime.Now;
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.OpenClientTicketsTab(customer);
            MainWindowViewModel.Instance.CloseTabItem(this);
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
                this.TicketPrice = Data.Fitness.GetTicketTypeByName(this.selectedTicketTypeName).Price;

                this.RaisePropertyChanged();
            }
        }

        public DateTime SelectedValidityDate
        {
            get
            {
                return this.selectedValidityDate;
            }
            set
            {
                this.selectedValidityDate = value;
                this.RaisePropertyChanged();
            }
        }

        public double TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
            set
            {
                this.ticketPrice = value;
                this.RaisePropertyChanged();
            }
        }

        private void BuyingTicketExecute()
        {
            if (SelectedValidityDate.CompareTo(DateTime.Today.Date) < 0)
            {
                SelectedValidityDate = DateTime.Today;
            }

            TicketType ticType = Data.Fitness.GetTicketTypeByName(selectedTicketTypeName);
            Data.Fitness.AddTicket(new Model.Ticket { Owner = customer, OwnerId = customer.Id, Type = ticType, TicketTypeId = ticType.Id, BuyingDate = DateTime.Now, FirstUsingDate = SelectedValidityDate, ExpirationDate = SelectedValidityDate.AddDays(ticType.DayNumber ?? 30), MaxLoginNumber = ticType.OccasionNumber ?? Int32.MaxValue, Price = ticType.Price, Seller = seller, SellerId = seller.Id, Status = "Active"});

            //todo pop up
            MainWindowViewModel.Instance.RefreshClientTickets(customer);
            CloseTabItemExecute();
        }
    }
}
