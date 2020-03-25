using Non_Member.dbBind;
using Non_Member.dbBind.AdminDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Account = Non_Member.dbBind.AdminDatabase.Account;

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
            var dealList = deal1.ToList();
            string query, acode, side;
            List<object> data = new List<object>();
            foreach (var i in dealList)
            {
                using (var context = new demoEntities1())
                {
                    var ac = (from s in context.Accounts
                                where s.id == i.accountid
                                select s).FirstOrDefault<Account>();
                    var ass = (from s in context.Assets
                                where s.id == i.assetid
                                select s ).FirstOrDefault<Asset>();
                    if(ac == null)
                    {
                        MessageBox.Show("deals accountid oldsongui");
                        return;
                    }
                    query = ac.accNumber;
                    acode= ass.code;
                    if (i.side == -1) { side = "Зарах"; }
                    else { side = "Авах"; }
                }
                data.Add(new forItems()
                {
                    accNumber = query,
                    assetId = acode,
                    sides = side,
                    qtys = Convert.ToDecimal(i.qty),
                    prices = Convert.ToDecimal(i.price),
                    fees = Convert.ToDecimal(i.fee)
                });
                using (var contx = new clearingEntities1())
                {
                    decimal lastPrice = Convert.ToDecimal(
                        contx.lastPrices.Where(s => s.assetid == i.assetid)
                        .FirstOrDefault<lastPrice>().ePrice);
                    decimal gainloss = Convert.ToDecimal(lastPrice * i.qty - i.price * i.qty);

                    var std = new pozit()
                    {
                        accNum = query,
                        side = side,
                        assetCode = acode,
                        qty = Convert.ToInt32(i.qty),
                        price = i.price,
                        fee = i.fee,
                        gainLoss = gainloss,

                    };
                    contx.pozits.Add(std);
                    contx.SaveChanges();
                    var poz = contx.pozits.ToList();
                }
            }
            unitedData.ItemsSource = data;
        }
        public class forItems
        {
            public forItems() { }
            public string accNumber { get; set; }
            public string assetId { get; set; }
            public string sides { get; set; }
            public decimal qtys { get; set; }
            public decimal prices { get; set; }
            public decimal fees { get; set; }
        }
        #endregion
    }
}
