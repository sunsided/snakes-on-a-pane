using System;
using System.Diagnostics;
using System.Drawing;
using GameLogic.Components;
using GameLogic.Rendering;
using JetBrains.Annotations;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class RenderSystem. This class cannot be inherited.
    /// </summary>
    public sealed class RenderSystem : ISystem
    {
        /// <summary>
        /// The render buffer
        /// </summary>
        [NotNull]
        private readonly IRenderBuffer _buffer;

        /// <summary>
        /// A white pen
        /// </summary>
        private readonly Pen _whitePen = new Pen(Color.White);

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderSystem"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="System.ArgumentNullException">Render buffer instance must not be null</exception>
        public RenderSystem([NotNull] IRenderBuffer buffer)
        {
            if(ReferenceEquals(buffer, null)) throw new ArgumentNullException("buffer", "Render buffer instance must not be null");
            _buffer = buffer;
        }

        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Process(IEntity entity)
        {
            if (ReferenceEquals(entity, null)) throw new ArgumentNullException("entity", "The given entity must not be null");

            // fetch the velocity component
            AABBComponent aabb;
            if (!entity.TryGetComponent(out aabb)) return;
            Debug.Assert(aabb != null, "aabb != null");

            // fetch the position component
            PositionComponent position;
            if (!entity.TryGetComponent(out position)) return;
            Debug.Assert(position != null, "position != null");

            // positions
            var width = aabb.Width;
            var height = aabb.Height;
            var x = position.X - width/2F;
            var y = position.Y - height/2F;

            // render the element
            var gr = _buffer.CurrentGraphics;
            gr.DrawRectangle(_whitePen, x, y, width, height);
        }
    }
}
