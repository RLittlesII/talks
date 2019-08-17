using System;
using System.Collections.Generic;
using System.Text;
using Splat;

namespace LoginState
{
    public interface IPlatformRegistrar
    {
        void RegisterPlatformServices(Action<IMutableDependencyResolver> mutableDependencyResolver);

        void RegisterPlatformServices();
    }
}
