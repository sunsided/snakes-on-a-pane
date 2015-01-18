namespace GameLogic.Components
{
    /// <summary>
    /// Struct DirectionChangeComponent
    /// <para>
    ///     Describes direction changes
    /// </para>
    /// </summary>
    public sealed class DirectionChangeComponent : IComponent
    {
        /// <summary>
        /// The previous x velocity
        /// </summary>
        public float PreviousXVelocity;

        /// <summary>
        /// The previous y velocity
        /// </summary>
        public float PreviousYVelocity;

        /// <summary>
        /// Determines if the direction has changed
        /// </summary>
        public bool DirectionChanged;
    }
}
