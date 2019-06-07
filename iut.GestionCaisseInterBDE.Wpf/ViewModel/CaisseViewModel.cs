using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private IPersistance persistance;
        private Collection<Product> fullProductList;
        private IDialogCoordinator dialogCoordinator;

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
                OnPropertyChanged("CanEncaisse");
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





        public CaisseViewModel(IDialogCoordinator instance)
        {
            
            BasketItems = new ObservableCollection<BasketItem>();
            AddProductCommand = new RelayCommand<Product>(AddProductToBasket);
            ClearBasketCommand = new RelayCommand(ClearBasket);
            DeleteBasketItemCommand = new RelayCommand(DeleteBasketItem);
            this.dialogCoordinator = instance;
            this.persistance = Singleton<IPersistance>.GetInstance();
            LoadProducts();
            Singleton<Event>.GetInstance().OnUpdateProduct += OnUpdateProduct;


        }

        public void OnUpdateProduct(object sender)
        {
            LoadProducts();
        }

        private void UpdateSearchResult(string searchString)
        {

            if (searchString == "" || searchString == "Rechercher...")
            {
                ProductsView = new ObservableCollection<Product>(fullProductList as Collection<Product>);
                return;
            }
            //TO DO : voir LINQ
            var list = fullProductList.Where(item => FuzzyMatcher.FuzzyMatch(item.Name, searchString)).ToList();
            ObservableCollection<Product> filteredList;
            if (list == null)
            {
                filteredList = new ObservableCollection<Product>();
            }
            else
            {
                filteredList = new ObservableCollection<Product>(list);
            }
                
            ProductsView = filteredList;
        }

        public void LoadProducts()
        {
            fullProductList = new Collection<Product>();

            foreach (Product p in persistance.GetProductList())
            {
                if (p.Stock != 0) fullProductList.Add(p);
            }

            ProductsView = new ObservableCollection<Product>(fullProductList as Collection<Product>);


        }

        public void ClearBasket()
        {
            BasketItems.Clear();
            UpdateDiscount();
            UpdateTotalPrice();

        }

        public bool CanEncaisse { get {
                return totalPrice != 0;
            } }

        public void AddProductToBasket(Product p)
        {
            bool exist = false;

            foreach (BasketItem item in BasketItems)
            {
                if (item.ProductName != p.Name) continue;
                if(item.Quantity == p.Stock)
                {
                    dialogCoordinator.ShowMessageAsync(this,"Erreur lors de l'ajout de produit", "Vous avez épuisé le stock!");
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




        public async void AddBasketToDB(BDE bdeChosen, BaseMetroDialog dialog)
        {
            var totalPrice = TotalPrice;
            var key = DateTime.Now.ToString().GetHashCode().ToString("x");
            var u = Singleton<User>.GetInstance();
            var ticket = new Ticket(key, new DateTime(), bdeChosen, BasketItems,u);
            persistance.AddTicket(ticket);
            await dialogCoordinator.HideMetroDialogAsync(this,dialog);
            await dialogCoordinator.ShowMessageAsync(this,"Encaissement Réussi", $"Un montant de {totalPrice.ToString("C2")} a été encaissé au {bdeChosen.Name} avec le ticket {key}");
            ClearBasket();
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
