using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCInjection.Injectables
{
    public class TitleProvider : ITitleProvider
    {
        public string Title => "Injected Title FTW!";
    }

    public interface ITitleProvider
    {
        string Title { get; }
    }
}
