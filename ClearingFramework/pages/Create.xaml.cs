using ClearingFramework;
using ClearingFramework.dbBind;
using ExcelDataReader;
using LinqToExcel.Extensions;
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
using MessageBox = System.Windows.MessageBox;


namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Page
    {

        public Create()
        {
            InitializeComponent();
            FillGrid();
        }
        long iid;
        #region fill, new & refresh
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            lName.Text = null;
            fName.Text = null;
            idNumber.Text = null;
            phonee.Text = null;
            accountn.Text = null;
            secAc.Text = null;
            email.Text = null;
            brokCode.Text = null;
            linkAc.SelectedItem = null;
        }
        private void FillGrid()
        {
            ClearingEntities CE = new ClearingEntities();
            //var data = from d in CE.Accounts select d;
            //vwOmniAccBalance.ItemsSource = data.ToList();
            //or
            var Accountss = CE.Accounts;
            vwOmniAccBalance.ItemsSource = Accountss.ToList();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
        #endregion
        #region update
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using(ClearingEntities context=new ClearingEntities())
            {
                Account acc = context.Accounts.FirstOrDefault(r => r.id == iid);
                acc.lname = lName.Text;
                acc.fname = fName.Text;
                acc.idNum = idNumber.Text;
                acc.phone = phonee.Text;
                acc.accNum = accountn.Text;
                acc.secAcc = secAc.Text;
                acc.mail = email.Text;
                acc.brokerCode = brokCode.Text;
                //state = stat.SelectedValue.To;String(),
                acc.linkAcc = linkAc.Text;
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region insert
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (brokCode.SelectedValue == null && stat.SelectedValue == null && linkAc.SelectedValue == null)
            {
                MessageBox.Show("Combos are empty Please fill them");
            }
            using (ClearingEntities context=new ClearingEntities())
            {
                var exist = context.Accounts.Count(a => a.accNum == accountn.Text);
                var idexist = context.Accounts.Count(a => a.idNum == idNumber.Text);
                if(idexist != 0) { 
                    MessageBox.Show("РД давтагдсан байна " + idNumber.Text.ToString() +" !!!") ;
                    return;
                }
                else
                if(exist != 0) { 
                    MessageBox.Show("Account number exists " + accountn.Text.ToString() +" !!!") ;
                    return;
                }
                Account acct = new Account
                {
                    lname = lName.Text,
                    fname = fName.Text,
                    idNum = idNumber.Text,
                    phone = phonee.Text,
                    accNum = accountn.Text,
                    secAcc = secAc.Text,
                    mail = email.Text,
                    brokerCode = brokCode.Text,
                    linkAcc = linkAc.Text,
                    modified=DateTime.Now
                };
                accountDetail adetail = new accountDetail
                {
                    accNum = accountn.Text,
                    modified=DateTime.Now
                };

                context.Accounts.Add(acct);
                context.accountDetails.Add(adetail);
                context.SaveChanges();
                FillGrid();
            }
        }
        #endregion
        #region delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            long iiid = (vwOmniAccBalance.SelectedItem as Account).id;
            using (ClearingEntities context=new ClearingEntities())
            {
                Account acc = context.Accounts.FirstOrDefault(r => r.id == iiid);
                context.Accounts.Remove(acc);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region edit
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            iid = (vwOmniAccBalance.SelectedItem as Account).id;
            using (ClearingEntities context = new ClearingEntities())
            {
                Account acc = context.Accounts.FirstOrDefault(r => r.id == iid);
                lName.Text = acc.lname;
                fName.Text = acc.fname;
                idNumber.Text = acc.idNum;
                phonee.Text = acc.phone;
                accountn.Text = acc.accNum;
                secAc.Text = acc.secAcc;
                email.Text = acc.mail;
                brokCode.Text = acc.brokerCode;
                linkAc.SelectedItem = cbi7;
            }
        }
        #endregion
        #region excel
        string filePath="";
        DataTableCollection tableCollection;
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    chosen.Text = filePath;
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
                List<Account> acct = new List<Account>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Account acc = new Account();
                    acc.lname = dt.Rows[i]["lname"].ToString();
                    acc.fname = dt.Rows[i]["fname"].ToString();
                    acc.idNum = dt.Rows[i]["idNum"].ToString();
                    acc.phone = dt.Rows[i]["phone"].ToString();
                    acc.mail = dt.Rows[i]["mail"].ToString();
                    acc.linkAcc = dt.Rows[i]["linkAcc"].ToString();
                    acc.accNum = dt.Rows[i]["accNum"].ToString();
                    acc.brokerCode = dt.Rows[i]["brokerCode"].ToString();
                    acc.state = 1;
                    //acc.state = dt.Rows[i]["state"].ToString();
                    acc.secAcc = dt.Rows[i]["secAcc"].ToString();
                    acct.Add(acc);
                }
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "Data Source=msx-1003;Initial Catalog=Clearing;Persist Security Info=True;User ID=sa;Password=Qwerty123456";
                DapperPlusManager.Entity<Account>().Table("Account");
                List<Account> newAcct = vwOmniAccBalance.ItemsSource as List<Account>;
                if (newAcct != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(newAcct);
                    }
                    MessageBox.Show("Inserted");
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
        public IEnumerable<Account> ConvertToAccountReadings(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Account
                {
                    lname = row["lname"].ToString(),
                    fname = row["fname"].ToString(),
                    idNum = row["idNum"].ToString(),
                    phone = row["phone"].ToString(),
                    mail = row["mail"].ToString(),
                    linkAcc = row["linkAcc"].ToString(),
                    accNum = row["accNum"].ToString(),
                    brokerCode = row["brokerCode"].ToString(),
                    state = Convert.ToInt16(row["state"]),
                    secAcc = row["secAcc"].ToString()
                };
            }
        }
        #endregion
        
    }
}
