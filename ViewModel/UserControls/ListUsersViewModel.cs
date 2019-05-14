using Fitness.Common.FitnessTabContents;
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
    public class ListUsersViewModel : ViewModelBase, IListUsersContent
    {
        private List<User> usersList;
        private User selectedUser;

        public List<User> UsersList
        {
            get
            {
                return this.usersList;
            }
            set
            {
                this.usersList = value;
                this.RaisePropertyChanged();
            }
        }
        public User SelectedUser
        {
            get
            {
                return this.selectedUser;
            }
            set
            {
                this.selectedUser = value;
                this.RaisePropertyChanged();
            }
        }

        public string Header => "Users";

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand AddNewUserCommand { get; set; }
        public RelayCommand ManageUserCommand { get; set; }

        public bool ShowCloseButton => true;

        public ListUsersViewModel()
        {
            this.UsersList = Data.Fitness.GetUsers();
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.AddNewUserCommand = new RelayCommand(this.AddNewUserExecute);
            this.ManageUserCommand = new RelayCommand(this.ManageUserExecute);
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void AddNewUserExecute()
        {
            MainWindowViewModel.Instance.OpenManageUserTab();
        }

        public void ManageUserExecute()
        {
            if (this.SelectedUser != null)
            {
                MainWindowViewModel.Instance.OpenManageUserTab(this.SelectedUser);
            }
        }

        public void RefreshList()
        {
            this.UsersList = Data.Fitness.GetActiveUsers();
        }
    }
}
