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
    class ListTicketTypesViewModel : ViewModelBase, IListTicketTypesContent
    {
        private List<TicketType> ticketTypes;
        private TicketType selectedTicketType;
        public string Header => "Ticket types";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => true;

        public List<TicketType> TicketTypes
        {
            get
            {
                return this.ticketTypes;
            }
            set
            {
                this.ticketTypes = value;
                this.RaisePropertyChanged();
            }
        }
        public TicketType SelectedTicketType
        {
            get
            {
                return this.selectedTicketType;
            }
            set
            {
                this.selectedTicketType = value;
                this.RaisePropertyChanged();
            }
        }
        public RelayCommand ManageTicketTypeCommand { get; set; }
        public RelayCommand AddNewTicketTypeCommand { get; set; }

        public ListTicketTypesViewModel()
        {
            this.TicketTypes = Data.Fitness.GetTicketTypes();
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.ManageTicketTypeCommand = new RelayCommand(this.ManageTicketTypeExecute);
            this.AddNewTicketTypeCommand = new RelayCommand(this.AddNewTicketTypeExecute);
        }

        public void AddNewTicketTypeExecute()
        {
            MainWindowViewModel.Instance.OpenAddNewTicketTypeTab();
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void ManageTicketTypeExecute()
        {
            if (this.SelectedTicketType != null)
            {
                MainWindowViewModel.Instance.OpenManageTicketTypeTab(this.SelectedTicketType);
            }
        }

        public void RefreshList()
        {
            this.TicketTypes = Data.Fitness.GetTicketTypes();
        }
    }
}
