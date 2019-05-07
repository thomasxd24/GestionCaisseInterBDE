using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Models.Utilities;
using MySql.Data.MySqlClient;

namespace iut.GestionCaisseInterBDE.Models
{
    public class Product : ObservableObject
    {
        private int id;

        public int ID
        {
            get { return id; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private float price;

        public float Price
        {
            get { return price; }
        }

        public string PriceString
        {
            get { return price.ToString("C2"); }
        }

        private string imageUrl;

        public string ImageURL
        {
            get { return imageUrl; }
        }

        private string colorTile;

        public string ColorTile
        {
            get { return colorTile; }
        }

        private int stock;

        public int Stock
        {
            get { return stock; }
        }

        private bool isDiscountable;

        public bool IsDiscountable
        {
            get { return isDiscountable; }
        }

        private float buyPrice;

        public float BuyPrice
        {
            get { return buyPrice; }
        }


        public Product(int id,string name,float price ,float buyPrice,string imageUrl,int stock, bool isDiscountable)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.buyPrice = buyPrice;
            this.imageUrl = imageUrl;
            this.colorTile = ColorPicker.getRandomColor();
            this.stock = stock;
            this.isDiscountable = isDiscountable;
        }
       
    }
}
