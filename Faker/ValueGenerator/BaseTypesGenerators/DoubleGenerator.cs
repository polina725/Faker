using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class DoubleGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            return rand.NextDouble();
        }

        public Type GetGeneratedType()
        {
            return typeof(double);
        }
    }
}
