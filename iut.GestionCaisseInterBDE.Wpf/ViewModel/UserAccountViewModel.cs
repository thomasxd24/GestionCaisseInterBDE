using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class UserAccountViewModel : BaseViewModel
    {
        private User selectedUser;
        private IPersistance persistance;

        public RelayCommand AddUserCommand { get; private set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set {
                selectedUser = value;
                OnPropertyChanged();
                SelectedBDEUser = ListBDE.FirstOrDefault(bde => bde.ID == selectedUser.BDE.ID);
            }
        }
        private BDE selectedBDEUser;
        public BDE SelectedBDEUser
        {
            get { return selectedBDEUser; }
            set
            {
                selectedBDEUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> ListUsers { get; private set; } 

        public ObservableCollection<BDE> ListBDE => new ObservableCollection<BDE>(Singleton<IPersistance>.GetInstance().GetBDEList());

        public UserAccountViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser);
            persistance = Singleton<IPersistance>.GetInstance();
            ListUsers = new ObservableCollection<User>(Singleton<IPersistance>.GetInstance().GetUsersDB());
        }

        public void AddUser()
        {
            var newU = new User(99999, "", "", null, "", "", "");
            ListUsers.Add(newU);
            SelectedUser = newU;
        }
    }
}
