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
    public partial class Repo2 : Page
    {
        public Repo2()
        {
            InitializeComponent();
            bindCombo();
        }
        int memid= Convert.ToInt32(App.Current.Properties["member_id"]);
        clearingEntities CE = new clearingEntities();
        private void bindCombo()
        {
            assett.ItemsSource = CE.AdminAssets.ToList();
            asset2.ItemsSource = CE.AdminAssets.SqlQuery("select * from AdminAssets" +
                " where id= any (select assetId from Clearing.dbo.AccountDetails t1 " +
                "inner join Clearing.dbo.account t2 on t1.accNum = t2.accNum " +
                "where t2.memId = "+memid+" )").ToList<AdminAsset>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ast = assett.SelectedItem as AdminAsset;
            decimal price =Convert.ToDecimal(ast.price);
            try
            {
                int qty = Convert.ToInt32(qtyss.Text);
                wagerValue.Text = (qty * price).ToString();
            }
            catch (System.FormatException)
            {
                return;
            }

        }
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
    }
}
