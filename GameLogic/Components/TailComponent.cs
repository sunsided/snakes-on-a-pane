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
        /// The snake's tail length
        /// </summary>
        public int Length;

        /// <summary>
        /// The creases in the tail
        /// </summary>
        public readonly Stack<PositionComponent> Creases = new Stack<PositionComponent>();
    }
}
