using System;

namespace Faker.ValueGenerators
{
    public interface IGenericGenerator
    {
        public object Generate();

        public Type GetGeneratedType();
    }
}
