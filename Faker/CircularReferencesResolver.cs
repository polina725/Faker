using System;
using System.Collections.Generic;

namespace Faker
{
    class CircularReferencesResolver
    {
        private Dictionary<Type, int> circularReferences = new Dictionary<Type, int>();
        private int maxRecursionLevel;

        
        public CircularReferencesResolver(int maxLevel)
        {
            maxRecursionLevel = maxLevel;
        }

        public void AddReference(Type t)
        {
            if (!circularReferences.ContainsKey(t))
                circularReferences.Add(t, 0);
            else
                circularReferences[t] += 1;
        }

        public void RemoveReference(Type t)
        {
            if (circularReferences.ContainsKey(t))
                circularReferences[t] -= 1;
            if (circularReferences[t] == 0)
                circularReferences.Remove(t);
        }

        public bool CanCreateAnObject(Type t)
        {
            return !circularReferences.ContainsKey(t) && circularReferences[t] < maxRecursionLevel;
        }
    }
}
