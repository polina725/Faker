using System;

namespace Faker.ValueGenerators
{
    class ShortGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            return (short)rand.Next();
        }

        public Type GetGeneratedType()
        {
            return typeof(short);
        }
    }
}
