using ClearingFramework;
using ClearingFramework.dbBind;
using ClearingFramework.dbBind.pageDatabase;
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
using Account2 = ClearingFramework.dbBind.pageDatabase.Account;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Repo.xaml
    /// </summary>
    public partial class Repo : Page
    {
        public Repo()
        {
            InitializeComponent();
            FillGrid();
            bindCombo();
        }
        string linkacs, accnum;
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
                    qty = Convert.ToInt32(qtyss.Text),
                    assetid = Convert.ToInt64(asset.SelectedValue),
                    connect = membee.SelectedValue.ToString(),
                    accountid = Convert.ToInt64(linkAc.SelectedValue),
                    day = Convert.ToInt32(dayy.Text),
                    modified = DateTime.Now
                };
                context.Orders.Add(order);
                context.SaveChanges();
                FillGrid();
            }
        }
        #endregion
        #region fill
        private void FillGrid()
        {
            totalOrder.ItemsSource = DE.Orders.ToList();
        }
        #endregion
        #region Нийт захиалга зөвшөөрөх
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region өөрийн захиалга Цуцлах
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region combos
        public List<Account2> acc { get; set; }
        public List<Asset> assets { get; set; }
        public List<Member> members{ get; set; }
        private void bindCombo()
        {
            var acclist = DE.Accounts.Where(s => s.memberid == memId && s.accType == 2).ToList();
            acc = acclist;
            linkAc.ItemsSource = acc;

            //var asslist = DE.Assets.ToList();
            //assets = asslist;
            //asset.ItemsSource = assets;

            var memlist = DE.Members.ToList();
            members = memlist;
            membee.ItemsSource = members;
        }

        private void linkAc_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void linkAc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = linkAc.SelectedItem as Account1;
            try
            {
                linkacs = item.linkAcc.ToString();
                accnum = item.accNum.ToString();
                var acclist = DE.Accounts.Where(s => s.memberid == memId && s.accType == 3 && s.accNum == accnum).ToList();
                asset.ItemsSource = acclist;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
