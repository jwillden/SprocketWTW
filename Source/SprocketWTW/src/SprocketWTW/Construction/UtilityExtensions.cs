using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SprocketWTW.Construction
{
    public static class UtilityExtensions
    {
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type t)
        {
            return t.GetTypeInfo().DeclaredConstructors.Where(x => !x.IsStatic && x.IsPublic)
                .OrderByDescending(c => c.GetParameters().Count());
        }

        
    }
}
