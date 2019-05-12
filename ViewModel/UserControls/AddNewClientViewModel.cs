using Fitness.Common.FitnessTabContents;
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
    public class AddNewClientViewModel : ViewModelBase, INewClientContent
    {
        public User Inserter { get; set; }
        public string Header => "Add new client";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => true;

        public string Barcode { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public AddNewClientViewModel(User user)
        {
            this.Inserter = user;
            this.CloseTabItemCommand = new RelayCommand(this.ClosetabItemExecute, this.CloseTabItemCanExecute);
            this.BirthDate = new DateTime(1980,1,1);
            this.RaisePropertyChanged();
        }

        public void ClosetabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public bool CloseTabItemCanExecute()
        {
            return true;
        }
    }
}
