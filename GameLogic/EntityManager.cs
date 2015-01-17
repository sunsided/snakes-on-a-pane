using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GameLogic
{
    /// <summary>
    /// Class EntityManager. This class cannot be inherited.
    /// </summary>
    public sealed class EntityManager : IProcessableEntities
    {
        /// <summary>
        /// The registered systems
        /// </summary>
        private readonly List<IEntity> _entities = new List<IEntity>();

        /// <summary>
        /// Adds the system.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>SystemManager.</returns>
        public EntityManager AddEntity([NotNull] IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "Entity must not be null");

            _entities.Add(entity);
            return this;
        }

        /// <summary>
        /// Processes the entities with the specified systems.
        /// </summary>
        /// <param name="systems">The systems.</param>
        /// <exception cref="System.ArgumentNullException">systems;Systems must not be null</exception>
        public void ProcessWith(IProcessEntities systems)
        {
            if (ReferenceEquals(systems, null)) throw new ArgumentNullException("systems", "Systems must not be null");

            systems.Process(_entities);
        }
    }
}
