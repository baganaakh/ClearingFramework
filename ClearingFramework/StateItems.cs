using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearingFramework
{
    public class StateItem
    {
        public short ID { get; set; }
        public string Name { get; set; }
    }

    public class AssetItem
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public string Note { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Ratio { get; set; }
        public decimal Price { get; set; }
        public int State { get; set; }
        public DateTime Modified { get; set; }
    }
    public class DealItem
    {
        public long ID { get; set; }
        public string AccNo { get; set; }
        public long MatchID { get; set; }
        public string Asset { get; set; }
        public string Side { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DealTime { get; set; }
        public decimal Fee { get; set; }
        public int State { get; set; }
    }

    public class DealHistoryItem
    {
        public long ID { get; set; }
        public string AccNo { get; set; }
        public long MatchID { get; set; }
        public string Asset { get; set; }
        public string Side { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DealTime { get; set; }
        public decimal Fee { get; set; }
        public short State { get; set; }
        public DateTime sDate { get; set; }
    }
    public class PositionItem
    {
        public long ID { get; set; }
        public string AccNo { get; set; }
        public short Side { get; set; }
        
    }
    public class PositionHistoryItem
    {

    }
    public class PostTradeViewModel
    {
        public PostTradeViewModel()
        {
            StateItems = new ObservableCollection<StateItem>();
            StateItems.Add(new StateItem() { ID = -1, Name = "Цуцалсан" });
            StateItems.Add(new StateItem() { ID = 0, Name = "Идэвхтэй" });

            AssetItems = new ObservableCollection<AssetItem>();
            using (var context = new Model1())
            {
                var ass = context.AdminAssets.ToList();
                foreach (var item in ass)
                {
                    AssetItems.Add(new AssetItem()
                    {
                        ID = item.id,
                        Code = item.code,
                        Name = item.name,
                        Volume = item.volume,
                        Note = item.note,
                        ExpireDate = item.expireDate,
                        Ratio = item.ratio,
                        Price = Convert.ToDecimal(item.price == null ? 0 : item.price),
                        State = item.state,
                        Modified = item.modified
                    });
                }
            }

            DealList = new ObservableCollection<DealItem>();
            DealHistoryList = new ObservableCollection<DealHistoryItem>();
            PositionList = new ObservableCollection<PositionItem>();
            PositionHistoryList = new ObservableCollection<PositionHistoryItem>();
        }

        public ObservableCollection<StateItem> StateItems { get; set; }
        public ObservableCollection<AssetItem> AssetItems { get; set; }
        public ObservableCollection<DealItem> DealList { get; set; }
        public ObservableCollection<DealHistoryItem> DealHistoryList { get; set; }
        public ObservableCollection<PositionItem> PositionList { get; set; }
        public ObservableCollection<PositionHistoryItem> PositionHistoryList { get; set; }
        public ObservableCollection<DealItem> DealNList { get; set; }
        public ObservableCollection<DealHistoryItem> DealNHistoryList { get; set; }
        public ObservableCollection<PositionItem> PositionNList { get; set; }
        public ObservableCollection<PositionHistoryItem> PositionNHistoryList { get; set; }

        public void PrepareDeal(string AssetCode, string AccountNo)
        {
            DealList.Add(new DealItem() { AccNo = "123456", Asset = AssetCode, Side="Авах", State=-1 });
            DealList.Add(new DealItem() { AccNo = AccountNo, 
                Asset = AssetCode, 
                Side = "Зарах", 
                State = 0, 
                Qty=100, 
                MatchID=213214, 
                ID=2, 
                Price=30, 
                TotalPrice=3000, Fee=30, DealTime=DateTime.Now
            });
        }
        public void PreparePosition(string AssetCode, string AccountNo)
        {
            PositionList.Add(new PositionItem());
        }
        public void PrepareDealHistory(string AssetCode, string AccountNo, DateTime sDate, DateTime eDate)
        {
            DealHistoryList.Add(new DealHistoryItem());
        }
        public void PreparePositionHistory(string AssetCode, string AccountNo, DateTime sDate, DateTime eDate)
        {
            PositionHistoryList.Add(new PositionHistoryItem());
        }
        public void PrepareNDealList(string AssetCode, string AccountNo)
        {
            DealNList.Add(new DealItem());
        }
        public void PrepareNDealHistory(string AssetCode, string AccountNo)
        {
            DealNHistoryList.Add(new DealHistoryItem());

        }
        public void PrepareNPositionList(string AssetCode, string AccountNo)
        {
            PositionNList.Add(new PositionItem());
        }
        public void PrepareNPositionHistoryList(string AssetCode, string AccountNo)
        {
            PositionNHistoryList.Add(new PositionHistoryItem());
        }
    }
}
