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
    public class ManageTicketTypesViewModel : ViewModelBase, IManageTicketTypeContent
    {
        private string ticketTypeName;
        private int? ticketTypeDayNumber;
        private int? ticketTypeOccasionNumber;
        private int? ticketTypeMaximumUsagePerDay;
        private int? ticketTypeStartHour;
        private int? ticketTypeEndHour;
        private double ticketTypePrice;
        private string ticketTypeDescription;
        private string ticketTypeStatus;

        public string TicketTypeName
        {
            get
            {
                return this.ticketTypeName;
            }
            set
            {
                this.ticketTypeName = value;
                this.RaisePropertyChanged();
            }
        }
        public int? TicketTypeDayNumber
        {
            get
            {
                return this.ticketTypeDayNumber;
            }
            set
            {
                this.ticketTypeDayNumber = value;
                this.RaisePropertyChanged();
            }
        }
        public int? TicketTypeOccasionNumber
        {
            get
            {
                return this.ticketTypeOccasionNumber;
            }
            set
            {
                this.ticketTypeOccasionNumber = value;
                this.RaisePropertyChanged();
            }
        }
        public int? TicketTypeMaximumUsagePerDay
        {
            get
            {
                return this.ticketTypeMaximumUsagePerDay;
            }
            set
            {
                this.ticketTypeMaximumUsagePerDay = value;
                this.RaisePropertyChanged();
            }
        }
        public int? TicketTypeStartHour
        {
            get
            {
                return this.ticketTypeStartHour;
            }
            set
            {
                this.ticketTypeStartHour = value;
                this.RaisePropertyChanged();
            }
        }
        public int? TicketTypeEndHour
        {
            get
            {
                return this.ticketTypeEndHour;
            }
            set
            {
                this.ticketTypeEndHour = value;
                this.RaisePropertyChanged();
            }
        }
        public double TicketTypePrice
        {
            get
            {
                return this.ticketTypePrice;
            }
            set
            {
                this.ticketTypePrice = value;
                this.RaisePropertyChanged();
            }
        }
        public string TicketTypeDescription
        {
            get
            {
                return this.ticketTypeDescription;
            }
            set
            {
                this.ticketTypeDescription = value;
                this.RaisePropertyChanged();
            }
        }
        public string TicketTypeStatus
        {
            get
            {
                return this.ticketTypeStatus;
            }
            set
            {
                this.ticketTypeStatus = value;
                this.RaisePropertyChanged();
            }
        }

        public List<string> StatusList { get; set; }
        public string LabelContent { get; set; }
        public string ButtonContent { get; set; }

        public TicketType TicketType { get; set; }

        public string Header { get; private set; }

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand AddTicketTypeCommand { get; set; }
        public RelayCommand DeleteTicketTypeCommand { get; set; }

        public bool ShowCloseButton => true;

        public int TicketTypeId { get; set; }

        public ManageTicketTypesViewModel(TicketType ticketType = null)
        {
            this.TicketType = ticketType;
            this.TicketTypeId = ticketType?.Id ?? -1;
            if (this.TicketTypeId == -1)
            {
                this.Header = "Add new ticket type";
                this.LabelContent = "Please fill out the following form:";
                this.ButtonContent = "Create the new ticket type";
            }
            else
            {
                this.Header = this.TicketType.Name;
                this.CompleteForm(this.TicketType);
                this.LabelContent = "Please modify the data you want to change in the following form:";
                this.ButtonContent = "Modify the ticket type";
            }
            this.InitializeStatusList();
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.AddTicketTypeCommand = new RelayCommand(this.AddTicketTypeExecute);
            this.DeleteTicketTypeCommand = new RelayCommand(this.DeleteTicketTypeExecute);
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void AddTicketTypeExecute()
        {
            if (FormIsValid())
            {
                if (this.TicketTypeId == -1)
                {   //create new TicketType
                    Data.Fitness.AddTicketType(new TicketType()
                    {
                        Id = Data.Fitness.GetNextTicketTypeId(),
                        DayNumber = this.TicketTypeDayNumber,
                        Description = this.TicketTypeDescription,
                        EndHour = this.TicketTypeEndHour,
                        MaximumUsagePerDay = this.TicketTypeMaximumUsagePerDay,
                        Name = this.TicketTypeName,
                        OccasionNumber = this.TicketTypeOccasionNumber,
                        Price = this.TicketTypePrice,
                        StartHour = this.TicketTypeStartHour,
                        Status = this.TicketTypeStatus
                    });

                    //display confirmation 
                    PopupMessage.OkButtonPopupMessage("Done", "Client added successfully!");
                }
                else
                {
                    //update ticketType
                    this.TicketType.DayNumber = this.TicketTypeDayNumber;
                    this.TicketType.Description = this.TicketTypeDescription;
                    this.TicketType.EndHour = this.TicketTypeEndHour;
                    this.TicketType.MaximumUsagePerDay = this.TicketTypeMaximumUsagePerDay;
                    this.TicketType.Name = this.TicketTypeName;
                    this.TicketType.OccasionNumber = this.TicketTypeOccasionNumber;
                    this.TicketType.Price = this.TicketTypePrice;
                    this.TicketType.StartHour = this.TicketTypeStartHour;
                    this.TicketType.Status = this.TicketTypeStatus;
                    Data.Fitness.SaveAllChanges();

                    //display confirmation
                    PopupMessage.OkButtonPopupMessage("Done", "Ticket type modified successfully!");
                }
            }
            else
            {
                PopupMessage.OkButtonPopupMessage("Warning", "Some of the required details are incorrect or missing");
                return;
            }

            //close the tab
            MainWindowViewModel.Instance.RefreshTicketTypeList();
            this.CloseTabItemCommand.Execute(this);
        }

        public void DeleteTicketTypeExecute()
        {
            this.TicketType.Status = "Inactive";
            Data.Fitness.SaveAllChanges();
            PopupMessage.OkButtonPopupMessage("Done", "Ticket type has been deleted from the current offer!");
            this.CloseTabItemCommand.Execute(this);
        }

        public void CompleteForm(TicketType ticketType)
        {
            this.TicketTypeDayNumber = this.TicketType.DayNumber;
            this.TicketTypeDescription = this.TicketType.Description;
            this.TicketTypeEndHour = this.TicketType.EndHour;
            this.TicketTypeMaximumUsagePerDay = this.TicketType.MaximumUsagePerDay;
            this.TicketTypeName = this.TicketType.Name;
            this.TicketTypeOccasionNumber = this.TicketType.OccasionNumber;
            this.TicketTypePrice = this.TicketType.Price;
            this.TicketTypeStartHour = this.TicketType.StartHour;
            this.TicketTypeStatus = this.TicketType.Status;
        }

        private bool FormIsValid()
        {
            if (this.TicketTypeName.Length > 0)
            {
                if (this.TicketTypeDayNumber >= 1 && this.TicketTypeDayNumber <= 31)
                {
                    if (this.TicketTypeStartHour == null || this.TicketTypeStartHour <= 23 || this.TicketTypeStartHour >= 0)
                    {
                        if (this.TicketTypeEndHour == null || this.TicketTypeEndHour <= 23 && this.TicketTypeEndHour >= 0)
                        {
                            if (this.TicketTypeMaximumUsagePerDay == null || this.TicketTypeMaximumUsagePerDay > 0)
                            {
                                if (this.TicketTypeOccasionNumber == null || this.TicketTypeOccasionNumber > 0 && this.TicketTypeOccasionNumber <= 99)
                                {
                                    if (this.TicketTypePrice >= 0)
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

        private void InitializeStatusList()
        {
            this.StatusList = new List<string>();
            this.StatusList.Add("Active");
            this.StatusList.Add("Inactive");
            if (this.TicketTypeStatus == null)
            {
                this.TicketTypeStatus = this.StatusList.First();
            }
        }
    }
}
