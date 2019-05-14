﻿using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
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
    public class HomeViewModel : ViewModelBase, IHomeContent
    {
        private List<Client> clients;
        private string searchBarcode;
        private string searchName;
        private Client selectedClient;
        private bool showClientsList;

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
                //this.searchBarcode = "";
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand CreateClientCommand { get; set; }
        public RelayCommand SearchClientCommand { get; set; }
        public RelayCommand ListTicketTypesCommand { get; set; }
        public RelayCommand ReportsCommand { get; set; }
        public RelayCommand ListUsersCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }
        public RelayCommand ListClientTicketsCommand { get; set; }

        public int UserId { get; private set; }

        public bool ShowClientsList
        {
            get
            {
                return this.showClientsList;
            }
            set
            {
                this.showClientsList = value;
                this.RaisePropertyChanged();
            }
        }

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
            this.Clients = new List<Client>();
            this.SearchBarcode = "";
            this.SearchName = "";
            this.ShowClientsList = false;
            IsAdmin = isAdmin;
            UserId = userId;
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.CreateClientCommand = new RelayCommand(this.CreateClientExecute);
            this.SearchClientCommand = new RelayCommand(this.SearchClientExecute);
            this.OpenClientTabCommand = new RelayCommand(this.OpenClientTabExecute);
            this.ListClientTicketsCommand = new RelayCommand(this.OpenClientTicketsExecute);
            if (IsAdmin)
            {
                this.ListTicketTypesCommand = new RelayCommand(this.ListTicketTypesExecute);
                this.ReportsCommand = new RelayCommand(this.ReportsExecute);
                this.ListUsersCommand = new RelayCommand(this.ListUsersExecute);
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void CreateClientExecute()
        {
            this.ShowClientsList = false;
            MainWindowViewModel.Instance.CreateAddNewClientTab();
        }

        public void SearchClientExecute()
        {
            this.ShowClientsList = false;
            this.Clients.Clear();
            //________________TODO_________________________________________________________
            //checks if search is by barcode or name
            if (SearchBarcode.Length > 0)
            {   //if it is by barcode => open new tab
                Client client = Data.Fitness.GetClientByBarcode(SearchBarcode);
                if (client != null)
                {
                    MainWindowViewModel.Instance.SetClientManageClientTab(client);
                }
                else
                {
                    PopupMessage.OkButtonPopupMessage("Client not found", "Please check if the barcode is correct");
                }
            }
            else
            {   //if it is by name 
                this.Clients = Data.Fitness.GetClientsByName(SearchName);
                if (this.Clients.Count > 1)
                {   //there are multiple results => show a list of clients first
                    this.ShowClientsList = true;
                    //todo what else??...
                }
                else
                {
                    if (this.Clients.Count == 1)
                    {   //there is only one result => open the new tab
                        MainWindowViewModel.Instance.SetClientManageClientTab(this.Clients.First());
                    }
                    else
                    {   //no results found
                        PopupMessage.OkButtonPopupMessage("No results found", "There is no client with the given name");
                    }
                }
            }
        }

        public void ListTicketTypesExecute()
        {
            this.ShowClientsList = false;
            MainWindowViewModel.Instance.OpenListTicketTypeTab();
        }

        public void ReportsExecute()
        {
            this.ShowClientsList = false;
            //________________TODO_________________________________________________________
        }

        public void ListUsersExecute()
        {
            this.ShowClientsList = false;
            MainWindowViewModel.Instance.OpenListUsersTab();
        }

        public void OpenClientTabExecute()
        {
            this.ShowClientsList = false;
            if (this.SelectedClient != null)
            {
                MainWindowViewModel.Instance.SetClientManageClientTab(this.SelectedClient);
            }
        }

        public void OpenClientTicketsExecute()
        {
            this.ShowClientsList = false;
            if (this.SelectedClient != null) //for a specific client
            {
                MainWindowViewModel.Instance.OpenClientTicketsTab(this.SelectedClient);
            }
        }
    }
}
