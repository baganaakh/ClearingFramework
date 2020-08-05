using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Clearing.pages
{
    /// <summary>
    /// Interaction logic for PostTrade.xaml
    /// </summary>
    public partial class PostTrade : Page
    {
        public PostTrade()
        {
            this.DataContext = new PostTradeViewModel();
            InitializeComponent();
            //FillGrid();
        }
        #region fill
        private void FillGrid()
        {

            Model1 DE = new Model1();
            var deal1 = DE.AdminDeals;
            var dealList = deal1.ToList();
            string query, acode, side;
            List<object> data = new List<object>();
            foreach (var i in dealList)
            {
                using (var context = new Model1())
                {
                    var ac = (from s in context.AdminAccounts
                                where s.id == i.accountid
                                select s).FirstOrDefault<AdminAccount>();
                    var ass = (from s in context.AdminAssets
                                where s.id == i.assetid
                                select s ).FirstOrDefault<AdminAsset>();
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
                using (var contx = new Model1())
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

        private void OnDealSelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                AssetItem item = (AssetItem)cboAsset2.SelectedItem;
                data.PrepareDeal(item==null ? "" : item.Code, txtAcc2.Text);
            }
        }
        private void OnDealHistorySelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;
            DateTime sdate;
            DateTime edate;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                sdate = sDate4.DisplayDate;
                edate = sDate4.DisplayDate;
                data.PrepareDealHistory(cboAsset4.Text, txtAcc4.Text, sdate, edate);
            }
        }
        private void OnPositionSelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                data.PreparePosition(cboAsset1.Text, txtAcc1.Text);
            }
        }
        private void OnPositionHistorySelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;
            DateTime sdate;
            DateTime edate;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                sdate = txtSDate4.DisplayDate;
                edate = txtEDate4.DisplayDate;
                data.PreparePositionHistory(cboAsset3.Text, txtAcc3.Text, sdate, edate);
            }
        }
        private void OnNDealSelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;
            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                data.PrepareNDealList(cboAsset02.Text, txtAcc02.Text);
            }
        }
        private void OnNDealHistorySelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;
            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                data.PrepareNDealList(cboAsset04.Text, txtAcc04.Text);
            }
        }
        private void OnNPositionSelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                data.PrepareNPositionList(cboAsset01.Text, txtAcc01.Text);
            }
        }
        private void OnNPositionHistorySelected(object sender, RoutedEventArgs e)
        {
            PostTradeViewModel data;
            if (this.DataContext == null) return;

            data = (PostTradeViewModel)this.DataContext;
            var tab = sender as TabItem;
            if (tab != null)
            {
                data.PrepareNPositionHistoryList(cboAsset03.Text, txtAcc03.Text);
            }
        }
    }


}
