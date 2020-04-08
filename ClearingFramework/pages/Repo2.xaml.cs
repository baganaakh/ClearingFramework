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
        int asst1, asst2,qty,qty2,day;
        decimal price,price2,inter,topay;
        int memid= Convert.ToInt32(App.Current.Properties["member_id"]);        
        clearingEntities CE = new clearingEntities();
        private void bindCombo()
        {
            assett.ItemsSource = CE.AdminAssets.ToList();
            asset2.ItemsSource = CE.AdminAssets.SqlQuery("select * from AdminAssets" +
                " where id= any (select assetId from Clearing.dbo.AccountDetails t1 " +
                "inner join Clearing.dbo.account t2 on t1.accNum = t2.accNum " +
                "where t2.memId = "+memid+" )").ToList<AdminAsset>();
            var memlist = CE.AdminMembers.ToList();
            var rem = memlist.Find(x => x.id == memid);
            memlist.Remove(rem);
            AdminMember allItem = new AdminMember() { code = "Бүгд", id = 0 };
            memlist.Add(allItem);
            membee.ItemsSource = memlist;            
            membee.SelectedIndex = memlist.Count() - 1;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ast = assett.SelectedItem as AdminAsset;
            decimal price =Convert.ToDecimal(ast.price);
            try
            {
                int qty = Convert.ToInt32(qtyss.Text);
                wagerValue.Text = (qty * price).ToString("0.##");
            }
            catch (System.FormatException)
            {
                return;
            }
            catch (System.NullReferenceException)
            {
                return;
            }

        }
        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }
        private void asset2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ast2 = asset2.SelectedItem as AdminAsset;
            asst2 = ast2.id;
            price2 =Convert.ToDecimal(ast2.price);
            ast2price.Text = price.ToString();
            var ast = CE.AccountDetails.SqlQuery("" +
                "select * from Clearing.dbo.AccountDetails t1 " +
                "inner join Clearing.dbo.account t2 on t1.accNum =t2 .accNum " +
                "where t2.memId = " + memid + " and t1.assetId=" + ast2.id + "").FirstOrDefault<AccountDetail>();
            remain.Text = Convert.ToInt32(ast.totalNumber).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            assett.SelectedItem = null;

        }

        private void qty2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                qty2 = Convert.ToInt32(qtyss2.Text);
                if (qty2 < Convert.ToInt32(remain.Text))
                {
                    decimal astprice2 = Convert.ToDecimal(ast2price.Text);
                    TotalSum.Text = (astprice2 * qty).ToString();
                }
                else
                {
                    MessageBox.Show("Хэмжээ үлдэгдэлээс илүү гарч болохгүй !!!!");
                    qtyss2.Text = null;
                    TotalSum.Text = null;
                    return;
                }
            }
            catch (System.FormatException)
            {
                return;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem days = (ComboBoxItem)dayy.SelectedItem;
            if (days == null)
            {
                return;
            }
            int days1 = Convert.ToInt32(days.Content);
            var asst2= asset2.SelectedItem as AdminAsset;
            var Interst = CE.AdminInterests.Where(s => s.assetid == asst2.id).FirstOrDefault<AdminInterest>();
            inter= Convert.ToDecimal(Interst.interest);
            decimal inters = Convert.ToDecimal(TotalSum.Text) * days1 * inter;
            Inter.Text = inter.ToString("0.##");
            topay = Convert.ToDecimal(TotalSum.Text) + Convert.ToDecimal(Inter.Text);
            TotalSum.Text = topay.ToString("0.##");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(clearingEntities conx =new clearingEntities())
            {
                AdminOrder order = new AdminOrder()
                {
                    assetid=Convert.ToInt64(assett.SelectedValue),
                    qty=Convert.ToInt32(qtyss),
                    price=price,
                    assetid2=Convert.ToInt64(asset2.SelectedValue),
                    qty2=qty2,
                    memberid=memid,
                    modified=DateTime.Now,
                    connect=membee.SelectedValue.ToString(),
                    interests=inter,
                    dealType=5,
                    day=Convert.ToInt32(dayy.SelectedValue),

                };
                conx.AdminOrders.Add(order);
                conx.SaveChanges();
            }
            
        }
       
        private void assett_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ast = assett.SelectedItem as AdminAsset;
            price = Convert.ToDecimal(ast.price);
            asst1 = ast.id;
        }

    }
}
