using System;
using System.Diagnostics;
using GameLogic.Components;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class DirectionChangeDetection. This class cannot be inherited.
    /// </summary>
    public sealed class DirectionChangeDetectionSystem : SystemBase
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Process(IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "The given entity must not be null");

            // fetch the velocity component
            VelocityComponent velocity;
            if (!entity.TryGetComponent(out velocity)) return;
            Debug.Assert(velocity != null, "velocity != null");

            // fetch the position component
            DirectionChangeComponent directionChange;
            if (!entity.TryGetComponent(out directionChange)) return;
            Debug.Assert(directionChange != null, "directionChange != null");

            // update the positions
            const float epsilon = 0.001F;
            directionChange.DirectionChanged = Math.Abs(directionChange.PreviousXVelocity - velocity.X) > epsilon ||
                                               Math.Abs(directionChange.PreviousYVelocity - velocity.Y) > epsilon;
            directionChange.PreviousXVelocity = velocity.X;
            directionChange.PreviousYVelocity = velocity.Y;
        }
    }
}
