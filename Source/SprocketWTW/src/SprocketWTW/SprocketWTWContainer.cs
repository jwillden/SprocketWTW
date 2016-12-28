using SprocketWTW.Lifetime;

namespace SprocketWTW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SprocketWTWContainer
    {
        private readonly Dictionary<Type, RegistrationDetails> _typeRegistrations
            = new Dictionary<Type, RegistrationDetails>();

        private readonly ILifetimeManagement _management;
        

        public SprocketWTWContainer()
        {
            _management = new LifetimeManagement();
        }

        public SprocketWTWContainer(ILifetimeManagement management)
        {
            _management = management;
        }

        public void Register<I, T>()
        {
            Register<I, T>(LifetimeEnum.Transient);
        }

        public void Register<I, T>(LifetimeEnum lifetime)
        {
            // If the type has already been registered, freak out.
            if (_typeRegistrations.ContainsKey(typeof(I)))
            {
                throw new InvalidOperationException($"Type {typeof(I).FullName} has already been registered and may not be registered again.");
            }

            var details = new RegistrationDetails
            {
                RegisteredType = typeof(I),
                ResolvedType = typeof(T),
                LifetimeEnum = lifetime
            };
            _typeRegistrations.Add(typeof(I), details);
        }

        public T Resolve<T>() where T: class
        {
            if (!_typeRegistrations.Keys.Contains(typeof(T)))
            {
                throw new InvalidOperationException($"Type {typeof(T).FullName} has not been registered. Please register this type before attempting to Resolve it.");
            }

            return (T)_management.Resolve(_typeRegistrations[typeof(T)]);
        }
    }
}
