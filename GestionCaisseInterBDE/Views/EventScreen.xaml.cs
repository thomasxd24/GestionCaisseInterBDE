using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace GestionCaisseInterBDE.Views
{
    /// <summary>
    /// Interaction logic for EventScreen.xaml
    /// </summary>
    public partial class EventScreen : Page
    {
        private string server;
        private string database;
        private string uid;
        private string password;

        private MySqlConnection connection;

        public EventScreen()
        {
            server = "51.68.230.58";
            database = "bde";
            uid = "bdeUser";
            password = "412qIrJSUkM0";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "Database=" + database + ";" + "Uid=" + uid + ";" + "Pwd=" + password + ";";
            Console.WriteLine(connectionString);
            connection = new MySqlConnection(connectionString);
            OpenConnection();
            InitializeComponent();
            //CloseConnection();
        }
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
