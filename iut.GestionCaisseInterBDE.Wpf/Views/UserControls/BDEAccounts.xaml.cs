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
    /// Interaction logic for BDEAccounts.xaml
    /// </summary>
    public partial class BDEAccounts : UserControl
    {
        public BDEAccounts()
        {
            InitializeComponent();
            DataContext = new BDEAccountViewModel();
        }
    }
}
