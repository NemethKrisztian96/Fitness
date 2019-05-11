using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Fitness.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                //Console.WriteLine(((dynamic)this.DataContext).Username);
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
                //Console.WriteLine(Marshal.PtrToStringBSTR(Marshal.SecureStringToBSTR(((dynamic)this.DataContext).SecurePassword)));
            }
        }
    }
}
