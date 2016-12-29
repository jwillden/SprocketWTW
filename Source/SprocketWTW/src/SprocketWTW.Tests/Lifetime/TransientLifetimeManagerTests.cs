using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SprocketWTW.Lifetime;
using SprocketWTW.Construction;
using Xunit;
using Moq;

namespace SprocketWTW.Tests.Lifetime
{
    public class TransientLifetimeManagerTests
    {
        [Fact]
        public void TransientNewObject()
        {
            var moqBuilder = new Moq.Mock<IObjectConstructor>();
            moqBuilder.Setup(t => t.Build(It.IsAny<RegistrationDetails>())).Returns(() => new SimpleClass());

            var manager = new TransientLifetimeManager(moqBuilder.Object);
            var obj1 = manager.CreateType(new RegistrationDetails());
            var obj2 = manager.CreateType(new RegistrationDetails());

            Assert.NotEqual(obj1, obj2);
        }
    }
}
