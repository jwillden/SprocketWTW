using System;

namespace SprocketWTW.Lifetime
{
    public interface ILifetimeManager
    {
        object CreateType(Type createMe);
    }
}
