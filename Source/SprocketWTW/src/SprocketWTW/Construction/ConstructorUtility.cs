using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SprocketWTW.Construction
{
    public class ConstructorUtility
    {
        public static IEnumerable<ConstructorInfo> GetConstructors(Type t)
        {
            return t.GetTypeInfo().DeclaredConstructors.Where(n => !n.IsStatic && n.IsPublic).OrderByDescending(c => c.GetParameters().Count());
        }
    }
}
