using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Models
{
    [Table("TableBasketItem")]
    public class BasketItem : ObservableObject
    {
        [Key]
        public string Id { get; set; }

        private string totalPriceString { get; set; }
        public int quantity { get; set; }

        public Product itemProduct { get; set; }

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
