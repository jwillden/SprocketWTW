namespace SprocketWTW.Tests.TestClasses
{
    public class MultipleConstructorParamsClass : IMultipleDependencies
    {
        public MultipleConstructorParamsClass(ISimpleInterface simple, IComplexInterface complex)
        {
            Complex = complex;
            Simple = simple;
        }

        public IComplexInterface Complex { get; set; }
        public ISimpleInterface Simple { get; set; }
    }

    public interface IMultipleDependencies
    {
        IComplexInterface Complex { get; set; }
        ISimpleInterface Simple { get; set; }
    }
}
