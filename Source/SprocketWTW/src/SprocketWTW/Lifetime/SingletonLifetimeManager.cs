using System;
using System.Collections.Concurrent;
using SprocketWTW.Construction;

namespace SprocketWTW.Lifetime
{
    public class SingletonLifetimeManager : ILifetimeManager
    {
        private readonly ConcurrentDictionary<Type, object> _createdTypes = new ConcurrentDictionary<Type, object>();
        private readonly IObjectConstructor _constructor;

        public SingletonLifetimeManager()
        {
            _constructor = new ObjectConstructor();
        }

        public SingletonLifetimeManager(IObjectConstructor constructor)
        {
            _constructor = constructor;
        }

        public object CreateType(RegistrationDetails details)
        {
            if (!_createdTypes.ContainsKey(details.RegisteredType))
            {
                _createdTypes.TryAdd(details.RegisteredType, _constructor.Build(details.Instructions));
            }
            return _createdTypes[details.RegisteredType];
        }

        public void AddInstance(Type t, object instance)
        {
            _createdTypes.TryAdd(t, instance);
        }

        public bool HasInstance(Type t)
        {
            return _createdTypes.ContainsKey(t);
        }
    }
}
