using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Persistance;

namespace GestionCaisseInterBDE.ViewModel
{
    public class ProductListViewModel : BaseViewModel
    {
        private IPersistance persistance;
        private IDialogCoordinator dialogCoordinator;
        public RelayCommand ModifyCommand { get; private set; }
        public RelayCommand ConfirmCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand<string> SortCommand { get; private set; }
        public RelayCommand AddProductToListCommand { get; private set; }
        public RelayCommand DeleteProductCommand { get; private set; }
        private Product nullProduct = new Product(999999, "", 0,0, "", 0, false);
        private Product oldProduct;
        private Product _selectedProduct;
        private Collection<Ticket> listTickets;
        private bool modifiable;

        public Collection<Ticket> VenteTickets
        {
            get { return new Collection<Ticket>(listTickets.Where(t => t.ProductItems.Any(it => it.ItemProduct.ID == SelectedProduct.ID)).ToList()); }
        }

        public bool Modifiable
        {
            get { return !modifiable && Singleton<User>.GetInstance().isAdmin; }
            set { modifiable = value;
                OnPropertyChanged("Modifiable");
                OnPropertyChanged("ThicknessModify");
                OnPropertyChanged("Enable");
                OnPropertyChanged("VisibleToModify");
                OnPropertyChanged("EditMode");
            }
        }

        public bool EditMode => !modifiable;

        public string ThicknessModify
        {
            get {
                if (modifiable) return "1";
                return "0";
            }
        }

        public bool Enable
        {
            get
            {
                return modifiable;
            }
        }

        public string VisibleToModify
        {
            get
            {
                if (modifiable) return "Visible";
                return "Hidden";
            }
        }


        public string ProductPageVisible
        {
            get
            {
                if (_selectedProduct != null) return "Visible";
                if(modifiable) CancelEdit();
                return "Hidden";
            }
        }

        public Product SelectedProduct
        {
            get { if (_selectedProduct == null) return nullProduct;
                else return _selectedProduct;
            }
            set { _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
                OnPropertyChanged("ProductPageVisible");
                OnPropertyChanged("VenteTickets");
            }
        }

        private ObservableCollection<Product> productsView;
        public ObservableCollection<Product> ProductsView
        {
            get { return productsView; }
            set
            {
                productsView = value;
                OnPropertyChanged("ProductsView");
            }
        }

        private void ModifyProduct()
        {
            Modifiable = true;
            oldProduct = new Product(SelectedProduct.ID,
                SelectedProduct.Name,
                SelectedProduct.Price,
                SelectedProduct.BuyPrice,
                SelectedProduct.ImageURL,
                SelectedProduct.Stock,
                SelectedProduct.IsDiscountable);
        }

        private void ConfirmEdit()
        {
            Modifiable = false;
            var originalProduct = persistance.GetProductList();
            if(oldProduct ==null)
            {
                var id = persistance.AddProductToDB(SelectedProduct);
                SelectedProduct.ID = id;
            }
            else
            {
                oldProduct = null;
                persistance.UpdateProductDB(SelectedProduct);
            }


        }

        private void CancelEdit()
        {
            Modifiable = false;
            if(SelectedProduct.ID == 999999)
            {
                productsView.Remove(SelectedProduct);
                oldProduct = null;
                return;
            }
            int index = productsView.IndexOf(SelectedProduct);
            productsView[index] = oldProduct;
            SelectedProduct = oldProduct;
            oldProduct = null;
        }

        private async void DeleteProduct()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Oui",
                NegativeButtonText = "Non"
            };

            MessageDialogResult result = await dialogCoordinator.ShowMessageAsync(this,"Suppression de produit", "Êtes-vous sûr de vouloir supprimer ce produit? \nAttention : cette opération est irréversible. ",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Negative) return;
            
            var success = persistance.RemoveProductDB(SelectedProduct);
            if(success)
            {
                ProductsView.Remove(SelectedProduct);
                return;
            }

            await dialogCoordinator.ShowMessageAsync(this, "Suppression de produit", "Erreur lors de la suppression \nBase de donnée non modifié");
            

        }

        public void AddProductToList()
        {
            var newP = new Product(999999, "", 0, 0, "", 0, false);
            ProductsView.Add(newP);
            SelectedProduct = newP;
            Modifiable = true;
        }

        private void sortProductView(string type)
        {
            switch (type)
            {
                case "libelle":
                    ProductsView = new ObservableCollection<Product>(ProductsView.OrderBy(p => p.Name));
                    break;
                case "prix":
                    ProductsView = new ObservableCollection<Product>(ProductsView.OrderBy(p => p.Price));
                    break;
                case "stock":
                    ProductsView = new ObservableCollection<Product>(ProductsView.OrderBy(p => p.Stock));
                    break;
                default:
                    break;
            }
        }


        public ProductListViewModel(IDialogCoordinator dialogCoordinator)
        {
            persistance = Singleton<IPersistance>.GetInstance();
            ProductsView = new ObservableCollection<Product>(persistance.GetProductList() as Collection<Product>);
            ModifyCommand = new RelayCommand(ModifyProduct);
            ConfirmCommand = new RelayCommand(ConfirmEdit);
            CancelCommand = new RelayCommand(CancelEdit);
            SortCommand = new RelayCommand<string>(sortProductView);
            AddProductToListCommand = new RelayCommand(AddProductToList);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            listTickets = persistance.GetTicketsDB();
            this.dialogCoordinator = dialogCoordinator;
        }
    }
}
