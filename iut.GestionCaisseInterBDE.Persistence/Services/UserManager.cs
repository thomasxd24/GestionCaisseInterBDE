using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistence.Services
{
    public static class UserManager
    {
        public static User GetUserfromCredentials(string username,string password)
        {
            IDatabase db = Singleton<IDatabase>.GetInstance();
            string md5password = Crypto.CalculateMD5Hash(password);
            var m = new Dictionary<string, object>
            {
                { "@user", username },
            { "@pass", md5password }
            };
            DataTable dt = db.Select("SELECT * FROM users where username = @user and md5password = @pass",m);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            BDE bde = BDEManager.GetBDEByID(int.Parse(dr["bdeID"].ToString()));
            User authedUser = new User(int.Parse(dr["userID"].ToString()), dr["username"].ToString(), dr["name"].ToString(),bde, dr["theme"].ToString(), dr["accent"].ToString());
            return authedUser;
        }
    }
}
