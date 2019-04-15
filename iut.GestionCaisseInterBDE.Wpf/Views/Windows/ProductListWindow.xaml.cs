using GestionCaisseInterBDE.ViewModel;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Models.Utilities;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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
            //var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            //DataTable dt = db.Select("SELECT dateCreated AS Date,bde.name AS Encaisseur,quantity AS Qte,round(products.prix*quantity,2) AS Total FROM `ligneTicket`, `bde`,`products` where bde.idBDE = ligneTicket.idBDE and ligneTicket.idProduit=products.idProduct");
            //testDG.ItemsSource = dt.DefaultView;
            this.DataContext = new ProductListViewModel();
        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            productNameTb.IsReadOnly = false;
            productNameTb.BorderThickness = new Thickness(1, 1, 1, 1);
            productImageUrlTb.IsReadOnly = false;
            productImageUrlTb.BorderThickness = new Thickness(1, 1, 1, 1);
            productBuyPriceNum.IsReadOnly = false;
            productBuyPriceNum.HideUpDownButtons = false;
            productBuyPriceNum.BorderThickness = new Thickness(1, 1, 1, 1);
            productPriceNum.IsReadOnly = false;
            productPriceNum.HideUpDownButtons = false;
            productPriceNum.BorderThickness = new Thickness(1, 1, 1, 1);

        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
