using System;
using System.Diagnostics;
using GameLogic.Components;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class PositionSystem. This class cannot be inherited.
    /// </summary>
    public sealed class PositionSystem : ISystem
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Process(IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "The given entity must not be null");

            // fetch the velocity component
            VelocityComponent velocity;
            if (!entity.TryGetComponent(out velocity)) return;
            Debug.Assert(velocity != null, "velocity != null");

            // fetch the position component
            PositionComponent position;
            if (!entity.TryGetComponent(out position)) return;
            Debug.Assert(position != null, "position != null");

            // update the positions
            position.X += velocity.X;
            position.Y += velocity.Y;

            Trace.WriteLine(String.Format("{0},{1}", position.X, position.Y));
        }
    }
}
