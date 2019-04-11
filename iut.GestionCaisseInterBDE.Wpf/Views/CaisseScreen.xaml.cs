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
using iut.GestionCaisseInterBDE.Models;
using System.Globalization;
using System.Collections.ObjectModel;
using GestionCaisseInterBDE.Windows;

namespace iut.GestionCaisseInterBDE.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaisseScreen : Page
    {
        MainWindow window;
        ObservableCollection<BasketItem> basketItems;
        Collection<Product> Products;
        private float totalPrice;
        public string TotalPriceFormat
        {
            get { return totalPrice.ToString("C2");}
        }

        public CaisseScreen()
        {
            window = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
            Products = ProductManager.GetProductList();
            productList.ItemsSource = Products;
            basketItems = new ObservableCollection<BasketItem>();
            basketListView.ItemsSource = basketItems;
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var dp = ((StackPanel)dialog.Content);
            var itemC = ((ItemsControl)dp.Children[0]);
            itemC.ItemsSource = this.window.BDEs;
            this.DataContext = this;
        }
        


        private float UpdateTotalPrice()
        {
            float totalPrice = 0;
            foreach (BasketItem basketItem in basketItems)
            {
                totalPrice = totalPrice + basketItem.ItemProduct.Price * basketItem.Quantity;
            }
            return totalPrice;
        }

        private void productItem_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = (Tile)sender;

            addProductToBasket((Product)tile.Tag);
            totalPrice = UpdateTotalPrice();
            totalPriceLabel.Content = totalPrice.ToString("C2");
        }

        


        private void addProductToBasket(Product p)
        {
            bool exist = false;
            foreach(BasketItem item in basketItems)
            {
                if (item.ProductName != p.Name) continue;
                exist = true;
                int oldQuantity = item.Quantity;
                item.Quantity = oldQuantity + 1;
            }
            if (exist) return;
            BasketItem newItem = new BasketItem(p);
            basketItems.Add(newItem);

        }

        private void clearBasketBtn_Click(object sender, RoutedEventArgs e)
        {
            basketItems.Clear();
            totalPrice = 0;
            totalPriceLabel.Content = totalPrice.ToString("C2");
        }

        private async void EncaisserBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await this.window.ShowMetroDialogAsync(dialog);

        }


        private async void BdeChoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var bdeChosen = (BDE)((Tile)sender).Tag;
            var key = DateTime.Now.ToString().GetHashCode().ToString("x");
            foreach(BasketItem basketItem in basketItems)
            {
                BasketManager.AddTicket(key, bdeChosen, basketItem.ItemProduct, basketItem.Quantity);
            }
            await this.window.HideMetroDialogAsync(dialog);
            await this.window.ShowMessageAsync("Encaissement Réussi", $"Un montant de {TotalPriceFormat} a été encaissé au {bdeChosen.Name} avec le numero de ticket {key}");

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            this.window.HideMetroDialogAsync(dialog);
        }

        private void ProductListBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog();
        }
    }
}
