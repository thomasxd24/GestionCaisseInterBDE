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
using iut.GestionCaisseInterBDE.Persistance;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class DetteViewModel : BaseViewModel
    {
        private ObservableCollection<BDE> listBDE;
        private IPersistance persistance;

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
                OnPropertyChanged("ListTickets");
                OnPropertyChanged("ListMembre");
            }
        }

        private DateTime fromDateTime = DateTime.Today.AddMonths(-1);
        public DateTime FromDateTime
        {
            get { return fromDateTime; }
            set
            {
                fromDateTime = value;
                OnPropertyChanged();
                OnPropertyChanged("ListTickets");
            }
        }
        private DateTime toDateTime = DateTime.Today.AddDays(1);
        public DateTime ToDateTime
        {
            get { return toDateTime; }
            set
            {
                toDateTime = value;
                OnPropertyChanged();
                OnPropertyChanged("ListTickets");
            }
        }

        private IEnumerable<Ticket> listTickets;

        public IEnumerable<Ticket> ListTickets
        {
            get { return listTickets.Where(c => c.DateCreated >= fromDateTime
                                          && c.DateCreated <= toDateTime && c.BDESale.ID == selectedBDE?.ID).ToList(); }
        }

        private IEnumerable<User> listUsers;
        public IEnumerable<User> ListMembre
        {
            get { return listUsers.Where(u => u.BDE.ID == selectedBDE?.ID).ToList(); }
        }


        public DetteViewModel()
        {
            persistance = Singleton<IPersistance>.GetInstance();
            listBDE = new ObservableCollection<BDE>(persistance.GetBDEList());
            listTickets = persistance.GetTicketsDB();
            listUsers = persistance.GetUsersDB();
        }



    }
}
