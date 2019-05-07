﻿using iut.GestionCaisseInterBDE.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BDEManager
    {
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

        public static BDE GetBDEByID(int id)
        {
            foreach(BDE bde in Singleton<Collection<BDE>>.GetInstance())
            {
                if (bde.ID == id) return bde;
            }
            return null;
        }
    }
}
