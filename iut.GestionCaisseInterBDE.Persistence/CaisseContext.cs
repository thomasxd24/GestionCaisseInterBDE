using iut.GestionCaisseInterBDE.Models;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistence
{
    public class CaisseContext : DbContext
    {

        public CaisseContext() : base($"Data Source={System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/bdeEF.db") { }
        public DbSet<BDE> BDEs { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CaisseContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
