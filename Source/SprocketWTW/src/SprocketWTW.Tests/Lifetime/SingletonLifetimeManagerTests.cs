using Xunit;
using Moq;
using SprocketWTW.Lifetime;
using SprocketWTW.Construction;

namespace SprocketWTW.Tests.Lifetime
{
    public class SingletonLifetimeManagerTests
    {

        [Fact]
        public void ResolveSameObjectTwiceYieldsSameObject()
        {

            var details1 = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface)
            };

            var details2 = new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface)
            };

            var returnClass = new SimpleClass();

            var moqBuilder = new Mock<IObjectConstructor>();
            moqBuilder.Setup(t => t.Build(details1)).Returns(returnClass);
            moqBuilder.Setup(t => t.Build(details2)).Returns(returnClass);


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
                RegisteredType = typeof(ISimpleInterface)
            };

            var details2 = new RegistrationDetails
            {
                RegisteredType = typeof(ILifetimeManager)
            };

            var moqBuilder = new Mock<IObjectConstructor>();
            moqBuilder.Setup(t => t.Build(details1)).Returns(new SimpleClass());
            moqBuilder.Setup(t => t.Build(details2)).Returns(new TransientLifetimeManager());

            ILifetimeManager manager = new SingletonLifetimeManager(moqBuilder.Object);
            var obj1 = manager.CreateType(details1);
            var obj2 = manager.CreateType(details2);

            Assert.NotEqual(obj1, obj2);
        }

       



    }
}
