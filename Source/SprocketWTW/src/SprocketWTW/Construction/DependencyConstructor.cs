using System;
using System.Linq;
using System.Reflection;

namespace SprocketWTW.Construction
{
    public class DependencyConstructor
    {
        public static void BuildGraph(Type T)
        {
            // Get cache info of the type to build
            var cache = SprocketWTWContainer.RegistrationCache;
            var details = cache.Get(T);

            // Get the constructors and order them by most detailed to less detailed. As soon as we find
            // one we can resolve all of the parameters for, use that constructor.
            var constructors = ConstructorUtility.GetConstructors(T).OrderByDescending(o => o.GetParameters().Length);

            foreach (var info in constructors)
            {
                bool foundConstructor = true;                
                ParameterInfo[] parms = info.GetParameters();
                foreach (var p in parms)
                {
                    // If a parameter type is not resolveable, this constructor cannot be used. Stop processing
                    // the loop and keep looking
                    if (!cache.Contains(p.ParameterType))
                    {
                        foundConstructor = false;
                        break;
                    }
                    else // make sure all dependencies have CtorInfo initialized
                    {
                        // Even though we may not end up using this type, build up its constructor info anyway
                        // because we'll need it later.
                        if (cache.Get(p.ParameterType).CtorInfo == null)
                            BuildGraph(p.ParameterType);
                    }
                }

                // If a constructor was found to be used, assign it and exit the loop.
                if (foundConstructor)
                { 
                    details.CtorInfo = info;
                    break;
                }
            }

            // If no constructor was found, the object can't be constructed so throw an exception.
            if (details.CtorInfo == null)
                throw new InvalidOperationException($"Cannot find an appropriate constructor for type {T.FullName}. Either create a default constructor or register more types.");
        }
    }
}
