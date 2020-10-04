using System;
using System.Collections.Generic;

using Faker.ValueGenerator.BaseTypesGenerators;
using Faker.ValueGenerator.GenericGenerators;

namespace Faker.ValueGenerator
{
    public class GeneratorFactory
    {
        public static Dictionary<Type,IBaseGenerator> CraeteBaseTypesGenerators()
        {
            Dictionary<Type, IBaseGenerator> dict = new Dictionary<Type, IBaseGenerator>();
            AddGenerator(new ShortGenerator(), dict);
            AddGenerator(new BoolGenerator(), dict);
            AddGenerator(new FloatGenerator(), dict);
            AddGenerator(new DoubleGenerator(), dict);
            AddGenerator(new LongGenerator(), dict);
            return dict;
        }

        private static void AddGenerator(IBaseGenerator gen, Dictionary<Type,IBaseGenerator> dict)
        {
            dict.Add(gen.GetGeneratedType(), gen);
        }

        public static Dictionary<Type,IGenericGenerator> CreateGenericTypesGenerator()
        {
            Dictionary<Type, IGenericGenerator> dict = new Dictionary<Type, IGenericGenerator>();
            AddGenerator(new ListGenerator<short>(), typeof(List<short>), dict);
            AddGenerator(new ListGenerator<int>(), typeof(List<int>), dict);
            AddGenerator(new ListGenerator<long>(), typeof(List<long>), dict);
            AddGenerator(new ListGenerator<string>(), typeof(List<string>), dict);
            AddGenerator(new ListGenerator<float>(), typeof(List<float>), dict);
            AddGenerator(new ListGenerator<double>(), typeof(List<double>), dict);
            AddGenerator(new ListGenerator<bool>(), typeof(List<bool>), dict);
            return dict;
        }

        private static void AddGenerator(IGenericGenerator gen, Type type,Dictionary<Type,IGenericGenerator> dict)
        {
            dict.Add(type, gen);
        }
    }
}
