using System;

namespace SprocketWTW
{
    public class RegistrationDetails
    {
        public Type RegisteredType { get; set; }

        public Type ResolvedType { get; set; }

        public Lifetime.LifetimeEnum LifetimeEnum { get; set; }
    }
}
