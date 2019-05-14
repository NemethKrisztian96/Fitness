using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitness.View.UserControls
{
    /// <summary>
    /// Interaction logic for ManageUser.xaml
    /// </summary>
    public partial class ManageUser : UserControl
    {
        public ManageUser()
        {
            InitializeComponent();
        }

        public void AllowOnlyDigits(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[.0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void PasswordChanged1(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword1 = ((PasswordBox)sender).SecurePassword;
                ((dynamic)this.DataContext).PasswordChanged = true;
            }
        }

        private void PasswordChanged2(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword2 = ((PasswordBox)sender).SecurePassword;
                ((dynamic)this.DataContext).PasswordChanged = true;
            }
        }
    }
}
