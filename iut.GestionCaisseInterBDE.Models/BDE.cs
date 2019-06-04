﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
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


        public BDE(int id,string name,string departement,string imageURL)
        {
            this.ID = id;
            this.Name = name;
            this.Departement = departement;
            this.ImageURL = imageURL;
        }

    }
}
