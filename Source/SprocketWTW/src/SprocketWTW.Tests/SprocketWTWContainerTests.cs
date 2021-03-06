﻿using System;
using Moq;
using Xunit;
using SprocketWTW.Lifetime;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests
{

    public class SprocketWTWContainerTests
    {

        [Fact]
        public void RegisterComponentWithDefaultLifeCycle()
        {
            var moqMang = GetMockLifetimeManagement();
            var moqCache = GetMockRegistrationCache();

            var container = new SprocketWTWContainer(moqMang.Object, moqCache.Object);

            Exception ex = Record.Exception(() => container.Register<ISimpleInterface, SimpleClass>());

            Assert.Null(ex);
        }

        [Fact]
        public void RegisterComponentWithExplicitLifeCycle()
        {
            var moqMang = GetMockLifetimeManagement();
            var moqCache = GetMockRegistrationCache();

            var container = new SprocketWTWContainer(moqMang.Object, moqCache.Object);

            Exception ex = Record.Exception(() => container.Register<ISimpleInterface, SimpleClass>(LifetimeEnum.Transient));

            Assert.Null(ex);
        }

        [Fact]
        public void ResolveTypeNotRegisteredThrowsInvalidOperationException()
        {
            var moqMang = GetMockLifetimeManagement();
            var moqCache = GetMockRegistrationCache();
            moqCache.Setup(t => t.Contains(typeof(ISimpleInterface))).Returns(false);

            var container = new SprocketWTWContainer(moqMang.Object, moqCache.Object);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => { container.Resolve<ISimpleInterface>(); });
            Assert.NotNull(ex);
        }

        private static Mock<ILifetimeManagement> GetMockLifetimeManagement()
        {
            Mock<ILifetimeManagement> moqMang = new Mock<ILifetimeManagement>();
            moqMang.Setup(t => t.Resolve(It.IsAny<RegistrationDetails>())).Returns(new SimpleClass());
            return moqMang;
        }

        private static Mock<IRegistrationCache> GetMockRegistrationCache()
        {
            Mock<IRegistrationCache> moqCache = new Mock<IRegistrationCache>();
            moqCache.Setup(t => t.RegisterType(It.IsAny<RegistrationDetails>()));
            return moqCache;
        }
    }
}
