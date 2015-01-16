namespace GameLogic.Components
{
    /// <summary>
    /// Struct AABBComponent
    /// <para>
    ///     Axis-aligned bounding box of the entity
    /// </para>
    /// </summary>
    public sealed class AABBComponent : IComponent
    {
        /// <summary>
        /// The box width
        /// </summary>
        public float Width;

        /// <summary>
        /// The box height
        /// </summary>
        public float Height;
    }
}
