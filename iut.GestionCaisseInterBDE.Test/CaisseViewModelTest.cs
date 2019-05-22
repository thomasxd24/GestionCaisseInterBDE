using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using iut.GestionCaisseInterBDE.Models;
using System.Collections.ObjectModel;
using iut.GestionCaisseInterBDE.Utilities;
using MahApps.Metro.Controls.Dialogs;

namespace iut.GestionCaisseInterBDE.Test
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestClass]
    public class CaisseViewModelTest
    {
        CaisseViewModel cVm;
        Product testP;
        BDE testBDE;
        Collection<Product> collectionP = new Collection<Product>();
        Collection<BDE> collectionBDE = new Collection<BDE>();

        public CaisseViewModelTest()
        {
            testP = new Product(5555, "hi", 2.5f, 2.8f, "hihiih", 85, true);
            testBDE = new BDE(85, "hibde", "info", "fdfdsfd");
            collectionP.Add(testP);
            collectionBDE.Add(testBDE);
            Singleton<Collection<BDE>>.SetInstance(collectionBDE);
            Singleton<Collection<Product>>.SetInstance(collectionP);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize()
        {

            cVm = new CaisseViewModel(DialogCoordinator.Instance);
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup()
        {
            cVm = null;
        }
        //
        #endregion

        [TestMethod]
        public void TestAddProductToBasket()
        {

            cVm.AddProductToBasket(testP);
            cVm.AddProductToBasket(testP);
            Assert.AreEqual(1, cVm.BasketItems.Count);
            Assert.AreEqual(cVm.BasketItems[0].ItemProduct, testP);
            Assert.AreEqual(cVm.BasketItems[0].Quantity, 2);
        }

        [TestMethod]
        public void TestRemoveProductFromBasket()
        {
            cVm.AddProductToBasket(testP);
            var item = cVm.BasketItems[0];
            cVm.SelectedItem = item;
            cVm.DeleteBasketItemCommand.Execute(null);
            Assert.AreEqual(0, cVm.BasketItems.Count);
        }

        [TestMethod]
        public void TestClearBasket()
        {
            cVm.AddProductToBasket(testP);
            cVm.AddProductToBasket(testP);
            cVm.ClearBasket();
            Assert.AreEqual(0, cVm.BasketItems.Count);
        }
    }
}
