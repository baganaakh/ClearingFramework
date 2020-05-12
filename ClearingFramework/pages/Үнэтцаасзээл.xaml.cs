using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
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
using AccountDetail = ClearingFramework.dbBind.AccountDetail;
using MessageBox = System.Windows.MessageBox;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Repo.xaml
    /// interst, refPrice, account-totalsum
    /// </summary>
    public partial class Үнэтцаасзээл : Page
    {
        public Үнэтцаасзээл()
        {
            InitializeComponent();
            bindCombo();            
        }
        int asst1, asst2,qty,qty2,day;
        decimal price,price2,inter,topay,totSum;
        int memid= Convert.ToInt32(App.Current.Properties["member_id"]);        
        Model1 CE = new Model1();
        #region bindcombo
        private void bindCombo()
        {
            assett.ItemsSource = CE.AdminAssets.ToList();
            List<Account> t1 = CE.Accounts.Where(s => s.memId == memid).ToList<Account>();
            var t = from tt in t1
                    join acde in CE.AccountDetails on tt.id equals acde.accountId
                    join ass in CE.AdminAssets on acde.assetId equals ass.id
                    select new
                    {
                        ass.id,
                        ass.name,
                    };
            var t2 = t.Distinct();


            //var ast2= CE.AdminAssets.SqlQuery("select * from AdminAssets" +
            //    " where id= any (select assetId from model1.dbo.AccountDetails t1 " +
            //    "inner join model1.dbo.account t2 on t1.accNum = t2.accNum " +
            //    "where t2.memId = " + memid + " )").ToList<AdminAsset>();


            asset2.ItemsSource = t2;
            var memlist = CE.AdminMembers.ToList();
            var rem = memlist.Find(x => x.id == memid);
            memlist.Remove(rem);
            AdminMember allItem = new AdminMember() { code = "Бүгд", id = 0 };
            memlist.Add(allItem);
            membee.ItemsSource = memlist;            
            membee.SelectedIndex = memlist.Count() - 1;
        }
        #endregion
        #region number
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }

        #endregion
        #region Цуцлах Clear
        private void ClearFields(object sender, RoutedEventArgs e)
        {
            assett.SelectedItem = null;
            qtyss.Text = null;
            wagerValue.Text = null;
            asset2.SelectedItem = null;
            ast2price.Text = null;
            remain.Text = null;
            qtyss2.Text = null;
            membee.SelectedItem = null;
            dayy.SelectedItem = null;
            Inter.Text = null;
            TotalSum.Text = null;
            ToPay.Text = null;
        }

        #endregion
        #region textboxes changed
        private void qtyss_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ast = assett.SelectedItem as AdminAsset;
            decimal price =Convert.ToDecimal(ast.price);
            try
            {
                qty = Convert.ToInt32(qtyss.Text);
                wagerValue.Text = (qty * price).ToString("0.#");
            }
            catch (System.FormatException)
            {
                return;
            }
            catch (System.NullReferenceException)
            {
                return;
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("Хязгаар хэтэрлээ !!!");
                qtyss.Text = null;
                return;
            }

        }
        private void qty2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                qty2 = Convert.ToInt32(qtyss2.Text);
                if (qty2 < Convert.ToInt32(remain.Text))
                {
                    totSum = price2 * qty2;
                    TotalSum.Text = totSum.ToString("0.#");
                }
                else
                {
                    MessageBox.Show("Хэмжээ үлдэгдэлээс илүү гарч болохгүй !!!!");
                    qtyss2.Text = null;
                    TotalSum.Text = null;
                    return;
                }
            }
            catch (System.FormatException)
            {
                return;
            }
        }
        #endregion
        #region Илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 conx =new Model1())
            {
                AdminOrder order = new AdminOrder()
                {
                    assetid=asst1,
                    qty=qty,
                    price=price,
                    assetid2=Convert.ToInt64(asset2.SelectedValue),
                    qty2=qty2,
                    memberid=memid,
                    modified=DateTime.Now,
                    connect=Convert.ToInt64(membee.SelectedValue),
                    interests=inter,
                    dealType=4,
                    day=day,
                    totSum=totSum,
                    toPay=topay,
                };
                conx.AdminOrders.Add(order);
                conx.SaveChanges();
                ClearFields(null, null);
            }            
        }
        #endregion
        #region combos selections changed
        private void asset2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ast2 = asset2.SelectedValue;
            asst2 = Convert.ToInt32(ast2);
            //price2 =Convert.ToDecimal(ast2.price);
            ast2price.Text = price.ToString();
            var t = from tt in CE.Accounts.Where(s => s.memId == memid)
                    join acde in CE.AccountDetails on tt.id equals acde.accountId
                    join ass in CE.AdminAssets.Where(s => s.id == asst2) on acde.assetId equals ass.id
                    select new
                    {
                        ass.price,
                        acde.totalNumber,
                    };
                  

            //var ast = CE.AccountDetails.SqlQuery("" +
            //    "select * from model1.dbo.AccountDetails t1 " +
            //    "inner join model1.dbo.account t2 on t1.accNum =t2 .accNum " +
            //    "where t2.memId = " + memid + " and t1.assetId=" + ast2.id + "").FirstOrDefault<AccountDetail>();

            //remain.Text = Convert.ToInt32(t.totalNumber).ToString();
        }
        private void assett_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ast = assett.SelectedItem as AdminAsset;
            price = Convert.ToDecimal(ast.price);
            asst1 = ast.id;
        }
        private void day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem days = (ComboBoxItem)dayy.SelectedItem;
            if (days == null)
                return;
            day = Convert.ToInt32(days.Content);            
            var Interst = CE.AdminInterests.Where(s => s.assetid == asst2).FirstOrDefault<AdminInterest>();
            decimal interest;
            try
            {
                 interest = Convert.ToDecimal(Interst.interest);
            }
            catch (System.NullReferenceException)
            {
                return;                
            }
            inter = totSum * day * interest;
            Inter.Text = inter.ToString("0.#");
            topay = totSum + inter;
            ToPay.Text = topay.ToString("0.#");
        }
        #endregion
        #region fill
            #region Зээлийн түүх
            private void Зээлийнтүү(object sender, RoutedEventArgs e)
            {
                
            }
        #endregion
        #region Нийт Зээл
        private void НийтЗээ(object sender, RoutedEventArgs e)
            {
                Model1 de = new Model1();
                var data= de.AdminOrders.Where(s => s.dealType == 4).ToList();
                НийтЗээл.ItemsSource = data;
                var t=from tt in data
                      join a1 in de.AdminAssets on tt.assetid equals a1.id
                      select new
                      {
                          a1.id,
                          a1.name,
                      };
                НийтЗэ.ItemsSource = t.Distinct();
            }

            private void НийтЗэ_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                НийтЗээл.ItemsSource = null;
                Model1 de = new Model1();
                int item = (int)НийтЗэ.SelectedValue;
                var data = de.AdminOrders.Where(s => s.dealType == 4 && s.assetid == item  ).ToList();
                НийтЗээл.ItemsSource = data;
            }
        #endregion
        #region Өөрийнзээл
        private void OwnAssetC(object sender, SelectionChangedEventArgs e)
        {
            int item = Convert.ToInt32(owncombo.SelectedValue);
            OwnTable.ItemsSource = null;
            Model1 de = new Model1();
            List<AdminOrder> ords = de.AdminOrders.Where(s => s.memberid == memid && s.assetid == item).ToList();
            OwnTable.ItemsSource = ords;

        }
        private void Өөрийнзээл(object sender, RoutedEventArgs e)
        {
            Model1 de = new Model1();
            List<AdminOrder> ords = de.AdminOrders.Where(s => s.memberid == memid).ToList();
            OwnTable.ItemsSource = ords;
            var t = from tt in ords
                    join a1 in de.AdminAssets on tt.assetid equals a1.id
                    select new
                    {
                        a1.id,
                        a1.name,
                    };
            OwnTable.ItemsSource = t.Distinct();
        }
        #endregion
        #endregion
    }
}
