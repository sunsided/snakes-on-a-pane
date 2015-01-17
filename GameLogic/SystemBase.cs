namespace GameLogic
{
    /// <summary>
    /// Class SystemBase.
    /// </summary>
    public abstract class SystemBase : ISystem
    {
        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Process(IEntity entity);
    }
}
