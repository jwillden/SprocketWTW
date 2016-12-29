using Xunit;
using SprocketWTW.Lifetime;
namespace SprocketWTW.Tests
{
    public class RegistrationCollectionTests
    {
        [Fact]
        public void RegisterType()
        {
            var coll = new StaticRegistrationCollection();
            coll.RegisterType(GetSimpleGetDetails());
            Assert.True((bool) coll.Contains(typeof(ISimpleInterface)));
        }

        [Fact]
        public void GetInstanceDetails()
        {
            var coll = new StaticRegistrationCollection();
            var inputDetails = GetSimpleGetDetails();
            coll.RegisterType(inputDetails);
            var outputDetails = coll.Get(typeof(ISimpleInterface));
            Assert.Equal(inputDetails, outputDetails);
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
