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
    public class ManageTicketViewModel : ViewModelBase, IManageTicketContent
    {
        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand CreateClientCommand { get; set; }
        public string Header { get; private set; }
        public bool ShowCloseButton => true;
        public int TicketId { get; set; }
        
        private int id;
        private Client owner;
        private int ownerId;
        private TicketType type;
        private int ticketTypeId;
        private DateTime buyingDate;
        private DateTime firstUsingDate;
        private DateTime expirationDate;
        private int maxLoginNumber;
        private double price;
        private User seller;
        private int sellerId;
        private string status;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.RaisePropertyChanged();
            }
        }
        public Client Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
                this.RaisePropertyChanged();
            }
        }
        public int OwnerId
        {
            get
            {
                return this.ownerId;
            }
            set
            {
                this.ownerId = value;
                this.RaisePropertyChanged();
            }
        }
        public TicketType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
                this.RaisePropertyChanged();
            }
        }
        public int TicketTypeId
        {
            get
            {
                return this.ticketTypeId;
            }
            set
            {
                this.ticketTypeId = value;
                this.RaisePropertyChanged();
            }
        }
        public DateTime BuyingDate { get; set; }
        public DateTime FirstUsingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MaxLoginNumber
        {
            get
            {
                return this.maxLoginNumber;
            }
            set
            {
                this.maxLoginNumber = value;
                this.RaisePropertyChanged();
            }
        }
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
                this.RaisePropertyChanged();
            }
        }
        public User Seller
        {
            get
            {
                return this.seller;
            }
            set
            {
                this.seller = value;
                this.RaisePropertyChanged();
            }
        }
        public int SellerId
        {
            get
            {
                return this.sellerId;
            }
            set
            {
                this.sellerId = value;
                this.RaisePropertyChanged();
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                this.RaisePropertyChanged();
            }
        }

        public ManageTicketViewModel(Ticket ticket = null)
        {
            this.Header = "Extend ticket";

            this.OwnerId = ticket.OwnerId;
            this.Type = ticket.Type;
            this.TicketTypeId = ticket.TicketTypeId;
            this.BuyingDate = ticket.BuyingDate;
            this.FirstUsingDate = ticket.FirstUsingDate;
            this.ExpirationDate = ticket.ExpirationDate;
            this.MaxLoginNumber = ticket.MaxLoginNumber;
            this.Price = ticket.Price;
            this.Seller = ticket.Seller;
            this.SellerId = ticket.SellerId;
            this.status = ticket.Status;

            this.CreateClientCommand = new RelayCommand(SaveChanges);
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void SaveChanges()
        {
            if (IsFormValid())
            {
                Data.Fitness.AddTicket(
                    new Ticket()
                    {
                        Id = Data.Fitness.GetNextTicketId(),
                        Owner = this.Owner,
                        OwnerId = this.OwnerId,
                        Type = this.Type,
                        TicketTypeId = this.TicketTypeId,
                        BuyingDate = this.BuyingDate,
                        FirstUsingDate = this.FirstUsingDate,
                        ExpirationDate = this.ExpirationDate,
                        MaxLoginNumber = this.MaxLoginNumber,
                        Price = this.Price,
                        Seller = this.Seller,
                        SellerId = this.SellerId,
                        Status = this.Status
                    }
                );

                //Data.Fitness.DisableTicket(Id);
                MainWindowViewModel.Instance.RefreshClientTickets(Owner);
            }
        }

        private bool IsFormValid()
        {
            Ticket original = Data.Fitness.GetTicketById(Id);

            if(this.ExpirationDate == original.ExpirationDate && this.MaxLoginNumber == original.MaxLoginNumber) { return false; }
            if(this.ExpirationDate < original.ExpirationDate) { return false; }
            if(this.MaxLoginNumber < original.MaxLoginNumber) { return false; }

            return true;
        }
    }
}
