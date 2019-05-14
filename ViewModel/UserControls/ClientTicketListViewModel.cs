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
        public ClientTicketListViewModel(Client client = null)
        {
            this.InitializeTicketList(client);

            //todo add buttons


            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public int ClientId { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public string Header { get; private set; }

        public bool ShowCloseButton => true;

        public List<Ticket> Tickets { get; set; }

        private void InitializeTicketList(Client client)
        {
            this.Tickets = Data.Fitness.GetAllTicketOfAClient(client);
        }
    }
}
