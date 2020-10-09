using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Faker.ValueGenerator;
using Faker.ValueGenerator.SystemClassGenerator;

namespace Faker
{
    public class Faker
    {
        public List<IBaseGenerator> generators;
        private CircularReferencesResolver resolver;
        private FakerConfig conf;
        private Random rand;

        public Faker(int maxLevel)
        {
            rand = new Random();
            resolver = new CircularReferencesResolver(maxLevel);
            generators = GeneratorFactory.CraeteBaseTypesGenerators();
            generators.Add(new DateGenerator());           
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
                generators.Add(d);
            }
        }

        public void AddConfig(FakerConfig c)
        {
            conf = c; 
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        internal object Create(Type t)
        {
            if (GeneratorExists(t, out IBaseGenerator gen))
                return gen.Generate(new GeneratorContext(rand,t,this));
            else if (t.IsClass)
                return CreateClassObject(t);
            else if (t.IsValueType && !t.IsPrimitive)
                return CreateStruct(t);
            throw new ArgumentException("Can't create an object");
        }

        private bool GeneratorExists(Type t, out IBaseGenerator gen)
        {
            gen = null;
            foreach (IBaseGenerator generator in generators)
            {
                Type tmp = t;
                if (t.IsGenericType)
                    tmp = t.GetGenericTypeDefinition();
                if (generator.CanGenerate(tmp))
                {
                    gen = generator;
                    return true;
                }
            }
            return false;
        }


        private object CreateStruct(Type type)
        {
            ConstructorInfo currentConstractor = GetConstructorWithMaxNumberOfParameters(type);
            object obj = null;
            if(currentConstractor==null)
                obj = Activator.CreateInstance(type);
            else
            {
                object[] parameters = GenerateConstructorParameters(currentConstractor.GetParameters(), type);
                obj = currentConstractor.Invoke(parameters);
            }
            FillObject(obj);
            return obj;
        }

        private object CreateClassObject(Type t)
        {
            ConstructorInfo currentConstructor = GetConstructorWithMaxNumberOfParameters(t);
            object[] parameters = GenerateConstructorParameters(currentConstructor.GetParameters(), t);
            object obj = currentConstructor.Invoke(parameters);
            FillObject(obj);
            return obj;
        }

        private ConstructorInfo GetConstructorWithMaxNumberOfParameters(Type t)
        {
            ConstructorInfo[] constructors = t.GetConstructors();
            if (constructors.Length == 0)
                return null;
            ConstructorInfo currentConstructor = constructors[0];
            foreach (ConstructorInfo c in constructors)
                if (c.GetParameters().Length > currentConstructor.GetParameters().Length)
                    currentConstructor = c;
            return currentConstructor;
        }

        private object[] GenerateConstructorParameters(ParameterInfo[] parameters,Type t)
        {
            if (parameters.Length == 0)
                return null;
            List<object> generatedParameters = new List<object>();
            bool canUseCustomGenerators = conf != null && conf.ClassHasCustomGenerators(t);
            foreach (ParameterInfo param in parameters)
            {

                if (canUseCustomGenerators)
                {
                    if (conf.FieldOrPropertyHasCustomGenerator(t, AdditionalFunction.ProceedParameterName(param.Name), out IBaseGenerator g))
                    {
                        generatedParameters.Add(g.Generate(new GeneratorContext(rand, param.ParameterType, this)));
                        continue;
                    }
                }
                else
                {
                    try
                    {
                        generatedParameters.Add(Create(param.ParameterType));
                    }
                    catch (ArgumentException)
                    {
                        generatedParameters.Add(null);
                    }
                }
                
            }
            return generatedParameters.ToArray();
        }

        private void FillObject(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            bool canUseCustomGenerators = conf != null && conf.ClassHasCustomGenerators(obj.GetType());
            foreach (FieldInfo field in fields)
                if (!AdditionalFunction.IsFilled(field, obj))
                {
                    resolver.AddReference(field.FieldType);
                    if (resolver.CanCreateAnObject(field.FieldType))
                    {
                        if (canUseCustomGenerators && conf.FieldOrPropertyHasCustomGenerator(obj.GetType(), field.Name, out IBaseGenerator g))
                        {
                            field.SetValue(obj, g.Generate(new GeneratorContext(rand, field.FieldType, this)));
                            resolver.RemoveReference(field.FieldType);
                            continue;
                        }
                        else
                            try
                            {
                                field.SetValue(obj, Create(field.FieldType));
                            }
                            catch(ArgumentException)
                            {
                                field.SetValue(obj, null);
                            }
                    }
                    else
                        field.SetValue(obj, null);
                    resolver.RemoveReference(field.FieldType);

                }
            foreach (PropertyInfo property in properties)
                if (property.CanWrite && !AdditionalFunction.IsFilled(property,obj))
                {
                    resolver.AddReference(property.PropertyType);
                    if (canUseCustomGenerators && conf.FieldOrPropertyHasCustomGenerator(obj.GetType(), property.Name, out IBaseGenerator g))
                    {
                        property.SetValue(obj, g.Generate(new GeneratorContext(rand, property.PropertyType, this)));
                        resolver.RemoveReference(property.PropertyType);
                        continue;
                    }
                    else
                        try
                        {
                            property.SetValue(obj, Create(property.PropertyType));
                        }
                        catch(ArgumentException)
                        {
                            property.SetValue(obj, null);
                        }
                    resolver.RemoveReference(property.PropertyType);
                }
        }
    }
}
