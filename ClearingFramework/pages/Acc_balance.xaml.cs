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
using Account2 = System.Collections.Generic.IEnumerable<ClearingFramework.dbBind.AdminDatabase.Account>;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for Acc_balance.xaml
    /// </summary>
    public partial class Acc_balance : Page
    {
        public Acc_balance()
        {
            InitializeComponent();
            FillGrid();
        }
        #region fill
        private void FillGrid()
        {
            using (demoEntities1 context = new demoEntities1())
            {
                long memid= Convert.ToInt32(App.Current.Properties["member_id"]);
                var acc = context.Accounts.Where(s=> s.memberid == memid && s.accType == 3).ToList();
                unitedData.ItemsSource = acc;
                //var level1 = context.Accounts.Where(s => s.memberid == memid && s.accType == 0);
                //select * from demo.dbo.Account where accType = 3 and LinkAcc in (select accNum from demo.dbo.Account where  LinkAcc IN (select accNum from demo.dbo.Account where memberid= 20 and accType = 0) and accType = 2)
               

            }
        }
        #endregion
    }
}
