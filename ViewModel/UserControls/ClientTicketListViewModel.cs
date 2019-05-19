﻿using Fitness.Common.MVVM;
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
using System.Windows.Forms;
using System.Windows;

namespace Fitness.ViewModel.UserControls
{
    class ClientTicketListViewModel : ViewModelBase, IClientTickets
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
            this.UseTicketCommand = new RelayCommand(this.UseTicketExecute);
            this.ExtendTicketCommand = new RelayCommand(this.ExtendTicketExecute);
            this.OneTimeEntryCommand = new RelayCommand(this.OneTimeEntryExecute);
        }

        public int ClientId { get; set; }
        public Image ClientImage { get; set; }

        public Ticket OriginalTicket { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand OpenClientTabCommand { get; set; }
        //public RelayCommand DeleteClientCommand { get; set; }
        public RelayCommand ManageTicketCommand { get; set; }
        public RelayCommand UseTicketCommand { get; set; }
        public RelayCommand ExtendTicketCommand { get; set; }
        public RelayCommand OneTimeEntryCommand { get; set; }

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
            if (this.OriginalTicket != null)
            {
                MainWindowViewModel.Instance.OpenManageTicketTab(this.OriginalTicket);
            }
        }

        private void InitializeTicketList(Client client)
        {
            this.Tickets = Data.Fitness.GetAllTicketOfAClient(client);
        }

        public void ExtendTicketExecute()
        {
            //___________________________________________TODO_________________________________________
        }

        public void UseTicketExecute()
        {
            if (OriginalTicket.Status.ToLower() == "active")
            {
                //AerobicOrFitnessPromptViewModel promptVM = new AerobicOrFitnessPromptViewModel(this.mClient.BarCode, this.OriginalTicket.Id, MainWindowViewModel.Instance.SignedInUser.Id);

                this.AddEntry(this.OriginalTicket.Id);

                this.NotifyExpiration();
            }
            else
            {
                PopupMessage.OkButtonPopupMessage("Warning", "The selected ticket is expired!");
            }

        }

        public void OneTimeEntryExecute()
        {
            this.AddEntry(0);
        }

        private void AddEntry(int ticketId)
        {
            MessageBoxResult result = WPFCustomMessageBox.CustomMessageBox.ShowYesNo("Please select what activity will be the client doing", "Activity", "Aerobic", "Fitness");
            Console.WriteLine(result);
            if (result == MessageBoxResult.Yes)
            {
                CreateNewEntry(ticketId, "Aerobic");
                ConfirmMessage();
            }
            else
            {
                if (result == MessageBoxResult.No)
                {
                    CreateNewEntry(ticketId, "Fitness");
                    ConfirmMessage();
                }
            }
        }

        private void CreateNewEntry(int ticketId, string entryType)
        {
            Data.Fitness.AddEntry(this.mClient.BarCode, MainWindowViewModel.Instance.SignedInUser.Id, ticketId, entryType);
        }

        private void ConfirmMessage()
        {
            PopupMessage.OkButtonPopupMessage("Success", "The client can enter the gym");
        }

        private void NotifyExpiration()
        {
            if (OriginalTicket?.ExpirationDate.Date.CompareTo(DateTime.Today.AddDays(-2)) < 0 || OriginalTicket.MaxLoginNumber - Data.Fitness.GetLoginCount(OriginalTicket.Id) < 2)
            {
                PopupMessage.OkButtonPopupMessage("Attention", "The client's ticket will expire soon!");
            }
        }
    }
}
