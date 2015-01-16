using System;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Class BufferFactoryEventArgs.
    /// </summary>
    public class BufferFactoryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>The factory.</value>
        [NotNull]
        public IBufferFactory Factory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferFactoryEventArgs" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">The factory must not be null</exception>
        public BufferFactoryEventArgs([NotNull] IBufferFactory factory)
        {
            if (ReferenceEquals(factory, null)) throw new ArgumentNullException("factory", "The factory must not be null");

            Factory = factory;
        }
    }
}
