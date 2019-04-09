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
using System.Globalization;

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
            fillProductPanel(listProd);
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            addProductToBasket(new Product(1, "ds", 5.50f, ""));
        }

        private void productItem_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = (Tile)sender;
            
            addProductToBasket((Product)tile.Tag);
        }

        private void fillProductPanel(List<Product> listProd)
        {
            productPanel.Children.Clear();
            foreach (Product p in listProd)
            {
                Tile tile = new Tile();
                tile.Height = 150;
                tile.Width = 150;
                tile.Title = p.name;
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
                price.Content = p.price.ToString("C2");
                price.HorizontalAlignment = HorizontalAlignment.Right;
                DockPanel.SetDock(price, Dock.Top);
                dp.Children.Add(price);
                var imageControl = new Image();
                imageControl.Source = new BitmapImage(new Uri(p.imageUrl));
                dp.Children.Add(imageControl);
                tile.Content = dp;

                productPanel.Children.Add(tile);
            }

        }

        private void addProductToBasket(Product p)
        {
            foreach (ListViewItem anItem in basketListView.Items)
            {
                if (anItem.Content.GetType() != typeof(DockPanel)) return;
                DockPanel dp = (DockPanel)anItem.Content;
                int oldQuantity = 1;
                foreach (Label lb in dp.Children)
                {
                    
                    if (lb.Tag == null) continue;
                    if (lb.Tag.ToString() == "productName")
                    {
                        if (lb.Content.ToString() != p.name)
                        {
                            break;
                        }

                    }
                    else if (lb.Tag.ToString() == "Price")
                    {
                        lb.Content = (p.price*(oldQuantity+1)).ToString("C2");
                        return;
                    }
                    else if (lb.Tag.ToString() == "Quantity")
                    {
                        oldQuantity = int.Parse(lb.Content.ToString().Remove(0, 1)); //TODO EXCPETIONS
                        string newString = "x" + (oldQuantity + 1).ToString();
                        lb.Content = newString;
                        totalPrice.Content = (float.Parse(totalPrice.Content.ToString())+p.price).ToString("0.00");
                    }
                    
                }
            }
            var itemProduct = new ListViewItem();
            var dpProd = new DockPanel();
            dpProd.LastChildFill = false;
            var labelPrice = new Label();
            var labelName = new Label();
            labelName.Content = p.name;
            labelName.Tag = "productName";
            dpProd.Children.Add(labelName);
            labelPrice.Content = p.price.ToString("C2");
            labelPrice.Tag = "Price";
            DockPanel.SetDock(labelPrice, Dock.Right);

            var labelQuanity = new Label();
            labelQuanity.Content = "x1";
            labelQuanity.Tag = "Quantity";
            DockPanel.SetDock(labelQuanity, Dock.Right);
            dpProd.Children.Add(labelQuanity);
            dpProd.Children.Add(labelPrice);

            itemProduct.Content = dpProd;
            basketListView.Items.Add(itemProduct);
            totalPrice.Content = (float.Parse(totalPrice.Content.ToString()) + p.price).ToString("0.00");
        }

        private void clearBasketBtn_Click(object sender, RoutedEventArgs e)
        {
            basketListView.Items.Clear();
        }

        private async void EncaisserBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
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
