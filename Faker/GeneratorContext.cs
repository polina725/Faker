using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public class GeneratorContext
    {

        public Random Random { get; }

        public Type TargetType { get; }

        public Faker Faker { get; }

        public GeneratorContext(Random random, Type targetType, Faker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }
}
