using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using ClearingFramework.dbBind;
using System.Linq;
using WinForms = System.Windows.Forms;
using System.Data;
using System.Diagnostics.Contracts;

namespace ClearingFramework
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class connect : Window
    {
        SqlConnection con;
         //con = new SqlConnection(ConfigurationManager.ConnectionStrings["Model"].ConnectionString);
        Configuration config;
        public connect()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }
        #region Backup & Restore
            #region BAckup
            private void browse_Click(object sender, EventArgs e)
            {
            WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog();
                if (dlg.ShowDialog() == WinForms.DialogResult.OK)
                {
                    ssd1.Text = dlg.SelectedPath;
                    Backup.IsEnabled = true;
                }
            }

            private void Backup_Click(object sender, EventArgs e)
            {
                string database = con.Database.ToString();
                try
                {
                    if (ssd1.Text == string.Empty)
                    {
                        MessageBox.Show("please enter backup file location");
                    }
                    else
                    {
                        string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + ssd1.Text +
                            "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                        using (SqlCommand command = new SqlCommand(cmd, con))
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("database backup done successefully");
                            Backup.IsEnabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            #endregion
            #region restore
        private void Browse2_Click(object sender, EventArgs e)
        {
            WinForms.OpenFileDialog dlg = new WinForms.OpenFileDialog();
            dlg.Filter = "SQL Server Database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == WinForms.DialogResult.OK)
            {
                ssd2.Text = dlg.FileName;
                Restore.IsEnabled = true;
            }
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK ='" + ssd2.Text + "' WITH REPLACE";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                bu4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #endregion
        #region Change connection
        private void Change(object sender, RoutedEventArgs e)
        {
            string serverName = server.Text;
            string userName = username.Text;
            string DBpassword = ssd.Text;
            string databaseName = databases.Text;
            string entityBuilder;
            if ( string.IsNullOrEmpty(username.Text) && string.IsNullOrEmpty(ssd.Text))
            {
                entityBuilder = "Data source=" + serverName + ";" +
                "initial catalog=" + databaseName + ";" +
                "App=EntityFramework;Pooling=false;Persist Security Info = True;Integrated Security=true;";
            }
            else
            {
                entityBuilder = "Data source=" + serverName + ";" +
                "initial catalog=" + databaseName + ";" +
                "User Id=" + userName + ";" +
                "Password=" + DBpassword + ";" +
                "App=EntityFramework;Pooling=false;Persist Security Info = True";
            }

            config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = entityBuilder;
            config.ConnectionStrings.ConnectionStrings["Model1"].ProviderName = "System.Data.SqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("done");
        }
        #endregion
        #region Exit & Check
        private void Exit(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
        private void check_Click(object sender, RoutedEventArgs e)
        {
            var connection = ConfigurationManager.ConnectionStrings["Model1"].ConnectionString;
            MessageBox.Show(connection.ToString());
        }
        #endregion
        #region USE LOCAL DATABASE
        private void uselocal_Click(object sender, RoutedEventArgs e)
        {      
            try
            {
                string cons = "data source=(LocalDB)\\v11.0;Initial catalog=ClearingDataBase;Integrated security=True;" +
                    "MultipleActiveResultSets=True;App=EntityFramework;Pooling=false;";
                config.ConnectionStrings.ConnectionStrings["Model1"].ConnectionString = cons;
                config.ConnectionStrings.ConnectionStrings["Model1"].ProviderName = "System.Data.SqlClient";
                config.Save(ConfigurationSaveMode.Modified);                
            }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message.ToString());
                return;
                }
        }
        #endregion
        #region DELETE Database
        private void DeleteDB_Click(object sender, RoutedEventArgs e)
        {
            if (server.Text == "accept" && username.Text == "delete" && ssd.Text == "localdb" && databases.Text == "confirm")
            {
                try
                {
                    using (var dbconx = new DbContext("Model1"))
                    {
                        dbconx.Database.Delete();
                    };
                    MessageBox.Show("Done 😀");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("Secret text wrong !!!! try again");
            }
        }
        #endregion
    }

}
