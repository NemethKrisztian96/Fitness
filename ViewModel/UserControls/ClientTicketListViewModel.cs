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
using Fitness.Common.Helpers;

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
            if (File.Exists(client.ImagePath))
            {
                this.ClientImage = Image.FromFile(client.ImagePath);
            }
            this.InitializeTicketList(client);

            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.OpenClientTabCommand = new RelayCommand(this.OpenClientTabExecute);
            //this.DeleteClientCommand = new RelayCommand(this.DeleteClientExecute);
            this.ManageTicketCommand = new RelayCommand(this.ManageTicketTabExecute);
        }

        public int ClientId { get; set; }
        public Image ClientImage { get; set; }

        public Ticket OriginalTicket { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }
        //public RelayCommand DeleteClientCommand { get; set; }
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

        /*public void DeleteClientExecute()
        {
            bool ans = PopupMessage.YesNoButtonPopupMessage("Warning!", "Are you sure you want to delete this client?");
            if (ans)
            {
                Data.Fitness.DeleteClient(mClient);
                CloseTabItemExecute();
            }
        }*/

        public void ManageTicketTabExecute()
        {
            if(this.OriginalTicket != null)
            {
                MainWindowViewModel.Instance.OpenManageTicketTab(this.OriginalTicket);
            }
        }

        private void InitializeTicketList(Client client)
        {
            this.Tickets = Data.Fitness.GetAllTicketOfAClient(client);
        }
    }
}
