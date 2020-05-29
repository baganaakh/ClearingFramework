using Clearing.pages;
using System.Windows;
using System.Windows.Input;

namespace ClearingFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Эхлэл();
        }
        #region DocPanel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                resizebtn.Content = FindResource("mini");
            }
            else
            {
                resizebtn.Background = null;
                resizebtn.Content = FindResource("maxi");
                this.WindowState = WindowState.Maximized;
            }
        }
        private void rectangle2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
        public void Button_Click_1(object sender, RoutedEventArgs e)    // Эхлэл 
        {
            MainFrame.Content = new Эхлэл();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)     // Данс нээх
        {
            MainFrame.Content = new Данснээх();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)    // гүйлгээ Бүртгэх
        {
            MainFrame.Content = new Гүйлгээбүртгэх();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)    // Дансны мэдээлэл
        {
            MainFrame.Content = new Данснымэдээлэл();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)    // Арилжааны дүн
        {
            MainFrame.Content = new Арилжааныдүн();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)    // Шимтгэл
        {
            MainFrame.Content = new Шимтгэл();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)    // Рэпо
        {
            MainFrame.Content = new Repo();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)     // ҮЦ зээл
        {
            MainFrame.Content = new ҮЦзээлийнсан();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)    // Барьцаа
        {
            MainFrame.Content = new Барьцаа();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)     // Гарах
        {
            App.Current.Properties["User_id"] = null;
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Үнэтцаасзээл();
        }
    }
}
