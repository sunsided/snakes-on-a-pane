using System;
using System.Diagnostics;
using GameLogic.Components;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class VelocitySystem. This class cannot be inherited.
    /// </summary>
    public sealed class VelocitySystem : ISystem
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Process(IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "The given entity must not be null");

            // fetch the input component
            InputComponent input;
            if (!entity.TryGetComponent(out input)) return;
            Debug.Assert(input != null, "input != null");

            // fetch the velocity component
            VelocityComponent velocity;
            if (!entity.TryGetComponent(out velocity)) return;
            Debug.Assert(velocity != null, "velocity != null");

            // define velocities
            const float upVelocity = 1.0F;
            const float downVelocity = -upVelocity;
            const float leftVelocity = -1.0F;
            const float rightVelocity = -leftVelocity;

            // set the velocity depending on the pressed key(s)
            if (input.North == KeyState.Pressed)
            {
                velocity.X = 0;
                velocity.Y = upVelocity;
            }
            else if (input.South == KeyState.Pressed)
            {
                velocity.X = 0;
                velocity.Y = downVelocity;
            }
            else if (input.West == KeyState.Pressed)
            {
                velocity.X = leftVelocity;
                velocity.Y = 0;
            }
            else if (input.East == KeyState.Pressed)
            {
                velocity.X = rightVelocity;
                velocity.Y = 0;
            }
        }
    }
}
