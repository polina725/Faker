using Faker.TestClasses;
using System;
using System.Collections.Generic;

namespace aaa
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Faker.Faker f = new Faker.Faker(1);
                Console.WriteLine(f.Create<C>());
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
