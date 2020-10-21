using Faker.TestClasses;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Faker.Faker(1).Create<A>();
        }
    }
}
