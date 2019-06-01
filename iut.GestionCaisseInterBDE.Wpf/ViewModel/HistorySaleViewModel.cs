using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisseInterBDE.ViewModel
{
    public class HistorySaleViewModel : BaseViewModel
    {
        private IPersistance persistance;
        private ObservableCollection<Ticket> ticketList;
        private IDialogCoordinator dialogCoordinator;

        public ObservableCollection<Ticket> TicketList
        {
            get { return ticketList; }
            set { ticketList = value;
                OnPropertyChanged("TicketList");
            }
        }

        public HistorySaleViewModel()
        {
            this.persistance = Singleton<IPersistance>.GetInstance();
            var ticketList = persistance.GetTicketsDB();
            TicketList = new ObservableCollection<Ticket>(ticketList);
            
        }
    }
}
