using iut.GestionCaisseInterBDE.Persistence.Services;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
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
        private IDialogCoordinator dialogCoordinator;

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

        public LoginDialogViewModel(IDialogCoordinator instance)
        {
            LoginCommand = new RelayCommand(Login);
            this.dialogCoordinator = instance;
        }

        private void Login()
        {
            UserManager.GetUserfromCredentials(username, password);


        }
    }
}
