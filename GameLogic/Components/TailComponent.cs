using System.Collections.Generic;

namespace GameLogic.Components
{
    /// <summary>
    /// Struct TailComponent
    /// <para>
    ///     Describes the snake's tail
    /// </para>
    /// </summary>
    public sealed class TailComponent : IComponent
    {
        /// <summary>
        /// Struct Crease
        /// </summary>
        public sealed class Crease
        {
            /// <summary>
            /// The x position
            /// </summary>
            public float X;
            
            /// <summary>
            /// The y position
            /// </summary>
            public float Y;

            /// <summary>
            /// The x velocity after the crease
            /// </summary>
            public float NextXVelocity;

            /// <summary>
            /// The y velocity after the crease
            /// </summary>
            public float NextYVelocity;

            /// <summary>
            /// The distance from the head
            /// </summary>
            public float DistanceFromHead;
        }

        /// <summary>
        /// The snake's tail length
        /// </summary>
        public float Length;

        /// <summary>
        /// The creases in the tail
        /// </summary>
        public readonly LinkedList<Crease> Creases = new LinkedList<Crease>();
    }
}
