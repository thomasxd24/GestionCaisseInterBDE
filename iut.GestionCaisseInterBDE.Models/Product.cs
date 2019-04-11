﻿using System;
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
    public class Product : INotifyPropertyChanged
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value;
                RaisePropertyChanged("ID");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                RaisePropertyChanged("Name");
            }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value;
                RaisePropertyChanged("Price");
            }
        }

        public string PriceString
        {
            get { return price.ToString("C2"); }
        }

        private string imageUrl;

        public string ImageURL
        {
            get { return imageUrl; }
            set { imageUrl = value;
                RaisePropertyChanged("ImageURL");
            }
        }


        public Product(int id,string name,float price ,string imageUrl)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.imageUrl = imageUrl;
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
