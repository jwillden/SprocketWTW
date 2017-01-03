using System;

namespace SprocketWTW.Lifetime
{
    public interface ILifetimeManagement
    {
        object Resolve(RegistrationDetails details);

        void RegisterInstance(Type t, object instance);
    }
}
