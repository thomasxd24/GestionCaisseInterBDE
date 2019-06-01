using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class LoginDialogViewModel : BaseViewModel
    {
        private IPersistance persistance;
        private string username;
        private IDialogCoordinator dialogCoordinator;
        private IDialog activeDialog;

        public string Username
        {
            get { return username; }
            set { username = value;
                Error = false;
                OnPropertyChanged();
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value;
                Error = false;
                OnPropertyChanged();
            }
        }

        private bool error = false;

        public bool Error
        {
            get { return error; }
            set { error = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand LoginCommand { get; private set; }

        public LoginDialogViewModel(IDialogCoordinator instance,IDialog dialog)
        {
            persistance = Singleton<IPersistance>.GetInstance();
            LoginCommand = new RelayCommand(Login);
            this.dialogCoordinator = instance;
            activeDialog = dialog;
        }

        private void Login()
        {
            User user = persistance.GetUserfromCredentials(username, password);
            if (user == null)
            { Error = true;
                return;
            }
            activeDialog.HideCurrentDialog();


        }
    }
}
