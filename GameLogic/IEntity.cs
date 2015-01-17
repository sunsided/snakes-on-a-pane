using JetBrains.Annotations;

namespace GameLogic
{
    /// <summary>
    /// Interface IEntity
    /// <para>
    ///     Tags an entity that is composed of <see cref="IComponent"/> instances.
    /// </para>
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Tries to obtains the component of the given type.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component.</typeparam>
        /// <param name="component">The component, if it exists.</param>
        /// <returns><see langword="true" /> if the component exists, <see langword="false" /> otherwise.</returns>
        bool TryGetComponent<TComponent>([CanBeNull] out TComponent component)
            where TComponent : IComponent;
    }
}
