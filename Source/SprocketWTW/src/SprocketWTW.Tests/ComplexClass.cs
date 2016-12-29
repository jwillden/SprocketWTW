namespace SprocketWTW.Tests
{
    public class ComplexClass : IComplexClass
    {
        public ComplexClass(ISimpleInterface injected)
        {
            InjectedDetails = injected;
        }


        // Used to ensure we're only getting public constructors
        private ComplexClass(string testString)
        {
        }

        // Used to ensure we're only getting public constructors
        static ComplexClass()
        {
        }

        public ISimpleInterface InjectedDetails { get; set; }
    }

    public interface IComplexClass
    {
    }
}
