using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
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

namespace Fitness.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<IFitnessContent> contents;
        private IFitnessContent selectedContent;
        private static readonly MainWindowViewModel instance = new MainWindowViewModel();
        private bool isSignedIn = false;
        private bool shouldSignIn = true;

        private User signedInUser;
        public User SignedInUser {
            get => signedInUser;
            set {
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

        public SecureString SecurePassword { private get; set; }

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
            foreach(var item in Contents.Reverse())
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

        private void GenerateHomeView(bool isAdmin)
        {
            this.Contents = new ObservableCollection<IFitnessContent>();
            IHomeContent homeViewModel = new HomeViewModel(isAdmin);
            this.contents.Add(homeViewModel);

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

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public void SignInDialog(ViewModelBase viewModel)
        {
            //_________________________TODO _________________________________________________________________________________________________________
            string password = "";
            if (SecurePassword?.Length>0)
            {
                IntPtr stringPointer = Marshal.SecureStringToBSTR(SecurePassword);
                //string normalString = Marshal.PtrToStringBSTR(stringPointer);
                //Marshal.ZeroFreeBSTR(stringPointer);
                //Console.WriteLine(normalString);
                password = GetHashString(Marshal.PtrToStringBSTR(stringPointer));
            }
            
            //get pwd from DB
            string dbPassword=""; //= get pwd where username == Username 
            
            //compare the pwd's
            if (password.Equals(dbPassword))
            {
                this.IsSignedIn = true;

                //get user role
                string role = ""; //= get role where username == Username
                this.GenerateHomeView(role.ToLower().Equals("admin") ? true : false);
            }
            else
            {
                PopupMessage.OkButtonPopupMessage("Incorrect credentials", "Incorrect username or password");
            }
            Username = "";
            SecurePassword?.Clear();
        }

        public void SetClientToClientOperationsTab(Client client)
        {
            //searching for duplicates of the exact same tab (same state...)
            IClientOperationsContent clientOpContent = this.Contents.FirstOrDefault(c => c is IClientOperationsContent && (c as IClientOperationsContent).ClientId == client.Id) as IClientOperationsContent;
            if (clientOpContent == null)
            {
                ClientOperationsViewModel clientOpViewModel = new ClientOperationsViewModel();
                clientOpViewModel.Client = client;
                this.Contents.Add(clientOpViewModel);

                this.SelectedContent = this.Contents.LastOrDefault();  //has at least 2 elements, but requires more attention(won't be last) if shopping basket is in the list too
            }
            else
            {
                this.SelectedContent = clientOpContent;
            }
        }
    }
}
