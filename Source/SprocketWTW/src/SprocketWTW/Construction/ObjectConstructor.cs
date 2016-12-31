using System;
using System.Collections.Generic;
using System.Linq;

namespace SprocketWTW.Construction
{
    public class ObjectConstructor : IObjectConstructor
    {
        public object Build(BuildDetails rootDetails)
        {
            var localConstructionGraph = new Dictionary<Type, object>();

            foreach (var subDetail in rootDetails.Dependencies)
            {
                {
                    // Add them empty, get to the bottom of the graph and then 
                    // build objects as it comes back up.
                    localConstructionGraph.Add(subDetail.TypeToCreate, Build(subDetail));
                }
            }

            if (localConstructionGraph.Count == 0)
            {
                return Activator.CreateInstance(rootDetails.TypeToCreate);
            }
            else
            {
                return rootDetails.ConstructorToUse.Invoke(localConstructionGraph.Select(t => t.Value).ToArray());
            }
        }
    }
}
