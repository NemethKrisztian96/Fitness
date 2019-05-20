using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using Fitness.Model;
using Fitness.ViewModel.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fitness.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<IFitnessContent> contents;
        private IFitnessContent selectedContent;
        private static readonly MainWindowViewModel instance = new MainWindowViewModel();
        private bool isSignedIn = false;
        private bool shouldSignIn = true;
        public SecureString securePassword;
        private User signedInUser;
        public User SignedInUser
        {
            get
            {
                return signedInUser;
            }
            set
            {
                signedInUser = value;
                if (signedInUser != null)
                {
                    this.IsSignedIn = true;
                }
                else
                {
                    this.IsSignedIn = false;
                }
                this.RaisePropertyChanged();
            }
        }

        static MainWindowViewModel() { }

        private MainWindowViewModel()
        {
            this.DeactivateExpiredTickets();
            this.CloseCommand = new RelayCommand(this.CloseCommandExecute);
            this.LogOutCommand = new RelayCommand(this.LogOutCommandExecute);
            this.SignInCommand = new RelayCommand(this.SignInCommandExecute);
        }

        public bool IsSignedIn
        {
            get
            {
                return this.isSignedIn;
            }
            set
            {
                this.isSignedIn = value;
                this.ShouldSignIn = !this.IsSignedIn;
                this.RaisePropertyChanged();
            }
        }

        public bool ShouldSignIn
        {
            get
            {
                return this.shouldSignIn;
            }
            private set
            {
                this.shouldSignIn = value;
                this.RaisePropertyChanged();
            }
        }

        public string Username { get; set; }

        public SecureString SecurePassword
        {
            get
            {
                return this.securePassword;
            }
            set
            {
                this.securePassword = value;
                this.RaisePropertyChanged();
            }
        }

        public static MainWindowViewModel Instance
        {
            get
            {
                return instance;
            }
        }

        public RelayCommand CloseCommand { get; private set; }

        public void CloseCommandExecute()
        {
            ViewService.CloseDialog(this);
        }

        public RelayCommand LogOutCommand { get; private set; }

        public void LogOutCommandExecute()
        {
            this.LogOutDialog(this);
        }

        private void LogOutDialog(ViewModelBase viewModel)
        {
            //TODO close all tabs
            foreach (var item in Contents.Reverse())
            {
                CloseTabItem(item);
            }
            this.SignedInUser = null;
        }

        public ObservableCollection<IFitnessContent> Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                this.contents = value;
                this.RaisePropertyChanged();
            }
        }

        public IFitnessContent SelectedContent
        {
            get
            {
                return this.selectedContent;
            }
            set
            {
                this.selectedContent = value;
                this.RaisePropertyChanged();
            }
        }

        private void GenerateHomeView(int userId, bool isAdmin)
        {
            this.Contents = new ObservableCollection<IFitnessContent>();
            IHomeContent homeViewModel = new HomeViewModel(userId, isAdmin);
            this.Contents.Add(homeViewModel);

            this.SelectedContent = this.Contents.First();  //not empty => no exception
        }

        public void CloseTabItem(IFitnessContent content)
        {
            if (content != null)
            {
                this.selectedContent = this.Contents.FirstOrDefault();
                this.Contents.Remove(content);
            }
        }

        public RelayCommand SignInCommand { get; private set; }

        public void SignInCommandExecute()
        {
            this.SignInDialog(this);
        }

        private void SignInDialog(ViewModelBase viewModel)
        {
            //Console.WriteLine(Username+"-"+ Marshal.PtrToStringBSTR(Marshal.SecureStringToBSTR(SecurePassword)));
            if ((Username?.Length ?? 0) < 1 || (SecurePassword?.Length ?? 0) < 1)
            {
                PopupMessage.OkButtonPopupMessage("Missing credentials", "Missing username or password");
            }
            else
            {
                string password = "";
                if (SecurePassword?.Length > 0)
                {
                    IntPtr stringPointer = Marshal.SecureStringToBSTR(SecurePassword);
                    string normalString = Marshal.PtrToStringBSTR(stringPointer);
                    Marshal.ZeroFreeBSTR(stringPointer);
                    //Console.WriteLine(normalString);
                    password = CustomHash.GetHashString(normalString);
                }
                //Console.WriteLine(password);
                //get pwd from DB
                string dbPassword = Data.Fitness.GetPassword(Username);
                //Console.WriteLine(dbPassword);
                
                //compare the pwd's
                if (password.Equals(dbPassword))
                {
                    this.IsSignedIn = true;

                    //get userId and role
                    int userId = Data.Fitness.GetUserId(Username);
                    string role = Data.Fitness.GetRole(Username);
                    this.SignedInUser = Data.Fitness.GetUserById(userId);
                    this.GenerateHomeView(userId, role.ToLower().Equals("admin") ? true : false);
                }
                else
                {
                    PopupMessage.OkButtonPopupMessage("Incorrect credentials", "Incorrect username or password");
                }
            }
            //Username = "";
            //SecurePassword?.Clear();
        }

        public void SetClientManageClientTab(Client client)
        {
            //searching for duplicates of the exact same tab (same state...)
            IManageClientContent clientManContent = this.Contents.FirstOrDefault(c => c is IManageClientContent && (c as IManageClientContent).ClientId == client.Id) as IManageClientContent;
            if (clientManContent == null)
            {
                ManageClientViewModel clientManViewModel = new ManageClientViewModel(SignedInUser,false,client);
                this.Contents.Add(clientManViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = clientManContent;
            }
        }

        public void CreateAddNewClientTab()
        {
            //searching for duplicates of the exact same tab (same state...)
            IManageClientContent clientManContent = this.Contents.FirstOrDefault(c => c is IManageClientContent && (c as IManageClientContent).ClientId == -1) as IManageClientContent;
            if (clientManContent == null)
            {
                ManageClientViewModel clientManViewModel = new ManageClientViewModel(this.SignedInUser,true);
                this.Contents.Add(clientManViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = clientManContent;
            }
        }

        public void OpenListTicketTypeTab()
        {
            //searching for duplicates of the exact same tab (same state...)
            IListTicketTypesContent listTicketTypesContent = this.Contents.FirstOrDefault(c => c is IListTicketTypesContent) as IListTicketTypesContent;
            if (listTicketTypesContent == null)
            {
                ListTicketTypesViewModel listTicketTypesViewModel = new ListTicketTypesViewModel();
                this.Contents.Add(listTicketTypesViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = listTicketTypesContent;
            }
        }

        public void OpenManageTicketTypeTab(TicketType ticketType)
        {
            //searching for duplicates of the exact same tab (same state...)
            IManageTicketTypeContent manTicketTypesContent = this.Contents.FirstOrDefault(c => c is IManageTicketTypeContent && (c as IManageTicketTypeContent).TicketTypeId == ticketType.Id) as IManageTicketTypeContent;
            if (manTicketTypesContent == null)
            {
                ManageTicketTypesViewModel ticketTypesManViewModel = new ManageTicketTypesViewModel(ticketType);
                this.Contents.Add(ticketTypesManViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = manTicketTypesContent;
            }
        }

        public void OpenAddNewTicketTypeTab()
        {
            //searching for duplicates of the exact same tab (same state...)
            IManageTicketTypeContent manTicketTypesContent = this.Contents.FirstOrDefault(c => c is IManageTicketTypeContent && (c as IManageTicketTypeContent).TicketTypeId == -1) as IManageTicketTypeContent;
            if (manTicketTypesContent == null)
            {
                ManageTicketTypesViewModel ticketTypesManViewModel = new ManageTicketTypesViewModel();
                this.Contents.Add(ticketTypesManViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = manTicketTypesContent;
            }
        }

        public void RefreshTicketTypeList()
        {
            IListTicketTypesContent listTicketTypesContent = this.Contents.FirstOrDefault(c => c is IListTicketTypesContent) as IListTicketTypesContent;
            if (listTicketTypesContent != null)
            {
                ListTicketTypesViewModel listTicketTypesViewModel = listTicketTypesContent as ListTicketTypesViewModel;
                if (listTicketTypesViewModel != null)
                {
                    listTicketTypesViewModel.RefreshList();
                }
            }
        }

        public void OpenListUsersTab()
        {
            //searching for duplicates of the exact same tab (same state...)
            IListUsersContent listUsersContent = this.Contents.FirstOrDefault(c => c is IListUsersContent) as IListUsersContent;
            if (listUsersContent == null)
            {
                ListUsersViewModel listUsersViewModel = new ListUsersViewModel();
                this.Contents.Add(listUsersViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = listUsersContent;
            }
        }

        public void OpenListClientsTab()
        {
            //searching for duplicates of the exact same tab (same state...)
            IListClientsContent listClientsContent = this.Contents.FirstOrDefault(c => c is IListClientsContent) as IListClientsContent;
            if (listClientsContent == null)
            {
                ListClientsViewModel listClientsViewModel = new ListClientsViewModel();
                this.Contents.Add(listClientsViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = listClientsContent;
            }
        }

        public void OpenManageUserTab(User user=null)
        {
            //searching for duplicates of the exact same tab (same state...)
            IManageUserContent manUserContent = this.Contents.FirstOrDefault(c => c is IManageUserContent && (c as IManageUserContent).UserId == (user?.Id ?? -1)) as IManageUserContent;
            if (manUserContent == null)
            {
                ManageUserViewModel userManViewModel = new ManageUserViewModel(user);
                this.Contents.Add(userManViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = manUserContent;
            }
        }

        public void RefreshUsersList()
        {
            IListUsersContent listUsersContent = this.Contents.FirstOrDefault(c => c is IListUsersContent) as IListUsersContent;
            if (listUsersContent != null)
            {
                ListUsersViewModel listUsersViewModel = listUsersContent as ListUsersViewModel;
                if (listUsersViewModel != null)
                {
                    listUsersViewModel.RefreshList();
                }
            }
        }

        public void OpenClientTicketsTab(Client client)
        {
            IClientTickets clientTickets = this.Contents.FirstOrDefault(c => c is IClientTickets && (c as IClientTickets).ClientId == client.Id) as IClientTickets;

            if (clientTickets == null)
            {
                ClientTicketListViewModel clientTicketViewModel = new ClientTicketListViewModel(client);
                this.Contents.Add(clientTicketViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();
            }
            else
            {
                this.SelectedContent = clientTickets;
            }
        }
        public void RefreshClientTickets(Client client)
        {
            IClientTickets clientTickets = this.Contents.FirstOrDefault(c => c is IClientTickets && (c as IClientTickets).ClientId == client.Id) as IClientTickets;
            if (clientTickets != null)
            {
                ClientTicketListViewModel clientTicketViewModel = clientTickets as ClientTicketListViewModel;
                if (clientTicketViewModel != null)
                {
                    clientTicketViewModel.RefreshList(client);
                }
            }
        }

        public void OpenManageTicketTab(Ticket mTicket)
        {
            IManageTicketContent manTicketContent = this.Contents.FirstOrDefault(c => c is IManageTicketContent && (c as IManageTicketContent).TicketId == mTicket.Id) as IManageTicketContent;

            if (manTicketContent == null)
            {
                ManageTicketViewModel manTicketViewModel = new ManageTicketViewModel(mTicket);
                this.Contents.Add(manTicketViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = manTicketContent;
            }
        }

        public void SendEmailTab()
        {
            IEmailContent emailContent = this.Contents.FirstOrDefault(c => c is IEmailContent) as IEmailContent;

            if (emailContent == null)
            {
                SendEmailViewModel emailViewModel = new SendEmailViewModel();
                this.Contents.Add(emailViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = emailContent;
            }
        }

        public void OpenTicketStatisticsTab()
        {
            ITicketStatistics ticketStatistics = this.Contents.FirstOrDefault(c => c is ITicketStatistics) as ITicketStatistics;

            if (ticketStatistics == null)
            {
                TicketStatisticsViewModel ticketStatisticsViewModel = new TicketStatisticsViewModel();
                this.Contents.Add(ticketStatisticsViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = ticketStatistics;
            }
        }

        public void OpenListEntriesTab()
        {
            IListEntriesContent listEntriesContent = this.Contents.FirstOrDefault(c => c is IListEntriesContent) as IListEntriesContent;

            if (listEntriesContent == null)
            {
                ListEntriesViewModel listEntriesViewModel = new ListEntriesViewModel();
                this.Contents.Add(listEntriesViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = listEntriesContent;
            }
        }

        public void OpenNewTicketTab(Client client)
        {
            INewTicketContent newTicketContent = this.Contents.FirstOrDefault(c => c is INewTicketContent) as INewTicketContent;

            if (newTicketContent == null)
            {
                NewTicketViewModel newTicketViewModel = new NewTicketViewModel(SignedInUser.Id, client.Id);
                this.Contents.Add(newTicketViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least one element
            }
            else
            {
                this.SelectedContent = newTicketContent;
            }
        }

        private void DeactivateExpiredTickets()
        {
            Data.Fitness.RefreshTicketsStatus();
        }
    }
}
