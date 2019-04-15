using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Models
{
    public class Singleton<T> where T : class,new()
    {
        private static T instanceUnique;

        protected Singleton()
        {

        }

        public static T GetInstance()
        {
            if (instanceUnique == null) instanceUnique = new T();
            return instanceUnique;
        }

        public static void SetInstance(T instance)
        {
            instanceUnique = instance;
        }


    }
}
