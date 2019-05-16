using Fitness.Common.FitnessTabContents;
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
    public class ListClientsViewModel : ViewModelBase, IListClientsContent
    {
        public string Header => "List of clients";

        public RelayCommand CloseTabItemCommand { get; set; }
        public RelayCommand ManageClientCommand { get; set; }
        public RelayCommand DeleteClientCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        public bool ShowCloseButton => true;

        public List<Client> ClientsList { get; set; }
        public Client SelectedClient { get; set; }

        public ListClientsViewModel()
        {
            this.CloseTabItemCommand = new RelayCommand(this.CloseTabItemExecute);
            this.ClientsList = Data.Fitness.GetClients();
            this.ManageClientCommand = new RelayCommand(this.ManageClientExecute);
            this.DeleteClientCommand = new RelayCommand(this.DeleteClientExecute);
            this.ExportCommand = new RelayCommand(this.ExportExecute);
        }

        public void ExportExecute()
        {
            /*string outputFile = "../../../ExportedClients.xls";
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            workbook.SaveAs(outputFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();*/
        }

        public void ManageClientExecute()
        {
            if (this.SelectedClient != null)
            {
                MainWindowViewModel.Instance.SetClientManageClientTab(this.SelectedClient);
            }
        }

        public void DeleteClientExecute()
        {
            if (this.SelectedClient != null)
            {
                this.SelectedClient.IsDeleted = true;
                Data.Fitness.SaveAllChanges();
            }
        }

        public void CloseTabItemExecute()
        {
            MainWindowViewModel.Instance.CloseTabItem(this);
        }
    }
}
