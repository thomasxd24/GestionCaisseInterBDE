using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    [Table("TableUsers")]
    public class User:ObservableObject
    {
        private int id;

        public int ID
        {
            get { return id; }
        }


        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string md5password;

        public string Md5password
        {
            get { return md5password; }
            set { md5password = value; }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();

            }
        }

        private BDE bde;

        public BDE BDE
        {
            get { return bde; }
            set
            {
                bde = value;
                OnPropertyChanged();
            }
        }


        private string theme;

        public string Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                OnPropertyChanged();
            }
        }

        private string accent;

        public string Accent
        {
            get { return accent; }
            set
            {
                accent = value;
                OnPropertyChanged();
            }
        }

        public bool isAdmin { get; private set; }

        public User(int id, string username, string name, BDE bde, string theme, string accent,string md5Password,bool isAdmin)
        {
            this.id = id;
            this.Username = username;
            this.Name = name;
            this.BDE = bde;
            this.theme = theme;
            this.accent = accent;
            this.Md5password = md5Password;
            this.isAdmin = isAdmin;
        }
    }
}
