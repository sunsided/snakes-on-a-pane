using System;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Class Renderer.
    /// </summary>
    sealed class Renderer : IRenderer
    {
        /// <summary>
        /// The buffer manager
        /// </summary>
        [NotNull]
        private readonly BufferManager _bm;

        /// <summary>
        /// The blit target
        /// </summary>
        [CanBeNull]
        private IBlit _blit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer" /> class.
        /// </summary>
        /// <param name="bm">The buffer manager.</param>
        /// <exception cref="System.ArgumentNullException">The buffer manager must not be null</exception>
        public Renderer([NotNull] BufferManager bm)
        {
            if (ReferenceEquals(bm, null)) throw new ArgumentNullException("bm", "The buffer manager must not be null");
            _bm = bm;
        }

        /// <summary>
        /// Initializes the specified blit.
        /// </summary>
        /// <param name="blit">The blit.</param>
        /// <exception cref="System.ArgumentNullException">The blit instance must not be null</exception>
        public void Initialize([NotNull] IBlit blit)
        {
            if (ReferenceEquals(blit, null)) throw new ArgumentNullException("blit", "The blit instance must not be null");
            _blit = blit;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The renderer must be initialized before rendering</exception>
        public void Render()
        {
            var blit = _blit;
            if (ReferenceEquals(blit, null)) throw new InvalidOperationException("The renderer must be initialized before rendering");

            var buffer = _bm.SwapBuffers();
            blit.Blit(buffer);
        }
    }
}
