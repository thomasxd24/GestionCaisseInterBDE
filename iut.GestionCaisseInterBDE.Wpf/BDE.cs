using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Models.Utilities;
using MySql.Data.MySqlClient;

namespace iut.GestionCaisseInterBDE.Models
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

        public static Collection<BDE> getBDEList()
        {
            var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            DataTable dt = db.Select("SELECT * FROM bde");
            Collection<BDE> bdeList = new Collection<BDE>();
            foreach (DataRow dr in dt.Rows)
            {
                BDE bde = new BDE(dr["name"].ToString(), dr["departement"].ToString());
                bdeList.Add(bde);
            }
            return bdeList;
        }
    }
}
