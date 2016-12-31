using System;

namespace SprocketWTW
{
    public interface IRegistrationCache
    {
        void RegisterType(RegistrationDetails details);

        bool Contains(Type T);

        RegistrationDetails Get(Type T);

    }
}
