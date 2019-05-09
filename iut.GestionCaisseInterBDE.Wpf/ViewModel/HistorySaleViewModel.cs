using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Persistence.Services;

namespace GestionCaisseInterBDE.ViewModel
{
    public class HistorySaleViewModel : BaseViewModel
    {
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
            var ticketList = TicketManager.GetTicketsDB();
            TicketList = new ObservableCollection<Ticket>(ticketList);
        }
    }
}
