namespace SprocketWTW.Lifetime
{
    public interface ILifetimeManagement
    {
        object Resolve(RegistrationDetails details);
    }
}
