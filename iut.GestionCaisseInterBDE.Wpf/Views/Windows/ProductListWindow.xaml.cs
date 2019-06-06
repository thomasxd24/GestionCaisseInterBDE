using GestionCaisseInterBDE.ViewModel;
using iut.GestionCaisseInterBDE.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace GestionCaisseInterBDE.Windows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : MetroWindow
    {



        public ProductListWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductListViewModel(DialogCoordinator.Instance);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListView;
            list?.ScrollIntoView(list.SelectedItem);
        }
    }
}
