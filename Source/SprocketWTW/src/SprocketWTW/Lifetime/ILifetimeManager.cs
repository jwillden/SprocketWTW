using System;

namespace SprocketWTW.Lifetime
{
    public interface ILifetimeManager
    {
        object Resolve(Type createMe);
    }
}
