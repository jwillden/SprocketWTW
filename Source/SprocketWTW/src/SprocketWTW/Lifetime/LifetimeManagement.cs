using System;
using System.Collections.Generic;
using SprocketWTW.Construction;

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
            // Ensure we don't already have a type. Don't build instances or dependency graphs for types already created.
            // Just short circuit the thing
        
            if (details.IsCreated)
                return _allManagers[details.Lifetime].CreateType(details);

            // Ensure the dependency graph for a given object is built
            if (details.Instructions == null)
            {
                DependencyGenerator buildDependencies = new DependencyGenerator();
                buildDependencies.BuildGraph(details);
            }

            return _allManagers[details.Lifetime].CreateType(details);
        }

        public void RegisterInstance(Type t, object instance)
        {
            ((SingletonLifetimeManager)(_allManagers)[LifetimeEnum.Singleton]).AddInstance(t, instance);
        }
    }
}
