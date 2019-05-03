using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketManager
    {

        public static int AddTicket(string ticketID, BDE bde, Product p,int quantity)
        {
            var db = new SQLiteDatabase("Data Source=C:\\Users\\Thomas\\Documents\\GestionCaisseInterBDE\\iut.GestionCaisseInterBDE.Wpf\\bin\\Debug\\bde.db", "");
            var bdeID = bde.ID;
            var productID = p.ID;
            var rowChanged = db.ExecuteCommand($"INSERT INTO ligneTicket values('{ticketID}','{productID}','{bdeID}',{quantity},now())");
            return rowChanged;
           
        }
    }
}
