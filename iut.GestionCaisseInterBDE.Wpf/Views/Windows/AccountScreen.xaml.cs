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
using System.Windows.Shapes;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using MahApps.Metro.Controls;

namespace iut.GestionCaisseInterBDE.Wpf.Views.Windows
{
    /// <summary>
    /// Interaction logic for AccountScreen.xaml
    /// </summary>
    public partial class AccountScreen : MetroWindow
    {
        public AccountScreen()
        {
            InitializeComponent();
            DataContext = new AccountViewModel();

        }
    }
}
