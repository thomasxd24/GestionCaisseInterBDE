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
            IDatabase db = Singleton<IDatabase>.GetInstance();
            var bdeID = bde.ID;
            var productID = p.ID;
            var rowChanged = db.ExecuteCommand($"INSERT INTO ligneTicket values('{ticketID}','{productID}','{bdeID}',{quantity},date('now'))");
            return rowChanged;
           
        }
    }
}
