using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using Fitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel.UserControls
{
    public class ManageUserViewModel : ViewModelBase, IManageUserContent
    {
        private string userUsername;
        private string userFirstname;
        private string userLastname;
        private string userRole;
        private string userStatus;
        private double userSalary;

        public string Header { get; set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }

        public bool ShowCloseButton => true;

        public int UserId { get; set; }
        public User User { get; private set; }

        public bool IsDeleteButtonVisible { get; private set; }
        public List<string> StatusList { get; set; }
        public List<string> RoleList { get; set; }
        public string LabelContent { get; set; }
        public string ButtonContent { get; set; }

        public string UserUsername
        {
            get
            {
                return this.userUsername;
            }
            set
            {
                this.userUsername = value;
                this.RaisePropertyChanged();
            }
        }
        public string UserFirstname
        {
            get
            {
                return this.userFirstname;
            }
            set
            {
                this.userFirstname = value;
                this.RaisePropertyChanged();
            }
        }
        public string UserLastname
        {
            get
            {
                return this.userLastname;
            }
            set
            {
                this.userLastname = value;
                this.RaisePropertyChanged();
            }
        }
        public string UserRole
        {
            get
            {
                return this.userRole;
            }
            set
            {
                this.userRole = value;
                this.RaisePropertyChanged();
            }
        }
        public double UserSalary
        {
            get
            {
                return this.userSalary;
            }
            set
            {
                this.userSalary = value;
                this.RaisePropertyChanged();
            }
        }
        public string UserStatus
        {
            get
            {
                return this.userStatus;
            }
            set
            {
                this.userStatus = value;
                this.RaisePropertyChanged();
            }
        }
        public string UserPassword { get; set; }
        public SecureString SecurePassword1 { get; set; }
        public SecureString SecurePassword2 { get; set; }
        public bool PasswordChanged { get; set; }

        public ManageUserViewModel(User user = null)
        {
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.User = user;
            this.UserId = user?.Id ?? -1;
            if (this.UserId == -1)
            {
                this.Header = "Add new user";
                this.LabelContent = "Please fill out the following form:";
                this.ButtonContent = "Create the new user";
                this.IsDeleteButtonVisible = false;
            }
            else
            {
                this.Header = this.User.FirstName + " " + this.User.LastName;
                this.CompleteForm(this.User);
                this.LabelContent = "Please modify the data you want to change in the following form:";
                this.ButtonContent = "Modify the user";
                this.IsDeleteButtonVisible = true;
            }
            this.InitializeStatusList();
            this.InitializeRoleList();
            this.PasswordChanged = false;
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.AddUserCommand = new RelayCommand(this.AddUserExecute);
            this.DeleteUserCommand = new RelayCommand(this.DeleteUserExecute);
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void AddUserExecute()
        {
            if (FormIsValid())
            {
                if (this.UserId == -1)
                {   //create new User
                    Data.Fitness.AddUser(new User()
                    {
                        Id = Data.Fitness.GetNextUserId(),
                        FirstName = this.UserFirstname,
                        LastName = this.UserLastname,
                        Password = this.UserPassword,
                        Role = this.UserRole,
                        Salary = this.UserSalary,
                        UserName = this.UserUsername,
                        Status = this.UserStatus
                    });

                    //display confirmation 
                    PopupMessage.OkButtonPopupMessage("Done", "User added successfully!");
                }
                else
                {
                    //update User
                    this.User.FirstName = this.UserFirstname;
                    this.User.LastName = this.UserLastname;
                    this.User.Password = this.UserPassword;
                    this.User.Role = this.UserRole;
                    this.User.Salary = this.UserSalary;
                    this.User.UserName = this.UserUsername;
                    this.User.Status = this.UserStatus;
                    Data.Fitness.SaveAllChanges();

                    //display confirmation
                    PopupMessage.OkButtonPopupMessage("Done", "User modified successfully!");
                }
            }
            else
            {
                PopupMessage.OkButtonPopupMessage("Warning", "Some of the required details are incorrect or missing");
                return;
            }

            //close the tab
            MainWindowViewModel.Instance.RefreshUsersList();
            this.CloseTabItemCommand.Execute(this);
        }

        public void DeleteUserExecute()
        {
            this.User.Status = "Inactive";
            Data.Fitness.SaveAllChanges();
            PopupMessage.OkButtonPopupMessage("Done", "User has been deleted!");
            this.CloseTabItemCommand.Execute(this);
        }

        public void CompleteForm(User user)
        {
            this.UserFirstname = this.User.FirstName;
            this.UserLastname = this.User.LastName;
            this.UserPassword = this.User.Password;
            this.UserRole = this.User.Role;
            this.UserSalary = this.User.Salary;
            this.UserUsername = this.User.UserName;
            this.UserStatus = this.User.Status;
        }

        private bool FormIsValid()
        {
            if (this.UserUsername?.Length > 3 && (this.UserId > 0 || Data.Fitness.IsUserUsernameUnique(this.UserUsername)))
            {
                if (this.UserFirstname.Length > 2)
                {
                    if (this.UserLastname.Length > 2)
                    {
                        if (this.UserRole != null)
                        {
                            if (this.UserStatus != null)
                            {
                                if (this.UserSalary > 0)
                                {
                                    if (CheckPasswordsToMatch())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckPasswordsToMatch()
        {
            if (!this.PasswordChanged)
            {
                return true;
            }
            if (this.SecurePassword1?.Length > 0)
            {
                IntPtr stringPointer = Marshal.SecureStringToBSTR(SecurePassword1);
                string normalString = Marshal.PtrToStringBSTR(stringPointer);
                Marshal.ZeroFreeBSTR(stringPointer);
                if (this.SecurePassword2?.Length > 0)
                {
                    IntPtr stringPointer2 = Marshal.SecureStringToBSTR(SecurePassword1);
                    string normalString2 = Marshal.PtrToStringBSTR(stringPointer2);
                    Marshal.ZeroFreeBSTR(stringPointer2);
                    if (normalString == normalString2)
                    {
                        this.UserPassword = CustomHash.GetHashString(normalString);
                        return true;
                    }
                }
            }
            PopupMessage.OkButtonPopupMessage("Warning", "The passwords do not match!");
            return false;
        }

        private void InitializeStatusList()
        {
            this.StatusList = new List<string>();
            this.StatusList.Add("Active");
            this.StatusList.Add("Inactive");
            if (this.UserStatus == null)
            {
                this.UserStatus = this.StatusList.First();
            }
        }

        private void InitializeRoleList()
        {
            this.RoleList = new List<string>();
            this.RoleList.Add("Trainer");
            this.RoleList.Add("Admin");
            this.RoleList.Add("Manager");
            this.RoleList.Add("Receptionist");
            this.RoleList.Add("Janitor");
            this.RoleList.Add("Asistent");
            if (this.UserRole == null)
            {
                this.UserRole = this.RoleList.First();
            }
        }
    }
}
