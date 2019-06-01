using iut.GestionCaisseInterBDE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistance
{
    public class CaisseContext : DbContext
    {

        public CaisseContext() : base("caisse") { }
        public virtual DbSet<BDE> BDEs { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<User> Users { get; set; }

    }
}
