﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace iut.GestionCaisseInterBDE.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)

        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR"); ;

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR"); ;



            FrameworkElement.LanguageProperty.OverrideMetadata(

              typeof(FrameworkElement),

              new FrameworkPropertyMetadata(

                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));



            base.OnStartup(e);

        }
    }
}
