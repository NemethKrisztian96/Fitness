using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Model;
using Fitness.ViewModel.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<IFitnessContent> contents;
        private IFitnessContent selectedContent;
        private static readonly MainWindowViewModel instance = new MainWindowViewModel();
        private bool isSignedIn = true;

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

            this.GenerateContents();
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

        private void GenerateContents()
        {
            this.Contents = new ObservableCollection<IFitnessContent>();
            IHomeContent homeViewModel = new HomeViewModel();
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

        public void LogOutDialog(ViewModelBase viewModel)
        {
            this.SignedInUser = null;
        }
    }
}
