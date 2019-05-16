using Fitness.Common.FitnessTabContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fitness.View.TemplateSelectors
{
    public class FitnessContentSelector : DataTemplateSelector 
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)  
        {
            //deciding which template to use and search for the appropriate control
            if (item is IHomeContent)
            {
                return Application.Current.MainWindow.TryFindResource("HomeTemplate") as DataTemplate;
                //                                                      WATCH IT!!!
            }
            else if (item is IManageClientContent)
            {
                return Application.Current.MainWindow.TryFindResource("ManageClientTemplate") as DataTemplate;
            }
            else if (item is IListTicketTypesContent)
            {
                return Application.Current.MainWindow.TryFindResource("ListTicketTypesTemplate") as DataTemplate;
            }
            else if (item is IManageTicketTypeContent)
            {
                return Application.Current.MainWindow.TryFindResource("ManageTicketTypeTemplate") as DataTemplate;
            }
            else if (item is IListUsersContent)
            {
                return Application.Current.MainWindow.TryFindResource("ListUsersTemplate") as DataTemplate;
            }
            else if (item is IManageUserContent)
            {
                return Application.Current.MainWindow.TryFindResource("ManageUserTemplate") as DataTemplate;
            }
            else if (item is IClientTickets)
            {
                return Application.Current.MainWindow.TryFindResource("ClientTicketListTemplate") as DataTemplate;
            }
            else if (item is IListClientsContent)
            {
                return Application.Current.MainWindow.TryFindResource("ListClientsTemplate") as DataTemplate;
            }
            else if (item is IEmailContent)
            {
                return Application.Current.MainWindow.TryFindResource("EmailTemplate") as DataTemplate;
            }
            else if (item is ITicketStatistics)
            {
                return Application.Current.MainWindow.TryFindResource("ITicketStatisticsTemplate") as DataTemplate;
            }
            /*else if (item is )
            {
                return Application.Current.MainWindow.TryFindResource("SomeTemplate") as DataTemplate;
            }*/

            return null;
        }
    }

}
