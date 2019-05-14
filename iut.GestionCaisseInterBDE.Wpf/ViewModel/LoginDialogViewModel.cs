using iut.GestionCaisseInterBDE.Wpf.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class LoginDialogViewModel : BaseViewModel
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value;
                OnPropertyChanged();
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; private set; }

        public LoginDialogViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        { }
    }
}
