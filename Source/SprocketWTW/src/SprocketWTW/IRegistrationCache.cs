using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprocketWTW
{
    public interface IRegistrationCache
    {
        void RegisterType(RegistrationDetails details);

        bool Contains(Type T);

        RegistrationDetails Get(Type T);

    }
}
