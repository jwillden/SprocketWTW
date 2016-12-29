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
                _createdTypes.TryAdd(details.RegisteredType, _constructor.Build(details));
            }
            return _createdTypes[details.RegisteredType];
        }
    }
}
