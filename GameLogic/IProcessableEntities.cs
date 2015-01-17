namespace GameLogic
{
    /// <summary>
    /// Interface IProcessableEntities
    /// </summary>
    public interface IProcessableEntities
    {
        /// <summary>
        /// Processes the entities with the specified systems.
        /// </summary>
        /// <param name="systems">The systems.</param>
        /// <exception cref="System.ArgumentNullException">systems;Systems must not be null</exception>
        void ProcessWith(IProcessEntities systems);
    }
}