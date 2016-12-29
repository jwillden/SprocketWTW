using System;
using System.Collections.Concurrent;

namespace SprocketWTW
{
    // A singleton style class to allow for a single place to get objects
    public class StaticRegistrationCollection : IRegistrationCache
    {
        private static readonly ConcurrentDictionary<Type, RegistrationDetails> TypeRegistrations
            = new ConcurrentDictionary<Type, RegistrationDetails>();

        public void RegisterType(RegistrationDetails details)
        {
            TypeRegistrations.TryAdd(details.RegisteredType, details);
        }

        public bool Contains(Type T)
        {
            return TypeRegistrations.ContainsKey(T);
        }

        public RegistrationDetails Get(Type T)
        {
            Console.WriteLine("I'm inside the get method");
            return TypeRegistrations[T];
        }
    }
}
