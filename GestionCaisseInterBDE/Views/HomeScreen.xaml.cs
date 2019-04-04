﻿using System;
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

namespace GestionCaisseInterBDE
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
        public HomeScreen()
        {
            InitializeComponent();
        }
        private void navigateConsoBtn_MouseClick(Object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }

        private void NavigateTresorBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }

        private void NavigateEventsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }

        private void NavigateAnnoncesBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }

        private void NavigateSauvegardeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }

        private void NavigateParametreBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views\\Tresorie.xaml", UriKind.Relative));
        }
    }
}
