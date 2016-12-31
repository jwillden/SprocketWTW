using System;
using SprocketWTW.Lifetime;

namespace SprocketWTW
{
    public class SprocketWTWContainer
    {
        private readonly ILifetimeManagement _management;
        private readonly IRegistrationCache _registrationCache;

        public SprocketWTWContainer()
        {
            _management = new LifetimeManagement();
            _registrationCache = new StaticRegistrationCollection();
        }

        public SprocketWTWContainer(ILifetimeManagement management)
        {
            _management = management;
            _registrationCache = new StaticRegistrationCollection();
        }

        public SprocketWTWContainer(IRegistrationCache cache)
        {
            _management = new LifetimeManagement();
            _registrationCache = cache;
        }

        public SprocketWTWContainer(ILifetimeManagement management, IRegistrationCache cache)
        {
            _management = management;
            _registrationCache = cache;
        }

        public void Register<I, T>()
        {
            Register<I, T>(LifetimeEnum.Transient);
        }

        public void Register<I, T>(LifetimeEnum lifetime)
        {
            // If the type has already been registered, freak out.
            if (_registrationCache.Contains(typeof(I)))
            {
                throw new InvalidOperationException($"Type {typeof(I).FullName} has already been registered and cannot be registered again.");
            }

            var details = new RegistrationDetails
            {
                RegisteredType = typeof(I),
                ResolvedType = typeof(T),
                Lifetime = lifetime
            };
            _registrationCache.RegisterType(details);
        }

        public T Resolve<T>() where T: class
        {
            if (!_registrationCache.Contains(typeof(T)))
            {
                throw new InvalidOperationException($"Type {typeof(T).FullName} has not been registered. Please register this type before attempting to Resolve it.");
            }

            return (T)_management.Resolve(_registrationCache.Get(typeof(T)));
        }
    }
}
