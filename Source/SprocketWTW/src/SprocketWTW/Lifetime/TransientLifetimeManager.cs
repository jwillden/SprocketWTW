using System;
using SprocketWTW.Construction;

namespace SprocketWTW.Lifetime
{
    public class TransientLifetimeManager : ILifetimeManager
    {

        private IObjectConstructor _constructor;

        public TransientLifetimeManager()
        {
            _constructor = new ObjectConstructor();
        }

        public TransientLifetimeManager(IObjectConstructor constructor)
        {
            _constructor = constructor;
        }

        public object CreateType(RegistrationDetails details)
        {
            var obj = _constructor.Build(details);
            return obj;
        }
    }
}