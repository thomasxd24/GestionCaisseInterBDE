using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketItem : INotifyPropertyChanged
    {
        private string productName { get; set; }
        private string totalPriceString { get; set; }
        private string quantity { get; set; }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; RaisePropertyChanged("ProductName"); }
        }


        public string TotalPriceString
        {
            get { return totalPriceString; }
            set { totalPriceString = value; RaisePropertyChanged("TotalPriceString"); }
        }
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; RaisePropertyChanged("Quantity"); }
        }

        public BasketItem(string productName, string totalPriceString, string quantity)
        {
            ProductName = productName;
            TotalPriceString = totalPriceString;
            Quantity = quantity;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
