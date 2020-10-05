﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Faker.ValueGenerator;
using Faker.ValueGenerator.SystemClassGenerator;

namespace Faker
{
    public class Faker
    {
        private Dictionary<Type, IBaseGenerator> baseGenerators;
        private Dictionary<Type, IGenericGenerator> genericGenerators;
        private CircularReferencesResolver resolver;

        public Faker(int maxLevel)
        {
            resolver = new CircularReferencesResolver(maxLevel);
            baseGenerators = GeneratorFactory.CraeteBaseTypesGenerators();
            baseGenerators.Add(typeof(DateTime), new DateGenerator());
            genericGenerators = GeneratorFactory.CreateGenericTypesGenerator();
            LoadPlugins();
        }

        public Faker(bool onlyBaseTypes)
        {
            baseGenerators = GeneratorFactory.CraeteBaseTypesGenerators();
            baseGenerators.Add(typeof(DateTime), new DateGenerator());
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


        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            if (baseGenerators.TryGetValue(t, out IBaseGenerator gen))
                return gen.Generate();
            else if (t.IsGenericType && genericGenerators.TryGetValue(t, out IGenericGenerator gener))
                return gener.Generate();
            else if (t.IsClass)
                return CreateClassObject(t);
            else if (t.IsValueType && !t.IsPrimitive)
               return CreateStruct(t);
            throw new FakerException("Can't create an object");
        }

        private object CreateStruct(Type type)
        {
            ConstructorInfo currentConstractor = GetConstructorWithMaxNumberOfParameters(type);
            object obj = null;
            if(currentConstractor==null)
                obj = Activator.CreateInstance(type);
            else
            {
                object[] parameters = GenerateConstructorParameters(currentConstractor.GetParameters());
                obj = currentConstractor.Invoke(parameters);
            }
            FillObject(obj);
            return obj;
        }

        private object CreateClassObject(Type t)
        {
            ConstructorInfo currentConstructor = GetConstructorWithMaxNumberOfParameters(t);
            object[] parameters = GenerateConstructorParameters(currentConstructor.GetParameters());
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

        private object[] GenerateConstructorParameters(ParameterInfo[] parameters)
        {
            if (parameters.Length == 0)
                return null;
            List<object> generatedParameters = new List<object>();
            foreach(ParameterInfo param in parameters)
            {
                bool parameterGenerated = false;
                if (baseGenerators.TryGetValue(param.ParameterType, out IBaseGenerator gen))
                {
                    generatedParameters.Add(gen.Generate());
                    parameterGenerated = true;
                }
                else if (genericGenerators.TryGetValue(param.ParameterType, out IGenericGenerator genr))
                {
                    generatedParameters.Add(genr.Generate());
                    parameterGenerated = true;
                }
                if (!parameterGenerated)
                    generatedParameters.Add(null);
            }
            return generatedParameters.ToArray();
        }

        private void FillObject(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            foreach (FieldInfo field in fields)
                if (field.GetValue(obj) == null)
                {
                    resolver.AddReference(field.FieldType);
                    if (resolver.CanCreateAnObject(field.FieldType))
                        field.SetValue(obj, Create(field.FieldType));
                    else
                        field.SetValue(obj, null);
                    resolver.RemoveReference(field.FieldType);
                }
            foreach (PropertyInfo property in properties)
                if (property.CanWrite)
                {
                    if (property.CanRead && property.GetValue(obj) != null)
                        continue;
                    resolver.AddReference(property.PropertyType);
                    if (resolver.CanCreateAnObject(property.PropertyType))
                        property.SetValue(obj, Create(property.PropertyType));
                    else
                        property.SetValue(obj, null);
                    resolver.RemoveReference(property.PropertyType);
                }
        }
    }
}
