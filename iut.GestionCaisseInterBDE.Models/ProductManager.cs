using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class ProductManager
    {
        public static Collection<Product> GetProductList()
        {

            var db = new SQLiteDatabase("Data Source=C:\\Users\\Thomas\\Documents\\GestionCaisseInterBDE\\iut.GestionCaisseInterBDE.Wpf\\bin\\Debug\\bde.db", "");
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
                    (bool)(int.Parse(dr[6].ToString())!=0)
                    );
                products.Add(product);
            }
            return products;

        }

        public static bool AddProductToDatabase(Product p)
        {
            return true;
        }

        public static bool RemoveProductDB(Product p)
        {
            var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");

            var rowChanged = db.ExecuteCommand($"DELETE FROM products where idProduct={p.ID}");
            if (rowChanged == 0) return false;
            return true; ;
        }

        public static bool UpdateProductDB(Product p)
        {
            var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
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
            var rowChanged = db.ExecuteCommand($"UPDATE products SET nameProduct=@name,prix=@price,prixAchat=@buyprice,stock=@stock,imageUrl=@imageurl,isDiscountable=@discountable where idProduct=@id",m);
            if (rowChanged == 0) return false;
            return true; ;
        }


        public static Product GetProductByID(int id)
        {
            foreach (Product p in Singleton<Collection<Product>>.GetInstance())
            {
                if (p.ID == id) return p;
            }
            return null;
        }
    }
}
