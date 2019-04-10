using iut.GestionCaisseInterBDE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class CaisseViewModel
    {
        public ObservableCollection<Product> Products
        {
            get;
            set;
        }

        public ObservableCollection<BasketItem> BasketItems
        {
            get;
            set;
        }

        public void LoadProducts()
        {
            Collection<Product> listProd = Product.getProductList();
            Products = new ObservableCollection<Product>(listProd as Collection<Product>) ;
        }

        
    }
}
