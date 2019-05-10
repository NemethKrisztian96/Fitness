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
    public class ClientOperationsViewModel : IClientOperationsContent
    {
        public Client Client { get; set; }

        public int ClientId { get; set; }

        public string Header => Client.FirstName + " " + Client.LastName;

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => true;

        public ClientOperationsViewModel()
        {
            this.CloseTabItemCommand = new RelayCommand(this.ClosetabItemExecute);
        }

        private void ClosetabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
