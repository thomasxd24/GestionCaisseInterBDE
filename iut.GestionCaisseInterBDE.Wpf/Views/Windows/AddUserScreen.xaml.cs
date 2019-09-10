using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;

namespace iut.GestionCaisseInterBDE.Wpf.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddUserScreen.xaml
    /// </summary>
    public partial class AddUserScreen : MetroWindow, INotifyPropertyChanged
    {
        public IEnumerable<BDE> BDE { get; }

        public BDE selectedBDE;
        public BDE SelectedBDE
        {
            set
            {
                selectedBDE = value;
                OnPropertyChanged();
            }
        }
        private AccountViewModel vm;
        public AddUserScreen(IEnumerable<BDE> bdes,AccountViewModel vm)
        {
            InitializeComponent();
            DataContext = this;
            BDE = bdes;
            this.vm = vm;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var persistance = Singleton<IPersistance>.GetInstance();
            var user = new User(9999, usernameTB.Text, nameTB.Text, selectedBDE, "BaseDark", "Indigo", md5TB.Text, true,
                Singleton<User>.GetInstance().Account);
            persistance.AddUser(user);
            vm.refreshUsers();
            this.Hide();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            md5TB.Text = Crypto.CalculateMD5Hash(PasswordBox.Password);
        }
    }
}
