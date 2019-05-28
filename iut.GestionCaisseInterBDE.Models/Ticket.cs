using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class Ticket
    {

        public string IDTicket { get; }

        public DateTime DateCreated { get; }

        public BDE BDESale { get; }

        public Collection<BasketItem> ProductItems { get; }


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
            this.IDTicket = idTicket;
            this.DateCreated = dateCreated;
            this.BDESale = bde;
            this.ProductItems = productItems;
        }





    }
}
