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
using iut.GestionCaisseInterBDE.Wpf.ViewModel;

namespace iut.GestionCaisseInterBDE.Wpf.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserAccounts.xaml
    /// </summary>
    public partial class UserAccounts : UserControl
    {
        public MainWindow Window { get; }
        public UserAccounts()
        {
            Window= Application.Current.MainWindow as MainWindow;
            InitializeComponent();
            DataContext = new UserAccountViewModel();
        }
    }
}
