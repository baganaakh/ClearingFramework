using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Fee.xaml
    /// </summary>
    public partial class Fee : Page
    {
        public Fee()
        {
            InitializeComponent();
            LoadData();
        }

        private DataTable tbl;
        private void LoadData()
        {
            tbl = new DataTable("TotalFee");
            tbl.Columns.Add("id", typeof(int));
            tbl.Columns.Add("AccNo", typeof(string));
            tbl.Columns.Add("Asset", typeof(string));
            tbl.Columns.Add("Total", typeof(decimal));
            tbl.Columns.Add("Deal", typeof(decimal));
            tbl.Columns.Add("Position", typeof(decimal));
            tbl.Columns.Add("Exchange", typeof(decimal));
            tbl.Columns.Add("Broker", typeof(decimal));
            tbl.Columns.Add("Clearing", typeof(decimal));

            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["id"]};

            DataRow row = tbl.NewRow();
            row["id"] = 1;
            row["AccNo"] = "1234H";
            row["Asset"] = "AND";
            row["Total"] = 150;
            row["Deal"] = 75;
            row["Position"] = 60;
            row["Exchange"] = 5;
            row["Broker"] = 5;
            row["Clearing"] = 5;
            tbl.Rows.Add(row);

            row = tbl.NewRow();
            row["id"] = 2;
            row["AccNo"] = "1234C";
            row["Asset"] = "AND";
            row["Total"] = 150;
            row["Deal"] = 75;
            row["Position"] = 60;
            row["Exchange"] = 5;
            row["Broker"] = 5;
            row["Clearing"] = 5;
            tbl.Rows.Add(row);

            vwTotalFee.ItemsSource = tbl.DefaultView;
        }
    }
}
