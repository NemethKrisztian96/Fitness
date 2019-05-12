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
        private bool formIsVisible = true;

        public bool FormIsVisible
        {
            get
            {
                return this.formIsVisible;
            }
            set
            {
                this.formIsVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public User Inserter { get; set; }
        public string Header => "Add new client";

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand CreateClientCommand { get; set; }

        public bool ShowCloseButton => true;

        public string Barcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }

        public AddNewClientViewModel(User user)
        {
            this.Inserter = user;
            this.CloseTabItemCommand = new RelayCommand(this.ClosetabItemExecute);
            this.CreateClientCommand = new RelayCommand(this.CreateClientExecute, this.CreateClientCanExecute);
            this.BirthDate = new DateTime(1980,1,1);
            this.RaisePropertyChanged();
        }

        public void ClosetabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }

        public void CreateClientExecute()
        {
            if (!this.IsValidForm())
            {
                PopupMessage.OkButtonPopupMessage("Warning", "Some of the required details are incorrect or missing");
                return;
            }

            //prompt to take picture
            bool takePicture = PopupMessage.YesNoButtonPopupMessage("Picture", "Would you like to take a picture of the client?");
            if (takePicture)
            {   //take picture
                this.FormIsVisible = false;
                //this.ImagePath =
            }
            else
            {
                this.ImagePath = "";
            }

            //create client
            Client client = new Client() {
                Id = Data.Fitness.GetNextClientId(),
                BarCode = this.Barcode,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Sex = this.Gender[0].ToString().ToUpper(),
                BirthDate = this.BirthDate,
                InsertDate = DateTime.Today,
                Inserter = this.Inserter,
                InserterId = this.Inserter.Id,
                ImagePath = this.ImagePath
            };

            //add client do DB
            Data.Fitness.AddClient(client);

            //display confirmation and close the tab
            PopupMessage.OkButtonPopupMessage("Done", "Client added successfully!");
            this.CloseTabItemCommand.Execute(this);
        }

        public bool CreateClientCanExecute()
        {
            if (this.Barcode?.Length > 5)
            {
                if (this.FirstName?.Length > 3)
                {
                    if(this.LastName?.Length > 3)
                    {
                        if(this.PhoneNumber?.Length>3 || (this.Email?.Length > 6))
                        {
                            if (this.Gender?.Length > 3)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool IsValidForm()
        {
            return false;
        }
    }
}
