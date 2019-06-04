using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;

namespace iut.GestionCaisseInterBDE.Persistance
{
    public class EFPersistance : IPersistance
    {
        public void AddTicket(string ticketID, BDE bde, Collection<BasketItem> basketItems)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var ticket = new Ticket(ticketID, new DateTime(), bde, basketItems);

                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
        }

        public BDE GetBDEByID(int id)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var bdeSelected = db.BDEs.SingleOrDefault(bde => bde.ID == id);
                db.SaveChanges();
                return bdeSelected;
            }
        }

        public Collection<BDE> GetBDEList()
        {
            using (CaisseContext db = new CaisseContext())
            {
                var bdeSelected = new Collection<BDE>(db.BDEs.ToList());
                db.SaveChanges();
                return bdeSelected;
            }
        }

        public Product GetProductByID(int id)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var product = db.Products.SingleOrDefault(p => p.ID == id);
                db.SaveChanges();
                return product;
            }
        }

        public Collection<Product> GetProductList()
        {
            using (CaisseContext db = new CaisseContext())
            {
                var productList = new Collection<Product>(db.Products.ToList());
                db.SaveChanges();
                return productList;
            }
        }

        public Collection<Ticket> GetTicketsDB()
        {
            using (CaisseContext db = new CaisseContext())
            {
                var ticketList = new Collection<Ticket>(db.Tickets.ToList());
                db.SaveChanges();
                return ticketList;
            }
        }

        public User GetUserfromCredentials(string username, string password)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var userSelected = db.Users.SingleOrDefault(user=> user.Username == username && user.Md5password == password);
                db.SaveChanges();
                return userSelected;
            }
        }

        public bool RemoveProductDB(Product p)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var user = db.Products.Remove(p);
                db.SaveChanges();
                
            }
            return true;
        }

        public bool UpdateProductDB(Product p)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var itemToRemove = db.Products.SingleOrDefault(x => x.ID == p.ID);
                db.Products.Remove(p);
                db.Products.Add(p);
                db.SaveChanges();
                
            }
            return true;
        }

        public void ChangeStyle(string theme, string style)
        {
            using (CaisseContext db = new CaisseContext())
            {
                var user = Singleton<User>.GetInstance();
                user.Accent = style;
                user.Theme = theme;
                db.SaveChanges();
            }
        }
    }
}
