using SprocketWTW.Lifetime;
using Xunit;

namespace SprocketWTW.Tests.Lifetime
{
    public class TransientLifetimeManagerTests
    {
        [Fact]
        public void TransientAlwaysNewObject()
        {
            var manager = new TransientLifetimeManager();
            var obj1 = manager.CreateType(typeof(SimpleClass));
            var obj2 = manager.CreateType(typeof(SimpleClass));

            Assert.NotEqual(obj1, obj2);
        }
    }
}
