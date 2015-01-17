using GameLogic.Systems;

namespace GameLogic.Components
{
    /// <summary>
    /// Struct InputComponent
    /// <para>
    ///     The input
    /// </para>
    /// </summary>
    public sealed class InputComponent : IComponent
    {
        /// <summary>
        /// The north key
        /// </summary>
        public KeyState North;

        /// <summary>
        /// The south key
        /// </summary>
        public KeyState South;

        /// <summary>
        /// The west key
        /// </summary>
        public KeyState West;

        /// <summary>
        /// The east key
        /// </summary>
        public KeyState East;
    }
}
