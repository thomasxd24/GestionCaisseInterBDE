using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class TicketManager
    {
        public static Collection<Ticket> GetTicketsDB()
        {
            var ticketList = new Collection<Ticket>();
            var db = new MySQLDatabase("SERVER=5.135.179.154;Port=8080;Database=bde;Uid=bdeUser;Pwd=412qIrJSUkM0;", "MySql.Data.MySqlClient");
            DataTable dt = db.Select("SELECT * FROM ligneTicket ORDER BY idTicket ASC");
            Ticket oneTicket = null;
            string numTicket = "";
            foreach (DataRow dr in dt.Rows)
            {
                if(numTicket == dr["idTicket"].ToString())
                {
                    var product = ProductManager.GetProductByID(int.Parse(dr["idProduit"].ToString()));
                    var basketItem = new BasketItem(product, int.Parse(dr["quantity"].ToString()));
                    oneTicket.ProductItems.Add(basketItem);
                }
                else
                {
                    if(oneTicket != null) ticketList.Add(oneTicket);
                    numTicket = dr["idTicket"].ToString();
                    var bde = BDEManager.GetBDEByID(int.Parse(dr["idBDE"].ToString()));
                    oneTicket = new Ticket(dr["idTicket"].ToString(), DateTime.Parse(dr["dateCreated"].ToString()), bde, new Collection<BasketItem>());
                    var product = ProductManager.GetProductByID(int.Parse(dr["idProduit"].ToString()));
                    var basketItem = new BasketItem(product, int.Parse(dr["quantity"].ToString()));
                    oneTicket.ProductItems.Add(basketItem);
                }
            }
            ticketList.Add(oneTicket);
            return ticketList;
        }
    }
}
