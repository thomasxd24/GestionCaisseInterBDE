using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Linq;

namespace iut.GestionCaisseInterBDE.Models
{
    public class ProductManager
    {
        public static Collection<Product> GetProductList()
        {

            IDatabase db = Singleton<IDatabase>.GetInstance();
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
            IDatabase db = Singleton<IDatabase>.GetInstance();
            var rowChanged = db.ExecuteCommand($"DELETE FROM products where idProduct={p.ID}");
            if (rowChanged == 0) return false;
            return true; ;
        }

        public static bool UpdateProductDB(Product p)
        {
            IDatabase db = Singleton<IDatabase>.GetInstance();
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
            var p = Singleton<Collection<Product>>.GetInstance().Where(item => item.ID == id).FirstOrDefault();
            return p;
        }
    }
}
