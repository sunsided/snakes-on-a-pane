using System.Drawing;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Interface IBlit
    /// </summary>
    public interface IBlit
    {
        /// <summary>
        /// Blits the specified bitmap onto the render target
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="System.ArgumentNullException">The buffer passed to the Blit function must not be null</exception>
        void Blit([NotNull] Bitmap buffer);
    }
}