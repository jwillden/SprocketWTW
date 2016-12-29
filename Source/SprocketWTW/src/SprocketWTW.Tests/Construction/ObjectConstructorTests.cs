using Newtonsoft.Json.Serialization;
using Moq;
using Xunit;
using SprocketWTW.Construction;

namespace SprocketWTW.Tests.Construction
{
    public class ObjectConstructorTests
    {
        [Fact]
        public void BuildParameterlessClass()
        {

            RegistrationDetails details = new RegistrationDetails();
            details.ResolvedType = typeof(SimpleClass);
            details.Instructions = new BuildDetails();

            var ctor = new ObjectConstructor();
            var obj = ctor.Build(details);
            Assert.NotNull(obj as SimpleClass);
        }

    }
}
