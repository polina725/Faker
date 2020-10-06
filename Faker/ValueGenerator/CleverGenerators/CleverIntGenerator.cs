using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.ValueGenerator.CleverGenerators
{
    public class CleverIntGenerator : IBaseGenerator
    {
        public object Generate()
        {
            return 42;
        }

        public Type GetGeneratedType()
        {
            return typeof(int);
        }
    }
}
