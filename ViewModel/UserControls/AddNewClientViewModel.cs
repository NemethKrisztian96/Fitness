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
    public class AddNewClientViewModel : INewClientContent
    {
        private User user;
        public string Header => "Add new client";

        public RelayCommand CloseTabItemCommand { get; set; }

        public bool ShowCloseButton => true;

        public AddNewClientViewModel(int userId)
        {
            this.user = user;
            this.CloseTabItemCommand = new RelayCommand(this.ClosetabItemExecute);
        }

        private void ClosetabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
