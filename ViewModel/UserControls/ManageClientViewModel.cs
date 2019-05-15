using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using Fitness.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fitness.ViewModel.UserControls
{
    public class ManageClientViewModel : ViewModelBase, IManageClientContent
    {
        private bool isCreating;
        private bool formIsVisible = true;
        private bool webcamIsVisible = false;

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
                this.WebcamIsVisible = !this.FormIsVisible;
                this.RaisePropertyChanged();
            }
        }

        public bool WebcamIsVisible
        {
            get
            {
                return this.webcamIsVisible;
            }
            set
            {
                this.webcamIsVisible = value;
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

        public Uri ImagePathUri { get; set; }
        public string ImagePath { get; set; }
        public bool ImageIsVisible { get; set; }
        public Image ClientImage { get; set; }

        public string LabelContent { get; set; }
        public string ButtonContent { get; set; }

        public List<string> GendersList { get; set; }
        public int ClientId { get; set; }

        public ManageClientViewModel(User user, bool create, Client client = null)
        {
            this.isCreating = create;
            this.Inserter = user;
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.CreateClientCommand = new RelayCommand(this.CreateClientExecute, this.CreateClientCanExecute);
            this.BirthDate = new DateTime(1980, 1, 1);
            this.InitializeGendersList();

            if (this.isCreating)
            {   //create new client mode
                this.ClientId = -1;
                this.Header = "Add new client";
                LabelContent = "Please fill out the following form:";
                ButtonContent = "Create client";
                this.ImageIsVisible = false;
            }
            else
            {   //modify existing mode
                this.Client = client;
                this.ClientId = this.Client.Id;
                this.Header = Client.FirstName + " " + Client.LastName;
                LabelContent = "Please modify the data you want to change in the following form:";
                ButtonContent = "Modify client";
                this.ImageIsVisible = true;
                this.FillForm(this.Client);
            }
            this.RaisePropertyChanged();
        }

        public void CloseTabItemExecute()
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

            if (this.isCreating)
            {
                this.ImagePath = "";
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
                this.Client = client;
                Data.Fitness.SaveAllChanges();
                //display confirmation and close the tab
                PopupMessage.OkButtonPopupMessage("Done", "Client added successfully!");
            }
            else
            {   //update client
                this.Client.BarCode = this.Barcode;
                this.Client.FirstName = this.FirstName;
                this.Client.LastName = this.LastName;
                this.Client.Email = this.Email;
                this.Client.PhoneNumber = this.PhoneNumber;
                this.Client.Sex = this.Gender[0].ToString().ToUpper();
                this.Client.BirthDate = this.BirthDate;
                this.Client.InsertDate = DateTime.Today;
                this.Client.Inserter = this.Inserter;
                this.Client.InserterId = this.Inserter.Id;
                this.Client.ImagePath = this.ImagePath;

                //commit
                Data.Fitness.SaveAllChanges();

                //display confirmation and close the tab
                PopupMessage.OkButtonPopupMessage("Done", "Client modified successfully!");
            }

            //prompt to take picture
            bool takePicture = PopupMessage.YesNoButtonPopupMessage("Picture", "Would you like to take a picture of the client?");
            if (takePicture)
            {   //take picture
                this.FormIsVisible = false;
                this.WebcamIsVisible = true;

                Data.Fitness.LastModified = this.Client;
                //SavePictureViewModel mVM = new SavePictureViewModel();
                /*CameraCaptureUI captureUI = new CameraCaptureUI();
                captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
                captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

                StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);*/
                //this.ImagePath =
            }
            else
            {
                this.CloseTabItemCommand.Execute(this);
            }
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
            if (!IsValidBarcode() || !IsValidName() || !IsAcceptablePhoneNumber() || !IsAcceptableEmail())
            {
                return false;
            }

            return true;
        }

        private bool IsValidBarcode()
        {
            if (this.Barcode.Length != 12) { return false; }
            if (!this.Barcode.All(char.IsDigit)) { return false; }

            return true;
        }

        private bool IsValidName()
        {
            if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName)) { return false; } //just in case
            if (!(new Regex("^[a-zA-Z0-9 ]*$")).IsMatch(this.FirstName) || !(new Regex("^[a-zA-Z0-9 ]*$")).IsMatch(this.LastName)) { return false; }
            if(!IsCapitalAllFirtsLetter(this.FirstName) || !IsCapitalAllFirtsLetter(this.LastName)) { return false; }

            return true;
        }

        private static bool IsCapitalAllFirtsLetter(string input)
        {
            char[] helper = input.ToCharArray();
            if(helper.Length >= 1)
            {
                if (char.IsLower(helper[0]))
                {
                    return false;
                }
            }

            for( int i = 1; i < helper.Length; i++)
            {
                if(helper[i-1] ==' ')
                {
                    if (char.IsLower(helper[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        //the phone num and the email are not required, so it is acceptable it these are empty
        private bool IsValidPhoneNumber()
        {
            //the length must be 10 and only contains digit
            if(this.PhoneNumber.Length != 10) { return false; }
            if(!this.PhoneNumber.All(char.IsDigit)) { return false; }

            return true;
        }

        private bool IsAcceptablePhoneNumber()
        {
            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                return true;
            }
            else
            {
                if (IsValidPhoneNumber())
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidEmail()
        {
            if(new EmailAddressAttribute().IsValid(this.Email)) { return true; }

            return false;
        }

        private bool IsAcceptableEmail()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                return true;
            }
            else
            {
                if (IsValidEmail())
                {
                    return true;
                }
            }

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
            //this.ImagePathUri = new Uri(client.ImagePath) ?? null;
            if (File.Exists(client.ImagePath))
            {
                this.ClientImage = Image.FromFile(client.ImagePath);
            }
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
