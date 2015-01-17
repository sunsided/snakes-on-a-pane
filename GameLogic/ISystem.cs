using System;
using JetBrains.Annotations;

namespace GameLogic
{
    /// <summary>
    /// Interface ISystem
    /// <para>
    ///     Tags a system that operates on <see cref="IComponent"/> instances.
    /// </para>
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">The given entity must not be null</exception>
        void Process([NotNull] IEntity entity);
    }
}
