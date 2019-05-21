using iut.GestionCaisseInterBDE.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class User : ObservableObject
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged();

            }
        }

        private BDE bde;

        public BDE BDE
        {
            get { return bde; }
            set { bde = value;
                OnPropertyChanged();
            }
        }


        private string theme;

        public string Theme
        {
            get { return theme; }
            set { theme = value;
                OnPropertyChanged();
            }
        }

        private string accent;

        public string Accent
        {
            get { return accent; }
            set { accent = value;
                OnPropertyChanged();
            }
        }





    }
}
