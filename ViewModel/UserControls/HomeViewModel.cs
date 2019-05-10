using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel.UserControls
{
    public class HomeViewModel : ViewModelBase, IHomeContent
    {
        private List<Client> clients;
        private string searchBarcode;
        private string searchName;
        private Client selectedClient;

        public bool IsAdmin { get; private set; }

        public string Header => "Home";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => false;

        public string SearchBarcode
        {
            get
            {
                return this.searchBarcode;
            }
            set
            {
                this.searchBarcode = value;
                this.SearchName = "";
                this.RaisePropertyChanged();
            }
        }

        public string SearchName
        {
            get
            {
                return this.searchName;
            }
            set
            {
                this.searchName = value;
                this.searchBarcode = "";
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand CreateClientCommand { get; set; }
        public RelayCommand SearchClientCommand { get; set; }
        public RelayCommand ListTicketsCommand { get; set; }
        public RelayCommand ReportsCommand { get; set; }
        public RelayCommand ListClientsCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }

        public int UserId { get; private set; }

        public bool ShowClientsList { get; set; }

        public Client SelectedClient
        {
            get
            {
                return this.selectedClient;
            }
            set
            {
                this.selectedClient = value;
                this.RaisePropertyChanged();
            }
        }
        public List<Client> Clients
        {
            get
            {
                return this.clients;
            }
            set
            {
                this.clients = value;
                this.RaisePropertyChanged();
            }
        }

        public HomeViewModel(int userId, bool isAdmin)
        {
            this.ShowClientsList = false;
            IsAdmin = isAdmin;
            UserId = userId;
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.CreateClientCommand = new RelayCommand(this.CreateClientExecute);
            this.SearchClientCommand = new RelayCommand(this.SearchClientExecute);
            this.OpenClientTabCommand = new RelayCommand(this.OpenClientTabExecute);
            if (IsAdmin)
            {
                this.ListTicketsCommand = new RelayCommand(this.ListTicketsExecute);
                this.ReportsCommand = new RelayCommand(this.ReportsExecute);
                this.ListClientsCommand = new RelayCommand(this.ListClientsExecute);
            }
        }

        private void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        private void CreateClientExecute()
        {
            this.ShowClientsList = false;
            MainWindowViewModel.Instance.CreateAddNewClientTab(UserId);
        }

        private void SearchClientExecute()
        {
            this.ShowClientsList = false;
            //________________TODO_________________________________________________________
            //checks if search is by barcode or name
            //if it is by barcode => open new tab
            //if it is by name 
            if (Clients?.Count > 1)
            {   //there are multiple results => show a list of clients first


                this.ShowClientsList = true;
            }
            else
            {   //there is only one result => open the new tab

            }

            //if no results found
            PopupMessage.OkButtonPopupMessage("No client found", "Please check if the barcode or the name is correct");
        }

        private void ListTicketsExecute()
        {
            this.ShowClientsList = false;
            //________________TODO_________________________________________________________
        }

        private void ReportsExecute()
        {
            this.ShowClientsList = false;
            //________________TODO_________________________________________________________
        }

        private void ListClientsExecute()
        {
            this.ShowClientsList = false;
            //________________TODO_________________________________________________________
        }

        private void OpenClientTabExecute()
        {
            this.ShowClientsList = false;
            if (this.SelectedClient != null)
            {
                MainWindowViewModel.Instance.SetClientToClientOperationsTab(this.selectedClient);
            }
        }
    }
}
