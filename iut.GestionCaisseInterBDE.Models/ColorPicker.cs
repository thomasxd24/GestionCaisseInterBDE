using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class ColorPicker
    {
        private static List<string> listColors = new List<string>()
            {
                "#00a300", //green
                "#1e7145", //dark green
                "#ff0097", //magenta
                "#9f00a7", //light purple
                "#7e3878", //purple
                "#603cba", //dark purple
                "#2d89ef", //blue
                "#2b5797", //dark blue
                "#e3a21a", //orange
                "#da532c", //dark orange
                "#ee1111", //red
                "#b91d47" //dark red
            };
        private static Random rnd = new Random();
        public static string getRandomColor()
        {
            int r = rnd.Next(listColors.Count);
            string colorChosen = (string)listColors[r];
            return colorChosen;
        }
    }
}
