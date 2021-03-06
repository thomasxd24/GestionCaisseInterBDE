﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using iut.GestionCaisseInterBDE.Models;

namespace iut.GestionCaisseInterBDE.Persistance
{
    public interface IPersistance
    {
        void AddTicket(Ticket t);

        void AddUser(User u);

        Collection<BDE> GetBDEList();

        BDE GetBDEByID(int id);

        Product GetProductByID(int id);


        Collection<Product> GetProductList();

        bool RemoveProductDB(Product p);

        int AddProductToDB(Product p);

        bool UpdateProductDB(Product p);

        User GetUserfromCredentials(string username, string password);

        Collection<Ticket> GetTicketsDB();

        IEnumerable<User> GetUsersDB();

        void ChangeStyle(User u,string theme, string style);

        User GetUserfromID(int id);

        void RemoveTicketFromDB(Ticket t);

        Account GetAccountFromID(int id);


    }
}
