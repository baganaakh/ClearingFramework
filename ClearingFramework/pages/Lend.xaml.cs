using ClearingFramework;
using ClearingFramework.dbBind;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Lend.xaml
    /// </summary>
    public partial class Lend : Page
    {
        public Lend()
        {
            InitializeComponent();
        }
        Model1 CE = new Model1();
        public List<AdminAsset> asst { get; set; }
        private void bindCombo()
        {
            //List<Asset> acclist = DE.Assets;
            //assett.ItemsSource = acclist;
        }
        private void day_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #region Илгээх
        private void Илгээх_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void qtyss_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            App.TextBox_PreviewTextInput(sender, e);
        }

        private void accId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void asset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void qtyss_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
