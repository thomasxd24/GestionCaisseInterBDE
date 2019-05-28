using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Utilities;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BDE
    {

        private readonly int id;

        public int ID
        {
            get { return id; }
        }


        private readonly string name;

        public string Name
        {
            get { return name; }
        }
        private readonly string departement;

        public string Departement
        {
            get { return departement; }
        }

        private readonly string imageURL;

        public string ImageURL
        {
            get { return imageURL; }
        }


        public BDE(int id,string name,string departement,string imageURL)
        {
            this.id = id;
            this.name = name;
            this.departement = departement;
            this.imageURL = imageURL;
        }

    }
}
