using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Models.Utilities;
using MySql.Data.MySqlClient;

namespace iut.GestionCaisseInterBDE.Models
{
    public class BDE : INotifyPropertyChanged
    {

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; ;
                RaisePropertyChanged("Name");
            }
        }
        private string departement;

        public string Departement
        {
            get { return departement; }
            set { departement = value; ;
                RaisePropertyChanged("Departement");
            }
        }

        private string imageURL;

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; ;
                RaisePropertyChanged("ImageURL");
            }
        }


        public BDE(int id,string name,string departement,string imageURL)
        {
            this.id = id;
            this.name = name;
            this.departement = departement;
            this.imageURL = imageURL;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
