using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class DetteViewModel : BaseViewModel
    {
        private ObservableCollection<BDE> listBDE;
        private IDialogCoordinator dialogCoordinator;

        public ObservableCollection<BDE> ListBDE
        {
            get { return listBDE; }
            set { listBDE = value;
                OnPropertyChanged("ListBDE");
            }
        }

        private BDE selectedBDE;

        public BDE SelectedBDE
        {
            get { return selectedBDE; }
            set { selectedBDE = value;
                OnPropertyChanged("SelectedBDE");
            }
        }

        public DetteViewModel()
        {
            listBDE = new ObservableCollection<BDE>(Singleton<Collection<BDE>>.GetInstance());
        }



    }
}
