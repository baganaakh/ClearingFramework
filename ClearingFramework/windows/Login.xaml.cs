using Non_Member.dbBind.AdminDatabase;
using System.Linq;
using System.Windows;

namespace Non_Member
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (demoEntities1 context = new demoEntities1())
            {
                var query = context.users.Where(s => s.uname == txtLoginName.Text).FirstOrDefault<user>();
                if(query == null)
                {
                    MessageBox.Show("Username doesn't match");
                    return;
                }
                App.Current.Properties["User_id"] = query.id;
                //var id = App.Current.Properties["User_id"];
                App.Current.Properties["member_id"] = query.memId;
                if (query.password == txtLoginPass.Password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.topPanel.Content = "MSX Clearing Workstation   " +query.uname +" ID : "+ query.memId;
                    mainWindow.Show();
                    this.Close();
                    //Window parentWindow = Window.GetWindow(this).Button;

                    //((this.Parent) as Window).Content.Bu;
                    //parentWindow.
                    //dashboard.start.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else
                {
                    MessageBox.Show("Password doesn't match");
                }
            }
        }
    }
}
