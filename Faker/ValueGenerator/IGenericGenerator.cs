using System;

namespace Faker.ValueGenerator
{
    public interface IGenericGenerator
    {
        public object Generate();

        public Type GetGeneratedType();
    }
}
