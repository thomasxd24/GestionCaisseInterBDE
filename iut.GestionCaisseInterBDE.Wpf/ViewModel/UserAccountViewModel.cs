using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class UserAccountViewModel : BaseViewModel
    {
        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }

        public IEnumerable<User> ListUsers => Singleton<IPersistance>.GetInstance().GetUsersDB();

        public IEnumerable<BDE> ListBDE => Singleton<IPersistance>.GetInstance().GetBDEList();
    }
}
