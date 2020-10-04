using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class BoolGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            if (rand.Next(2) == 1)
                return true;
            else
                return false;
        }

        public Type GetGeneratedType()
        {
            return typeof(bool);
        }
    }
}
