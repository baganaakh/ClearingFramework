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
    /// Interaction logic for Барьцаа.xaml
    /// </summary>
    /// 

    public partial class Барьцаа : Page
    {
        Model1 CE = new Model1();
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        string linkacs;
        int minval;
        decimal assId;
        public Барьцаа()
        {
            InitializeComponent();
            bindCombo();
            AdminMember memb = CE.AdminMembers.Where(r => r.id == memId).FirstOrDefault<AdminMember>();
            if (memb == null)
            {
                MessageBox.Show("There is no member has id : " + memId);
                return;
            }
            short type = Convert.ToInt16(memb.type);
            List<Adminmtype> list = new List<Adminmtype>()
            {
                new Adminmtype(){id=0,minValue=50},
                new Adminmtype(){id=1,minValue=100},
                new Adminmtype(){id=3,minValue=150},
            };
            Adminmtype mty = list.Where(r => r.id == type).FirstOrDefault<Adminmtype>();
            minval = Convert.ToInt32(mty.minValue);

        }
        public partial class Adminmtype
        {
            public short id { get; set; }
            public string mtype { get; set; }
            public decimal? minValue { get; set; }
        }
        #region fill
        private void Брокерийнбарьцаа(object sender, RoutedEventArgs e)
        {

        }
        #region Хүлээгдэж Буй гүйлгээ
        private void Хүлээгдэжбуйгүйлгээ(object sender, RoutedEventArgs e)
        {
            List<ForGrid> ToDisplay2 = new List<ForGrid>();
            Model1 ce = new Model1();
            List<AdminOrder> requs = ce.AdminOrders.Where(r => r.state == 1 || r.state == 0 && r.memberid == memId).ToList<AdminOrder>();
            var t = from tt in requs
                    join a1 in ce.AdminAssets on tt.assetid equals a1.id
                    select new
                    {
                        tt.id,
                        astprice = a1.price,
                        tt.accountid,
                        tt.state,
                        a1.name,
                        a1.ratio,
                        tt.qty,
                        a1.price,
                    };
            foreach (var items in t)
            {
                decimal price = Convert.ToDecimal(items.price);
                decimal ratio = Convert.ToDecimal(items.ratio);
                decimal qty = Convert.ToDecimal(items.qty);
                decimal totval = qty * (ratio * price);
                ForGrid data = new ForGrid()
                {
                    id = Convert.ToInt64(items.id),
                    accNumber = items.accountid.ToString(),
                    Барьцаа = items.name,
                    Хэмжээ = Convert.ToInt32(qty),
                    ЖишигҮнэ = (ratio * price).ToString("0.###"),
                    НийтДүн = totval.ToString("0.###"),
                    ДоодДүн = minval.ToString("0.###"),
                    ЗөрүүДүн = (totval - minval).ToString("0.###"),
                    state = items.state == 0 ? "Denied" : "Pending",
                };
                ToDisplay2.Add(data);
            }
            pendingColl.ItemsSource = ToDisplay2;
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            List<ForGrid> data = pendingColl.ItemsSource as List<ForGrid>;
            List<ForGrid> filtered = data.Where(s => s.accNumber.StartsWith(srchacc1.Text)).ToList();
            pendingColl.ItemsSource = null;
            pendingColl.ItemsSource = filtered;
        }
        #endregion
        #region Барьцаа түүх
        private void БарьцааТүүх(object sender, RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            List<AdminTransaction> trans = ce.AdminTransactions.ToList();
            List<ForGrid> ToDisplay = new List<ForGrid>();

            var t = from tt in trans
                    join a in ce.AdminAssets on tt.assetId equals a.id
                    select new
                    {
                        tt.id,
                        tt.accountId,
                        a.name,
                        tt.amount,
                        a.ratio,
                        a.price,
                        totval = tt.amount * a.ratio * a.price,
                        refprice = a.ratio * a.price,
                        diff = tt.amount * a.ratio * a.price - minval
                    };

            foreach (var item in t)
            {
                decimal qty = Convert.ToDecimal(item.amount);
                decimal totval = Convert.ToDecimal(item.totval);
                decimal refprice = Convert.ToDecimal(item.refprice);
                decimal diff = Convert.ToDecimal(item.diff);

                ForGrid data = new ForGrid()
                {
                    id = Convert.ToInt64(item.id),
                    accNumber = item.accountId.ToString(),
                    Барьцаа = item.name.ToString(),
                    Хэмжээ = Convert.ToInt32(qty),
                    ЖишигҮнэ = refprice.ToString("0.###"),
                    НийтДүн = totval.ToString("0.###"),
                    ДоодДүн = minval.ToString("0.###"),
                    ЗөрүүДүн = diff.ToString("0.###"),
                    //БуцаахДүн=,
                };
                ToDisplay.Add(data);
            }
            //foreach (AdminTransaction items in trans)
            //{
            //    asset1 = Convert.ToInt32(items.assetId);
            //    AdminAsset asst = ce.AdminAssets.Where(r => r.id == asset1).FirstOrDefault<AdminAsset>();
            //    decimal price = Convert.ToDecimal(asst.price);

            //    decimal ratio = Convert.ToDecimal(asst.ratio);
            //    decimal qty = Convert.ToDecimal(items.amount);
            //    decimal totval = qty * (ratio * price);
            //    ForGrid data = new ForGrid()
            //    {
            //        id = Convert.ToInt64(items.id),
            //        accNumber = items.accountId.ToString(),
            //        Барьцаа = asst.name.ToString(),
            //        Хэмжээ = Convert.ToInt32(qty),
            //        ЖишигҮнэ = (ratio * price).ToString("0.###"),
            //        НийтДүн = totval.ToString("0.###"),
            //        ДоодДүн = minval.ToString("0.###"),
            //        ЗөрүүДүн = (totval - minval).ToString("0.###"),
            //        //БуцаахДүн=,
            //    };
            //    ToDisplay.Add(data);
            //}
            collHistory.ItemsSource = ToDisplay;
        }

        private void srchacc2_KeyUp(object sender, KeyEventArgs e)
        {
            List<ForGrid> data = collHistory.ItemsSource as List<ForGrid>;
            List<ForGrid> filtered = data.Where(s => s.accNumber.StartsWith(srchacc2.Text)).ToList();
            collHistory.ItemsSource = null;
            collHistory.ItemsSource = filtered;
        }
        #endregion
        #region Биржийн барьцаа
        private void Биржийнбарьца(object sender, RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            List<ForGrid> Биржбарьцаа = new List<ForGrid>();

            var accNums = ce.Accounts.Where(s => s.memId == memId).Select(s => s.id).ToArray();

            foreach (var acnum in accNums)
            {
                var adet = ce.AccountDetails.Where(s => s.accountId == acnum).FirstOrDefault<AccountDetail>();
                if (adet == null)
                {
                    MessageBox.Show("Asset empty");
                    return;
                }
                AdminAsset asst = ce.AdminAssets.Where(s => s.id == adet.assetId).FirstOrDefault<AdminAsset>();
                if (asst == null)
                {
                    MessageBox.Show("Дансны дугаарууд Үц байхгүй байна");
                    return;
                }
                decimal price = Convert.ToDecimal(asst.price);
                decimal jish = Convert.ToDecimal(asst.ratio * price);
                decimal tot = Convert.ToDecimal(adet.totalNumber * jish);
                //Adminmtype mty = ce.Adminmtypes.Where(r => r.id == 1).FirstOrDefault<Adminmtype>();
                //int minval = Convert.ToInt32(mty.minValue);
                ForGrid data = new ForGrid()
                {
                    id = adet.id,
                    Барьцаа = asst.name,
                    Хэмжээ = Convert.ToDecimal(adet.totalNumber),
                    ЖишигҮнэ = (jish).ToString("0.##"),
                    НийтДүн = (tot).ToString("0.##"),
                    ДоодДүн = minval.ToString("0.##"),
                    ЗөрүүДүн = (tot - minval).ToString("0.##"),
                };
                Биржбарьцаа.Add(data);
            }
            Биржийнбарьцаа.ItemsSource = Биржбарьцаа;
        }
        #endregion
        public class ForGrid
        {
            public ForGrid()
            {
            }
            public long id { get; set; }
            public string accNumber { get; set; }
            public string Барьцаа { get; set; }
            public decimal Хэмжээ { get; set; }
            public string ЖишигҮнэ { get; set; }
            public string НийтДүн { get; set; }
            public string ДоодДүн { get; set; }
            public string ЗөрүүДүн { get; set; }
            public string БуцаахДүн { get; set; }
            public string state { get; set; }
        }
        #endregion
        #region илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            short mod = Convert.ToInt16(Types.SelectedIndex);
            int qtyy = Convert.ToInt32(qtyss.Text);
            if (mod == 1)
                qtyy *= (-1);
            using (Model1 contx = new Model1())
            {
                AdminOrder order = new AdminOrder()
                {
                    accountid = Convert.ToInt64(accId.SelectedValue),
                    assetid = Convert.ToInt32(asset.SelectedValue),
                    side = mod,
                    qty = qtyy,
                    modified = DateTime.Now,
                    memberid = memId,
                    state = 1,
                    dealType = 4,
                };
                contx.AdminOrders.Add(order);
                contx.SaveChanges();
            }
        }
        #endregion
        #region combos
        private void bindCombo()
        {
            accId.ItemsSource = CE.AdminAccounts.Where(s => s.memberid == memId && s.accountType == 3).ToList();
        }
        private void accId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            asset.IsEnabled = true;
            asset.ItemsSource = null;
            var item = accId.SelectedItem as AdminAccount;
            try
            {
                linkacs = item.id.ToString();
                List<AdminAsset> assets = new List<AdminAsset>();
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.id).ToList();
                foreach (var i in acclist)
                {
                    var detail = CE.AccountDetails.Where(s => s.id == i).Select(s => s.assetId).ToArray();
                    int ids = Convert.ToInt32(detail[0]);
                    var asst = CE.AdminAssets.Where(s => s.id == ids).FirstOrDefault<AdminAsset>();
                    assets.Add(asst);
                }
                asset.ItemsSource = assets.Distinct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        private void asset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = asset.SelectedItem as AdminAsset;
            qtyss.IsEnabled = true;
            qtyss.Text = null;
            if (item == null)
                return;
            assId = item.id;
            var totNumber = CE.AccountDetails.Where(s => s.assetId == assId && s.linkAcc == linkacs).ToArray();
            decimal sum = 0, freezesum = 0;
            foreach (AccountDetail i in totNumber)
            {
                sum += Convert.ToDecimal(i.totalNumber);
                freezesum += Convert.ToDecimal(i.freezeValue);
            }
            remainder.Text = sum.ToString("0.##");
            try
            {
                int iid = item.id;
                decimal eprice = Convert.ToDecimal(item.price) / 100;
                decimal ratio = Convert.ToDecimal(item.ratio);
                decimal lastPrice = ratio * eprice;
                exPrice.Text = lastPrice.ToString("0.##");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        #endregion
        #region QTYSS text change number
        private void qtyss_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int qty = Convert.ToInt32(qtyss.Text);
                if (qty > Convert.ToInt32(remainder.Text))
                {
                    MessageBox.Show("Хэмжээ үлдэгдэлээс илүү гарч болохгүй !!!!");
                    qtyss.Text = null;
                    collateralValue.Text = null;
                    return;
                }
                decimal expr = Convert.ToDecimal(exPrice.Text);
                collateralValue.Text = (expr * qty).ToString();
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
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        #endregion

    }
}