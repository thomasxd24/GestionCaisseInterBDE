using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisseInterBDE.Utilities;
using MySql.Data.MySqlClient;

namespace GestionCaisseInterBDE.Model
{
    class Product
    {
        public int id { get; set; }
        public string name { get; set; }

        public Product(int id,string name)
        {
            this.id = id;
            this.name = name;
        }
        public static List<Product> getProductList()
        {

            var db = new MySQLDatabase("SERVER=51.68.230.58;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            DataTable dt = db.Select("SELECT * FROM products");
            List<Product> products = new List<Product>();
            foreach(DataRow dr in dt.Rows)
            {
                Product product = new Product(int.Parse(dr["idProduct"].ToString()), dr["nameProduct"].ToString());
                products.Add(product);
            }
            return products;

        }
    }
}
