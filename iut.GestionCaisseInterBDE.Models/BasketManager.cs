using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BasketManager
    {
        /// <summary>
        /// Add a a product to the ticket and insert it in the database
        /// </summary>
        /// <param name="ticketID">ID of the ticket</param>
        /// <param name="bde">BDE of the ticket</param>
        /// <param name="p">Product</param>
        /// <param name="quantity">Quantity of the product added</param>
        /// <returns></returns>
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
