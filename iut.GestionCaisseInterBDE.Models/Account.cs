using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Models
{
    public class Account
    {
        public int ID { get; }


        public string Name { get; }

        public Account(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
