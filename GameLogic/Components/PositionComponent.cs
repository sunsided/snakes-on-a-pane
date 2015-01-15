namespace GameLogic.Components
{
    /// <summary>
    /// Struct PositionComponent
    /// <para>
    ///     Describes the position of an entity.
    /// </para>
    /// </summary>
    struct PositionComponent : IComponent
    {
        /// <summary>
        /// The x position
        /// </summary>
        public int X;

        /// <summary>
        /// The y position
        /// </summary>
        public int Y;
    }
}
