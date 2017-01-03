using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SprocketWTW.Lifetime;

namespace SprocketWTW.ASPNet.Core
{
    public class SprocketWTWServiceProvider : IServiceProvider, ISupportRequiredService
    {
        private readonly SprocketWTWContainer _container;

        public SprocketWTWServiceProvider(SprocketWTWContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetRequiredService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public void Populate(IEnumerable<ServiceDescriptor> descriptors)
        {
            // Be sure I can resolve IServiceProvider
            _container.Register<IServiceProvider, SprocketWTWServiceProvider>();

            foreach (var descriptor in descriptors)
            {
                if (descriptor.ImplementationType != null)
                {
                    _container.Register(
                        descriptor.ServiceType,
                        descriptor.ImplementationType,
                        (LifetimeEnum)descriptor.Lifetime);
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    _container.Register(descriptor.ServiceType, descriptor.CreateFactory());
                }
                else
                {
                    _container.Register(descriptor.ServiceType, descriptor.ImplementationInstance);
                }
            }
        }
    }
}
