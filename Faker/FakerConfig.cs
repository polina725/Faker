using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker.ValueGenerator;

namespace Faker
{
    public class FakerConfig
    {
        private Dictionary<Type, Dictionary<PropertyInfo, IBaseGenerator>> customGenerators = new Dictionary<Type, Dictionary<PropertyInfo, IBaseGenerator>>();

        public void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
        {
            Expression body = expression.Body;
            if (body.NodeType != ExpressionType.MemberAccess)
                throw new FakerException("Invalid lambda expression");
            IBaseGenerator generator = (IBaseGenerator)Activator.CreateInstance(typeof(TGenerator));
            if (!generator.GetGeneratedType().Equals(typeof(TPropertyType)))
            {
                throw new FakerException("Illegal generator");
            }

            if (customGenerators.ContainsKey(typeof(TClass)))
            {
                Dictionary<PropertyInfo, IBaseGenerator> innerDict = customGenerators[typeof(TClass)];
                innerDict.Add((PropertyInfo)((MemberExpression)body).Member, generator);
            }
            else
            {
                Dictionary<PropertyInfo, IBaseGenerator> innerDict = new Dictionary<PropertyInfo, IBaseGenerator>();
                innerDict.Add((PropertyInfo)((MemberExpression)body).Member, generator);
                customGenerators.Add(typeof(TClass), innerDict);
            }
        }

        public bool ClassHasCustomGenerators(Type t)
        {
            return customGenerators.ContainsKey(t);
        }

        public bool FieldOrPropertyHasCustomGenerator(Type classType,PropertyInfo member,out IBaseGenerator gen)
        {
            Dictionary<PropertyInfo, IBaseGenerator> innerDict = customGenerators[classType];
            Console.WriteLine(member.GetType());
            foreach (KeyValuePair<PropertyInfo, IBaseGenerator> pair in innerDict)
                if (pair.Key.Name.Equals(member.Name) && pair.Key.PropertyType.Equals(member.PropertyType))
                {
                    gen = pair.Value;
                    return true;
                }
            gen = null;
            return false;                
        }
    }

    public struct fieldOrPropertyInfo
    {
        public string Name { get; }
        public Type MemberType { get; }
        public fieldOrPropertyInfo(string name,Type memberType)
        {
            Name = name;
            MemberType = memberType;
        }
    }
}
