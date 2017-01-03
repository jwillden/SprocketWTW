using System;
using SprocketWTW.Construction;

namespace SprocketWTW
{
    public class RegistrationDetails
    {
        public Type RegisteredType { get; set; }

        public Type ResolvedType { get; set; }

        public Lifetime.LifetimeEnum Lifetime { get; set; }

        public BuildDetails Instructions { get; set; }

        public bool IsCreated { get; set; }

        public Func<SprocketWTWContainer, object> ImplementationFactory { get; set; }

    }
}
