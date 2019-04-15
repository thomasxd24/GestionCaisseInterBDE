using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisseInterBDE.ViewModel
{
    public class ProductListViewModel : BaseViewModel
    {
        public RelayCommand ModifyCommand { get; private set; }
        public RelayCommand ConfirmCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand DeleteProductCommand { get; private set; }
        private Product nullProduct = new Product(999999, "", 0,0, "", 0, false);
        private Product oldProduct;
        private Product _selectedProduct;
        private bool modifiable;

        public bool Modifiable
        {
            get { return !modifiable; }
            set { modifiable = value;
                OnPropertyChanged("Modifiable");
                OnPropertyChanged("ThicknessModify");
                OnPropertyChanged("Enable");
                OnPropertyChanged("VisibleToModify");
            }
        }

        public string ThicknessModify
        {
            get {
                if (modifiable) return "1";
                return "0";
            }
        }

        public bool Enable
        {
            get
            {
                return modifiable;
            }
        }

        public string VisibleToModify
        {
            get
            {
                if (modifiable) return "Visible";
                return "Hidden";
            }
        }

        public Product SelectedProduct
        {
            get { if (_selectedProduct == null) return nullProduct;
                else return _selectedProduct;
            }
            set { _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

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

        private void ModifyProduct()
        {
            Modifiable = true;
            oldProduct = new Product(SelectedProduct.ID,
                SelectedProduct.Name,
                SelectedProduct.Price,
                SelectedProduct.BuyPrice,
                SelectedProduct.ImageURL,
                SelectedProduct.Stock,
                SelectedProduct.IsDiscountable);
        }

        private void ConfirmEdit()
        {
            Modifiable = false;
            oldProduct = null;
            //TODO save changes to database
        }

        private void CancelEdit()
        {
            Modifiable = false;
            SelectedProduct.ID = oldProduct.ID;
            SelectedProduct.Name = oldProduct.Name;
            SelectedProduct.Price = oldProduct.Price;
            SelectedProduct.BuyPrice = oldProduct.BuyPrice;
            SelectedProduct.ImageURL = oldProduct.ImageURL;
            SelectedProduct.Stock = oldProduct.Stock;
            SelectedProduct.IsDiscountable = oldProduct.IsDiscountable;
            oldProduct = null;
        }

        private void DeleteProduct()
        {
            ProductsView.Remove(SelectedProduct);

        }



        public ProductListViewModel()
        {
            ProductsView = new ObservableCollection<Product>(Singleton<Collection<Product>>.GetInstance() as Collection<Product>);
            ModifyCommand = new RelayCommand(ModifyProduct);
            ConfirmCommand = new RelayCommand(ConfirmEdit);
            CancelCommand = new RelayCommand(CancelEdit);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
        }
    }
}
