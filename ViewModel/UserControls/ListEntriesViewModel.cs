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
    public class ListEntriesViewModel : ViewModelBase, IListEntriesContent
    {
        private List<Entry> entriesList;
        private string searchBarcode;
        private string searchName;
        private string selectedTicketType;
        private DateTime? searchDate;

        public string Header => "Entries";

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

        public bool ShowCloseButton => true;

        public ListEntriesViewModel()
        {
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.FilterCommand = new RelayCommand(this.FilterExecute, this.FilterCanExecute);
            this.ResetCommand = new RelayCommand(this.ResetExecute, this.FilterCanExecute);
            this.EntriesList = Data.Fitness.GetEntries();
            this.TicketTypesList = new List<string>();
            this.TicketTypesList.Add("");
            this.TicketTypesList.AddRange(Data.Fitness.GetTicketTypeNames());
        }

        public List<Entry> EntriesList
        {
            get
            {
                return this.entriesList;
            }
            set
            {
                this.entriesList = value;
                this.RaisePropertyChanged();
            }
        }

        public string SearchBarcode
        {
            get
            {
                return this.searchBarcode;
            }
            set
            {
                this.searchBarcode = value;
                this.RaisePropertyChanged();
            }
        }
        public string SearchName
        {
            get
            {
                return this.searchName;
            }
            set
            {
                this.searchName = value;
                this.RaisePropertyChanged();
            }
        }
        public List<string> TicketTypesList { get; set; }
        public string SelectedTicketType
        {
            get
            {
                return this.selectedTicketType;
            }
            set
            {
                this.selectedTicketType = value;
                this.RaisePropertyChanged();
            }
        }
        public DateTime? SearchDate
        {
            get { return this.searchDate; }
            set
            {
                this.searchDate = value;
                this.RaisePropertyChanged();
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void ResetExecute()
        {
            this.SearchBarcode = "";
            this.SearchName = "";
            this.SelectedTicketType = "";
            this.SearchDate = null;
            this.EntriesList = Data.Fitness.GetEntries();
        }

        public void FilterExecute()
        {
            bool isAccepted = true;
            List<Entry> list = new List<Entry>();
            foreach (Entry item in Data.Fitness.GetEntries())
            {
                isAccepted = true;
                if (isAccepted && !string.IsNullOrEmpty(this.SearchBarcode) && item.BarCode != this.SearchBarcode)
                {
                    isAccepted = false;
                }
                if (isAccepted && !string.IsNullOrEmpty(this.SearchName) && !(this.SearchName.Split(' ').ToList().Contains(item.UserTicket.Owner.LastName) || this.SearchName.Split(' ').ToList().Contains(item.UserTicket.Owner.FirstName)))
                {
                    isAccepted = false;
                }
                if (isAccepted && !string.IsNullOrEmpty(this.SelectedTicketType) && item.UserTicket.Type.Name != this.SelectedTicketType)
                {
                    isAccepted = false;
                }
                if (isAccepted && (this.SearchDate?.Date.CompareTo(item.LoginTime.Date) ?? 0) != 0)
                {
                    isAccepted = false;
                }
                if (isAccepted)
                {
                    list.Add(item);
                }
            }
            EntriesList = list;
            this.RaisePropertyChanged();
        }

        public bool FilterCanExecute()
        {
            if (!string.IsNullOrEmpty(this.SearchBarcode))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(this.SearchName))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(this.SelectedTicketType))
            {
                return true;
            }
            if (this.SearchDate?.Date.CompareTo(DateTime.Today.Date) <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
