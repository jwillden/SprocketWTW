using System;
using Xunit;

namespace SprocketWTW.Tests
{

    public class SprocketWTWContainerTests
    {
        [Fact]
        public void RegisterComponentTransientLifeCycle()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>();
            var obj = container.Resolve<ISimpleInterface>();
            Assert.IsType<SimpleClass>(obj);
        }

        [Fact]
        public void RegigsterComponentTransientAlwaysNewObject()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>();
            var obj1 = container.Resolve<ISimpleInterface>();
            var obj2 = container.Resolve<ISimpleInterface>();
            Assert.NotEqual(obj1, obj2);
        }

        [Fact]
        public void ResolveTypeNotRegisteredThrowsInvalidOperationException()
        {
            var container = new SprocketWTWContainer();
            Exception ex = Assert.Throws<InvalidOperationException>(() => { container.Resolve<ISimpleInterface>(); });
            Assert.NotNull(ex);
        }

        [Fact]
        public void RegisterComponentSingleton
    }
}
