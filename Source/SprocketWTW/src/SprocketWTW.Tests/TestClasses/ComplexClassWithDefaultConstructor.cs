namespace SprocketWTW.Tests.TestClasses
{
    public class ComplexClassWithDefaultConstructor : IComplexInterface
    {
        public ISimpleInterface InjectedDetails { get; set; }

        public ComplexClassWithDefaultConstructor()
        {
        }

        public ComplexClassWithDefaultConstructor(ISimpleInterface simple)
        {
            InjectedDetails = simple;
        }
    }
}
