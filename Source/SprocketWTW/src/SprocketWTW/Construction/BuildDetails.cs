using System;
using System.Collections.Generic;
using System.Reflection;
using SprocketWTW.Lifetime;

namespace SprocketWTW.Construction
{
    public class BuildDetails
    {

        public BuildDetails()
        {
            Dependencies = new List<BuildDetails>();
        }

        public Type TypeToCreate { get; set; }

        public LifetimeEnum Lifetime { get; set; }

        public ConstructorInfo ConstructorToUse { get; set; }

        public IEnumerable<BuildDetails> Dependencies { get; set; }
    }
}
