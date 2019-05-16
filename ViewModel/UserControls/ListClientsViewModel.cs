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
        public RelayCommand ManageClientCommand { get; set; }
        public RelayCommand DeleteClientCommand { get; set; }

        public bool ShowCloseButton => true;

        public List<Client> ClientsList { get; set; }
        public Client SelectedClient { get; set; }

        public ListClientsViewModel()
        {
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.ClientsList = Data.Fitness.GetClients();
            this.ManageClientCommand = new RelayCommand(this.ManageClientExecute);
            this.DeleteClientCommand = new RelayCommand(this.DeleteClientExecute);
        }

        public void ManageClientExecute()
        {
            if (this.SelectedClient != null)
            {
                MainWindowViewModel.Instance.SetClientManageClientTab(this.SelectedClient);
            }
        }

        public void DeleteClientExecute()
        {
            if (this.SelectedClient != null)
            {
                this.SelectedClient.IsDeleted = true;
                Data.Fitness.SaveAllChanges();
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
