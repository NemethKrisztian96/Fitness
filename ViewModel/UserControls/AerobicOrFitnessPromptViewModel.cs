using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel.UserControls
{
    public class AerobicOrFitnessPromptViewModel : ViewModelBase
    {
        private string barcode;
        private int ticketId;
        private int inserterId;

        public RelayCommand AerobicCommand { get; set; }
        public RelayCommand FitnessCommand { get; set; }

        public AerobicOrFitnessPromptViewModel(string barcode, int ticketId, int inserterId)
        {
            this.barcode = barcode;
            this.inserterId = inserterId;
            this.ticketId = ticketId;
            this.AerobicCommand = new RelayCommand(this.AerobicExecute);
            this.FitnessCommand = new RelayCommand(this.FitnessExecute);
        }

        public void AerobicExecute()
        {
            CreateNewEntry("Aerobic");
            ConfirmMessage();
        }

        public void FitnessExecute()
        {
            CreateNewEntry("Fitness");
            ConfirmMessage();
        }

        private void CreateNewEntry(string entryType)
        {
            Data.Fitness.AddEntry(this.barcode, this.inserterId, this.ticketId, entryType);
        }

        private void ConfirmMessage()
        {
            PopupMessage.OkButtonPopupMessage("Success", "The client can enter the gym");
        }
    }
}
