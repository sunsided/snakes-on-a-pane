using System;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Class BlitEventArgs.
    /// </summary>
    public class BlitEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the blit instance.
        /// </summary>
        /// <value>The blit instance.</value>
        [NotNull]
        public IBlit Blit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlitEventArgs" /> class.
        /// </summary>
        /// <param name="blit">The blit.</param>
        /// <exception cref="System.ArgumentNullException">The blit reference must not be null</exception>
        public BlitEventArgs([NotNull] IBlit blit)
        {
            if (ReferenceEquals(blit, null)) throw new ArgumentNullException("blit", "The blit reference must not be null");

            Blit = blit;
        }
    }
}
