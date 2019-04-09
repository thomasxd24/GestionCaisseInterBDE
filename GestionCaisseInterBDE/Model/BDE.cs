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
    public class BDE
    {
        public string name { get; set; }
        public string departement { get; set; }
        public BDE(string name,string departement)
        {
            this.name = name;
            this.departement = departement;
        }

        public static List<BDE> getBDEList()
        {
            var db = new MySQLDatabase("SERVER=51.68.230.58;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            DataTable dt = db.Select("SELECT * FROM bde");
            List<BDE> bdeList = new List<BDE>();
            foreach (DataRow dr in dt.Rows)
            {
                BDE bde = new BDE(dr["name"].ToString(), dr["departement"].ToString());
                bdeList.Add(bde);
            }
            return bdeList;
        }
    }
}
