using System;
using System.Collections.Generic;
using System.Reflection;

namespace SprocketWTW.Construction
{

    /* Contains all details needed to construct an object including
     * the Constructor to use
     * the concrete type to build
     * any dependencies
     */
    public class BuildDetails
    {

        public BuildDetails()
        {
            Dependencies = new List<BuildDetails>();
        }

        public Type TypeToCreate { get; set; }

        public ConstructorInfo ConstructorToUse { get; set; }

        public List<BuildDetails> Dependencies { get; set; }

        
    }
}
