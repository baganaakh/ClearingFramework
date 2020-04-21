using ClearingFramework;
using ClearingFramework.dbBind;
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

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Lend.xaml
    /// </summary>
    public partial class Lend : Page
    {
        public Lend()
        {
            InitializeComponent();
            bindCombo();
        }
        int memId = Convert.ToInt32(App.Current.Properties["member_id"]);
        Model1 CE = new Model1();
        string linkacs;
        int assId;
        public List<AdminAsset> asst { get; set; }
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
                decimal ratio =Convert.ToDecimal(item.ratio);
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
        private void day_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #region Илгээх
        private void Илгээх_Click(object sender, RoutedEventArgs e)
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
                    day=Convert.ToInt32(day.Text),
                };
                contx.AdminOrders.Add(order);
                contx.SaveChanges();
            }
        }
        #endregion
    }
}
