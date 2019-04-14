using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class CaisseViewModel : BaseViewModel
    {
        private Collection<Product> fullProductList;

        private ObservableCollection<Product> productsView;
        public ObservableCollection<Product> ProductsView
        {
            get { return productsView; }
            set
            {
                productsView = value;
                OnPropertyChanged("ProductsView");
            }
        }

        public ObservableCollection<BasketItem> BasketItems
        {
            get;
            set;
        }


        public RelayCommand<Product> AddProductCommand { get; private set; }
        public RelayCommand ClearBasketCommand { get; private set; }


        private float totalPrice;
        public float TotalPrice
        {
            get { return totalPrice; }
            set {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private string productSearchText;

        public string ProductSearchText
        {
            get { return productSearchText; }
            set { productSearchText = value;
                OnPropertyChanged("ProductSearchText");
                UpdateSearchResult(value);
            }
        }



        public CaisseViewModel()
        {
            LoadProducts();
            BasketItems = new ObservableCollection<BasketItem>();
            AddProductCommand = new RelayCommand<Product>(AddProductToBasket);
            ClearBasketCommand = new RelayCommand(ClearBasket);
        }

        private void UpdateSearchResult(string searchString)
        {

            if (searchString == "" || searchString == "Rechercher...")
            {
                ProductsView = new ObservableCollection<Product>(fullProductList as Collection<Product>);
                return;
            } 
            var filteredList = new ObservableCollection<Product>();
            foreach(Product p in fullProductList)
            {
                if (FuzzyMatcher.FuzzyMatch(p.Name, searchString))
                    filteredList.Add(p);
            }
            ProductsView = filteredList;
        }

        public void LoadProducts()
        {
            fullProductList = ProductManager.GetProductList();
            ProductsView = new ObservableCollection<Product>(fullProductList as Collection<Product>) ;
            
        }

        public void ClearBasket()
        {
            BasketItems.Clear();
        }

        public void AddProductToBasket(Product p)
        {
            bool exist = false;

            foreach (BasketItem item in BasketItems)
            {
                if (item.ProductName != p.Name) continue;
                exist = true;
                int oldQuantity = item.Quantity;
                item.Quantity = oldQuantity + 1;
            }
            
            if (!exist)
            {
                BasketItem newItem = new BasketItem(p);
                BasketItems.Add(newItem);
            }
            UpdateTotalPrice();

        }




        public string AddBasketToDB(BDE bdeChosen)
        {
            var key = DateTime.Now.ToString().GetHashCode().ToString("x");
            foreach (BasketItem basketItem in BasketItems)
            {
                BasketManager.AddTicket(key, bdeChosen, basketItem.ItemProduct, basketItem.Quantity);
            }
            return key;
        }

        private void UpdateTotalPrice()
        {
            float totalPriceTemp = 0;
            foreach (BasketItem basketItem in BasketItems)
            {
                totalPriceTemp = totalPriceTemp + basketItem.ItemProduct.Price * basketItem.Quantity;
            }
            TotalPrice = totalPriceTemp;
        }


    }
}
