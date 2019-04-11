using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class ProductManager
    {
        public static Collection<Product> GetProductList()
        {

            var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            DataTable dt = db.Select("SELECT * FROM products");
            Collection<Product> products = new Collection<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                Product product = new Product(
                    int.Parse(dr["idProduct"].ToString()),
                    dr["nameProduct"].ToString(),
                    float.Parse(dr["prix"].ToString()),
                    dr["imageUrl"].ToString());
                products.Add(product);
            }
            return products;

        }

        public static bool AddProductToDatabase(Product p)
        {
            return true;
        }

        public static bool RemoveProductFromDatabase(Product p)
        {
            return true;
        }
    }
}
