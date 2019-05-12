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
            else if (item is IClientOperationsContent)
            {
                return Application.Current.MainWindow.TryFindResource("ClientOperationsTemplate") as DataTemplate;
            }
            else if (item is IManageClientContent)
            {
                return Application.Current.MainWindow.TryFindResource("AddNewClientTemplate") as DataTemplate;
            }
            /*else if (item is )
            {
                return Application.Current.MainWindow.TryFindResource("SomeTemplate") as DataTemplate;
            }*/

            return null;
        }
    }

}
