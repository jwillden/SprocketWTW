using Xunit;
using SprocketWTW.Lifetime;
using SprocketWTW.Tests.TestClasses;

namespace SprocketWTW.Tests
{
    public class StaticRegistrationCollectionTests
    {
        [Fact]
        public void RegisterType()
        {
            var coll = new StaticRegistrationCollection();
            coll.RegisterType(GetSimpleGetDetails());
            Assert.True(coll.Contains(typeof(ISimpleInterface)));
        }

        [Fact]
        public void GetInstanceDetails()
        {
            var coll = new StaticRegistrationCollection();
            
            var inputDetails = GetSimpleGetDetails();
            coll.RegisterType(inputDetails);
            var outputDetails = coll.Get(typeof(ISimpleInterface));

            // Because tests are executed in a threaded fashion, the call to .Get may or may not
            // return the same reference. So, instead, just make sure the correct types are registered
            // and the Lifetime is the same.
            Assert.Equal(inputDetails.RegisteredType, outputDetails.RegisteredType);
            Assert.Equal(inputDetails.ResolvedType, outputDetails.ResolvedType);
            Assert.Equal(inputDetails.Lifetime, outputDetails.Lifetime);            
        }

        private RegistrationDetails GetSimpleGetDetails()
        {
            return new RegistrationDetails
            {
                RegisteredType = typeof(ISimpleInterface),
                ResolvedType = typeof(SimpleClass),
                Lifetime = LifetimeEnum.Transient
            };
        }
    }
}
