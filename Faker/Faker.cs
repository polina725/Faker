using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Faker.ValueGenerator;

namespace Faker
{
    public class Faker
    {
        private Dictionary<Type, IBaseGenerator> baseGenerators;
        private Dictionary<Type, IGenericGenerator> genericGenerators;

        public Faker()
        {
            baseGenerators = GeneratorFactory.CraeteBaseTypesGenerators();
            genericGenerators = GeneratorFactory.CreateGenericTypesGenerator();
            LoadPlugins();
        }

        public Faker(bool onlyBaseTypes)
        {
            baseGenerators = GeneratorFactory.CraeteBaseTypesGenerators();
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            string pluginPath = "D:\\3course\\5sem\\SPP\\Faker\\Plugins";
            List<Assembly> dlls = new List<Assembly>();
            string[] f = Directory.GetFiles(pluginPath, "*.dll");
            foreach (string s in f)
                dlls.Add(Assembly.LoadFrom(s));
            foreach (Assembly a in dlls)
            {
                IBaseGenerator d = (IBaseGenerator) a.CreateInstance(a.GetTypes()[0].ToString());
                baseGenerators.Add(d.GetGeneratedType(), d);
            }
        }


        public T Create<T>() // публичный метод для пользователя
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) // метод для внутреннего использования
        {
            if (baseGenerators.TryGetValue(t, out IBaseGenerator gen))
                return gen.Generate();
            else if (t.IsGenericType && genericGenerators.TryGetValue(t,out IGenericGenerator gener))
                return gener.Generate();
            return null;
        }
    }
}
