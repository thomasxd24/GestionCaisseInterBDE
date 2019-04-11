﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using MahApps.Metro.Controls;
namespace iut.GestionCaisseInterBDE.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Collection<BDE> BDEs;
        public MainWindow()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");
            InitializeComponent();
            BDEs = BDEManager.GetBDEList();
        }

    }
}
