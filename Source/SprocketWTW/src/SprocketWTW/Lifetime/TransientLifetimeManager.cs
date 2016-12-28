using System;
using SprocketWTW.Construction;

namespace SprocketWTW.Lifetime
{
    public class TransientLifetimeManager : ILifetimeManager
    {
        public object Resolve(Type createMe)
        {
            var obj = ObjectConstructor.Build(createMe);
            return obj;
        }
    }
}