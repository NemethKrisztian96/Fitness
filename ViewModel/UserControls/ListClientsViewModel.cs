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
using Excel = Microsoft.Office.Interop.Excel;

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
            string outputFile = "../../../ExportedClients.xls";
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
            worksheet.Name = "Exported from datagrid";

            int noOfCols = 9;
            // storing header part in Excel  
            int i = 1;
            worksheet.Cells[1, i++] = "BarCode";
            worksheet.Cells[1, i++] = "FirstName";
            worksheet.Cells[1, i++] = "LastName";
            worksheet.Cells[1, i++] = "PhoneNumber";
            worksheet.Cells[1, i++] = "Email";
            worksheet.Cells[1, i++] = "BirthDate";
            worksheet.Cells[1, i++] = "Sex";
            worksheet.Cells[1, i++] = "InsertDate";
            worksheet.Cells[1, i++] = "Inserter.UserName";

            // storing Each row and column value to excel sheet  
            for (i = 0; i < this.ClientsList.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = this.ClientsList.First().BarCode;
                worksheet.Cells[i + 2, 2] = this.ClientsList.First().FirstName;
                worksheet.Cells[i + 2, 3] = this.ClientsList.First().LastName;
                worksheet.Cells[i + 2, 4] = this.ClientsList.First().PhoneNumber;
                worksheet.Cells[i + 2, 5] = this.ClientsList.First().Email;
                worksheet.Cells[i + 2, 6] = CustomDateTime.GetFormatedString(this.ClientsList.First().BirthDate);
                worksheet.Cells[i + 2, 7] = this.ClientsList.First().Sex;
                worksheet.Cells[i + 2, 8] = CustomDateTime.GetFormatedString(this.ClientsList.First().InsertDate);
                worksheet.Cells[i + 2, 9] = this.ClientsList.First().Inserter?.UserName;
            }
            // save the application  
            //workbook.SaveAs(outputFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

            /*var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned 
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template. 
            // Because no argument is sent in this example, Add creates a new workbook. 
            excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "ID Number";
            workSheet.Cells[1, "B"] = "Current Balance";
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();*/
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
