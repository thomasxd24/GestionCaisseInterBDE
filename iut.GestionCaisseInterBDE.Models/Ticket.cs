using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    [Table("TableTicket")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDTicket { get; set; }

        public DateTime DateCreated { get; }

        public BDE BDESale { get; }

        public Collection<BasketItem> ProductItems { get; }

        public User SellerUser { get; }

        public Account Account { get; }

        public float Reduction { get; }


        [NotMapped]
        public string TotalPaid
        {
            get {
                float totalPaid = 0;
                foreach (BasketItem basketItem in ProductItems)
                {
                    totalPaid = totalPaid + basketItem.TotalPrice;
                }
                totalPaid = totalPaid - Reduction;
                return totalPaid.ToString("C2");
            }
        }

        public Ticket(string idTicket, DateTime dateCreated, BDE bde, Collection<BasketItem> productItems, User u,Account acc,float reduc)
        {
            this.IDTicket = idTicket;
            this.DateCreated = dateCreated;
            this.BDESale = bde;
            this.ProductItems = productItems;
            this.SellerUser = u;
            this.Account = acc;
            this.Reduction = reduc;
        }





    }
}
