using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
