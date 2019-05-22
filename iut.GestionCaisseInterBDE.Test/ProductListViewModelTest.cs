using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionCaisseInterBDE.ViewModel;
using iut.GestionCaisseInterBDE.Models;
using System.Collections.ObjectModel;
using iut.GestionCaisseInterBDE.Utilities;
using MahApps.Metro.Controls.Dialogs;

namespace iut.GestionCaisseInterBDE.Test
{
    /// <summary>
    /// Description résumée pour ProductListViewModelTest
    /// </summary>
    [TestClass]
    public class ProductListViewModelTest
    {
        ProductListViewModel pVm;
        Product testP;
        BDE testBDE;
        Collection<Product> collectionP = new Collection<Product>();
        Collection<BDE> collectionBDE = new Collection<BDE>();
        public ProductListViewModelTest()
        {
            testP = new Product(5555, "hi", 2.5f, 2.8f, "hihiih", 85, true);
            testBDE = new BDE(85, "hibde", "info", "fdfdsfd");
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
            pVm = new ProductListViewModel(DialogCoordinator.Instance);
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup()
        {
            pVm = null;
        }
        //
        #endregion

        [TestMethod]
        public void TestModify()
        {
            pVm.ProductsView.Add(testP);
            Assert.AreEqual(1, pVm.ProductsView.Count);
            pVm.SelectedProduct = testP;
            pVm.ModifyCommand.Execute(null);
            Assert.AreEqual(false, pVm.Modifiable);
            Assert.AreEqual("1", pVm.ThicknessModify);
            Assert.AreEqual("Visible", pVm.VisibleToModify);
            Assert.AreEqual(true, pVm.Enable);

        }

        [TestMethod]
        public void TestCancel()
        {
            TestModify();
            pVm.CancelCommand.Execute(null);
            Assert.AreEqual(true, pVm.Modifiable);
            Assert.AreEqual("0", pVm.ThicknessModify);
            Assert.AreEqual("Hidden", pVm.VisibleToModify);
            Assert.AreEqual(false, pVm.Enable);
        }
    }
}
