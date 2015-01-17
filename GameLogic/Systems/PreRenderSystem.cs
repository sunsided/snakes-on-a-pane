using System;
using System.Drawing;
using GameLogic.Rendering;
using JetBrains.Annotations;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class PreRenderSystem. This class cannot be inherited.
    /// </summary>
    public sealed class PreRenderSystem : SystemBase
    {
        /// <summary>
        /// The render buffer
        /// </summary>
        [NotNull]
        private readonly IRenderBuffer _buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreRenderSystem"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="System.ArgumentNullException">Render buffer instance must not be null</exception>
        public PreRenderSystem([NotNull] IRenderBuffer buffer)
        {
            if(ReferenceEquals(buffer, null)) throw new ArgumentNullException("buffer", "Render buffer instance must not be null");
            _buffer = buffer;
        }

        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Process(IEntity entity)
        {
            var gr = _buffer.CurrentGraphics;
            gr.Clear(Color.Black);
        }
    }
}
