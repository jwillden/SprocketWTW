using System.Collections.Generic;

namespace SprocketWTW.Lifetime
{
    public class LifetimeManagement : ILifetimeManagement
    {
        private readonly Dictionary<LifetimeEnum, ILifetimeManager> _allManagers;
        public LifetimeManagement()
        {
            _allManagers = new Dictionary<LifetimeEnum, ILifetimeManager>
            {
                {LifetimeEnum.Transient, new TransientLifetimeManager()},
                {LifetimeEnum.Singleton, new SingletonLifetimeManager()}
            };
        }

        public object Resolve(RegistrationDetails details)
        {
            return _allManagers[details.LifetimeEnum].CreateType(details.ResolvedType);
        }
    }
}
