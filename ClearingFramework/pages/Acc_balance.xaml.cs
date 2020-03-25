using Non_Member;
using Non_Member.dbBind.AdminDatabase;
using System;
using System.Linq;
using System.Windows.Controls;

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
                var acc = context.Accounts.Where(s=> s.memberid == memid && s.accountType == 3).ToList();
                unitedData.ItemsSource = acc;
                //var level1 = context.Accounts.Where(s => s.memberid == memid && s.accType == 0);
                //select * from demo.dbo.Account where accType = 3 and LinkAcc in (select accNum from demo.dbo.Account where  LinkAcc IN (select accNum from demo.dbo.Account where memberid= 20 and accType = 0) and accType = 2)
               

            }
        }
        #endregion
    }
}
