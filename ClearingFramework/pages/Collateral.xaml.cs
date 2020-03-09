using ClearingFramework;
using ClearingFramework.dbBind;
using ClearingFramework.dbBind.AdminDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Account1 = ClearingFramework.dbBind.Account;
using Account2 = ClearingFramework.dbBind.AdminDatabase.Account;
namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Collateral.xaml
    /// </summary>
    public partial class Collateral : Page
    {
        ClearingEntities CE = new ClearingEntities();
        demoEntities1 DE = new demoEntities1();
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
            demoEntities1 de = new demoEntities1();
            List<ColReq> requs = de.ColReqs.ToList();
            List<forGrid> ToDisplay=new List<forGrid>();
            List<forGrid> ToDisplay2 = new List<forGrid>();
            List<Tran> trans = de.Trans.ToList();
            #region Барьцаа түүх
            
            foreach(Tran items in trans)
            {
                asset1 =Convert.ToInt32( items.currency);
                RefPrice refpri =de.RefPrices.Where(r => r.assetId ==  asset1).FirstOrDefault<RefPrice>();
                if (refpri == null)
                {
                    MessageBox.Show("RefPices has no value to connected to asset  "+asset1+" ");
                    return;
                }
                decimal refPrice = Convert.ToDecimal(refpri.refprice1);
                Asset asst = de.Assets.Where(r => r.id == asset1).FirstOrDefault<Asset>();
                Member memb = de.Members.Where(r => r.id == memId).FirstOrDefault<Member>();
                int type = memb.type;
                mtype mty = de.mtypes.Where(r => r.id == type).FirstOrDefault<mtype>();
                int minval =Convert.ToInt32( mty.minValue);
                decimal ratio = asst.ratio;
                decimal qty =Convert.ToDecimal(items.amount);
                decimal totval = qty * (ratio * refPrice);
                forGrid data = new forGrid()
                {
                    id=Convert.ToInt64( items.id),
                    accNumber = items.accountId.ToString(),
                    Барьцаа = asset1.ToString("0.###"),
                    Хэмжээ = qty.ToString("0.###"),
                    ЖишигҮнэ=(ratio*refPrice).ToString("0.###"),
                    НийтДүн=totval.ToString("0.###"),
                    ДоодДүн= minval.ToString("0.###"),
                    ЗөрүүДүн=(totval-minval).ToString("0.###"),
                    //БуцаахДүн=,

                };
                ToDisplay.Add(data);
            }
            collHistory.ItemsSource = ToDisplay;
            #endregion
            #region Хүлээгдэж Буй гүйлгээ
            requs = requs.Where(s => s.state == 0 || s.state == 2 ).ToList();
            foreach(ColReq items in requs)
            {
                asset1 =Convert.ToInt32( items.assetId);
                RefPrice refpri =de.RefPrices.Where(r => r.assetId ==  asset1).FirstOrDefault<RefPrice>();
                decimal refPrice = Convert.ToDecimal(refpri.refprice1);
                Asset asst = de.Assets.Where(r => r.id == asset1).FirstOrDefault<Asset>();
                Member memb = de.Members.Where(r => r.id == memId).FirstOrDefault<Member>();
                int type = memb.type;
                mtype mty = de.mtypes.Where(r => r.id == type).FirstOrDefault<mtype>();
                int minval =Convert.ToInt32( mty.minValue);
                decimal ratio = asst.ratio;
                decimal qty =Convert.ToDecimal(items.value);
                decimal totval = qty * (ratio * refPrice);
                forGrid data = new forGrid()
                {
                    id = Convert.ToInt64(items.id),
                    accNumber = items.accId.ToString(),
                    Барьцаа = asset1.ToString("0.###"),
                    Хэмжээ = qty.ToString("0.###"),
                    ЖишигҮнэ=(ratio*refPrice).ToString("0.###"),
                    НийтДүн=totval.ToString("0.###"),
                    ДоодДүн= minval.ToString("0.###"),
                    ЗөрүүДүн=(totval-minval).ToString("0.###"),
                    //БуцаахДүн=,

                };
                ToDisplay2.Add(data);
            }
                pendingColl.ItemsSource = ToDisplay2;
            #endregion
        }
        #endregion
        #region илгээх
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int qty = Convert.ToInt32(qtyss.Text);
            using (demoEntities1 contx = new demoEntities1())
            {
                ColReq req = new ColReq()
                {
                    accId = Convert.ToInt64(accId.SelectedValue),
                    assetId = Convert.ToInt32(asset.SelectedValue),
                    value = Convert.ToDecimal(qtyss.Text),
                    mode = Convert.ToInt16(Types.SelectedIndex),
                    modified = DateTime.Now,
                    memid = memId,
                    state = 0,
                };
                contx.ColReqs.Add(req);
                contx.SaveChanges();
            }
        }
        #endregion
        #region combos
        public List<Account2> acc { get; set; }
        private void bindCombo()
        {
            var acclist = DE.Accounts.Where(s => s.memberid == memId && s.accType == 3).ToList();
            acc = acclist;
            accId.ItemsSource = acc;
        }
        private void accId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            asset.IsEnabled = true;
            var item = accId.SelectedItem as Account2;
            try
            {
                linkacs = item.id.ToString();
                List<Asset> assets = new List<Asset>();
                var acclist = CE.Accounts.Where(s => s.linkAcc == linkacs).Select(s => s.idNum).ToList();
                foreach (var i in acclist)
                {
                    var detail = CE.Accounts.Where(s => s.idNum == i).Select(s => s.assetid).ToArray();
                    int ids = Convert.ToInt32(detail[0]);
                    var asst = DE.Assets.Where(s => s.id == ids).FirstOrDefault<Asset>();
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
            var item = asset.SelectedItem as Asset;
            qtyss.IsEnabled = true;
            qtyss.Text = null;
            assId = item.id;
            var totNumber = CE.Accounts.Where(s => s.assetid == assId && s.linkAcc == linkacs).ToArray();
            decimal sum = 0, freezesum = 0;
            foreach (Account1 i in totNumber)
            {
                sum += Convert.ToDecimal(i.totalNumber);
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
        public class forGrid
        {
            public forGrid()
            {
            }
            public long id { get; set; }
            public string accNumber { get; set; }
            public string Барьцаа { get; set; }
            public string Хэмжээ { get; set; }
            public string ЖишигҮнэ { get; set; }
            public string НийтДүн { get; set; }
            public string ДоодДүн { get; set; }
            public string ЗөрүүДүн { get; set; }
            public string БуцаахДүн { get; set; }
        }
    }
}