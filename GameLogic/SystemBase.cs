namespace GameLogic
{
    /// <summary>
    /// Class SystemBase.
    /// </summary>
    public abstract class SystemBase : ISystem
    {
        /// <summary>
        /// Prepares the processing.
        /// </summary>
        public virtual void PreProcess()
        {}

        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Process(IEntity entity);
    }
}
