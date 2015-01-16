namespace GameWindow.Rendering
{
    /// <summary>
    /// Interface IRenderer
    /// </summary>
    internal interface IRenderer
    {
        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The renderer must be initialized before rendering</exception>
        void Render();
    }
}