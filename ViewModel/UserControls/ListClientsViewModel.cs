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
    public class ListClientsViewModel : ViewModelBase, IListClientsContent
    {
        public string Header => "List of clients";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => true;

        public List<Client> ClientsList { get; set; }

        public ListClientsViewModel()
        {
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.ClientsList = Data.Fitness.GetClients();
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
