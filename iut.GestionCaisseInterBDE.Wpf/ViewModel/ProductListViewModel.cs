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
        private Product nullProduct = new Product(999999, "", 0, "", 0, false);
        private Product _selectedProduct;

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



        public ProductListViewModel()
        {
            ProductsView = new ObservableCollection<Product>(Singleton<Collection<Product>>.GetInstance() as Collection<Product>);
        }
    }
}
