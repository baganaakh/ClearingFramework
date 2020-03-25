using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;
using DataTable = System.Data.DataTable;

namespace Non_Member
{
    class Excel
    {
        string path="";
        
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        
        public Excel(string path, int Sheet)
        {
            this.path = path;
            //path = this.path;
            
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }
        public string ReadCell(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2;
            else
                return "";
            
        }
        public void WriteToCell(int i, int j, string s)
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = s;
        }
        public void Save()
        {
            wb.Save();
        }
        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }
        public void Close()
        {
            wb.Close();
        }
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            this.ws = wb.Worksheets[1];
        }
        public void  CreateNewSheet() 
        {
            Worksheet tempSheet = wb.Worksheets.Add(After: ws);
        }
        public void SelectWorkSheet(int sheetNumber)
        {
            this.ws = wb.Worksheets[sheetNumber];
        }
        public void DeleteWorkSheet(int SheetNumber)
        {
            wb.Worksheets[SheetNumber].Delete();
        }
        BackgroundWorker bw = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        
    }
}
