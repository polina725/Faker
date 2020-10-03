using System;

using Faker.ValueGenerator;

namespace StringGeneratorPlugin
{
    public class StringGenerator : IBaseGenerator
    {
        private Random rand = new Random();

            public object Generate()
            {
                byte[] arr = new byte[rand.Next(2, 255)];
                rand.NextBytes(arr);
                return Convert.ToBase64String(arr);
            }

            public Type GetGeneratedType()
            {
                return typeof(string);
            }
    }
}
