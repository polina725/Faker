using System;
using System.Collections.Generic;
using System.Text;

namespace Faker.ValueGenerators.SystemClassGenerator
{
    class DateGenerator : IBaseGenerator
    {
        private Random rand = new Random();

        public object Generate()
        {
            var buf = new byte[8];
            rand.NextBytes(buf);
            var ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % (DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks)) + DateTime.MinValue.Ticks);
        }

        public Type GetGeneratedType()
        {
            return typeof(DateTime);
        }
    }
}
