using Faker.TestClasses;
using Faker.ValueGenerator.CleverGenerators;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace sometrash
{
    class Program
    {

        /// TODO: exceptions???
        /// 
        static void Main(string[] args)
        {
          /*  Faker.Faker f = new Faker.Faker(3);
            Faker.FakerConfig conf = new Faker.FakerConfig();
            conf.Add<A, string, CleverStringGenerator>(a => a.str);
            conf.Add<A, int, CleverIntGenerator>(a => a.Number);
            f.AddConfig(conf);
            Console.WriteLine(f.Create<A>().Number);*/
        }

        public struct member
        {
            public short n;
            private short b;
            public A a;

        }

        public struct person
        {
            public int n;
            private int b;

            public person(int a,int k)
            {
                n = a;
                b = k;
            }
        }
    }
}

