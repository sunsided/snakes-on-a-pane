using System.Drawing;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Interface IBufferFactory
    /// </summary>
    public interface IBufferFactory
    {
        /// <summary>
        /// Creates a buffer.
        /// </summary>
        /// <returns>Bitmap.</returns>
        Bitmap CreateBuffer();
    }
}