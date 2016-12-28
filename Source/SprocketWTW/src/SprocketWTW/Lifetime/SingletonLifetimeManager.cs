using System;
using System.Collections.Concurrent;
using SprocketWTW.Construction;

namespace SprocketWTW.Lifetime
{
    public class SingletonLifetimeManager : ILifetimeManager
    {
        private readonly ConcurrentDictionary<Type, object> _createdTypes;

        public SingletonLifetimeManager()
        {
            _createdTypes = new ConcurrentDictionary<Type, object>();
        }

        public object CreateType(Type createMe)
        {
            if (!_createdTypes.ContainsKey(createMe))
            {
                _createdTypes.TryAdd(createMe, ObjectConstructor.Build(createMe));
            }
            return _createdTypes[createMe];
        }
    }
}
