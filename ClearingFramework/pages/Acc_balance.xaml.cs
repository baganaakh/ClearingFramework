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
        private void FillGrid()
        {
            using (demoEntities1 context = new demoEntities1())
            {
                var acc = context.Accounts.Where(r => r.accNum == 123).ToList();
                unitedData.ItemsSource = acc;
            }

            using(var context=new demoEntities1())
            {
                var query = from st in context.Accounts
                            where st.accNum == 123
                            select st;
                var acct = query.FirstOrDefault<Account>();
            }
        }
    }
}
