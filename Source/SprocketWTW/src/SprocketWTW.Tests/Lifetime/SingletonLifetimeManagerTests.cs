using Xunit;
using SprocketWTW.Lifetime;

namespace SprocketWTW.Tests.Lifetime
{
    public class SingletonLifetimeManagerTests
    {

        [Fact]
        public void ResolveSameObjectTwiceYieldsSameObject()
        {
            ILifetimeManager manager = new SingletonLifetimeManager();
            var obj1 = manager.CreateType(typeof(SimpleClass));
            var obj2 = manager.CreateType(typeof(SimpleClass));

            Assert.Equal(obj1, obj2);
        }

        [Fact]
        public void ResolveDifferentObjectYieldsDifferentObjects()
        {
            ILifetimeManager manager = new SingletonLifetimeManager();
            var obj1 = manager.CreateType(typeof(SimpleClass));
            var obj2 = manager.CreateType(typeof(TransientLifetimeManager));

            Assert.NotEqual(obj1, obj2);
        }

       



    }
}
