using Fitness.Common.MVVM;
using Fitness.Common.FitnessTabContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Model;
using Fitness.Logic;

namespace Fitness.ViewModel.UserControls
{
    class ClientTicketListViewModel: ViewModelBase, IClientTickets
    {
        private Client mClient;
        private Ticket mTicket;

        public ClientTicketListViewModel(Client client)
        {
            this.mClient = client;
            this.Header = client.FirstName + " " + client.LastName + " tickets"; //change it if you want
            this.ClientId = client.Id;
            this.InitializeTicketList(client);

            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.OpenClientTabCommand = new RelayCommand(this.OpenClientTabExecute);
            this.ManageTicketCommand = new RelayCommand(this.ManageTicketTabExecute);
        }

        public int ClientId { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }
        public RelayCommand ManageTicketCommand { get; set; }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public string Header { get; private set; }

        public bool ShowCloseButton => true;

        public List<Ticket> Tickets { get; set; }

        public void OpenClientTabExecute()
        {
            if (this.mClient != null)
            {
                MainWindowViewModel.Instance.SetClientManageClientTab(this.mClient);
            }
        }

        public void ManageTicketTabExecute()
        {
            if(this.mTicket != null)
            {
                MainWindowViewModel.Instance.OpenManageTicketTab(this.mTicket);
            }
        }

        private void InitializeTicketList(Client client)
        {
            this.Tickets = Data.Fitness.GetAllTicketOfAClient(client);
        }
    }
}
