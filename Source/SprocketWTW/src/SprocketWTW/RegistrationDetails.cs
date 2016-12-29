using System;
using System.Reflection;
using SprocketWTW.Construction;

namespace SprocketWTW
{
    public class RegistrationDetails
    {
        public Type RegisteredType { get; set; }

        public Type ResolvedType { get; set; }

        public Lifetime.LifetimeEnum Lifetime { get; set; }

        public BuildDetails Instructions { get; set; }

        public ConstructorInfo CtorInfo { get; set; }
    }
}
