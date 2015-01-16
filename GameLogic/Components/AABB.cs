namespace GameLogic.Components
{
    /// <summary>
    /// Struct AABB
    /// <para>
    ///     Axis-aligned bounding box of the entity
    /// </para>
    /// </summary>
    struct AABB : IComponent
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
