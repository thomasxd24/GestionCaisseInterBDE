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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using GestionCaisseInterBDE.Model;

namespace GestionCaisseInterBDE.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaisseScreen : Page
    {
        MainWindow window;
        public CaisseScreen()
        {
            window = (MainWindow)Application.Current.MainWindow;

            InitializeComponent();
            List<Product> listProd = Product.getProductList();
            productPanel.Children.Clear();
            foreach (Product p in listProd)
            {
                Tile tile = new Tile();
                tile.Height = 150;
                tile.Width = 150;
                tile.Title = p.name;
                tile.Tag = p.id;
                productPanel.Children.Add(tile);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
