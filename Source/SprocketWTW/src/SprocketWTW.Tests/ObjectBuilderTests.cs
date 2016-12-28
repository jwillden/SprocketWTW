using Newtonsoft.Json.Serialization;
using Xunit;
using SprocketWTW.Construction;

namespace SprocketWTW.Tests
{
    public class ObjectBuilderTests
    {
        [Fact]
        public void CanBuildSimpleClass()
        {
            var obj = ObjectConstructor.Build(typeof(SimpleClass));
            Assert.NotNull(obj as SimpleClass);
        }
    }
}
