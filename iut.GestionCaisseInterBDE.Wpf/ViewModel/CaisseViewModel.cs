using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

        private MainWindow window;
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

        public ObservableCollection<BasketItem> BasketItems { get; set; }
        private BasketItem _selectedItem;
        public BasketItem SelectedItem
        {
            set
            {
                _selectedItem = value;
                if (_selectedItem == null) return;
                SelectedItemQuantity = _selectedItem.Quantity;
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("SelectedItemQuantity");
            }
        }

        public int SelectedItemQuantity
        {
            get {
                if (_selectedItem != null) return _selectedItem.Quantity;
                else return 0;
                        }
            set {
                if (value == 0)
                {
                    DeleteBasketItem();
                    OnPropertyChanged("HeightSelectedItem");
                    return;
                } 
                _selectedItem.Quantity = value;
                OnPropertyChanged("HeightSelectedItem");
                OnPropertyChanged("SelectedItemQuantity");
                UpdateDiscount();
                UpdateTotalPrice();
                
            }
        }

        public int HeightSelectedItem
        {
            get {
                if (_selectedItem != null) return 38;
                else return 0;
            }
        }



        public RelayCommand<Product> AddProductCommand { get; private set; }
        public RelayCommand DeleteBasketItemCommand { get; private set; }
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

        private void DeleteBasketItem()
        {
            BasketItems.Remove(_selectedItem);
            OnPropertyChanged("HeightSelectedItem");
            UpdateDiscount();
            UpdateTotalPrice();

        }

        private int reductionQuantity;

        public int ReductionQuantity
        {
            get { return reductionQuantity; }
            set { reductionQuantity = value;
                OnPropertyChanged("ReductionQuantity");
                OnPropertyChanged("ReductionPrice");
            }
        }

        public float ReductionPrice
        {
            get { return reductionQuantity * -0.2f; }
        }





        public CaisseViewModel(MainWindow window)
        {
            LoadProducts();
            BasketItems = new ObservableCollection<BasketItem>();
            AddProductCommand = new RelayCommand<Product>(AddProductToBasket);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            DeleteBasketItemCommand = new RelayCommand(DeleteBasketItem);
            this.window = window;
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
            fullProductList = new Collection<Product>();

            foreach (Product p in Singleton<Collection<Product>>.GetInstance())
            {
                if (p.Stock != 0) fullProductList.Add(p);
            }

            ProductsView = new ObservableCollection<Product>(fullProductList as Collection<Product>);


        }

        public void ClearBasket()
        {
            BasketItems.Clear();
            ReductionQuantity = 0;
        }

        public void AddProductToBasket(Product p)
        {
            bool exist = false;

            foreach (BasketItem item in BasketItems)
            {
                if (item.ProductName != p.Name) continue;
                if(item.Quantity == p.Stock)
                {
                    window.ShowMessageAsync("Erreur lors de l'ajout de produit", "Vous avez épuisé le stock!");
                    return;
                }
                exist = true;
                int oldQuantity = item.Quantity;
                item.Quantity = oldQuantity + 1;
            }
            
            if (!exist)
            {
                BasketItem newItem = new BasketItem(p);
                BasketItems.Add(newItem);
            }

            UpdateDiscount();
            UpdateTotalPrice();
            OnPropertyChanged("SelectedItemQuantity");


        }


        private void UpdateDiscount()
        {
            int quantity = 0;
            foreach (BasketItem item in BasketItems)
            {
                if (!item.ItemProduct.IsDiscountable) continue;
                quantity = quantity + item.Quantity;
            }
            ReductionQuantity = quantity / 2;
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

            TotalPrice = totalPriceTemp+ReductionPrice;
        }


    }
}
