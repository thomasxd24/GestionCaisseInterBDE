using System;
using System.Collections.ObjectModel;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistence;
using iut.GestionCaisseInterBDE.Persistence.Services;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjet
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddProductToBasket()
        {
            Product testP = new Product(5555, "hi", 2.5f, 2.8f, "hihiih", 85, true);
            var collectionP = new Collection<Product>();
            collectionP.Add(testP);
            BDE testBDE = new BDE(85, "hibde", "info", "fdfdsfd");
            var collectionBDE = new Collection<BDE>();
            collectionBDE.Add(testBDE);
            Singleton<Collection<BDE>>.SetInstance(collectionBDE);
            Singleton<Collection<Product>>.SetInstance(collectionP);

            CaisseViewModel cVm = new CaisseViewModel(DialogCoordinator.Instance);
            
            cVm.AddProductToBasket(testP);
            cVm.AddProductToBasket(testP);
            Assert.AreEqual(1, cVm.BasketItems.Count);
            Assert.AreEqual(cVm.BasketItems[0].ItemProduct, testP);
            Assert.AreEqual(cVm.BasketItems[0].Quantity, 2);

        }
    }
}
