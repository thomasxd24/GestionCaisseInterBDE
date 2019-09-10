using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistance
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
        public void AddTicket(Ticket t)
        {
            var bdeID = t.BDESale.ID;

            foreach (var basketitem in t.ProductItems)
            {
                var productID = basketitem.ItemProduct.ID;
                db.ExecuteCommand($"INSERT INTO ligneTicket values('{t.IDTicket}','{productID}','{bdeID}',{basketitem.Quantity},datetime('now'),{t.SellerUser.ID},{t.Account.ID},{t.Reduction.ToString("F", CultureInfo.InvariantCulture)})");
                db.ExecuteCommand($"UPDATE products SET stock={basketitem.itemProduct.Stock - basketitem.quantity} where idProduct={basketitem.itemProduct.ID}");

            }

        }

        /// <summary>
        /// returns the collection of BDE from database
        /// </summary>
        /// <returns>the collection of BDE from database</returns>
        public Collection<BDE> GetBDEList()
        {
            var bdeList = new Collection<BDE>();
            var user = Singleton<User>.GetInstance();
            DataTable dt = db.Select($"SELECT * FROM bde");
            foreach (DataRow dr in dt.Rows)
            {
                var acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
                BDE bde = new BDE(int.Parse(dr["idBDE"].ToString()), dr["name"].ToString(), dr["departement"].ToString(), dr["imageUrl"].ToString(),acc);
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


        public void AddUser(User u)
        {
            db.ExecuteCommand($"INSERT INTO users (name,username,md5password,bdeID,theme,accent,isAdmin,accountid) values('{u.Name}','{u.Username}','{u.Md5password}',{u.BDE.ID},'{u.Theme}','{u.Accent}',{u.isAdmin},{u.Account.ID})");

        }

        public Collection<Product> GetProductList()
        {
            var user = Singleton<User>.GetInstance();
            DataTable dt = db.Select($"SELECT * FROM products WHERE accountid={user.Account.ID}");
            Collection<Product> products = new Collection<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                var acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
                Product product = new Product(
                    int.Parse(dr[0].ToString()),
                    dr[1].ToString(),
                    float.Parse(dr[2].ToString()),
                    float.Parse(dr[3].ToString()),
                    dr[5].ToString(),
                    int.Parse(dr[4].ToString()),
                    (bool)(int.Parse(dr[6].ToString()) != 0),
                    acc
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
            var m = getDictionaryFromProduct(p);
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
            var user = Singleton<User>.GetInstance();
            DataTable dt = db.Select($"SELECT * FROM ligneTicket WHERE accountid={user.Account.ID} ORDER BY idTicket ASC");
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
                    var u = GetUserfromID(int.Parse(dr["idUserSeller"].ToString()));
                    var acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
                    oneTicket = new Ticket(dr["idTicket"].ToString(), DateTime.Parse(dr["dateCreated"].ToString()), bde, new Collection<BasketItem>(),u,acc,float.Parse(dr["reduction"].ToString()));
                    var product = GetProductByID(int.Parse(dr["idProduit"].ToString()));
                    if (product == null) continue;
                    var basketItem = new BasketItem(product, int.Parse(dr["quantity"].ToString()));
                    oneTicket.ProductItems.Add(basketItem);
                }
            }
            ticketList.Add(oneTicket);
            return ticketList;
        }

        public IEnumerable<User> GetUsersDB()
        {
            DataTable dt = db.Select("SELECT * FROM users");
            Collection<User> users = new Collection<User>();
            
            foreach (DataRow dr in dt.Rows)
            {
                BDE bde = GetBDEByID(int.Parse(dr["bdeID"].ToString()));
                Account acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
                User u = new User(int.Parse(dr["userID"].ToString()), dr["username"].ToString(), dr["name"].ToString(), bde, dr["theme"].ToString(), dr["accent"].ToString(), dr["md5password"].ToString(),dr["isAdmin"].ToString() == "1", acc);
                users.Add(u);
            }
            return users;
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
            Account acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
            User authedUser = new User(int.Parse(dr["userID"].ToString()), dr["username"].ToString(), dr["name"].ToString(), bde, dr["theme"].ToString(), dr["accent"].ToString(), dr["md5password"].ToString(), dr["isAdmin"].ToString() == "1", acc);
            return authedUser;
        }

        public void ChangeStyle(User u,string theme, string style)
        {
            var m = new Dictionary<string, object>
            {
                { "@theme", theme },
                { "@style", style },
                {"@id", u.ID }
            };
            var rowChanged = db.ExecuteCommand($"UPDATE users SET theme=@theme,accent=@style where userID=@id", m);
            if (rowChanged == 0) throw new Exception("Failed while updating the style");
        }

        public int AddProductToDB(Product p)
        {
            var m = getDictionaryFromProduct(p);
            var rowChanged = db.ExecuteCommand($"INSERT INTO products (nameProduct,prix,prixAchat,stock,imageUrl,isDiscountable,accountid) VALUES (@name, @price, @buyprice,@stock,@imageurl,@discountable,@account)", m);
            if (rowChanged == 0) throw new Exception("Insertion failed");
            DataTable dt = db.Select("SELECT * FROM products where nameProduct = @name and prix = @price", m);
            if (dt.Rows.Count == 0) throw new Exception("Insertion failed");
            DataRow dr = dt.Rows[0];
            var id = int.Parse(dr["idProduct"].ToString());
            return id;
        }

        private Dictionary<string, object> getDictionaryFromProduct(Product p)
        {
            var m = new Dictionary<string, object>
            {
                { "@name", p.Name },
                { "@price", p.Price },
                { "@buyprice", p.BuyPrice },
                { "@stock", p.Stock },
                { "@imageurl", p.ImageURL },
                { "@discountable", p.IsDiscountable },
                { "@id", p.ID },
                {"@account",p.Account.ID }
            };
            return m;
        }

        public User GetUserfromID(int id)
        {
            var m = new Dictionary<string, object>()
            {
                {"@id",id}
            };
            DataTable dt = db.Select("SELECT * FROM users where userID=@id", m);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            BDE bde = GetBDEByID(int.Parse(dr["bdeID"].ToString()));
            Account acc = GetAccountFromID(int.Parse(dr["accountid"].ToString()));
            User authedUser = new User(int.Parse(dr["userID"].ToString()), dr["username"].ToString(), dr["name"].ToString(), bde, dr["theme"].ToString(), dr["accent"].ToString(), dr["md5password"].ToString(), dr["isAdmin"].ToString() == "1", acc);
            return authedUser;
        }

        public void RemoveTicketFromDB(Ticket t)
        {
            var m = new Dictionary<string, object>()
            {
                {"@id",t.IDTicket }
            };
            var i = db.ExecuteCommand("DELETE FROM ligneTicket where idTicket=@id", m);
            if (i == 0) throw new Exception("Error while deleting the ticket");
        }

        public Account GetAccountFromID(int id)
        {
            var m = new Dictionary<string, object>()
            {
                {"@id",id}
            };
            DataTable dt = db.Select("SELECT * FROM account where id=@id", m);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            Account acc = new Account(int.Parse(dr["id"].ToString()), dr["name"].ToString());
            return acc;
        }
    }
}
