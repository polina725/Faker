using System;

using Faker.ValueGenerator;

namespace IntGenerator
{
    public class IntGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            return rand.Next();
        }

        public Type GetGeneratedType()
        {
            return typeof(int);
        }
    }
}
