using System;
using System.Collections.Generic;

namespace sometrash
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker.Faker f = new Faker.Faker();
            List<int> l = f.Create<List<int>>();
            foreach (int n in l)
                Console.WriteLine(n);
            List<string> list = f.Create<List<string>>();
            foreach (string n in list)
                Console.WriteLine(n);
        }

        public static bool MyInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            Console.WriteLine(typeObj + " " + criteriaObj);
            if (typeObj.ToString() == criteriaObj.ToString())
                return true;
            else
                return false;
        }
    }
}
