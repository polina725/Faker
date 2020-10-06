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
            Faker.Faker f = new Faker.Faker(3);
            Faker.FakerConfig conf = new Faker.FakerConfig();
            conf.Add<A, string, CleverStringGenerator>(a => a.str);
            object obj = f.Create<A>();
            Console.WriteLine(obj.GetType() + " " + ((A)obj).str);
            f.AddConfig(conf);
            Console.WriteLine(f.Create<A>().str);
/*            Type t = typeof(A);
            ConstructorInfo[] constructors = t.GetConstructors();
            ConstructorInfo currentConstructor = null;
            if (constructors.Length!=0)
                currentConstructor = constructors[0];
            foreach (ConstructorInfo c in constructors)
                if (c.GetParameters().Length > currentConstructor.GetParameters().Length)
                    currentConstructor = c;
            ParameterInfo[] parameters = currentConstructor.GetParameters();
            foreach (ParameterInfo p in parameters)
            {
                Console.WriteLine(p.Name + " " + p.ParameterType + " " + p.Member);
            }*/
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

