using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class Ticket : ObservableObject
    {
        #region declaration
        private string idTicket;

        public string IDTicket
        {
            get { return idTicket; }
            set
            {
                idTicket = value;
                OnPropertyChanged("IDTicket");
            }
        }

        private DateTime dateCreated;

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                dateCreated = value;
                OnPropertyChanged("DateCreated");
            }
        }

        private BDE bde;

        public BDE BDESale
        {
            get { return bde; }
            set
            {
                bde = value;
                OnPropertyChanged("BDE");
            }
        }

        private Collection<BasketItem> productItems;

        public Collection<BasketItem> ProductItems
        {
            get { return productItems; }
            set { productItems = value;
                OnPropertyChanged("ProductItems");
            }
        }


        public string TotalPaid
        {
            get {
                float totalPaid = 0;
                foreach (BasketItem basketItem in ProductItems)
                {
                    totalPaid = totalPaid + basketItem.TotalPrice;
                }
                return totalPaid.ToString("C2");
            }
        }

        public Ticket(string idTicket, DateTime dateCreated, BDE bde, Collection<BasketItem> productItems)
        {
            this.idTicket = idTicket;
            this.dateCreated = dateCreated;
            this.bde = bde;
            this.productItems = productItems;
        }



        #endregion





    }
}
