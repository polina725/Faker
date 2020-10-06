using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Faker.ValueGenerator;

namespace Faker
{
    public class FakerConfig
    {
        private Dictionary<Type, Dictionary<FieldInfo, IBaseGenerator>> customGenerators = new Dictionary<Type, Dictionary<FieldInfo, IBaseGenerator>>();

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
                Dictionary<FieldInfo, IBaseGenerator> innerDict = customGenerators[typeof(TClass)];
                innerDict.Add((FieldInfo)((MemberExpression)body).Member, generator);
            }
            else
            {
                Dictionary<FieldInfo, IBaseGenerator> innerDict = new Dictionary<FieldInfo, IBaseGenerator>();
                innerDict.Add((FieldInfo)((MemberExpression)body).Member, generator);
                customGenerators.Add(typeof(TClass), innerDict);
            }
        }

        public bool ClassHasCustomGenerators(Type t)
        {
            return customGenerators.ContainsKey(t);
        }

        public IBaseGenerator FindFieldOrPropertyCustomGenerator(Type t,MemberInfo member)
        {
            Dictionary<FieldInfo, IBaseGenerator> innerDict = customGenerators[t];
            foreach (KeyValuePair<FieldInfo, IBaseGenerator> pair in innerDict)
                if (pair.Key.Name.Equals(member.Name) && pair.Key.FieldType.Equals(member.MemberType))
                    return (pair.Value);
            return null;   
        }
    }
}
