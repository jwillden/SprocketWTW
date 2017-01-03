using System;
using System.Reflection;
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

        public SprocketWTWContainer Register<I, T>()
        {
            return Register<I, T>(LifetimeEnum.Transient);
        }

        public SprocketWTWContainer Register<I, T>(LifetimeEnum lifetime)
        {
            return Register(typeof(I), typeof(T), lifetime);
        }

        public SprocketWTWContainer Register(Type typeToRegister, Type typeToBuild, LifetimeEnum lifetime)
        {
            var details = new RegistrationDetails
            {
                RegisteredType = typeToRegister,
                ResolvedType = typeToBuild,
                Lifetime = lifetime
            };
            _registrationCache.RegisterType(details);
            return this;
        }

        public SprocketWTWContainer Register<T>(object instance)
        {
            return Register(typeof(T), instance);
        }

        public SprocketWTWContainer Register(Type typeToRegister, object instance)
        {
            var details = new RegistrationDetails
            {
                RegisteredType = typeToRegister,
                Lifetime = LifetimeEnum.Singleton,
                IsCreated = true
            };

            _registrationCache.RegisterType(details);
            _management.RegisterInstance(typeToRegister, instance);

            return this;
        }

        public T Resolve<T>() where T: class
        {
            return Resolve(typeof(T)) as T;
        }

        public object Resolve(Type resolveMe)
        {
            Type ultimateType = resolveMe;

            if (!_registrationCache.Contains(resolveMe))
            {
                throw new InvalidOperationException($"Type {ultimateType.FullName} has not been registered. Please register this type before attempting to Resolve it.");
            }

            return _management.Resolve(_registrationCache.Get(ultimateType));
        }
    }
}
