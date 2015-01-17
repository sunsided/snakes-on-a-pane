using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GameLogic.Entities
{
    /// <summary>
    /// Class Entity.
    /// </summary>
    public sealed class Entity : IEntity
    {
        /// <summary>
        /// The registered components
        /// </summary>
        [NotNull]
        private readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="TComponent">The type of the t component.</typeparam>
        /// <param name="component">The component.</param>
        /// <exception cref="System.ArgumentNullException">The component to be registered must not be <see langword="null"/></exception>
        /// <exception cref="System.ArgumentException">A component of the given type was already registered</exception>
        public void AddComponent<TComponent>([NotNull] TComponent component)
            where TComponent : IComponent
        {
            if (ReferenceEquals(component, null)) throw new ArgumentNullException("component", "Component must not be null");

            var key = typeof(TComponent);
            if (_components.ContainsKey(key)) throw new ArgumentException("A component of the given type was already registered", "component");

            _components.Add(key, component);
        }

        /// <summary>
        /// Removes the component of the given type.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component.</typeparam>
        public void RemoveComponent<TComponent>()
            where TComponent : IComponent
        {
            _components.Remove(typeof (TComponent));
        }

        /// <summary>
        /// Obtains the component of the given type.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component.</typeparam>
        /// <returns>TComponent.</returns>
        /// <exception cref="System.InvalidOperationException">No component of the given type was registered.</exception>
        [NotNull]
        public TComponent GetComponent<TComponent>()
            where TComponent : IComponent
        {
            var key = typeof(TComponent);
            IComponent component;
            if (_components.TryGetValue(key, out component))
            {
                return (TComponent) component;
            }

            throw new InvalidOperationException("No component of the given type was registered.");
        }

        /// <summary>
        /// Tries to obtains the component of the given type.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component.</typeparam>
        /// <param name="component">The component, if it exists.</param>
        /// <returns><see langword="true" /> if the component exists, <see langword="false" /> otherwise.</returns>
        public bool TryGetComponent<TComponent>([CanBeNull] out TComponent component)
            where TComponent : IComponent
        {
            var key = typeof(TComponent);
            IComponent componentInterface;
            if (_components.TryGetValue(key, out componentInterface))
            {
                component = (TComponent) componentInterface;
                return true;
            }

            component = default(TComponent);
            return false;
        }
    }
}
