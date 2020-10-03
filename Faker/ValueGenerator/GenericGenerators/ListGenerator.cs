using System;
using System.Collections.Generic;


namespace Faker.ValueGenerator.GenericGenerators
{
    public class ListGenerator<T> : IGenericGenerator
    {

        public object Generate()
        {
            List<T> list = new List<T>();
            Faker faker = new Faker(true);
            int n = new Random().Next(1, 5);
            for (int i = 0; i < n; i++)
                list.Add(faker.Create<T>());
            return list;
        }

        public Type GetGeneratedType()
        {
            return typeof(List<T>);
        }
    }
}
