using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for Дансны мэдээлэл.xaml
    /// </summary>
    public partial class Данснымэдээлэл : Page
    {
        int memid= Convert.ToInt32(App.Current.Properties["member_id"]);
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
            unitedData.ItemsSource = t;
        }

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
            tuhBalance.ItemsSource = t;
        }
    }
}
