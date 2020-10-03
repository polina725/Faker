using System;

namespace Faker.ValueGenerators
{
    public interface IBaseGenerator
    {
        public object Generate();

        public Type GetGeneratedType();
    }
}
