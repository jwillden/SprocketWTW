using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SprocketWTW.Construction
{
    public class DependencyGenerator
    {
        private readonly IRegistrationCache _cache;

        public DependencyGenerator()
        {
            _cache = new StaticRegistrationCollection();
        }

        public DependencyGenerator(IRegistrationCache cache)
        {
            _cache = cache;
        }

        public void BuildGraph(RegistrationDetails details)
        {

            if (details.IsCreated)
                return;

            // Get the constructors. They are already ordered by parameter count descending.
            // Work from most descriptive to least to find one that is resolveable.
            var constructors = details.ResolvedType.GetConstructors();

            foreach (var info in constructors)
            {
                ParameterInfo[] parms = info.GetParameters();

                // If all parameters are registered or it's the default constructor, 
                // use this constructor.
                var tempNonGenericCache = new List<Type>();
                foreach (var parm in parms)
                {
                    if (parm.ParameterType.GetTypeInfo().IsGenericType)
                        tempNonGenericCache.Add(parm.ParameterType.GetGenericTypeDefinition());
                    else
                    {
                        tempNonGenericCache.Add(parm.ParameterType);
                    }
                }

                // If it's an IEnumerable<T>, we need to do something special I think, but I'm not really sure what.

                if (tempNonGenericCache.All(p => _cache.Contains(p)) || parms.Length == 0)
                {
                    details.Instructions = new BuildDetails
                    {
                        ConstructorToUse = info,
                        TypeToCreate = details.ResolvedType
                    };

                    // ensure all subdependencies have their trees built as well.
                    foreach (var p in tempNonGenericCache)
                    {
                        RegistrationDetails subDetails = _cache.Get(p);
                        if (subDetails.Instructions == null)
                            BuildGraph(subDetails);
                        details.Instructions.Dependencies.Add(subDetails.Instructions);
                    }
                }
            }

            // If no constructor was found, the object can't be constructed so throw an exception.
            if (details.Instructions == null)
                throw new InvalidOperationException($"Cannot find an appropriate constructor for type {details.RegisteredType.FullName}. Either create a default constructor or register more types.");
        }
    }
}
