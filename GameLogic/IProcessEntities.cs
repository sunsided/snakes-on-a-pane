using System.Collections.Generic;

namespace GameLogic
{
    /// <summary>
    /// Interface IProcessEntities
    /// </summary>
    public interface IProcessEntities
    {
        /// <summary>
        /// Processes the specified entities.
        /// </summary>
        /// <typeparam name="TEnumerable">The type of the enumerable.</typeparam>
        /// <param name="entities">The entities.</param>
        void Process<TEnumerable>(TEnumerable entities)
            where TEnumerable : ICollection<IEntity>;
    }
}