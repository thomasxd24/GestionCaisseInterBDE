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
using MahApps.Metro.Controls;
namespace iut.GestionCaisseInterBDE.Wpf
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            int i;
            int.TryParse((sender as Tile).Uid,out i);
            if (i == 2)
            {
                var main = Application.Current.MainWindow as MainWindow;
                main.SettingsBtn_Click(sender, e);
                return;
            }
            ((MainWindow)Application.Current.MainWindow).mainTab.SelectedIndex = i;
        }
    }
}
