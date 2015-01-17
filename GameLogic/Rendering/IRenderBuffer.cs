using System.Drawing;

namespace GameLogic.Rendering
{
    public interface IRenderBuffer
    {
        /// <summary>
        /// Gets the current buffer.
        /// </summary>
        /// <value>The current buffer.</value>
        /// <exception cref="System.InvalidOperationException">The buffer manager is required to be initialized first</exception>
        /// <seealso cref="CurrentGraphics" />
        Bitmap CurrentBuffer { get; }

        /// <summary>
        /// Gets the graphics instance associated with the current buffer.
        /// </summary>
        /// <value>The current graphics instance.</value>
        /// <exception cref="System.InvalidOperationException">The buffer manager is required to be initialized first</exception>
        /// <seealso cref="CurrentBuffer" />
        Graphics CurrentGraphics { get; }
    }
}