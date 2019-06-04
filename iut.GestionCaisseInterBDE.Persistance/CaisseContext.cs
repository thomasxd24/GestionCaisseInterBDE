using iut.GestionCaisseInterBDE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using SQLite.CodeFirst;

namespace iut.GestionCaisseInterBDE.Persistance
{
    public class CaisseContext : DbContext
    {

        public CaisseContext() : base("CaisseContext") { }
        public virtual DbSet<BDE> BDEs { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CaisseContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}
