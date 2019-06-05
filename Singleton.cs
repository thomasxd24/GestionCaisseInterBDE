using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Utilities
{
    public class Singleton<T>
    {
        private static T instanceUnique;

        protected Singleton()
        {

        }

        public static T GetInstance()
        {
            if (instanceUnique == null) return default(T);
            return instanceUnique;
        }

        public static void SetInstance(T instance)
        {
            instanceUnique = instance;
        }


    }
}
