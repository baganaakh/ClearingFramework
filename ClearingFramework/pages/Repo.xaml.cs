using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class Repo : Page
    {
        public Repo()
        {
            InitializeComponent();
            
            bindCombo();
        }
        string linkacs;
        long toId = 0;
        decimal assId,totSum,toPay,inter;
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        Model1 CE = new Model1();
        #region Илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                using (Model1 context = new Model1())
            {
                AdminOrder order = new AdminOrder()
                {
                    accountid = Convert.ToInt64(linkacs),
                    assetid = Convert.ToInt64(asset.SelectedValue),
                    price=Convert.ToDecimal(exPrice.Text),
                    qty = Convert.ToInt32(qtyss.Text),
                    day = Convert.ToInt32(dayy.Text),
                    totSum = totSum,
                    connect = toId,
                    modified = DateTime.Now,
                    memberid = memId,
                    state = 0,
                    toPay = toPay,
                    interests = inter,
                    dealType=3,
                    side=0,
                };
                context.AdminOrders.Add(order);
                context.SaveChanges();
                
            }
        }
        #endregion
        #region datagrid fill
            #region Нийтзахиалга
            private void Нийтзахиалг(object sender, RoutedEventArgs e)
            {
                Model1 de = new Model1();
            //List<AdminOrder> ord= de.AdminOrders.Where(s => (s.memberid != memId && s.connect == "0" && s.state == 0)
            //|| (s.memberid != memId && s.connect == memId.ToString() && s.state == 0) ).ToList();

            //dealtype=3   connect=0 || connect=memid state=0
            List<AdminOrder> ord= de.AdminOrders.Where(s =>s.memberid != memId 
                                                            && s.dealType == 3 
                                                            && s.state == 0 
                                                            && s.connect == 0 
                                                        ).ToList();
                Нийтзахиалга.ItemsSource = ord;
                var t=from tt in ord
                      join a1 in de.AdminAssets on tt.assetid equals a1.id
                      select new
                      {
                          a1.id,
                          a1.name,
                      };
                totalasset.ItemsSource = t.Distinct();
            }
            private void totalasset_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                int item =(int) totalasset.SelectedValue;
                Model1 de = new Model1();
                List<AdminOrder> ord = de.AdminOrders.Where(s => (s.memberid != memId && s.connect == 0 && s.state == 0 && s.assetid == item)
                 || (s.memberid != memId && s.connect == memId && s.state == 0 && s.assetid == item)).ToList();
                Нийтзахиалга.ItemsSource = null;
                Нийтзахиалга.ItemsSource = ord;
            }
            #endregion
            #region Өөрийнзахиалга
        private void OwnAssetC(object sender, SelectionChangedEventArgs e)
        {
            int item =Convert.ToInt32(OwnAsset.SelectedValue);
            Өөрийнзахиалга.ItemsSource = null;
            Model1 de = new Model1();
            List<AdminOrder> ords = de.AdminOrders.Where(s => s.memberid == memId && s.assetid==item).ToList();
            Өөрийнзахиалга.ItemsSource = ords;
            
        }
        private void Өөрийнзахиалг(object sender, RoutedEventArgs e)
        {
            Model1 de = new Model1();
            List<AdminOrder> ords = de.AdminOrders.Where(s => s.memberid == memId).ToList();
            Өөрийнзахиалга.ItemsSource = ords;
            var t = from tt in ords
                    join a1 in de.AdminAssets on tt.assetid equals a1.id
                    select new
                    {
                        a1.id,
                        a1.name,
                    };
            OwnAsset.ItemsSource = t.Distinct();
        }
        #endregion
            #region Зарсанхэлцэл
            private void Зарсанхэлцэ(object sender, RoutedEventArgs e)
            {
                Model1 de = new Model1();
                var data= de.AdminDeals.Where(s => s.memberid == memId && s.side == -1).ToList();
                Зарсанхэлцэл.ItemsSource = data;
                var t = from tt in data
                        join a1 in de.AdminAssets on tt.assetid equals a1.id
                        select new
                        {
                            a1.id,
                            a1.name,
                        };
                soldre.ItemsSource = t.Distinct();
            }

            private void soldre_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                int item = (int)soldre.SelectedValue;
                Model1 de = new Model1();
                var data = de.AdminDeals.Where(s => s.memberid == memId && s.side == -1 && s.assetid == item).ToList();
                Зарсанхэлцэл.ItemsSource = null;
                Зарсанхэлцэл.ItemsSource = data;
            }

            #endregion
            #region Авсанхэлцэл
            private void Авсанхэлц(object sender, SelectionChangedEventArgs e)
            {
                int item =Convert.ToInt32(ast.SelectedValue);
                Model1 de = new Model1();
                var data = de.AdminDeals.Where(s => s.memberid != memId && s.side == 1 && s.assetid == item).ToList();
                Авсанхэлцэл.ItemsSource = null;
                Авсанхэлцэл.ItemsSource = data;

            }
            private void Авсанхэлцэ(object sender, RoutedEventArgs e)
            {
                Model1 de = new Model1();
                var data= de.AdminDeals.Where(s => s.memberid != memId && s.side == 1).ToList();
                Авсанхэлцэл.ItemsSource = data;
                var t = from tt in data
                        join a1 in de.AdminAssets on tt.assetid equals a1.id
                        select new
                        {
                            a1.id,
                            a1.name,
                        };
                ast.ItemsSource = t.Distinct();
            }
        #endregion
            #region Хэлцлийнтүүх
        private void Хэлцлийнтүү(object sender, RoutedEventArgs e)
            {
                Model1 de = new Model1();
                var data= de.AdminDeals.Where(s => s.memberid == memId && s.dealType==3).ToList();
                Хэлцлийнтүүх.ItemsSource = data;
                var t=from tt in data
                      join a1 in de.AdminAssets on tt.assetid equals a1.id
                      select new
                      {
                          a1.id,
                          a1.name,
                      };
                histasst.ItemsSource = t.Distinct();
            }

            private void histasst_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                int item = (int)histasst.SelectedValue;
                Model1 de = new Model1();
                var data = de.AdminDeals.Where(s => s.memberid == memId && s.assetid == item).ToList();
                Хэлцлийнтүүх.ItemsSource = null;
                Хэлцлийнтүүх.ItemsSource = data;
            }
        private void sdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time1, time2;
            time1 = (DateTime)sdate.SelectedDate;
            if (edate.SelectedDate == null)
            {
                time2 = DateTime.Now;
            }
            else
            {
                time2 = (DateTime)edate.SelectedDate;
            }
            List<AdminDeal> transs = Хэлцлийнтүүх.ItemsSource as List<AdminDeal>;
            var t = transs.Where(s => s.modified > time1 && s.modified < time2).ToList();
            Хэлцлийнтүүх.ItemsSource = null;
            Хэлцлийнтүүх.ItemsSource = t;
        }

        private void edate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time1, time2;
            time2 = (DateTime)edate.SelectedDate;
            if (sdate.SelectedDate == null)
                return;
            time1 = (DateTime)sdate.SelectedDate;
            List<AdminDeal> transs = Хэлцлийнтүүх.ItemsSource as List<AdminDeal>;
            var t = transs.Where(s => s.modified > time1 && s.modified < time2).ToList();
            Хэлцлийнтүүх.ItemsSource = null;
            Хэлцлийнтүүх.ItemsSource = t;
        }
        #endregion
        #endregion
        #region Нийт захиалга зөвшөөрөх
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminOrder value = (AdminOrder)Нийтзахиалга.SelectedItem;
            if (null == value) return;
            if(linkAc_Copy.SelectedItem == null)
            {
                MessageBox.Show("Авах дансаа сонго нуу");
                return;
            }
            int useracc = Convert.ToInt32(linkAc_Copy.SelectedValue);
            using (Model1 contx=new Model1())
            {
            AdminDeal pageDeal1 = new AdminDeal()
                {
                    accountid=useracc,
                    assetid=value.assetid,
                    day=value.day,
                    interests=value.interests,
                    modified=DateTime.Now,
                    price=value.price,
                    qty=value.qty,
                    side=1,
                    toPay=value.toPay,
                    totalPrice=value.totSum,
                    memberid=memId,
                    dealType=value.dealType,
                    connect=value.connect,
                };
            AdminDeal pageDeal2 = new AdminDeal()
                {
                    accountid=value.accountid,
                    assetid=value.assetid,
                    day=value.day,
                    interests=value.interests,
                    modified=DateTime.Now,
                    price=value.price,
                    qty=-value.qty,
                    side=-1,
                    toPay=-value.toPay,
                    totalPrice=-value.totSum,
                    memberid=value.memberid,
                    dealType=value.dealType,
                    connect=value.connect,
                };
            contx.AdminDeals.Add(pageDeal1);
            contx.AdminDeals.Add(pageDeal2);
            contx.SaveChanges();
            }
            int id = Convert.ToInt32(value.id);
            using (var contx=new Model1())
            {
            AdminOrder statss = contx.AdminOrders.FirstOrDefault(s=> s.id == id);
            statss.state = 1;
            contx.SaveChanges();
            }
            
        }
        #endregion
        #region өөрийн захиалга Цуцлах Устгах
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var value = Өөрийнзахиалга.SelectedItem as AdminOrder;
            if (value == null) return;
            using (Model1 conx = new Model1())
            {
                var del = conx.AdminOrders.Where(x => x.id == value.id).First();
                conx.AdminOrders.Remove(del);
                conx.SaveChanges();
            }
            
            //var checkedRows = from DataGridViewRow r in OwnTable.Items
            //                  where Convert.ToBoolean(r.Cells[0].Value) == true
            //                  select r;
            //foreach(var row in checkedRows)
            //{
            //    MessageBox.Show("dsdsd");
 

            //Order value = (Order)OwnTable.SelectedItem;
            //if (null == value) return;
            //int id = Convert.ToInt32(value.id);
            //using (var contx = new demoEntities1())
            //{
            //    Order statss = contx.Orders.FirstOrDefault(s => s.id == id);
            //    contx.Orders.Remove(statss);
            //    contx.SaveChanges();
            //}
            
        }
        #endregion
        #region combos selection change                 
        public List<AdminMember> members{ get; set; }
        private void bindCombo()
        {
            var acclist = CE.AdminAccounts.Where(s => s.memberid == memId && s.accountType == 3).ToList();
            
            linkAc.ItemsSource = acclist;
            linkAc_Copy.ItemsSource = acclist;

            var memlist = CE.AdminMembers.ToList();
            members = memlist;
            var rem = memlist.Find(x => x.id == memId);
            members.Remove(rem);

            AdminMember allItem = new AdminMember() {code= "Бүгд", id= 0};
            members.Add(allItem);
            membee.ItemsSource = members;
            int indexx = members.Count();
            membee.SelectedIndex = indexx-1;
        }
        private void linkAc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            asset.IsEnabled = true;
            asset.ItemsSource = null;
            var item = linkAc.SelectedItem as AdminAccount;
            try
            {
                linkacs = item.id.ToString();
                List<AdminAsset> assets = new List<AdminAsset>();
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.id).ToList();
                foreach(var i in acclist)
                {
                    var detail = CE.AccountDetails.Where(s=>s.id == i).Select(s=> s.assetId).ToArray();
                    int ids = Convert.ToInt32(detail[0]);
                    var asst = CE.AdminAssets.Where(s => s.id == ids).FirstOrDefault<AdminAsset>();
                    assets.Add(asst);
                }
                if(assets.Count == 0)
                {
                    MessageBox.Show("Таны "+item.accNumber+" дансанд үнэт цаас байхгүй байна");
                    return;
                }
                asset.ItemsSource = assets.Distinct();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
                //throw;
            }
        }
        private void asset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            qtyss.IsEnabled = true;
            var item = asset.SelectedItem as AdminAsset;
            if (item == null)
            {
                remainder.Text = null;
                exPrice.Text = null;
                possible.Text = null;
                return;
            }
            assId = item.id;
            var totNumber = CE.AccountDetails.Where(s => s.assetId == assId && s.linkAcc == linkacs).ToArray();
            decimal eprice =Convert.ToDecimal(item.price);
            decimal sum=0,freezesum=0;
            foreach(AccountDetail i in totNumber)
            {
                sum += Convert.ToDecimal( i.totalNumber);
                freezesum += Convert.ToDecimal(i.freezeValue);
            }
            remainder.Text = sum.ToString(".##");
            try
            {
                int iid = item.id;
                decimal ratio = Convert.ToDecimal(item.ratio);
                decimal lastPrice = ratio * eprice;
                possible.Text = (lastPrice * sum).ToString(".##");
                exPrice.Text = lastPrice.ToString(".##");
            }
            catch
            {
                throw;
            }
        }

        #region number
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion
        private void dayy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem days = (ComboBoxItem)dayy.SelectedItem;
            if (days == null)
            {
                return;
            }
            int days1 = Convert.ToInt32(days.Content);
            var Interst = CE.AdminInterests.Where(s => s.assetid == assId).FirstOrDefault<AdminInterest>();
            if(Interst == null)
            {
                MessageBox.Show("asset songoogui bna");
                return;
            }
            decimal Interst1 =Convert.ToDecimal(Interst.interest);
            inter = Convert.ToDecimal(TotalSum.Text) * days1 * Interst1;
            Inter.Text = inter.ToString("0.##");
            toPay = Convert.ToDecimal(TotalSum.Text) + Convert.ToDecimal(Inter.Text);
            ToPay.Text = toPay.ToString("0.##");
        }
        private void membee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = membee.SelectedItem as AdminMember;
            try
            {
                toId = item.id;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        #endregion
        #region QTYSS textchanged
        private void qtyss_TextChanged(object sender, TextChangedEventArgs e)
        {
            dayy.IsEnabled = true;
            dayy.SelectedItem = null;
            Inter.Text = null;
            ToPay.Text = null;
            try
            {
            int qty=Convert.ToInt32( qtyss.Text);
                if (qty < Convert.ToInt32(remainder.Text))
                {
                    totSum = qty * Convert.ToDecimal(exPrice.Text);
                    TotalSum.Text = totSum.ToString();
                }
                else
                {
                    MessageBox.Show("Хэмжээ үлдэгдэлээс илүү гарч болохгүй !!!!");
                    qtyss.Text = null;
                    TotalSum.Text = null;
                    return;
                }
            }
            catch (System.FormatException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        #endregion
    }
}
