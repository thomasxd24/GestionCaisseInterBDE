using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
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



    }
}
