using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    [Table("TableUsers")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Username { get; }

        public string Md5password { get; }

        public string Name { get; }

        public BDE BDE { get; }


        private string theme;

        public string Theme
        {
            get { return theme; }
            set { theme = value;
            }
        }

        private string accent;

        public string Accent
        {
            get { return accent; }
            set { accent = value;
            }
        }

        public User(int id, string username, string name, BDE bde, string theme, string accent,string md5Password)
        {
            this.ID = id;
            this.Username = username;
            this.Name = name;
            this.BDE = bde;
            this.theme = theme;
            this.accent = accent;
            this.Md5password = md5Password;
        }
    }
}
