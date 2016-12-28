using System.Collections.Generic;

namespace SprocketWTW.Lifetime
{
    public class LifetimeManagement
    {
        private readonly Dictionary<LifeTime, ILifetimeManager> _allManagers;
        public LifetimeManagement()
        {
            _allManagers = new Dictionary<LifeTime, ILifetimeManager>
            {
                {LifeTime.Transient, new TransientLifetimeManager()},
                {LifeTime.Singleton, new SingletonLifetimeManager()}
            };
        }

        public object Resolve(RegistrationDetails details)
        {
            return _allManagers[details.LifeTime].Resolve(details.ResolvedType);
        }
    }
}
