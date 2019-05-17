using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Persistence.Services;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using iut.GestionCaisseInterBDE.Wpf.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisseInterBDE.ViewModel
{
    public class ProductListViewModel : BaseViewModel
    {
        private IDialogCoordinator dialogCoordinator;
        public RelayCommand ModifyCommand { get; private set; }
        public RelayCommand ConfirmCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand AddProductToListCommand { get; private set; }
        public RelayCommand DeleteProductCommand { get; private set; }
        private Product nullProduct = new Product(999999, "", 0,0, "", 0, false);
        private Product oldProduct;
        private Product _selectedProduct;
        private bool modifiable;

        public bool Modifiable
        {
            get { return !modifiable; }
            set { modifiable = value;
                OnPropertyChanged("Modifiable");
                OnPropertyChanged("ThicknessModify");
                OnPropertyChanged("Enable");
                OnPropertyChanged("VisibleToModify");
            }
        }

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
            oldProduct = null;
            ProductManager.UpdateProductDB(SelectedProduct);
        }

        private void CancelEdit()
        {
            Modifiable = false;
            int index = productsView.IndexOf(SelectedProduct);
            productsView[index] = oldProduct;
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
            
            var success = ProductManager.RemoveProductDB(SelectedProduct);
            if(success)
            {
                ProductsView.Remove(SelectedProduct);
                return;
            }

            await dialogCoordinator.ShowMessageAsync(this, "Suppression de produit", "Erreur lors de la suppression \nBase de donnée non modifié");

        }

        public void AddProductToList()
        {
            var newP = new Product(8858, "", 0, 0, "", 0, false);
            ProductsView.Add(newP);
            SelectedProduct = newP;
        }



        public ProductListViewModel(IDialogCoordinator dialogCoordinator)
        {
            ProductsView = new ObservableCollection<Product>(Singleton<Collection<Product>>.GetInstance() as Collection<Product>);
            ModifyCommand = new RelayCommand(ModifyProduct);
            ConfirmCommand = new RelayCommand(ConfirmEdit);
            CancelCommand = new RelayCommand(CancelEdit);
            AddProductToListCommand = new RelayCommand(AddProductToList);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            this.dialogCoordinator = dialogCoordinator;
        }
    }
}
