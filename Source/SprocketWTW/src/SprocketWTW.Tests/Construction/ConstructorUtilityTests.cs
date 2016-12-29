using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using SprocketWTW.Construction;

namespace SprocketWTW.Tests.Construction
{
    public class ConstructorUtilityTests
    {
        [Fact]
        public void GetPublicNonStaticConstructors()
        {
            var results = ConstructorUtility.GetConstructors(typeof(ComplexClass));
            Assert.Equal(1, results.Count());
        }
    }
}
