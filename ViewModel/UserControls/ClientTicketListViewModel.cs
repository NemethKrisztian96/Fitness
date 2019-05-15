using Fitness.Common.MVVM;
using Fitness.Common.FitnessTabContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Model;
using Fitness.Logic;
using System.Drawing;
using System.IO;

namespace Fitness.ViewModel.UserControls
{
    class ClientTicketListViewModel: ViewModelBase, IClientTickets
    {
        private Client mClient;

        public ClientTicketListViewModel(Client client)
        {
            this.mClient = client;
            this.Header = client.FirstName + " " + client.LastName + " tickets"; //change it if you want
            this.ClientId = client.Id;
            if (File.Exists(client.ImagePath))
            {
                this.ClientImage = Image.FromFile(client.ImagePath);
            }
            this.InitializeTicketList(client);
            //todo add buttons


            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.OpenClientTabCommand = new RelayCommand(this.OpenClientTabExecute);
        }

        public int ClientId { get; set; }
        public Image ClientImage { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }

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

        private void InitializeTicketList(Client client)
        {
            this.Tickets = Data.Fitness.GetAllTicketOfAClient(client);
        }
    }
}
