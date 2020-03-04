using ClearingFramework;
using ClearingFramework.dbBind;
using ClearingFramework.dbBind.pageDatabase;
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
using Account1 = ClearingFramework.dbBind.Account;
using Account2 = ClearingFramework.dbBind.pageDatabase.Account;
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
        string linkacs, accnum,toId="0";
        decimal assId,totSum,toPay,inter,fe;
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        ClearingEntities CE = new ClearingEntities();
        demoEntities1 DE = new demoEntities1();
        #region Илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {

                using (demoEntities1 context = new demoEntities1())
            {
                Order order = new Order()
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
                    dealType=4,
                    
                };
                context.Orders.Add(order);
                context.SaveChanges();
                FillGrid();
            }
        }
        #endregion
        #region datagrid fill
        private void FillGrid()
        {
            demoEntities1 de = new demoEntities1();
            List<Order> ord= de.Orders.Where(s => (s.memberid != memId && s.connect == "0" && s.state == 0) || (s.memberid != memId && s.connect == memId.ToString() && s.state == 0) ).ToList();
            totalOrder.ItemsSource = ord;
            List<Order> ords = de.Orders.Where(s => s.memberid == memId).ToList();
            OwnTable.ItemsSource = ords;
            soldTable.ItemsSource= de.Deals.Where(s => s.memberid == memId && s.side == -1).ToList();
            boughtTable.ItemsSource= de.Deals.Where(s => s.memberid != memId && s.side == 1).ToList();
            repoHistory.ItemsSource = de.Deals.Where(s=> s.memberid == memId).ToList();
        }
        #endregion
        #region Нийт захиалга зөвшөөрөх
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Order value = (Order)totalOrder.SelectedItem;
            if (null == value) return;
            if(linkAc_Copy.SelectedItem == null)
            {
                MessageBox.Show("Авах дансаа сонго нуу");
                return;
            }
            int useracc = Convert.ToInt32(linkAc_Copy.SelectedValue);
            using (demoEntities1 contx=new demoEntities1())
            {
            Deal pageDeal1 = new Deal()
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
            Deal pageDeal2 = new Deal()
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
            contx.Deals.Add(pageDeal1);
            contx.Deals.Add(pageDeal2);
            contx.SaveChanges();
            }
            int id = Convert.ToInt32(value.id);
            using (var contx=new demoEntities1())
            {
            Order statss = contx.Orders.FirstOrDefault(s=> s.id == id);
            statss.state = 1;
            contx.SaveChanges();
            }
            FillGrid();
        }
        #endregion
        #region өөрийн захиалга Цуцлах Устгах
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var checkedRows = from DataGridViewRow r in OwnTable.Items
                              where Convert.ToBoolean(r.Cells[0].Value) == true
                              select r;
            foreach(var row in checkedRows)
            {
                MessageBox.Show("dsdsd");
            }

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
        public List<Account2> acc { get; set; }
        public List<Asset> assets { get; set; }
        public List<Member> members{ get; set; }
        private void bindCombo()
        {
            var acclist = DE.Accounts.Where(s => s.memberid == memId && s.accType == 3).ToList();
            acc = acclist;
            linkAc.ItemsSource = acc;
            linkAc_Copy.ItemsSource = acc;
            var memlist = DE.Members.ToList();
            members = memlist;
            var rem = members.Find(x => x.id == memId);
            members.Remove(rem);
            Member allItem = new Member() {name= "Бүгд", id= 0};
            members.Add(allItem);
            membee.ItemsSource = members;
            int indexx = members.Count();
            membee.SelectedIndex = indexx-1;
        }
        private void linkAc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            asset.IsEnabled = true;
            var item = linkAc.SelectedItem as Account2;
            try
            {
                linkacs = item.id.ToString();
                List<Asset> assets = new List<Asset>();
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.idNum).ToList();
                foreach(var i in acclist)
                {
                    var detail = CE.Accounts.Where(s => s.idNum == i).Select(s=> s.assetid).ToArray();
                    int ids = Convert.ToInt32(detail[0]);
                    var asst = DE.Assets.Where(s => s.id == ids).FirstOrDefault<Asset>();
                    assets.Add(asst);
                }
                asset.ItemsSource = assets.Distinct();
            }
            catch
            {
                throw;
            }
        }
        private void asset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            qtyss.IsEnabled = true;
            var item = asset.SelectedItem as Asset;
            assId = item.id;
            var totNumber = CE.Accounts.Where(s => s.assetid == assId && s.linkAcc == linkacs).ToArray();
            decimal sum=0,freezesum=0;
            foreach(Account1 i in totNumber)
            {
                sum += Convert.ToDecimal( i.totalNumber);
                freezesum += Convert.ToDecimal(i.freezeValue);
            }
            remainder.Text = sum.ToString("0.##");
            try
            {
                int iid = item.id;
                RefPrice eprice = DE.RefPrices.Where(s => s.assetId == iid).FirstOrDefault<RefPrice>(); //error if no refprice found releated to asset
                decimal eprice2 = eprice.refprice1 / 100;
                decimal ratio = item.ratio;
                decimal lastPrice = ratio * eprice2;
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
            var Interst = DE.Interests.Where(s => s.assetid == assId).FirstOrDefault<Interest>();
            decimal Interst1 =Convert.ToDecimal( Interst.interest1);
            inter = Convert.ToDecimal(TotalSum.Text) * days1 * Interst1;
            Inter.Text = inter.ToString("0.##");
            toPay = Convert.ToDecimal(TotalSum.Text) + Convert.ToDecimal(Inter.Text);
            ToPay.Text = toPay.ToString("0.##");
        }
        private void membee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = membee.SelectedItem as Member;
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
