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
    public class BDEAccountViewModel:BaseViewModel
    {
        private BDE selectedBDE;

        public BDE SelectedBDE
        {
            get { return selectedBDE; }
            set
            {
                selectedBDE = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<BDE> ListBDEs => Singleton<IPersistance>.GetInstance().GetBDEList();
    }
}
