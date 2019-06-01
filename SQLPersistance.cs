using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistence
{
    public class SQLPersistance : IPersistance
    {

        private ISQLDatabase db;

        public SQLPersistance(ISQLDatabase db)
        {
            this.db = db;
        }

        /// <summary>
        /// Add a a product to the ticket and insert it in the database
        /// </summary>
        /// <param name="ticketID">ID of the ticket</param>
        /// <param name="bde">BDE of the ticket</param>
        /// <param name="p">Product</param>
        /// <param name="quantity">Quantity of the product added</param>
        /// <returns></returns>
        public void AddTicket(string ticketID, BDE bde, Collection<BasketItem> basketItems)
        {
            var bdeID = bde.ID;

            foreach (var basketitem in basketItems)
            {
                var productID = basketitem.ItemProduct.ID;
                db.ExecuteCommand($"INSERT INTO ligneTicket values('{ticketID}','{productID}','{bdeID}',{basketitem.Quantity},date('now'))");
            }

        }

        /// <summary>
        /// returns the collection of BDE from database
        /// </summary>
        /// <returns>the collection of BDE from database</returns>
        public Collection<BDE> GetBDEList()
        {
            var bdeList = new Collection<BDE>();
            DataTable dt = db.Select("SELECT * FROM bde");
            foreach (DataRow dr in dt.Rows)
            {
                BDE bde = new BDE(int.Parse(dr["idBDE"].ToString()), dr["name"].ToString(), dr["departement"].ToString(), dr["imageUrl"].ToString());
                bdeList.Add(bde);
            }
            return bdeList;
        }


        /// <summary>
        /// return a bde for its ID, null if none found
        /// </summary>
        /// <param name="id">ID of the BDE</param>
        /// <returns>The instance of BDE</returns>
        public BDE GetBDEByID(int id)
        {

            var bdes = GetBDEList();
            var bde = bdes.Where(item => item.ID == id).FirstOrDefault();
            return bde;
        }

        public Collection<Product> GetProductList()
        {

            DataTable dt = db.Select("SELECT * FROM products");
            Collection<Product> products = new Collection<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                Product product = new Product(
                    int.Parse(dr[0].ToString()),
                    dr[1].ToString(),
                    float.Parse(dr[2].ToString()),
                    float.Parse(dr[3].ToString()),
                    dr[5].ToString(),
                    int.Parse(dr[4].ToString()),
                    (bool)(int.Parse(dr[6].ToString()) != 0)
                    );
                products.Add(product);
            }
            return products;

        }

        public bool AddProductToDatabase(Product p)
        {
            return true;
        }

        public bool RemoveProductDB(Product p)
        {
            var rowChanged = db.ExecuteCommand($"DELETE FROM products where idProduct={p.ID}");
            if (rowChanged == 0) return false;
            return true; ;
        }

        public bool UpdateProductDB(Product p)
        {
            var m = new Dictionary<string, object>
            {
                { "@name", p.Name },
            { "@price", p.Price },
            { "@buyprice", p.BuyPrice },
            { "@stock", p.Stock },
            { "@imageurl", p.ImageURL },
            { "@discountable", p.IsDiscountable },
            { "@id", p.ID }
            };
            var rowChanged = db.ExecuteCommand($"UPDATE products SET nameProduct=@name,prix=@price,prixAchat=@buyprice,stock=@stock,imageUrl=@imageurl,isDiscountable=@discountable where idProduct=@id", m);
            if (rowChanged == 0) return false;
            return true;
        }


        public Product GetProductByID(int id)
        {
            var products = GetProductList();
            var p = products.Where(item => item.ID == id).FirstOrDefault();
            return p;
        }

        public Collection<Ticket> GetTicketsDB()
        {
            var ticketList = new Collection<Ticket>();
            DataTable dt = db.Select("SELECT * FROM ligneTicket ORDER BY idTicket ASC");
            Ticket oneTicket = null;
            string numTicket = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (numTicket == dr["idTicket"].ToString())
                {
                    var product = GetProductByID(int.Parse(dr["idProduit"].ToString()));
                    if (product == null) continue;
                    var basketItem = new BasketItem(product, int.Parse(dr["quantity"].ToString()));
                    oneTicket.ProductItems.Add(basketItem);
                }
                else
                {
                    if (oneTicket != null) ticketList.Add(oneTicket);
                    numTicket = dr["idTicket"].ToString();
                    var bde = GetBDEByID(int.Parse(dr["idBDE"].ToString()));
                    oneTicket = new Ticket(dr["idTicket"].ToString(), DateTime.Parse(dr["dateCreated"].ToString()), bde, new Collection<BasketItem>());
                    var product = GetProductByID(int.Parse(dr["idProduit"].ToString()));
                    if (product == null) continue;
                    var basketItem = new BasketItem(product, int.Parse(dr["quantity"].ToString()));
                    oneTicket.ProductItems.Add(basketItem);
                }
            }
            ticketList.Add(oneTicket);
            return ticketList;
        }

        public User GetUserfromCredentials(string username, string password)
        {
            string md5password = Crypto.CalculateMD5Hash(password);
            var m = new Dictionary<string, object>
            {
                { "@user", username },
            { "@pass", md5password }
            };
            DataTable dt = db.Select("SELECT * FROM users where username = @user and md5password = @pass", m);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            BDE bde = GetBDEByID(int.Parse(dr["bdeID"].ToString()));
            User authedUser = new User(int.Parse(dr["userID"].ToString()), dr["username"].ToString(), dr["name"].ToString(), bde, dr["theme"].ToString(), dr["accent"].ToString(), dr["md5password"].ToString());
            return authedUser;
        }

    }
}
