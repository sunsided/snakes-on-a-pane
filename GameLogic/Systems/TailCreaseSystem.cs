using System;
using System.Diagnostics;
using GameLogic.Components;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class TailCreaseSystem. This class cannot be inherited.
    /// </summary>
    public sealed class TailCreaseSystem : SystemBase
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Process(IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "The given entity must not be null");
            
            // fetch the direction change component
            DirectionChangeComponent directionChange;
            if (!entity.TryGetComponent(out directionChange)) return;
            Debug.Assert(directionChange != null, "directionChange != null");

            // early exit
            if (!directionChange.DirectionChanged) return;

            // fetch the tail component
            TailComponent tail;
            if (!entity.TryGetComponent(out tail)) return;
            Debug.Assert(tail != null, "tail != null");

            // fetch the position component
            PositionComponent position;
            if (!entity.TryGetComponent(out position)) return;
            Debug.Assert(position != null, "position != null");

            // the direction has changed, so add a crease at the current position
            tail.Creases.Push(new PositionComponent {X = position.X, Y = position.Y});
        }
    }
}
