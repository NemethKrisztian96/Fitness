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
    public class ManageClientViewModel : ViewModelBase, IManageClientContent
    {
        private bool isCreating;
        private bool formIsVisible = true;

        public Client Client { get; set; }

        public string Header { get; private set; }

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

        public string LabelContent { get; set; }
        public string ButtonContent { get; set; }

        public List<string> GendersList { get; set; }
        public int ClientId { get; set; }

        public ManageClientViewModel(User user, bool create, Client client = null)
        {
            this.isCreating = create;
            this.Inserter = user;
            this.CloseTabItemCommand = new RelayCommand(this.ClosetabItemExecute);
            this.CreateClientCommand = new RelayCommand(this.CreateClientExecute, this.CreateClientCanExecute);
            this.BirthDate = new DateTime(1980, 1, 1);
            this.InitializeGendersList();

            if (this.isCreating)
            {   //create new client mode
                this.ClientId = -1;
                this.Header = "Add new client";
                LabelContent = "Please fill out the following form:";
                ButtonContent = "Create client";
            }
            else
            {   //modify existing mode
                this.Client = client;
                this.ClientId = this.Client.Id;
                this.Header = Client.FirstName + " " + Client.LastName;
                LabelContent = "Please modify the data you want to change in the following form:";
                ButtonContent = "Modify client";
                this.FillForm(this.Client);
            }
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

            if (this.isCreating)
            {
                //create client
                Client client = new Client()
                {
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
            }
            else
            {   //update client


                //display confirmation and close the tab
                PopupMessage.OkButtonPopupMessage("Done", "Client modified successfully!");
            }

            this.CloseTabItemCommand.Execute(this);
        }

        public bool CreateClientCanExecute()
        {
            if (this.Barcode?.Length > 5)
            {
                if (this.FirstName?.Length > 3)
                {
                    if (this.LastName?.Length > 3)
                    {
                        if (this.PhoneNumber?.Length > 3 || (this.Email?.Length > 6))
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

        private void FillForm(Client client)
        {
            this.Barcode = client.BarCode;
            this.FirstName = client.FirstName;
            this.LastName = client.LastName;
            this.PhoneNumber = client.PhoneNumber;
            this.Email = client.Email;
            this.Gender = ConvertGender(client.Sex);
            this.BirthDate = client.BirthDate ?? DateTime.Today;
        }

        private string ConvertGender(string gender)
        {
            switch (gender)
            {
                case "M":
                    return "Male";
                case "F":
                    return "Female";
                default:
                    return "Other";
            }
        }

        private void InitializeGendersList()
        {
            this.GendersList = new List<string>();
            GendersList.Add("Male");
            GendersList.Add("Female");
            GendersList.Add("Other");
        }
    }
}
