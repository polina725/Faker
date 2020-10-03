using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
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
