using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using iut.GestionCaisseInterBDE.Models;

namespace iut.GestionCaisseInterBDE.Persistence
{
    public class EFPersistance : IPersistance
    {
        public void AddTicket(string ticketID, BDE bde, Collection<BasketItem> basketItems)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var ticket = new Ticket(ticketID, new DateTime(), bde, basketItems);
                db.Tickets.Add(ticket);
            }
        }

        public BDE GetBDEByID(int id)
        {
            using (CaisseContext db = new CaisseContext())
            {
            }
        }

        public Collection<BDE> GetBDEList()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public Collection<Product> GetProductList()
        {
            throw new NotImplementedException();
        }

        public Collection<Ticket> GetTicketsDB()
        {
            throw new NotImplementedException();
        }

        public User GetUserfromCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProductDB(Product p)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProductDB(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
