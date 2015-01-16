using System;
using System.Drawing;
using System.Threading;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Class BufferManager. This class cannot be inherited.
    /// </summary>
    sealed class BufferManager
    {
        /// <summary>
        /// The buffer array
        /// </summary>
        [NotNull]
        private readonly Bitmap[] _buffers;

        /// <summary>
        /// The graphics array
        /// </summary>
        [NotNull]
        private readonly Graphics[] _graphics;

        /// <summary>
        /// The current buffer
        /// </summary>
        private int _currentBuffer;

        /// <summary>
        /// The buffer count
        /// </summary>
        private readonly int _bufferCount;

        /// <summary>
        /// The initialized flag
        /// <para>
        ///     This flag is nonzero if the manager has been initialized.
        /// </para>
        /// </summary>
        private int _initializedFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferManager"/> class.
        /// </summary>
        /// <param name="bufferCount">The buffer count.</param>
        public BufferManager(int bufferCount = 2)
        {
            _bufferCount = bufferCount;
            _buffers = new Bitmap[bufferCount];
            _graphics = new Graphics[bufferCount];
        }

        /// <summary>
        /// Initializes the specified buffer factory.
        /// </summary>
        /// <param name="bufferFactory">The buffer factory.</param>
        /// <exception cref="System.ArgumentNullException">Buffer factory must not be null</exception>
        /// <exception cref="System.InvalidOperationException">The buffer manager has already been initialized</exception>
        public void Initialize([NotNull] IBufferFactory bufferFactory)
        {
            if (ReferenceEquals(bufferFactory, null)) throw new ArgumentNullException("bufferFactory", "Buffer factory must not be null");
            if (0 != Interlocked.CompareExchange(ref _initializedFlag, 0, 0)) throw new InvalidOperationException("The buffer manager has already been initialized");

            var bufferCount = _bufferCount;
            var buffers = _buffers;
            var graphics = _graphics;

            for (var i = 0; i < bufferCount; ++i)
            {
                var buffer = buffers[i] = bufferFactory.CreateBuffer();
                graphics[i] = Graphics.FromImage(buffer);
            }

            // flag as initialized
            Interlocked.Increment(ref _initializedFlag);
        }

        /// <summary>
        /// Gets the current buffer.
        /// </summary>
        /// <value>The current buffer.</value>
        /// <exception cref="System.InvalidOperationException">The buffer manager is required to be initialized first</exception>
        /// <seealso cref="CurrentGraphics" />
        [NotNull]
        public Bitmap CurrentBuffer
        {
            get
            {
                if (0 == Interlocked.CompareExchange(ref _initializedFlag, 0, 0)) throw new InvalidOperationException("The buffer manager is required to be initialized first");
                var i = Interlocked.CompareExchange(ref _currentBuffer, 0, 0);
                var buffers = _buffers;
                return buffers[i];
            }
        }

        /// <summary>
        /// Gets the graphics instance associated with the current buffer.
        /// </summary>
        /// <value>The current graphics instance.</value>
        /// <exception cref="System.InvalidOperationException">The buffer manager is required to be initialized first</exception>
        /// <seealso cref="CurrentBuffer" />
        [NotNull]
        public Graphics CurrentGraphics
        {
            get
            {
                if (0 == Interlocked.CompareExchange(ref _initializedFlag, 0, 0)) throw new InvalidOperationException("The buffer manager is required to be initialized first");
                var i = Interlocked.CompareExchange(ref _currentBuffer, 0, 0);
                var graphics = _graphics;
                return graphics[i];
            }
        }

        /// <summary>
        /// Swaps the buffers and returns the previous selected buffer.
        /// </summary>
        /// <returns>Bitmap.</returns>
        /// <exception cref="System.InvalidOperationException">The buffer manager is required to be initialized first</exception>
        [NotNull]
        public Bitmap SwapBuffers()
        {
            if (0 == Interlocked.CompareExchange(ref _initializedFlag, 0, 0)) throw new InvalidOperationException("The buffer manager is required to be initialized first");

            // if the buffer index is at the last position,
            // wrap it around to zero
            var lastIndex = _bufferCount - 1;
            var originalIndex = Interlocked.CompareExchange(ref _currentBuffer, value: 0, comparand: lastIndex);

            // if the index value was not equal to the last index,
            // then the exchange operation did not take place and we can increment
            if (originalIndex != lastIndex)
            {
                Interlocked.Increment(ref _currentBuffer);
            }

            // either way, the original value was our last
            // selected buffer, so we can return it
            return _buffers[originalIndex];
        }
    }
}
