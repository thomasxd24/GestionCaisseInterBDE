using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iut.GestionCaisseInterBDE.Wpf.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ChoiceBDEUC.xaml
    /// </summary>
    public partial class ChoiceBDEUC : UserControl, IDialog
    {
        BaseMetroDialog dialog;
        MainWindow main;
        public ChoiceBDEUC(BaseMetroDialog dialog,  Collection<BasketItem> items,float reduction)
        {
            this.main = Application.Current.MainWindow as MainWindow;
            DataContext = new ChoiceBDEViewModel(DialogCoordinator.Instance, items ,this,this.main,reduction);
            this.dialog = dialog;
            

            InitializeComponent();

        }
        public async Task HideCurrentDialog()
        {
            await main.HideMetroDialogAsync(dialog);
        }
    }
}
