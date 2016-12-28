using System;

namespace SprocketWTW.Construction
{
    public static class ObjectConstructor
    {
        public static object Build(Type buildType)
        {
            return Activator.CreateInstance(buildType);
        }
    }
}
