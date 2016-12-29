using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprocketWTW.Construction
{
    public interface IObjectConstructor
    {
        object Build(RegistrationDetails details);
    }
}
