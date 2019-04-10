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

namespace iut.GestionCaisseInterBDE.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaisseScreen : Page
    {
        MainWindow window;
        ObservableCollection<BasketItem> basketItems;
        public CaisseScreen()
        {
            window = (MainWindow)Application.Current.MainWindow;

            InitializeComponent();

            Collection<Product> listProd = Product.getProductList();
            fillProductPanel(listProd);

            basketItems = new ObservableCollection<BasketItem>();
            basketListView.ItemsSource = basketItems;

        }

        private void productItem_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = (Tile)sender;

            addProductToBasket((Product)tile.Tag);
        }

        private void fillProductPanel(Collection<Product> listProd)
        {
            productPanel.Children.Clear();
            foreach (Product p in listProd)
            {
                Tile tile = new Tile();
                tile.Height = 150;
                tile.Width = 150;
                tile.Title = p.Name;
                tile.Tag = p;
                Random random = new Random();
                SolidColorBrush brush =
                    new SolidColorBrush(
                        Color.FromRgb(
                        (byte)random.Next(255),
                        (byte)random.Next(255),
                        (byte)random.Next(255)
                        ));
                tile.Background = brush;
                tile.Click += productItem_Click;
                var dp = new DockPanel();
                dp.Width = 150;
                dp.Margin = new Thickness(0, 0, 0, 30);
                var price = new Label();
                price.Content = p.Price.ToString("C2");
                price.HorizontalAlignment = HorizontalAlignment.Right;
                DockPanel.SetDock(price, Dock.Top);
                dp.Children.Add(price);
                var imageControl = new Image();
                imageControl.Source = new BitmapImage(new Uri(p.ImageURL));
                dp.Children.Add(imageControl);
                tile.Content = dp;

                productPanel.Children.Add(tile);
            }

        }

        private void addProductToBasket(Product p)
        {
            bool exist = false;
            foreach(BasketItem item in basketItems)
            {
                if (item.ProductName != p.Name) continue;
                exist = true;
                int oldQuantity = int.Parse(item.Quantity.Remove(0, 1));
                item.TotalPriceString = (p.Price * (oldQuantity + 1)).ToString("C2");
                item.Quantity = $"x{oldQuantity + 1}";
            }
            if (exist) return;
            BasketItem newItem = new BasketItem(p.Name,p.Price.ToString("C2"),"x1");
            basketItems.Add(newItem);
        }

        private void clearBasketBtn_Click(object sender, RoutedEventArgs e)
        {
            basketItems.Clear();
        }

        private async void EncaisserBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            List<BDE> listBDE = BDE.getBDEList();
            var wp = new WrapPanel();
            wp.HorizontalAlignment = HorizontalAlignment.Center;
            wp.Orientation = Orientation.Horizontal;
            wp.Name = "panelBDE";
            foreach(BDE bde in listBDE)
            {
                Tile tile = new Tile();
                tile.Title = bde.name;
                tile.Width = 150;
                tile.Height = 150;
                tile.Margin = new Thickness(7);
                tile.Click += BdeChoiceBtn_Click;
                tile.Tag = bde;

                wp.Children.Add(tile);
            }
            var dp = ((DockPanel)dialog.Content);
            if(dp.Children.Count == 2) dp.Children.Remove(dp.Children[1]); 
            dp.Children.Add(wp);
            await this.window.ShowMetroDialogAsync(dialog);

        }

        private async void BdeChoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await this.window.HideMetroDialogAsync(dialog);
            await this.window.ShowMessageAsync("Encaissement Réussi", "Un montant de 13.5 EURO a été encaissé");

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            this.window.HideMetroDialogAsync(dialog);
        }
    }
}
