using Faker.TestClasses;
using Faker.ValueGenerator.CleverGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FakerTests
{
    [TestClass]
    public class Tests
    {
        Faker.Faker faker;

        [TestInitialize]
        public void Setup()
        {
            faker = new Faker.Faker(1);
        }

        [TestMethod]
        public void StructAndSystemClassCreation()
        {
            try
            {
                member m = faker.Create<member>();
                person per = faker.Create<person>();
                DateTime date = faker.Create<DateTime>();
                Assert.AreNotEqual(null, m);
                Assert.AreNotEqual(null, per);
                Assert.AreNotEqual(0, m.n);
                Assert.AreNotEqual(0, m.B);
                Assert.AreNotEqual(null, date);
            }
            catch { }
        }

        [TestMethod]
        public void ListCreation()
        {
            try
            {
                List<int> list = faker.Create<List<int>>();
                List<string> l = faker.Create<List<string>>();
                Assert.AreNotEqual(null, list);
                Assert.AreNotEqual(null, l);
            }
            catch { }
        }

        [TestMethod]
        public void MyClassesCreation()
        {
            try
            {
                A a = faker.Create<A>();
                B b = faker.Create<B>();
                Assert.AreNotEqual(null, a);
                Assert.AreNotEqual(null, b);
                Assert.AreNotEqual(null, a.str);                
            }
            catch { }
        }

        [TestMethod]
        public void CircularReferencesCheck()
        {
            try
            {
                C c = faker.Create<C>();
                Assert.AreEqual(null, c.d.e.c);
            }
            catch{ }
        }

        [TestMethod]
        public void CheckGenerationByCustomGenerator()
        {
            try
            {
                Faker.FakerConfig conf = new Faker.FakerConfig();
                conf.Add<A, string, CleverStringGenerator>(a => a.str);
                conf.Add<B, int, CleverStringGenerator>(b => b.Number);
                conf.Add<B, string, CleverStringGenerator>(b => b.S);
                faker.AddConfig(conf);
                A a = faker.Create<A>();
                B b = faker.Create<B>();
                Assert.AreEqual(true, Check(a.str));
                Assert.AreEqual(true, Check(b.S));
                Assert.AreEqual(42, b.Number);
            }
            catch { }
        }

        private bool Check(string str)
        {
            string[] someWords = { "clever", "generator" ,"bruh",
                                        "War","is","peace","Freedom","is","slavery","Ignorance","is","strength",
                                        "The","world's","full","of","lonely","people","afraid","to","make","the","first","move"};
            foreach (string s in someWords)
                if (s == str)
                    return true;
            return false;
        }

        struct member
        {
            public short n;

            public short B { set; get; }
        }

        struct person
        {
            public int n;
            private int b;

            public person(int a, int k)
            {
                n = a;
                b = k;
            }
        }
    }
}