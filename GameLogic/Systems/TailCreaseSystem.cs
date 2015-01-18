using System;
using System.Diagnostics;
using System.Linq;
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
            
            // fetch the tail component
            TailComponent tail;
            if (!entity.TryGetComponent(out tail)) return;
            Debug.Assert(tail != null, "tail != null");

            // fetch the position component
            PositionComponent position;
            if (!entity.TryGetComponent(out position)) return;
            Debug.Assert(position != null, "position != null");

            // fetch the velocity component
            VelocityComponent vc;
            if (!entity.TryGetComponent(out vc)) return;
            Debug.Assert(vc != null, "vc != null");
            
            // as we move forward, increment the distance of each crease
            var velocity = Math.Abs(vc.X) + Math.Abs(vc.Y);
            var pointer = tail.Creases.First;
            while (pointer != null)
            {
                var distance = (pointer.Value.DistanceFromHead += velocity);

                // if this segment is now at a position after the tail's end,
                // move it one step towards it's velocity direction
                if (distance > tail.Length)
                {
                    pointer.Value.X += pointer.Value.NextXVelocity;
                    pointer.Value.Y += pointer.Value.NextYVelocity;
                    pointer.Value.DistanceFromHead -= velocity;

                    // since we moved the tail's end, we may now be
                    // at the same position as the previous crease
                    // (if it exists), so check for that and delete if necessary
                    var prev = pointer.Previous;
                    const float epsilon = 0.0001F;
                    if (prev != null 
                        && Math.Abs(prev.Value.X - pointer.Value.X) < epsilon 
                        && Math.Abs(prev.Value.Y - pointer.Value.Y) < epsilon)
                    {
                        tail.Creases.Remove(pointer);

                        // point to the previous item, so that the finishing
                        // traversal to "pointer.Next" yields the correct result
                        pointer = prev;
                    }
                }

                // take care of the next segment
                pointer = pointer.Next;
            }

            // early exit
            if (directionChange.DirectionChanged)
            {
                // the direction has changed, so add a crease at the current position
                tail.Creases.AddFirst(new TailComponent.Crease
                                      {
                                          X = position.X,
                                          Y = position.Y,
                                          DistanceFromHead = 0,
                                          NextXVelocity = vc.X,
                                          NextYVelocity = vc.Y
                                      });
            }
        }
    }
}
