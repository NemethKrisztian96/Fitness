using Fitness.Common.Helpers;
using Fitness.Common.MVVM;
using Fitness.Logic;
using Fitness.Model.DBContext;
using Fitness.View;
using Fitness.ViewModel;
using System;
using System.Windows;

namespace Fitness
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.Initialize();
            this.InitializeData();
            this.OpenMainWindow();
            Data.Fitness.FitnessDatabase.SaveChanges();
        }

        private void Initialize()
        {
            ViewService.RegisterView(typeof(MainWindowViewModel), typeof(MainWindow));
        }

        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowViewModel = MainWindowViewModel.Instance;

            ViewService.AddMainWindowToOpened(mainWindowViewModel, mainWindow);
            ViewService.ShowDialog(mainWindowViewModel);
            ViewService.CloseDialog(mainWindowViewModel);
        }

        private void InitializeData()
        {
            try
            {
                DBInitializer dbinit = new DBInitializer();
                dbinit.InitializeDatabase(Data.Fitness.FitnessDatabase);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
