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
    /// Interaction logic for Collateral.xaml
    /// </summary>
    public partial class Collateral : Page
    {
        clearingEntities CE = new clearingEntities();
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        string linkacs;
        decimal assId;
        public Collateral()
        {
            InitializeComponent();
            bindCombo();
            FillGrid();
        }
        #region fill
        private void FillGrid()
        {
            int asset1;            
            clearingEntities ce = new clearingEntities();

            List<ForGrid> ToDisplay = new List<ForGrid>();
            List<ForGrid> ToDisplay2 = new List<ForGrid>();
            List<ForGrid> Биржбарьцаа = new List<ForGrid>();

            List<AdminTransaction> trans = ce.AdminTransactions.ToList();
            #region Барьцаа түүх
            foreach (AdminTransaction items in trans)
            {
                asset1 = Convert.ToInt32(items.assetId);
                AdminAsset asst = ce.AdminAssets.Where(r => r.id == asset1).FirstOrDefault<AdminAsset>();
                decimal price = Convert.ToDecimal(asst.price);

                AdminMember memb = ce.AdminMembers.Where(r => r.id == memId).FirstOrDefault<AdminMember>();
                short type =Convert.ToInt16( memb.type);

                Adminmtype mty = ce.Adminmtypes.Where(r => r.id == type).FirstOrDefault<Adminmtype>();
                int minval = Convert.ToInt32(mty.minValue);

                decimal ratio = asst.ratio;
                decimal qty = Convert.ToDecimal(items.amount);
                decimal totval = qty * (ratio * price);
                ForGrid data = new ForGrid()
                {
                    id = Convert.ToInt64(items.id),
                    accNumber = items.accountId.ToString(),
                    Барьцаа = asst.name.ToString(),
                    Хэмжээ = Convert.ToInt32(qty),
                    ЖишигҮнэ = (ratio * price).ToString("0.###"),
                    НийтДүн = totval.ToString("0.###"),
                    ДоодДүн = minval.ToString("0.###"),
                    ЗөрүүДүн = (totval - minval).ToString("0.###"),
                    //БуцаахДүн=,
                };
                ToDisplay.Add(data);
            }
            collHistory.ItemsSource = ToDisplay;
            #endregion
            #region Хүлээгдэж Буй гүйлгээ
            List<AdminOrder> requs = ce.AdminOrders.ToList();
            foreach (AdminOrder items in requs)
            {
                asset1 = Convert.ToInt32(items.assetid);
                AdminAsset asst = ce.AdminAssets.Where(r => r.id == asset1).FirstOrDefault<AdminAsset>();
                decimal price= Convert.ToDecimal(asst.price);
                AdminMember memb = ce.AdminMembers.Where(r => r.id == memId).FirstOrDefault<AdminMember>();
                short type = Convert.ToInt16(memb.type);

                Adminmtype mty = ce.Adminmtypes.Where(r => r.id == type).FirstOrDefault<Adminmtype>();
                int minval = Convert.ToInt32(mty.minValue);

                decimal ratio = asst.ratio;
                decimal qty = Convert.ToDecimal(items.qty);
                decimal totval = qty * (ratio * price);
                ForGrid data = new ForGrid()
                {
                    id = Convert.ToInt64(items.id),
                    accNumber = items.accountid.ToString(),
                    Барьцаа = asset1.ToString("0.###"),
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
            #endregion
            #region Биржийн барьцаа
            var accNums = ce.Accounts.Where(s => s.memId == memId).Select(s => s.accNum).ToArray();
            foreach (var acnum in accNums)
            {
                var adet = ce.AccountDetails.Where(s => s.accNum == acnum).FirstOrDefault<AccountDetail>();
                AdminAsset asst = ce.AdminAssets.Where(s => s.id == adet.assetId).FirstOrDefault<AdminAsset>();
                if (asst == null) {
                                    MessageBox.Show("Asset empty");
                                    return;
                                  }
                decimal price= Convert.ToDecimal(asst.price);
                decimal jish = asst.ratio * price;
                decimal tot = Convert.ToDecimal(adet.totalNumber * jish);
                Adminmtype mty = ce.Adminmtypes.Where(r => r.id == 0).FirstOrDefault<Adminmtype>();
                int minval = Convert.ToInt32(mty.minValue);
                ForGrid data = new ForGrid()
                {
                    id = adet.id,
                    Барьцаа = asst.name,
                    Хэмжээ = Convert.ToInt32(adet.totalNumber),
                    ЖишигҮнэ = (jish).ToString("0.##"),
                    НийтДүн = (tot).ToString("0.##"),
                    ДоодДүн = minval.ToString("0.##"),
                    ЗөрүүДүн = (tot - minval).ToString("0.##"),
                };
                Биржбарьцаа.Add(data);
            }
            Биржийнбарьцаа.ItemsSource = Биржбарьцаа;
            #endregion
        }
        public class ForGrid
        {
            public ForGrid()
            {
            }
            public long id { get; set; }
            public string accNumber { get; set; }
            public string Барьцаа { get; set; }
            public int Хэмжээ { get; set; }
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
                qtyy *=(-1);
            using (clearingEntities contx = new clearingEntities())
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
            var item = accId.SelectedItem as AdminAccount;
            try
            {
                linkacs = item.id.ToString();
                List<AdminAsset> assets = new List<AdminAsset>();
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.accNum).ToList();
                foreach (var i in acclist)
                {
                    var detail = CE.AccountDetails.Where(s => s.accNum == i).Select(s => s.assetId).ToArray();
                    int ids = Convert.ToInt32(detail[0]);
                    var asst = CE.AdminAssets.Where(s => s.id == ids).FirstOrDefault<AdminAsset>();
                    assets.Add(asst);
                }
                asset.ItemsSource = assets.Distinct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        private void asset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = asset.SelectedItem as AdminAsset;
            qtyss.IsEnabled = true;
            qtyss.Text = null;
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
                decimal eprice =Convert.ToDecimal(item.price)/ 100;
                decimal ratio = item.ratio;
                decimal lastPrice = ratio * eprice;
                exPrice.Text = lastPrice.ToString("0.##");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                MessageBox.Show(ex.ToString());
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
//                      C:\Users\altanbagana.n\Documents\GitHub\ClearingFramework\ClearingFramework\functions\Гүйлгээ бүртгэх.xlsx
//Could not find file 'C:\Users\altanbagana.n\Documents\GitHub\ClearingFramework\ClearingFramework\bin\functions\Гүйлгээ бүртгэх.xlsx'.
//Could not find file 'C:\Users\altanbagana.n\Documents\GitHub\ClearingFramework\ClearingFramework\bin\Debug\functions\Гүйлгээ бүртгэх.xlsx'.
//Could not find file 'C:\Users\altanbagana.n\Documents\GitHub\ClearingFramework\ClearingFramework\bin\Debug\...\functions\Гүйлгээ бүртгэх.xlsx'.