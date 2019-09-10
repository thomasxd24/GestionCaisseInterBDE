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

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class ChoiceBDEViewModel
    {
        public RelayCommand<BDE> PickBDE { get; private set; }
        public RelayCommand CancelChoice { get; private set; }


        public Collection<BDE> ListBDE { get; private set; }
        private IPersistance persistance;

        private IDialogCoordinator dialogCoordinator;
        private float reduction;
        private Collection<BasketItem> baskets;
        private IDialog dialog;
        private MainWindow main;

        public ChoiceBDEViewModel(IDialogCoordinator instance, Collection<BasketItem> items, IDialog dialog,MainWindow main,float reduc)
        {
            persistance = Singleton<IPersistance>.GetInstance();
            this.dialog = dialog;
            ListBDE = new Collection<BDE>(persistance.GetBDEList().Where(b=> b.Account.ID == Singleton<User>.GetInstance().Account.ID).ToList<BDE>());
            PickBDE = new RelayCommand<BDE>(addBasketToDB);
            dialogCoordinator = instance;
            CancelChoice = new RelayCommand(cancelChoice);
            baskets = items;
            this.main = main;
            this.reduction = reduc;
        }

        private async void addBasketToDB(BDE bdeChosen)
        {
            var totalPrice = baskets.Sum(items => items.quantity * items.ItemProduct.Price);
            var key = DateTime.Now.ToString().GetHashCode().ToString("x");
            var u = Singleton<User>.GetInstance();
            var ticket = new Ticket(key, new DateTime(), bdeChosen, baskets, u,u.Account,reduction);
            persistance.AddTicket(ticket);
            await dialog.HideCurrentDialog();
            await main.ShowMessageAsync("Encaissement Réussi", $"Un montant de {ticket.TotalPaid} a été encaissé au {bdeChosen.Name} avec le ticket {key}");
            Singleton<Event>.GetInstance()?.InvolveClearBasket();
            Singleton<Event>.GetInstance()?.InvolveUpdateProduct();
        }

        private void cancelChoice()
        {
           dialog.HideCurrentDialog();
        }
    }
}
