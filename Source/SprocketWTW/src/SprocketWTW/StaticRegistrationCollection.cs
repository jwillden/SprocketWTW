using System;
using System.Collections.Concurrent;

namespace SprocketWTW
{
    // A singleton style class to allow for a single place to get objects
    public class StaticRegistrationCollection : IRegistrationCache
    {
        private static readonly Lazy<ConcurrentDictionary<Type, RegistrationDetails>> TypeRegistrations
            = new Lazy<ConcurrentDictionary<Type, RegistrationDetails>>();

        public void RegisterType(RegistrationDetails details)
        {
            TypeRegistrations.Value.TryAdd(details.RegisteredType, details);
        }

        public bool Contains(Type T)
        {
            bool c = TypeRegistrations.Value.ContainsKey(T);
            return c;
        }

        public RegistrationDetails Get(Type T)
        {
            return TypeRegistrations.Value[T];
        }

       
    }
}
