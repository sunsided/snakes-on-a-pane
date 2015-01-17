using System;
using System.Collections.Generic;
using GameLogic.Entities;
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
        /// Creates and adds an entity.
        /// </summary>
        /// <returns>Entity.</returns>
        [NotNull]
        public Entity CreateEntity()
        {
            var entity = new Entity();
            _entities.Add(entity);
            return entity;
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
