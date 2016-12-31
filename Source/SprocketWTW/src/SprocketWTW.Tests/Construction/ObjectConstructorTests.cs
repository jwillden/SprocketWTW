using System.Collections.Generic;
using System.Linq;
using Xunit;
using SprocketWTW.Construction;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests.Construction
{
    public class ObjectConstructorTests
    {
        [Fact]
        public void BuildParameterlessClass()
        {

            RegistrationDetails details = new RegistrationDetails
            {
                ResolvedType = typeof(SimpleClass),
                Instructions = new BuildDetails
                {
                    ConstructorToUse = ConstructorUtility.GetConstructors(typeof(SimpleClass)).First(),
                    TypeToCreate = typeof(SimpleClass)
                }
            };

            var ctor = new ObjectConstructor();
            var obj = ctor.Build(details.Instructions);
            Assert.NotNull(obj as SimpleClass);
        }

        [Fact]
        public void BuildClassWithRegisteredDependency()
        {

            var rootDetails = GetComplexClassDetails();
            ObjectConstructor ctor = new ObjectConstructor();
            var builtObject = ctor.Build(rootDetails) as ComplexClass;

            Assert.NotNull(builtObject);
            Assert.NotNull(builtObject.InjectedDetails);
        }

        [Fact]
        public void BuildClassWithMultipleParameterConstructor()
        {
            var complexDetails = GetComplexClassDetails();
            var simpleDetails = GetSimpleClassDetails();
            var rootDetails = new BuildDetails
            {
                ConstructorToUse = ConstructorUtility.GetConstructors(typeof(MultipleConstructorParamsClass)).First(),
                TypeToCreate = typeof(MultipleConstructorParamsClass),
                Dependencies = new List<BuildDetails>
                {
                    simpleDetails,
                    complexDetails
                }
            };

            ObjectConstructor ctor = new ObjectConstructor();
            var builtObject = ctor.Build(rootDetails) as MultipleConstructorParamsClass;
            Assert.NotNull(builtObject);
            Assert.NotNull(builtObject.Simple);
            Assert.NotNull(builtObject.Complex);
        }

        private BuildDetails GetSimpleClassDetails()
        {
            return new BuildDetails
            {
                ConstructorToUse = ConstructorUtility.GetConstructors(typeof(SimpleClass)).First(),
                TypeToCreate = typeof(SimpleClass)
            };
        }

        private BuildDetails GetComplexClassDetails()
        {
            return new BuildDetails()
            {
                ConstructorToUse = ConstructorUtility
                    .GetConstructors(typeof(ComplexClass))
                    .FirstOrDefault(t => t.GetParameters().Count() == 1),
                TypeToCreate = typeof(ComplexClass),
                Dependencies = new List<BuildDetails> { GetSimpleClassDetails() }
            };
        }
    }
}
