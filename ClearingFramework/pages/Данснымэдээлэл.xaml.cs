using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Дансны мэдээлэл.xaml
    /// </summary>
    public partial class Данснымэдээлэл : Page
    {
        int memid= Convert.ToInt32(App.Current.Properties["member_id"]);
        List<ForGrid> forgrid=new List<ForGrid>();
        List<ForGrid> forgrid1=new List<ForGrid>();
        public Данснымэдээлэл()
        {
            InitializeComponent();
            bindCombo();
        }
        #region bindCombo
        private void bindCombo()
        {
            #region Нэгдсэн дансны үлдэгдэл
            Model1 ce = new Model1();
                var acc1 = ce.AdminAccounts.Where(s=> s.memberid == memid && s.accountType == 3).ToList();
                var t1 = from tt1 in acc1
                        join accde1 in ce.AdminAccountDetails on tt1.id equals accde1.accountId
                        join ass1 in ce.AdminAssets on accde1.assetId equals ass1.id
                        select new
                        {
                            ass1.id,
                            ass1.name,
                        };
                assets.ItemsSource = t1;
                assets.SelectedIndex = 0;
            #endregion
            #region Тухайлсан дансны үлдэгдэл
                var acc = ce.Accounts.Where(s=> s.memId == memid).ToList();
                var t = from tt in acc
                        join accde in ce.AdminAccountDetails on tt.id equals accde.accountId
                        join ass in ce.AdminAssets on accde.assetId equals ass.id
                        select new
                        {
                            ass.id,
                            ass.name,
                        };
                assets2.ItemsSource = t;
                assets2.SelectedIndex = 0;
            #endregion

        }
        #endregion

        private void assets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model1 ce = new Model1();
            var item = assets.SelectedValue;
            int id = Convert.ToInt32(item);
            var acc = ce.AdminAccounts.Where(s => s.memberid == memid && s.accountType == 3).ToList();
            var t = from tt in acc
                    join accde in ce.AdminAccountDetails on tt.id equals accde.accountId
                    join ass in ce.AdminAssets on accde.assetId equals ass.id
                    where accde.assetId == id
                    select new
                    {
                        tt.accNumber,
                        accde.amount,
                        accde.freezeValue,
                        ass.name,
                        боломжит = accde.amount - accde.freezeValue,
                        tt.state,
                    };
            foreach (var i in t)
            {
                ForGrid data = new ForGrid()
                {
                    accNum = i.accNumber,
                    name = i.name,
                    totalNumber = (int)i.amount,
                    freezeValue = (int)i.freezeValue,
                    боломжит = (int)i.боломжит,
                    state = (short)i.state,
                };
                forgrid1.Add(data);
            };
            unitedData.ItemsSource = forgrid1;
        }
        #region Тухайлсан дансны үлдэгдэл Search
        private void assets2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tuhBalance.ItemsSource = null;
            Model1 ce = new Model1();
            var item = assets.SelectedValue;
            int id = Convert.ToInt32(item);
            var acc = ce.Accounts.Where(s => s.memId== memid).ToList();
            var t = from tt in acc
                    join accde in ce.AccountDetails on tt.id equals accde.accountId
                    join ass in ce.AdminAssets on accde.assetId equals ass.id
                    where accde.assetId == id
                    select new
                    {
                        tt.accNum,
                        accde.totalNumber,
                        accde.freezeValue,
                        ass.name,
                        боломжит = accde.totalNumber- accde.freezeValue,
                        tt.state,
                    };
            foreach(var i in t)
            {
                ForGrid data = new ForGrid()
                {
                    accNum=i.accNum,
                    name=i.name,
                    totalNumber=(decimal)i.totalNumber,
                    freezeValue=(decimal)i.freezeValue,
                    боломжит=(decimal)i.боломжит,
                    state=(short)i.state,
                };
                forgrid.Add(data);
            };
            tuhBalance.ItemsSource = t;
        }
        private void AccountSrch_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = forgrid.Where(s =>s.accNum.StartsWith(AccountSrch.Text) );
            tuhBalance.ItemsSource = null;
            tuhBalance.ItemsSource = filtered;
        }
        #endregion
        public class ForGrid
        {            
            public string accNum { get; set; }
            public string name{ get; set; }
            public decimal totalNumber { get; set; }
            public decimal freezeValue { get; set; }
            public string авлага { get; set; }
            public string өглөг { get; set; }
            public decimal боломжит { get; set; }
            public short state { get; set; }            
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void Нэгдсэнданс_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = forgrid1.Where(s => s.accNum.StartsWith(Нэгдсэнданс.Text)).ToList();
            unitedData.ItemsSource = null;
            unitedData.ItemsSource = filtered;
        }

        private void Дансныхуулга(object sender, System.Windows.RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            linkedmem.ItemsSource = ce.AdminMembers.Where(r => r.linkMember == memid).ToList();
        }

        private void linkedmem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            acdatas.ItemsSource = null;
            var item = linkedmem.SelectedItem as AdminMember;
            Model1 ce = new Model1();
            var accounts= ce.Accounts.Where(r => r.memId == item.id).ToList();
            acdatas.ItemsSource = accounts;
        }
        
        private void acsrch_KeyUp(object sender, KeyEventArgs e)
        {
            var acclist = acdatas.ItemsSource as List<Account>;
            try
            {
                var filtered = acclist.Where(s => s.accNum.StartsWith(acsrch.Text)).ToList();
                acdatas.ItemsSource = null;
                acdatas.ItemsSource = filtered;
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("Empty !!!");
                acsrch.Text = null;
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            datagrid.ItemsSource = null;
            var item = acdatas.SelectedItem as Account;
            Model1 ce = new Model1();
            var transacts = ce.transactions.Where(s => s.accid == item.id).ToList();
            datagrid.ItemsSource = transacts;
        }

        private void sdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time1,time2;
            time1 = (DateTime)sdate.SelectedDate;
            if (edate.SelectedDate == null)
            {
                time2 = DateTime.Now;
            }
            else
            {
                time2 = (DateTime)edate.SelectedDate;
            }
            List<transaction> transs = datagrid.ItemsSource as List<transaction>;
            var t = transs.Where(s => s.modified > time1 && s.modified < time2).ToList();
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = t;
        }

        private void edate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time1, time2;
            time2 = (DateTime)edate.SelectedDate;
            if (sdate.SelectedDate == null)
                return;
            time1 = (DateTime)sdate.SelectedDate;
            List<transaction> transs = datagrid.ItemsSource as List<transaction>;
            var t = transs.Where(s => s.modified > time1 && s.modified < time2).ToList();
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = t;
        }
    }
}
