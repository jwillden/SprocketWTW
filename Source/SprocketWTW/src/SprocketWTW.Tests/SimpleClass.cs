namespace SprocketWTW.Tests
{
    public class SimpleClass : ISimpleInterface
    {
        public bool IsOdd(int number)
        {
            return number % 2 == 1;
        }
    }

    public interface ISimpleInterface
    {
        bool IsOdd(int number);
    }
}
