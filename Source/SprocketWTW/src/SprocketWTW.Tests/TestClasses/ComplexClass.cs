namespace SprocketWTW.Tests.TestClasses
{
    public class ComplexClass : IComplexInterface
    {
        public ComplexClass(ISimpleInterface injected)
        {
            InjectedDetails = injected;
        }

        public ISimpleInterface InjectedDetails { get; set; }
    }

    public interface IComplexInterface
    {
        ISimpleInterface InjectedDetails { get; set; }
    }
}
