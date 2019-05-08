using Fitness.Common.FitnessTabContents;
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

        public bool IsAdmin => false;

        public string Header => "Home";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => false;

        public int UserId => -1; //TODO

        public HomeViewModel()
        { 
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        private void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
