using Fitness.Common.FitnessTabContents;
using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.ViewModel.UserControls
{
    class SendEmailViewModel : ViewModelBase, IEmailContent
    {
        public string Header => "Email";

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand SendEmailCommand { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public bool ShowCloseButton => true;

        public SendEmailViewModel()
        {
            this.SendEmailCommand = new RelayCommand(this.SendEmailExecute);
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
        }

        public void SendEmailExecute()
        {
            string adminEmailAddress = "nkmiklos96@gmail.com";
            //List<string> emails = Data.Fitness.GetClientEmails();
            List<string> emails = new List<string>();
            emails.Add("fuloptimea1427@gmail.com");
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    foreach (string email_to in emails)
                    {
                        mail.To.Add(email_to);
                    }
                    mail.Subject = Subject;
                    mail.From = new MailAddress(adminEmailAddress);
                    mail.Body = Body;

                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                    //{
                    //    Credentials = new NetworkCredential(adminEmailAddress, "1234asdfQWER"),
                    //    EnableSsl = true
                    //};
                    //smtp.Send(mail);

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(adminEmailAddress, "1234asdfQWER");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        PopupMessage.OkButtonPopupMessage("Success", "The email has been sent");
                    }
                }
            }
            catch(Exception e) {
                PopupMessage.OkButtonPopupMessage("Error", "We were unable to send your email");
                Console.WriteLine(e.Message);
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
