using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SprocketWTW.Construction
{
    public class ObjectConstructor : IObjectConstructor
    {
        IRegistrationCache _cache;
        public ObjectConstructor()
        {
            _cache = new StaticRegistrationCollection();
        }

        public ObjectConstructor(IRegistrationCache cache)
        {
            _cache = cache;
        }

        public object Build(RegistrationDetails rootDetails)
        {
            var localConstructionGraph = new Dictionary<Type, object>();

            foreach (var detail in rootDetails.Instructions.Dependencies)
            {
                ParameterInfo[] parameters = detail.ConstructorToUse.GetParameters();
                foreach (var parm in parameters)
                {
                    // Add them empty, we need to get to the bottom of the graph and then come back up.
                    localConstructionGraph.Add(parm.ParameterType, Build(_cache.Get(parm.ParameterType)));
                }
            }

            if (localConstructionGraph.Count == 0)
            {
                return Activator.CreateInstance(rootDetails.ResolvedType);
            }
            else
            {
                return rootDetails.CtorInfo.Invoke(localConstructionGraph.Select(t => t.Value).ToArray());
            }
        }
    }
}
