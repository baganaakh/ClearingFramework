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
    public partial class Repo : Page
    {
        public Repo()
        {
            InitializeComponent();
            FillGrid();
            bindCombo();
        }
        string linkacs, toId="0";
        decimal assId,totSum,toPay,inter;
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        clearingEntities CE = new clearingEntities();        
        #region Илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                using (clearingEntities context = new clearingEntities())
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
                    dealType=1,
                };
                context.AdminOrders.Add(order);
                context.SaveChanges();
                FillGrid();
            }
        }
        #endregion
        #region datagrid fill
        private void FillGrid()
        {
            clearingEntities de = new clearingEntities();
            List<AdminOrder> ord= de.AdminOrders.Where(s => (s.memberid != memId && s.connect == "0" && s.state == 0)
            || (s.memberid != memId && s.connect == memId.ToString() && s.state == 0) ).ToList();
            totalOrder.ItemsSource = ord;
            List<AdminOrder> ords = de.AdminOrders.Where(s => s.memberid == memId).ToList();
            OwnTable.ItemsSource = ords;
            soldTable.ItemsSource= de.AdminDeals.Where(s => s.memberid == memId && s.side == -1).ToList();
            boughtTable.ItemsSource= de.AdminDeals.Where(s => s.memberid != memId && s.side == 1).ToList();
            repoHistory.ItemsSource = de.AdminDeals.Where(s=> s.memberid == memId).ToList();
        }
        #endregion
        #region Нийт захиалга зөвшөөрөх
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminOrder value = (AdminOrder)totalOrder.SelectedItem;
            if (null == value) return;
            if(linkAc_Copy.SelectedItem == null)
            {
                MessageBox.Show("Авах дансаа сонго нуу");
                return;
            }
            int useracc = Convert.ToInt32(linkAc_Copy.SelectedValue);
            using (clearingEntities contx=new clearingEntities())
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
            using (var contx=new clearingEntities())
            {
            AdminOrder statss = contx.AdminOrders.FirstOrDefault(s=> s.id == id);
            statss.state = 1;
            contx.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region өөрийн захиалга Цуцлах Устгах
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var value = OwnTable.SelectedItem as AdminOrder;
            if (value == null) return;
            using (clearingEntities conx = new clearingEntities())
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
            //}

            //Order value = (Order)OwnTable.SelectedItem;
            //if (null == value) return;
            //int id = Convert.ToInt32(value.id);
            //using (var contx = new demoEntities1())
            //{
            //    Order statss = contx.Orders.FirstOrDefault(s => s.id == id);
            //    contx.Orders.Remove(statss);
            //    contx.SaveChanges();
            //}
            FillGrid();
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
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.accNum).ToList();
                foreach(var i in acclist)
                {
                    var detail = CE.AccountDetails.Where(s=>s.accNum == i).Select(s=> s.assetId).ToArray();
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
                MessageBox.Show(ex.ToString());
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
            remainder.Text = sum.ToString("0.##");
            try
            {
                int iid = item.id;
                decimal ratio = item.ratio;
                decimal lastPrice = ratio * eprice;
                possible.Text = (lastPrice * sum).ToString("0.##");
                exPrice.Text = lastPrice.ToString("0.##");
            }
            catch
            {
                throw;
            }
        }
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void dayy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem days = (ComboBoxItem)dayy.SelectedItem;
            if (days == null)
            {
                return;
            }
            int days1 = Convert.ToInt32(days.Content);
            var Interst = CE.AdminInterests.Where(s => s.assetid == assId).FirstOrDefault<AdminInterest>();
            decimal Interst1 =Convert.ToDecimal( Interst.interest);
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
                toId = item.id.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                if (qty <= Convert.ToInt32(remainder.Text))
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
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        #endregion
    }
}
