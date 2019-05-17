using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketItem : ObservableObject
    {

        private string totalPriceString { get; set; }
        private int quantity { get; set; }

        private Product itemProduct { get; set; }

        public string ProductName
        {
            get { return itemProduct.Name; }
        }

        public Product ItemProduct
        {
            get { return itemProduct; }
        }

        public float TotalPrice
        {
            get { return itemProduct.Price * quantity; }
        }
        public string TotalPriceString
        {
            get { return (itemProduct.Price*quantity).ToString("C2"); }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("TotalPriceString");

            }
        }

        public BasketItem(Product itemProduct)
        {
            this.itemProduct = itemProduct;
            quantity = 1;
        }

        public BasketItem(Product itemProduct,int quantity)
        {
            this.itemProduct = itemProduct;
            this.quantity = quantity;
        }
    }
}
