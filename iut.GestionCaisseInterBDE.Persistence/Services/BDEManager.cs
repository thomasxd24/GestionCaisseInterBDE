using iut.GestionCaisseInterBDE.Models.Utilities;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Linq;

namespace iut.GestionCaisseInterBDE.Persistence.Services
{
    public class BDEManager
    {
        /// <summary>
        /// returns the collection of BDE from database
        /// </summary>
        /// <returns>the collection of BDE from database</returns>
        public static Collection<BDE> GetBDEList()
        {
            var bdeList = new Collection<BDE>();
            IDatabase db = Singleton<IDatabase>.GetInstance();
            DataTable dt = db.Select("SELECT * FROM bde");
            foreach (DataRow dr in dt.Rows)
            {
                BDE bde = new BDE(int.Parse(dr["idBDE"].ToString()),dr["name"].ToString(), dr["departement"].ToString(),dr["imageUrl"].ToString());
                bdeList.Add(bde);
            }
            return bdeList;
        }


        /// <summary>
        /// return a bde for its ID, null if none found
        /// </summary>
        /// <param name="id">ID of the BDE</param>
        /// <returns>The instance of BDE</returns>
        public static BDE GetBDEByID(int id)
        {

            var bde = Singleton<Collection<BDE>>.GetInstance().Where(item => item.ID == id).FirstOrDefault();
            return bde;
        }
    }
}
