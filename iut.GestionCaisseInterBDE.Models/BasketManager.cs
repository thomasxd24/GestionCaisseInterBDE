using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketManager
    {
        public static int AddTicket(string ticketID, BDE bde, Product p,int quantity)
        {
            var db = new MySQLDatabase("SERVER=51.68.230.58;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            var bdeID = bde.ID;
            var productID = p.ID;
            var rowChanged = db.ExecuteCommand($"INSERT INTO ligneTicket values('{ticketID}','{productID}','{bdeID}',{quantity},now())");
            return rowChanged;
           
        }
    }
}
