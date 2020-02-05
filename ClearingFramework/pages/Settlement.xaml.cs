using ClearingFramework;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Z.Dapper.Plus;
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
        string accountID,accnum;
        #region combos
        public List<Account> acct { get; set; }
        private void bindCombo()
        {
        ClearingEntities ce = new ClearingEntities();
            var acid = ce.Accounts.ToList();
            acct = acid;
            accid.ItemsSource = acct;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = accid.SelectedItem as Account;
            try
            {
                accountID= item.id.ToString();
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
        #region бүртгэх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal value= Decimal.Parse(trvalue.Text);
            using (ClearingEntities context=new ClearingEntities())
            {
                var tran = new transaction()
                {
                    accid = accountID,
                    transType = transType.Text,
                    value = Decimal.Parse(trvalue.Text),
                    note = trnote.Text,
                    side = "1"
                };
                accountDetail accdet= context.accountDetails.FirstOrDefault(r => r.accNum == accnum);
                if (accdet != null)
                    accdet.totalValue += value;
                context.transactions.Add(tran);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region fill & number
        private void FillGrid()
        {
            ClearingEntities CE = new ClearingEntities();
            var trans = CE.transactions;
            datagrid1.ItemsSource = trans.ToList();
        }
        private void trvalue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion
        #region excel
        DataTableCollection tableCollection;
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
        string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
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
            }
        }
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
                    acc.accid = dt.Rows[i]["accid"].ToString();
                    acc.transType = dt.Rows[i]["transType"].ToString();
                    acc.value = Convert.ToDecimal(dt.Rows[i]["value"]);
                    acc.note = dt.Rows[i]["note"].ToString();
                    acc.side = dt.Rows[i]["side"].ToString();
                    acct.Add(acc);
                }
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "Data Source=msx-1003;Initial Catalog=Clearing;Persist Security Info=True;User ID=sa;Password=Qwerty123456";
                DapperPlusManager.Entity<transaction>().Table("transaction");
                List<transaction> newAcct = datagrid1.ItemsSource as List<transaction>;
                if (newAcct != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(newAcct);
                    }
                    MessageBox.Show("inserted !!!!!");
                }
                else
                {
                    MessageBox.Show("Fail !!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
            }
        }
        public IEnumerable<transaction> ConvertToAccountReadings(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new transaction
                {
                    accid= row["accid"].ToString(),
                    transType = row["transType"].ToString(),
                    value = Convert.ToDecimal(row["value"]),
                    note = row["note"].ToString(),
                    side = row["side"].ToString(),
                };
            }
        }
        #endregion
    }
}
