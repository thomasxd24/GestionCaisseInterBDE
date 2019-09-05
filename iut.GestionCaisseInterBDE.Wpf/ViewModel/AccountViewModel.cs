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
using iut.GestionCaisseInterBDE.Wpf.Views.Windows;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    class AccountViewModel: BaseViewModel
    {
        private User selectedUser;
        private IPersistance persistance;
        private IEnumerable<String> accents => new Collection<string>()
        {
            "Red",
            "Green",
            "Blue",
            "Purple",
            "Orange",
            "Lime",
            "Emerald",
            "Teal",
            "Cyan",
            "Cobalt",
            "Indigo",
            "Violet",
            "Pink",
            "Magenta",
            "Crimson",
            "Amber",
            "Yellow",
            "Brown",
            "Olive",
            "Steel",
            "Mauve",
            "Taupe",
            "Sienna"

        };
        private IEnumerable<String> themes => new Collection<string>()
        {
            "BaseDark",
            "BaseLight"
        };

        public IEnumerable<String> Themes
        { get
            { return themes; }
        }

        public IEnumerable<String> Accents
        {
            get
            { return accents; }
        }
        public IEnumerable<BDE> BDE;

        public RelayCommand AddUserCommand { get; private set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
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

        public AccountViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser);
            persistance = Singleton<IPersistance>.GetInstance();
            ListUsers = new ObservableCollection<User>(Singleton<IPersistance>.GetInstance().GetUsersDB());
        }

        public void AddUser()
        {
            new AddUserScreen().ShowDialog();
        }
    }
}
