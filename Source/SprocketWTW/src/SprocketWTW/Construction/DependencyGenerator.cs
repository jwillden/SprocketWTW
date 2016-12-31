using System;
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
            // Get the constructors. They are already ordered by parameter count descending.
            // Work from most descriptive to least to find one that is resolveable.
            var constructors = ConstructorUtility.GetConstructors(details.ResolvedType);

            foreach (var info in constructors)
            {
                ParameterInfo[] parms = info.GetParameters();

                // If all parameters are registered or it's the default constructor, 
                // use this constructor.
                if (parms.All(p => _cache.Contains(p.ParameterType)) || parms.Length == 0)
                {
                    details.Instructions = new BuildDetails
                    {
                        ConstructorToUse = info,
                        TypeToCreate = details.ResolvedType
                    };

                    // ensure all subdependencies have their trees built as well.
                    foreach (var p in parms)
                    {
                        RegistrationDetails subDetails = _cache.Get(p.ParameterType);
                        if (subDetails.Instructions == null)
                            BuildGraph(subDetails);
                    }
                }
            }

            // If no constructor was found, the object can't be constructed so throw an exception.
            if (details.Instructions == null)
                throw new InvalidOperationException($"Cannot find an appropriate constructor for type {details.RegisteredType.FullName}. Either create a default constructor or register more types.");
        }
    }
}
