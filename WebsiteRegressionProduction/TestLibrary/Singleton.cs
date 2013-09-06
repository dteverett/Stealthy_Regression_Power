using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    class Singleton
    {
        private static Singleton uniqueInstance;

        public static object lockObject;
        public static object lockObject2;

        private Singleton()
        {
            lockObject = new object();
            lockObject2 = new object();
        }

        public static Singleton getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Singleton();
            }
            return uniqueInstance;
        }
    }
}
