using ClearingFramework;
using ClearingFramework.dbBind;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Settlement.xaml
    /// </summary>
    public partial class Settlement : Page
    {
        public Settlement()
        {
            InitializeComponent();
            bindCombo();
            FillGrid();
        }
        string accountID, accnum;
        #region combos
        public List<Account> acct { get; set; }
        private void bindCombo()
        {
            Model1 ce = new Model1();
            var acid = ce.Accounts.ToList();
            acct = acid;
            accid.ItemsSource = acct;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = accid.SelectedItem as Account;
            try
            {
                accountID = item.id.ToString();
                sname.Text = item.fname.ToString();
                accnum = item.accNum.ToString();
                idnum.Text = item.idNum.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion
        #region бүртгэх and sum to totalValue
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal value = Decimal.Parse(trvalue.Text);
            Int16 s = Convert.ToInt16(side.SelectedIndex);
            if (s == 0)
            {
                s = -1;
            }
            using (Model1 context = new Model1())
            {
                var tran = new transaction()
                {
                    accNum = accountID,
                    transType = Convert.ToInt16(transType.SelectedIndex),
                    value = Decimal.Parse(trvalue.Text),
                    note = trnote.Text,
                    side = s,
                };
                AccountDetail accdet = context.AccountDetails.FirstOrDefault(r => r.accNum == accnum);
                if (accdet != null)
                    accdet.totalNumber += value;
                context.transactions.Add(tran);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region fill & number
        private void FillGrid()
        {
            Model1 CE = new Model1();
            var requs = CE.Requests;
            datagrid1.ItemsSource = requs.ToList();
            var trans = CE.transactions;
            datagrid2.ItemsSource = trans.ToList();
        }
        private void trvalue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion
        #region excel
        DataTableCollection tableCollection;
        #region insert excel file
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            using (OpenFileDialog fbd = new OpenFileDialog() { Filter = "Excel 97-2003|*.xls|Excel Workbook|*.xlsx" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    filePath = fbd.FileName;
                    try
                    {
                        using (var stream = File.Open(fbd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                cboSheet.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                    cboSheet.Items.Add(table.TableName);//add sheet to combobox
                            }
                        }
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show("Сонгосон файлыг өөр процесс ашиглаж байна файлаа хадгалаад хаана уу \n" + ex.ToString());
                        return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
        }
        #endregion
        #region select sheet's from combo and display into datatable
        private void cboSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            exceltab.IsEnabled = true;
            exceltab.IsSelected = true;
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];
            exceldata.ItemsSource = ConvertToAccountReadings(dt);
            if (dt != null)
            {
                List<transaction> acct = new List<transaction>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    transaction acc = new transaction();
                    acc.accNum = dt.Rows[i]["accNum"].ToString();
                    acc.transType =Convert.ToInt16(dt.Rows[i]["transType"]);
                    acc.value = Convert.ToDecimal(dt.Rows[i]["value"]);
                    acc.note = dt.Rows[i]["note"].ToString();
                    acc.side =Convert.ToInt16(dt.Rows[i]["side"]);
                    acct.Add(acc);
                }
            }
        }
        public IEnumerable<transaction> ConvertToAccountReadings(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new transaction
                {
                    accNum = row["accNum"].ToString(),
                    transType =Convert.ToInt16(row["transType"]),
                    value = Convert.ToDecimal(row["value"]),
                    note = row["note"].ToString(),
                    side =Convert.ToInt16(row["side"]),
                };
            }
        }

        #endregion
        #region xls хуулах
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string paths = Path.Combine(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location),
                @".\functions\Гүйлгээ бүртгэх.xlsx");
            string filePath = "";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() )
            {
                DialogResult result = fbd.ShowDialog();
                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    filePath = fbd.SelectedPath;
                    try { 
                System.IO.File.Move(paths, filePath + "\\Гүйлгээ бүртгэх.xlsx");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        #endregion
        #region bulk insert from datatable
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var newAcct = exceldata.ItemsSource as IEnumerable<transaction>;
            try
            {                
                if (newAcct != null)
                {
                    using (Model1 context = new Model1())
                    {
                        foreach (var i in newAcct)
                        {
                            context.transactions.Add(i);
                        }
                            context.SaveChanges();
                    }

                    MessageBox.Show("inserted !!!!!");
                }
                else
                {
                    MessageBox.Show("Fail !!!!!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
                return;
            }
        }
        #endregion
        #endregion
    }
}
