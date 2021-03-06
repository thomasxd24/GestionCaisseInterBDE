﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Utilities;

namespace iut.GestionCaisseInterBDE.Models
{
    [Table("TableBDE")]
    public class BDE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        public string Name { get; }

        public string Departement { get; }

        public string ImageURL { get; }

        public Account Account { get; }


        public BDE(int id,string name,string departement,string imageURL,Account acc)
        {
            this.ID = id;
            this.Name = name;
            this.Departement = departement;
            this.ImageURL = imageURL;
            this.Account = acc;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
               BDE bde = (BDE)obj;
                return this.ID == bde.ID;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
