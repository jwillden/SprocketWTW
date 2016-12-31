using System;
using System.Linq;
using Xunit;
using Moq;
using SprocketWTW.Construction;
using SprocketWTW.Lifetime;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests.Construction
{
    public class DependencyGeneratorTests
    {
        [Fact]
        public void GenerateGraphWithNoDependencies()
        {
            Mock <IRegistrationCache> moqCache = new Mock<IRegistrationCache>();

            var testDetails = GetSimpleRegDetails();
            DependencyGenerator ctor = new DependencyGenerator(moqCache.Object);
            ctor.BuildGraph(testDetails);

            Assert.NotNull(testDetails.Instructions.ConstructorToUse);
        }

        [Fact]
        public void GenerateGraphWithNoUseableConstructor()
        {
            Mock<IRegistrationCache> moqCache = new Mock<IRegistrationCache>();
            var testDetails = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                ResolvedType = typeof(UnresolveableConstructorClass),
                Lifetime = LifetimeEnum.Transient
            };

            DependencyGenerator ctor = new DependencyGenerator(moqCache.Object);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => { ctor.BuildGraph(testDetails); });
            Assert.NotNull(ex);
        }

        [Fact]
        public void GenerateGraphWithRegisteredDependentConstructor()
        {
            Mock<IRegistrationCache> moqCache = new Mock<IRegistrationCache>();
            moqCache.Setup(t => t.Contains(typeof(ISimpleInterface))).Returns(true);
            moqCache.Setup(t => t.Get(typeof(ISimpleInterface))).Returns(GetSimpleRegDetails());

            DependencyGenerator ctor = new DependencyGenerator(moqCache.Object);

            var complexDetails = GetComplexDetails();

            ctor.BuildGraph(complexDetails);

            Assert.NotNull(complexDetails.Instructions.ConstructorToUse);
            Assert.Equal(1, complexDetails.Instructions.ConstructorToUse.GetParameters().Length);
            Assert.NotNull(complexDetails.Instructions);
        }

        [Fact]
        public void GenerateGraphComplexClassWithDefaultConstructorWhenSimpleClassNotRegistered()
        {
            Mock<IRegistrationCache> moqCache = new Mock<IRegistrationCache>();
            moqCache.Setup(t => t.Contains(typeof(ISimpleInterface))).Returns(false);

            RegistrationDetails details = new RegistrationDetails
            {
                Lifetime = LifetimeEnum.Transient,
                RegisteredType = typeof(IComplexInterface),
                ResolvedType = typeof(ComplexClassWithDefaultConstructor)
            };

            DependencyGenerator generator = new DependencyGenerator(moqCache.Object);
            generator.BuildGraph(details);

            Assert.NotNull(details.Instructions);
            Assert.Equal(0, details.Instructions.ConstructorToUse.GetParameters().Length);
            Assert.Equal(0, details.Instructions.Dependencies.Count());
        }

        private RegistrationDetails GetSimpleRegDetails()
        {
            return new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                ResolvedType = typeof(SimpleClass),
                Lifetime = LifetimeEnum.Transient
            };
        }

        private RegistrationDetails GetComplexDetails()
        {
            return new RegistrationDetails
            {
                RegisteredType = typeof(IComplexInterface),
                ResolvedType = typeof(ComplexClass),
                Lifetime = LifetimeEnum.Transient
            };
        }
    }
}
