using System;
using System.Collections.Generic;

namespace Faker.ValueGenerators
{
    public class GeneratorFactory
    {
        public static Dictionary<Type,IBaseGenerator> CraeteBaseTypesGenerators()
        {
            Dictionary<Type, IBaseGenerator> dict = new Dictionary<Type, IBaseGenerator>();
            Add(new ShortGenerator(), dict);
            return dict;
        }

        private static void Add(IBaseGenerator gen, Dictionary<Type,IBaseGenerator> dict)
        {
            dict.Add(gen.GetGeneratedType(), gen);
        }

        public static Dictionary<Type,IGenericGenerator> CreateGenericTypesGenerator()
        {
            Dictionary<Type, IGenericGenerator> dict = new Dictionary<Type, IGenericGenerator>();
            
            return dict;
        }

        private static void Add(IBaseGenerator gen, Dictionary<Type,IGenericGenerator> dict)
        {
            
        }
    }
}
