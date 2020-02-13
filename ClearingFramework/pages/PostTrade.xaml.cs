using ClearingFramework.dbBind.pageDatabase;
using System;
using System.Collections;
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
    /// Interaction logic for PostTrade.xaml
    /// </summary>
    public partial class PostTrade : Page
    {
        public PostTrade()
        {
            InitializeComponent();
            FillGrid();
        }
        #region fill
        private void FillGrid()
        {
            demoEntities1 DE = new demoEntities1();
            var deal1 = DE.Deals;
            var dealList=deal1.ToList();
            string query, acode, side;
            List<object> data=new List<object>();
            foreach (var i in deal1)
            {
                using (var context = new demoEntities1())
                {
                    query = context.Accounts.Where(s => s.id == i.accountid).FirstOrDefault<Account>().accNum;
                    acode = context.Assets.Where(s => s.id == i.assetid).FirstOrDefault<Asset>().code;
                    if (i.side == -1)
                    {
                        side = "Зарах";
                    }
                    else
                    {
                        side = "Авах";
                    }
                }
                data.Add(new forItems() { accNumber = query, 
                                            assetId = acode, 
                                            sides = side,
                                            qtys=i.qty,
                                            prices=i.price,
                                            fees=Convert.ToDecimal(i.fee)
                });
            }
                unitedData.ItemsSource = data;
        }

        public class forItems
        {
            public forItems() { }
            public string accNumber { get; set; }
            public string assetId { get; set; }
            public string sides { get; set; }
            public decimal qtys{ get; set; }
            public decimal prices{ get; set; }
            public decimal fees{ get; set; }
        }
        #endregion
    }
}
