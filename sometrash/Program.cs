﻿using Faker.TestClasses;
using Faker.ValueGenerator.CleverGenerators;
using System;
using System.Linq.Expressions;

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
         /*   Console.WriteLine(typeof(person).IsPrimitive);
            try
            {
                Console.WriteLine(f.Create<char>());
            }
            catch(Faker.FakerException ex) 
            {
                Console.WriteLine(ex.Message);
            }

            ConstructorInfo[] constructors = t.GetConstructors();
            ConstructorInfo currentConstructor = null;
            if (constructors.Length!=0)
                currentConstructor = constructors[0];
            foreach (ConstructorInfo c in constructors)
                if (c.GetParameters().Length > currentConstructor.GetParameters().Length)
                    currentConstructor = c;
            object[] o = { 3, 5 };
            person per = (person)currentConstructor.Invoke(o);
            object obj = Activator.CreateInstance(t);
            Console.WriteLine(obj+" "+per);   */         
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

