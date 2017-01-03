using System.Linq;

using Xunit;
using SprocketWTW.Construction;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests.Construction
{
    public class ConstructorUtilityTests
    {
        [Fact]
        public void GetPublicNonStaticConstructors()
        {
            var results = UtilityExtensions.GetConstructors(typeof(ComplexClass));
            Assert.Equal(1, results.Count());
        }
    }
}
