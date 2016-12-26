namespace SprocketWTW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SprocketWTWContainer
    {
        private readonly Dictionary<Type, Type> typeRegistrations;

        public SprocketWTWContainer()
        {
            this.typeRegistrations = new Dictionary<Type, Type>();
        }

        public void Register<I, T>()
        {
            this.typeRegistrations.Add(typeof(I), typeof(T));
        }

        public T Resolve<T>() where T: class
        {
            if (!this.typeRegistrations.Keys.Contains(typeof(T)))
            {
                throw new InvalidOperationException($"Type {typeof(T).FullName} has not been registered. Please register this type using Register before attempting to Resolve it.");
            }

            var typeToCreate = this.typeRegistrations[typeof(T)];

            return Activator.CreateInstance(typeToCreate) as T;
        }
    }
}
