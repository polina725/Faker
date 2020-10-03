using System;

namespace Faker.ValueGenerator
{
    public interface IBaseGenerator
    {
        public object Generate();

        public Type GetGeneratedType();
    }
}
