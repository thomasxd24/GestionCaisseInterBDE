using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
using iut.GestionCaisseInterBDE.Models;
using MahApps.Metro;

namespace iut.GestionCaisseInterBDE.Wpf.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : UserControl, IDialog
    {
        BaseMetroDialog dialog;
        MainWindow main;
        public LoginDialog(BaseMetroDialog dialog,MainWindow main)
        {
            DataContext = new LoginDialogViewModel(DialogCoordinator.Instance,this);
            this.dialog = dialog;
            this.main = main;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public async Task HideCurrentDialog()
        {
            var user = Singleton<User>.GetInstance();
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(user.Accent), ThemeManager.GetAppTheme(user.Theme));
            main.username.Content = user.Name;
            await main.HideMetroDialogAsync(dialog);

        }
    }
}
