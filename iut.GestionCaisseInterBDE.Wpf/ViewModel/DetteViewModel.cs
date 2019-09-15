using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Persistance;

namespace iut.GestionCaisseInterBDE.Wpf.ViewModel
{
    public class DetteViewModel : BaseViewModel
    {
        private ObservableCollection<BDE> listBDE;
        public RelayCommand ShowAllCommand { get; private set; }
        private IPersistance persistance;

        public ObservableCollection<BDE> ListBDE
        {
            get { return listBDE; }
            set { listBDE = value;
                OnPropertyChanged("ListBDE");
            }
        }

        private BDE selectedBDE;
        public float SaleTotal
        {
            get
            {
                if (Singleton<User>.GetInstance() == null) return 0;
                return ListTickets.Sum(t => float.Parse(t.TotalPaid.Replace(" €", string.Empty)));
            }
        }

        public float ProfitTotal
        {
            get
            {
                if (Singleton<User>.GetInstance() == null) return 0;
                float totalBuyPrice = 0;
                foreach (var ticket in ListTickets)
                {
                    foreach (var item in ticket.ProductItems)
                    {
                        totalBuyPrice = totalBuyPrice + (item.ItemProduct.BuyPrice * item.Quantity);
                    }
                }
                return SaleTotal - totalBuyPrice;
            }
        }

        public float MargeTotal
        {
            get
            {
                if (Singleton<User>.GetInstance() == null) return 0;
                return ProfitTotal/ SaleTotal;
            }
        }

        public BDE SelectedBDE
        {
            get { return selectedBDE; }
            set { selectedBDE = value;
                OnPropertyChanged("SelectedBDE");
                OnPropertyChanged("ListTickets");
                OnPropertyChanged("ListMembre");
                OnPropertyChanged("SaleTotal");
                OnPropertyChanged("ProfitTotal");
                OnPropertyChanged("MargeTotal");
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
                OnPropertyChanged("SaleTotal");
                OnPropertyChanged("ProfitTotal");
                OnPropertyChanged("MargeTotal");
            }
        }
        private DateTime toDateTime = DateTime.Today;
        public DateTime ToDateTime
        {
            get { return toDateTime; }
            set
            {
                toDateTime = value;
                OnPropertyChanged();
                OnPropertyChanged("ListTickets");
                OnPropertyChanged("SaleTotal");
                OnPropertyChanged("ProfitTotal");
                OnPropertyChanged("MargeTotal");
            }
        }

        private IEnumerable<Ticket> listTickets;
        public IEnumerable<Ticket> ListTickets
        {
            get {if(selectedBDE == null)
                {
                    return listTickets?.Where(c => c.DateCreated >= fromDateTime.AddDays(-1)
                                          && c.DateCreated <= toDateTime.AddDays(+1) ).ToList(); ;
                }
                return listTickets?.Where(c => c.DateCreated >= fromDateTime.AddDays(-1)
                                          && c.DateCreated <= toDateTime.AddDays(+1) && c.BDESale.ID == selectedBDE?.ID).ToList(); }
        }




        public DetteViewModel()
        {
            persistance = Singleton<IPersistance>.GetInstance();
            Singleton<Event>.GetInstance().OnChangeUser += updateInfo;
            ShowAllCommand = new RelayCommand(ShowAll);
        }
        private void updateInfo(object sender)
        {
            ListBDE = new ObservableCollection<BDE>(persistance.GetBDEList().Where(b=>b.Account.ID == Singleton<User>.GetInstance().Account.ID).ToList());
            listTickets = persistance.GetTicketsDB();
            selectedBDE = null;
            OnPropertyChanged("ListTickets");
            OnPropertyChanged("SaleTotal");
            OnPropertyChanged("ProfitTotal");
            OnPropertyChanged("MargeTotal");
        }

        private void ShowAll()
        {
            selectedBDE = null;
            OnPropertyChanged("ListTickets");
            OnPropertyChanged("SaleTotal");
            OnPropertyChanged("ProfitTotal");
            OnPropertyChanged("MargeTotal");
        }

        private void GenerateCSV()
        {
            string fileName = @"hi.csv";

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    //sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                    //sw.WriteLine("Author: Mahesh Chand");
                    //sw.WriteLine("Add one more line ");
                    //sw.WriteLine("Add one more line ");
                    //sw.WriteLine("Done! ");
                }

                // Write file contents on console.     
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }


    }
}
