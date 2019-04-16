﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketItem : INotifyPropertyChanged
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
            set { itemProduct = value; RaisePropertyChanged("ItemProduct"); }
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
            set { quantity = value; RaisePropertyChanged("Quantity");
                RaisePropertyChanged("TotalPriceString");

            }
        }

        public BasketItem(Product itemProduct)
        {
            ItemProduct = itemProduct;
            quantity = 1;
        }

        public BasketItem(Product itemProduct,int quantity)
        {
            ItemProduct = itemProduct;
            this.quantity = quantity;
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
