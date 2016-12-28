using System;
using Xunit;

namespace SprocketWTW.Tests
{

    public class SprocketWTWContainerTests
    {
        [Fact]
        public void RegisterComponentWithDefaultLifeCycle()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>();
            var obj = container.Resolve<ISimpleInterface>();
            Assert.IsType<SimpleClass>(obj);
        }

        [Fact]
        public void RegisterComponentWithExplicitTransientLifeCycle()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>(LifeTime.Transient);
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
        public void RegisterSameComponentTwiceThrowsException()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>();
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => { container.Register<ISimpleInterface, SimpleClass>(); });

            Assert.NotNull(ex);
        }

        [Fact]
        public void RegisterComponentExplicitTransientAlwaysNewObject()
        {
            var container = new SprocketWTWContainer();
            container.Register<ISimpleInterface, SimpleClass>(LifeTime.Transient);
            var obj1 = container.Resolve<ISimpleInterface>();
            var obj2 = container.Resolve<ISimpleInterface>();
            Assert.NotEqual(obj1, obj2);
        }

        [Fact]
        public void ResolveTypeNotRegisteredThrowsInvalidOperationException()
        {
            var container = new SprocketWTWContainer();
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => { container.Resolve<ISimpleInterface>(); });

            Assert.NotNull(ex);
        }

        [Fact]
        public void RegisterComponentSingletonAlwaysSameObject()
        {
            var container = new SprocketWTWContainer();

            container.Register<ISimpleInterface, SimpleClass>(LifeTime.Singleton);
            var obj1 = container.Resolve<ISimpleInterface>();
            var obj2 = container.Resolve<ISimpleInterface>();

            Assert.Equal(obj1, obj2);
        }
    }
}
