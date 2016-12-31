using Xunit;
using Moq;
using SprocketWTW.Lifetime;
using SprocketWTW.Construction;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests.Lifetime
{
    public class SingletonLifetimeManagerTests
    {

        [Fact]
        public void ResolveSameObjectTwiceYieldsSameObject()
        {

            var details1 = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                Instructions = new BuildDetails()
            };

            var details2 = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                Instructions = new BuildDetails()
            };

            var returnClass = new SimpleClass();

            var moqBuilder = new Mock<IObjectConstructor>();
            moqBuilder.Setup(t => t.Build(details1.Instructions)).Returns(returnClass);
            moqBuilder.Setup(t => t.Build(details2.Instructions)).Returns(returnClass);


            ILifetimeManager manager = new SingletonLifetimeManager(moqBuilder.Object);
            var obj1 = manager.CreateType(details1);
            var obj2 = manager.CreateType(details2);

            Assert.Equal(obj1, obj2);
        }

        [Fact]
        public void ResolveDifferentObjectYieldsDifferentObjects()
        {
            var details1 = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                Instructions = new BuildDetails()
            };

            var details2 = new RegistrationDetails
            {
                RegisteredType = typeof(ILifetimeManager),
                Instructions = new BuildDetails()
            };

            var moqBuilder = new Mock<IObjectConstructor>();
            moqBuilder.Setup(t => t.Build(details1.Instructions)).Returns(new SimpleClass());
            moqBuilder.Setup(t => t.Build(details2.Instructions)).Returns(new TransientLifetimeManager());

            ILifetimeManager manager = new SingletonLifetimeManager(moqBuilder.Object);
            var obj1 = manager.CreateType(details1);
            var obj2 = manager.CreateType(details2);

            Assert.NotEqual(obj1, obj2);
        }
    }
}
