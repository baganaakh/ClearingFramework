using ClearingFramework;
using ClearingFramework.dbBind;
using ClearingFramework.dbBind.AdminDatabase;
using ExcelDataReader;
using LinqToExcel.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
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
using Account1 = ClearingFramework.dbBind.Account;                  //clearing database nickname
using Account2 = ClearingFramework.dbBind.AdminDatabase.Account;     //page     database nickname
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
            bindCombo();
            FillGrid();
        }
        long iid;
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        string pcode;
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
            stat.SelectedItem = null;
            fee.Text = null;
            denchinPercent.Text = null;
            contractFee.Text = null;
            pozfee.Text = null;
        }
        private void FillGrid()
        {
            ClearingEntities CE = new ClearingEntities();
            var Accountss = CE.Accounts;
            vwOmniAccBalance.ItemsSource = Accountss.Where(s=>s.memId == memId).ToList();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
        #endregion
        #region update
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (ClearingEntities context = new ClearingEntities())
            {
                Account1 acc = context.Accounts.FirstOrDefault(r => r.id == iid);
                acc.lname = lName.Text;
                acc.fname = fName.Text;
                acc.idNum = idNumber.Text;
                acc.phone = phonee.Text;
                acc.accNum = accountn.Text;
                acc.secAcc = secAc.Text;
                acc.mail = email.Text;
                acc.brokerCode = brokCode.Text;
                //state = stat.SelectedValue.To;String(),
                acc.linkAcc = linkAc.SelectedValue.ToString();
                acc.fee = Convert.ToDecimal(fee.Text);
                acc.denchinPercent = Convert.ToDecimal(denchinPercent.Text);
                acc.contractFee = Convert.ToDecimal(contractFee.Text);
                acc.pozFee = Convert.ToDecimal(pozfee.Text);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region insert
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (brokCode.SelectedValue == null && stat.SelectedValue == null
                && linkAc.SelectedValue == null)
            {
                MessageBox.Show("Combos are empty Please fill them");
            }
            using (ClearingEntities context = new ClearingEntities())
            {
                var exist = context.Accounts.Count(a => a.accNum == accountn.Text);
                var idexist = context.Accounts.Count(a => a.idNum == idNumber.Text);
                if (idexist != 0)
                {
                    MessageBox.Show("РД давтагдсан байна " + idNumber.Text.ToString() + " !!!");
                    return;
                }
                else
                if (exist != 0)
                {
                    MessageBox.Show("Account number exists " + accountn.Text.ToString() + " !!!");
                    return;
                }
                Account1 acct = new Account1
                {
                    accNum = accountn.Text,
                    idNum = idNumber.Text,
                    lname = lName.Text,
                    fname = fName.Text,
                    phone = phonee.Text,
                    mail = email.Text,
                    linkAcc = linkAc.SelectedValue.ToString(),
                    brokerCode = pcode,
                    state = Convert.ToInt16(stat.SelectedIndex),
                    modified = DateTime.Now,
                    secAcc = secAc.Text,
                    fee = Convert.ToDecimal(fee.Text),
                    denchinPercent = Convert.ToDecimal(denchinPercent.Text),
                    contractFee = Convert.ToDecimal(contractFee.Text),
                    pozFee = Convert.ToDecimal(pozfee.Text),
                    memId = memId
                };
                ClearingFramework.dbBind.AccountDetail acd = new ClearingFramework.dbBind.AccountDetail
                {
                    freezeValue=10,
                    totalNumber=100,
                    accNum= accountn.Text,
                    linkAcc = linkAc.SelectedValue.ToString(),
                };
                context.AccountDetails.Add(acd);
                context.Accounts.Add(acct);
                context.SaveChanges();
                FillGrid();
            }
        }
        #endregion
        #region delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            long iiid = (vwOmniAccBalance.SelectedItem as Account1).id;
            using (ClearingEntities context = new ClearingEntities())
            {
                Account1 acc = context.Accounts.FirstOrDefault(r => r.id == iiid);
                context.Accounts.Remove(acc);
                context.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region edit
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            iid = (vwOmniAccBalance.SelectedItem as Account1).id;
            using (ClearingEntities context = new ClearingEntities())
            {
                Account1 acc = context.Accounts.FirstOrDefault(r => r.id == iid);
                lName.Text = acc.lname;
                fName.Text = acc.fname;
                idNumber.Text = acc.idNum;
                phonee.Text = acc.phone;
                accountn.Text = acc.accNum;
                secAc.Text = acc.secAcc;
                email.Text = acc.mail;
                brokCode.Text = acc.brokerCode;
                linkAc.SelectedValue = acc.linkAcc;
                stat.SelectedIndex = Convert.ToInt32(acc.state);
                fee.Text = acc.fee.ToString();
                denchinPercent.Text = acc.denchinPercent.ToString();
                contractFee.Text = acc.contractFee.ToString();
                pozfee.Text = acc.pozFee.ToString();
            }
        }
        #endregion
        #region excel
        string filePath = "";
        DataTableCollection tableCollection;
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            { Filter = "Excel 97-2003|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    chosen.Text = filePath;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open,
                                                    FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                { UseHeaderRow = true }
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
                List<Account1> acct = new List<Account1>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Account1 acc = new Account1();
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
                string connectionString = "Data Source=msx-1003;Initial Catalog=Clearing;" +
                    "Persist Security Info=True;User ID=sa;Password=Qwerty123456";
                DapperPlusManager.Entity<Account1>().Table("Account");
                List<Account1> newAcct = vwOmniAccBalance.ItemsSource as List<Account1>;
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
        public IEnumerable<Account1> ConvertToAccountReadings(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Account1
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
        #region combos
        public List<Member> mem { get; set; }
        public List<Account2> acc { get; set; }
        private void bindCombo()
        {
            int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
            demoEntities1 de = new demoEntities1();
            //var memid = de.Members.Where(s=>s.partid == partId).ToList();
            var memid = de.Members.ToList();
            mem = memid;
            brokCode.ItemsSource = mem;

            var acclist = de.Accounts.Where(s => s.memberid == memId && s.accountType == 3).ToList();
            acc = acclist;
            linkAc.ItemsSource = acc;
        }
        private void brokCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = brokCode.SelectedItem as Member;
            try
            {
                pcode = item.code.ToString();
            }
            catch
            {
                return;
            }
        }
        #endregion

        private void fee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
    }
}
