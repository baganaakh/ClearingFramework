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
        private async void Нэгдсэндансны(object sender, RoutedEventArgs e)
        {
            
            //var t =from tt in adme
            //        join ac in ce.Accounts on tt.id equals ac.memId
            //        join de in ce.AccountDetails on ac.accNum equals de.accNum 
            //         select new
            //        {
            //            ac.accNum,
            //            de.totalNumber,
            //            de.freezeValue,
            //            //Үлд=de.totalNumber-de.freezeValue,
            //        };
            //Нэгдсэнданснымэдээлэл.ItemsSource = t;
        }
        #endregion

        private async void linkedmem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Нэгдсэнданснымэдээлэл.ItemsSource = null;
            Model1 ce = new Model1();
            var item = linkedmem.SelectedItem as AdminMember;
            var acs = ce.Accounts.Where(r=>r.memId == item.id).ToList<Account>();
            var t = from tt in acs
                    join de in ce.AccountDetails on tt.accNum equals de.accNum
                    select new
                    {
                        tt.accNum,
                        de.totalNumber,
                        de.freezeValue,
                        үлд=de.totalNumber-de.freezeValue,
                        tt.state
                    };
            Нэгдсэнданснымэдээлэл.ItemsSource = t;
        }
    }
}
