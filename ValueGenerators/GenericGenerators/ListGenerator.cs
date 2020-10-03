using System;
using System.Collections.Generic;


namespace Faker.ValueGenerators.GenericGenerators
{
    class ListGenerator<T> : IGenericGenerator
    {
        public object Generate()
        {
            List<T> list = new List<T>();
            
            int n = new Random().Next(1, 5);
            for (int i = 0; i < n; i++)
                list.Add();
            return list;
        }

        public Type GetGeneratedType()
        {
            return typeof(List<T>);
        }
    }
}
