using System;

namespace Faker.ValueGenerator.BaseTypesGenerators
{
    class LongGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            var buffer = new byte[8];
            long result;
            do
            {
                rand.NextBytes(buffer);
                result = BitConverter.ToInt64(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGeneratedType()
        {
            return typeof(long);
        }
    }
}
