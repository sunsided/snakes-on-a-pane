using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GameLogic
{
    /// <summary>
    /// Class SystemManager. This class cannot be inherited.
    /// </summary>
    public sealed class SystemManager : IProcessEntities
    {
        /// <summary>
        /// The registered systems
        /// </summary>
        private readonly List<ISystem> _systems = new List<ISystem>();

        /// <summary>
        /// Adds the system.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns>SystemManager.</returns>
        public SystemManager AddSystem([NotNull] ISystem system)
        {
            if (ReferenceEquals(system, null)) throw new ArgumentNullException("system", "System must not be null");

            _systems.Add(system);
            return this;
        }

        /// <summary>
        /// Processes the specified entities.
        /// </summary>
        /// <typeparam name="TEnumerable">The type of the enumerable.</typeparam>
        /// <param name="entities">The entities.</param>
        public void Process<TEnumerable>(TEnumerable entities)
            where TEnumerable : ICollection<IEntity>
        {
            if (ReferenceEquals(entities, null)) throw new ArgumentNullException("entities", "Entity enumeration must not be null");

            var systems = _systems;
            var systemCount = systems.Count;
            for (var i = 0; i < systemCount; ++i)
            {
                var system = systems[i];
                system.PreProcess();

                foreach (var entity in entities)
                {
                    system.Process(entity);
                }
            }
        }
    }
}
