using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class FloatGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            return (float)rand.NextDouble();
        }

        public Type GetGeneratedType()
        {
            return typeof(float);
        }
    }
}
