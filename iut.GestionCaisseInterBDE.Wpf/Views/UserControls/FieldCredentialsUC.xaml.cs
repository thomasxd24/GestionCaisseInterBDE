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

namespace iut.GestionCaisseInterBDE.Wpf.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FieldCredentialsUC.xaml
    /// </summary>
    public partial class FieldCredentialsUC : UserControl
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public static readonly DependencyProperty UsernameProperty =
         DependencyProperty.Register("Username", typeof(string), typeof(FieldCredentialsUC), new
            PropertyMetadata(""));

        public static readonly DependencyProperty PasswordProperty =
         DependencyProperty.Register("Password", typeof(string), typeof(FieldCredentialsUC), new
            PropertyMetadata(""));


        public FieldCredentialsUC()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }

        }
    }
}
