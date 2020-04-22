using ClearingFramework;
using ClearingFramework.dbBind;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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
            banks.SelectedItem = null;
            bankaccount.Text = null;
            bankaccname.Text = null;
        }
        private void FillGrid()
        {
            Model1 CE = new Model1();
            vwOmniAccBalance.ItemsSource = CE.Accounts.Where(s => s.memId == memId).ToList();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }
        #endregion
        #region update
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (Model1 context = new Model1())
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
                acc.linkAcc = linkAc.SelectedValue.ToString();
                acc.fee = Convert.ToDecimal(fee.Text);
                acc.denchinPercent = Convert.ToDecimal(denchinPercent.Text);
                acc.contractFee = Convert.ToDecimal(contractFee.Text);
                acc.pozFee = Convert.ToDecimal(pozfee.Text);
                acc.bank = banks.SelectedIndex;
                acc.bankAccName = bankaccname.Text;
                acc.bankAccount = bankaccount.Text;
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
            using (Model1 context = new Model1())
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
                Account acct = new Account
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
                    memId = memId,
                    bank=banks.SelectedIndex,
                    bankAccount=bankaccount.Text,
                    bankAccName=bankaccname.Text,
                };
                AccountDetail acd = new AccountDetail
                {
                    freezeValue = 10,
                    totalNumber = 0,
                    accNum = accountn.Text,
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
            long iiid = (vwOmniAccBalance.SelectedItem as Account).id;
            string accnu = (vwOmniAccBalance.SelectedItem as Account).accNum;
            using (Model1 context = new Model1())
            {
                Account acc = context.Accounts.FirstOrDefault(r => r.id == iiid);
                var aacd = context.AccountDetails.Where(r => r.accNum == accnu);
                foreach (var i in aacd)
                {
                    context.AccountDetails.Remove(i);
                }
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
            using (Model1 context = new Model1())
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
                    //chosen.Text = filePath;
                    try
                    {
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
                                    cboSheet.Items.Add(table.TableName);//add sheets to combobox
                            }
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Файл нээх боломжгүй "+filePath + " файлийг өөр программ ашиглаж байна");
                        return;
                    }
                }
            }
        }
        private void cboSheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            exceltab.IsEnabled = true;
            exceltab.IsSelected = true;
            if (cboSheet.SelectedItem == null)
                return;
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];
            exceldata.ItemsSource = ConvertToAccountReadings(dt);

        }
        #region Import
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            try
            {
                var newAcct = exceldata.ItemsSource as IEnumerable<Account>;
                if (newAcct != null)
                {
                    using (var contx = new Model1())
                    {
                        foreach (var i in newAcct)
                        {
                            contx.Accounts.Add(i);                           
                        }
                        try
                        {
                            contx.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var eve in ex.EntityValidationErrors)
                            {
                                MessageBox.Show("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:");
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property: " + ve.PropertyName + ", Value: " + eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName) + ", Error: " + ve.ErrorMessage);
                                }
                            }
                            throw;
                        }
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
        #endregion
        public IEnumerable<Account> ConvertToAccountReadings(DataTable dataTable)
        {
            Model1 CE = new Model1();
            string accNumber;
            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    accNumber = row["accNum"].ToString();
                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message.ToString());
                    throw;
                }
                var exist = (from s in CE.Accounts where s.accNum == accNumber select s).FirstOrDefault<Account>();
                if (exist != null)
                {
                    MessageBox.Show("Account number exists " + accountn.Text.ToString() + " !!!");
                    break;
                }
                yield return new Account
                {
                    accNum = row["accNum"].ToString(),
                    idNum = row["idNum"].ToString(),
                    lname = row["lname"].ToString(),
                    fname = row["fname"].ToString(),
                    phone = row["phone"].ToString(),
                    mail = row["mail"].ToString(),
                    state =Convert.ToInt16(row["state"]),
                    modified = Convert.ToDateTime(DateTime.Now),
                    secAcc = row["secAcc"].ToString(),
                    fee = Convert.ToDecimal(row["fee"]),
                    denchinPercent = Convert.ToDecimal(row["denchinPercent"]),
                    contractFee = Convert.ToDecimal(row["contractFee"]),
                    pozFee = Convert.ToDecimal(row["pozFee"]),
                    bank = Convert.ToInt32(row["bank"]),
                    bankAccName = row["bankAccName"].ToString(),
                    bankAccount = row["bankAccount"].ToString(),
                };
            }
        }
        #endregion
        #region combos        
        public List<AdminAccount> acc { get; set; }
        private void bindCombo()
        {
            int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
            Model1 ce = new Model1();
            //var memid = de.Members.Where(s=>s.partid == partId).ToList();            
            brokCode.ItemsSource = ce.AdminMembers.ToList();
            linkAc.ItemsSource = ce.AdminAccounts.Where(s => s.memberid == memId && s.accountType == 3).ToList();
        }
        private void brokCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = brokCode.SelectedItem as AdminMember;
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
        #region xls хуулах
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string paths = Path.Combine(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location),
                @".\functions\Данс.xlsx");
            string filePath = "";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    filePath = fbd.SelectedPath;
                    try
                    {
                        System.IO.File.Move(paths, filePath + "\\Данс.xlsx");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
        #endregion
    }
}
