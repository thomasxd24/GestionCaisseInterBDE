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

        private Collection<BasketItem> baskets;
        private IDialog dialog;

        public ChoiceBDEViewModel(IDialogCoordinator instance, Collection<BasketItem> items, IDialog dialog)
        {
            persistance = Singleton<IPersistance>.GetInstance();
            this.dialog = dialog;
            ListBDE = persistance.GetBDEList();
            PickBDE = new RelayCommand<BDE>(addBasketToDB);
            dialogCoordinator = instance;
            CancelChoice = new RelayCommand(cancelChoice);
            baskets = items;
        }

        private async void addBasketToDB(BDE bdeChosen)
        {
            var totalPrice = baskets.Sum(items => items.quantity * items.ItemProduct.Price);
            var key = DateTime.Now.ToString().GetHashCode().ToString("x");
            var u = Singleton<User>.GetInstance();
            var ticket = new Ticket(key, new DateTime(), bdeChosen, baskets, u);
            persistance.AddTicket(ticket);
            dialog.HideCurrentDialog();
            await dialogCoordinator.ShowMessageAsync(this, "Encaissement Réussi", $"Un montant de {totalPrice.ToString("C2")} a été encaissé au {bdeChosen.Name} avec le ticket {key}");
            Singleton<Event>.GetInstance()?.InvolveClearBasket();
        }

        private void cancelChoice()
        {
           dialog.HideCurrentDialog();
        }
    }
}
