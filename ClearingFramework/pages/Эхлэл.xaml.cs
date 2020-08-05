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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Эхлэл : Page
    {
        public Эхлэл()
        {
            InitializeComponent();
            bindCombo();
        }
        int memId= Convert.ToInt32(App.Current.Properties["member_id"]);
        private void bindCombo()
        {
            Model1 ce = new Model1();
            var adme = ce.AdminMembers.Where(r => r.linkMember == memId).ToList();
            linkedmem.ItemsSource = adme;
        }
        #region fill
        private void Идэвхитэйнэхэмжлэх(object sender, RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            List<AdminInvoice> invo = ce.AdminInvoices.Where(s => s.memberid == memId).ToList<AdminInvoice>();
            //var t = from tt in invo
            //        join a1 in ce.AdminAccounts on tt.accountid equals a1.id
            //        select new 
            //        {
            //            a1.accNumber,
            //            a1.mask,
            //            //tt.mo,
            //        };
            Идэвхитэйнэх.ItemsSource = invo;
        }
        private void Нэхэмжлэхдэлгэрэнгүй(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        private void Рэпо(object sender, RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            var ador = ce.AdminOrders.Where(r => r.dealType == 3 && r.state == 0).ToList<AdminOrder>();
            var t = from tt in ador
                    join ass in ce.AdminAssets on tt.assetid equals ass.id
                    join ac in ce.Accounts on tt.accountid equals ac.id
                    join intr in ce.AdminInterests on tt.assetid equals intr.assetid 
                    select new
                    {
                        modifieds=tt.modified,
                        accounts=ac.accNum,
                        assets=ass.name,
                        sides=tt.side,
                        qtys=tt.qty,
                        //жишиг үнэ = refPrice*asset.ratio
                        examps = ass.price*ass.price,
                        //нийт дүн = хэмжээ * жишиг үнэ
                        toprices = ass.price * ass.price * tt.qty,
                        days=tt.day,
                        activedate = ((DateTime)tt.modified).AddDays((double)tt.day),
                        //хүү = нийт дүн * хоног * Pageхүү
                        inttotal = ass.price * ass.price * tt.qty * intr.interest * tt.day
                    };
            ЭхлэлРэпо.ItemsSource=t;
        }
        private void ҮЦЗ(object semder, RoutedEventArgs e)
        {
            Model1 ce = new Model1();
            var ador = ce.AdminOrders.Where(r => r.dealType == 4&& r.state == 0).ToList<AdminOrder>();
            var t = from tt in ador
                    join ass in ce.AdminAssets on tt.assetid equals ass.id
                    join ass2 in ce.AdminAssets on tt.assetid2 equals ass2.id
                    join ac in ce.Accounts on tt.accountid equals ac.id
                    join intr in ce.AdminInterests on tt.assetid equals intr.assetid
                    select new
                    {
                        modifieds = tt.modified,
                        accounts = ac.accNum,
                        assets = ass.name,
                        qtys = tt.qty,
                        //жишиг үнэ = refPrice*asset.ratio
                        examps = ass.price * ass.price,
                        //нийт дүн = хэмжээ * жишиг үнэ
                        toprices = ass.price * ass.price * tt.qty,
                        days = tt.day,
                        activedate = ((DateTime)tt.modified).AddDays((double)tt.day),
                        toprices2 = ass2.price * ass2.price * tt.qty2,
                        asset2=ass2.name,
                        examps2=ass2.price*ass2.price,
                        tt.qty2,
                        //хүү = нийт дүн * хоног * Pageхүү
                        inttotal = ass.price * ass.price * tt.qty * intr.interest * tt.day,
                    };
            ҮЦзээл.ItemsSource = t;
        }
        private void linkedmem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Нэгдсэнданснымэдээлэл.ItemsSource = null;
            Model1 ce = new Model1();
            var item = linkedmem.SelectedItem as AdminMember;
            var acs = ce.Accounts.Where(r=>r.memId == item.id).ToList<Account>();
            var t = from tt in acs
                    join de in ce.AccountDetails on tt.id equals de.accountId
                    select  new 
                    {
                        tt.accNum,
                        de.totalNumber,
                        de.freezeValue,
                        үлд=de.totalNumber-de.freezeValue,
                        tt.state
                    };
            Нэгдсэнданснымэдээлэл.ItemsSource = t;
        }
        private void BankNames(object sender, SelectionChangedEventArgs e)
        {
            int i= bankCombo.SelectedIndex;
            switch (i)
            {
                case 0:
                    accountName.Text = "МҮЦБ / MSX";
                    accountNumber.Text = "3000028888";
                    break;
                case 1:
                    accountName.Text = "Монголын үнэт цаасны бирж";
                    accountNumber.Text = "5166542085";
                    break;
                case 2:
                    accountName.Text = "Монголын үнэт цаасны бирж";
                    accountNumber.Text = "1175119142";
                    break;
                case 3:
                    accountName.Text = "Монголын үнэт цаасны бирж";
                    accountNumber.Text = "343100225841";
                    break;
                case 4:
                    accountName.Text = "Монголын үнэт цаасны бирж";
                    accountNumber.Text = "495084513";
                    break;
                default:
                    MessageBox.Show("Буруу өгөгдөл");
                    break;
            }
        }
    }
}
