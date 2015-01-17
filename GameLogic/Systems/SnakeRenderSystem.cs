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
    public sealed class SnakeRenderSystem : SystemBase
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
        /// A white brush
        /// </summary>
        private readonly SolidBrush _whiteBrush = new SolidBrush(Color.White);

        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeRenderSystem"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="System.ArgumentNullException">Render buffer instance must not be null</exception>
        public SnakeRenderSystem([NotNull] IRenderBuffer buffer)
        {
            if(ReferenceEquals(buffer, null)) throw new ArgumentNullException("buffer", "Render buffer instance must not be null");
            _buffer = buffer;
        }

        /// <summary>
        /// Prepares the processing.
        /// </summary>
        public override void PreProcess()
        {
            // clear the buffer
            var gr = _buffer.CurrentGraphics;
            gr.Clear(Color.Black);
        }

        /// <summary>
        /// Processes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Process(IEntity entity)
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

            // fetch the tail component
            TailComponent tail;
            if (!entity.TryGetComponent(out tail)) return;
            Debug.Assert(tail != null, "tail != null");

            // constants
            const float gridStep = 5F;

            // cache and calculate values
            var width = aabb.Width;
            var height = aabb.Height;
            var x = position.X * gridStep - width / 2F;
            var y = position.Y * gridStep - height / 2F;

            // prepare rendering
            var gr = _buffer.CurrentGraphics;

            // render the tail
            var lastX = position.X * gridStep;
            var lastY = position.Y * gridStep;
            foreach (var crease in tail.Creases)
            {
                var cx = crease.X * gridStep;
                var cy = crease.Y * gridStep;
                gr.DrawLine(_whitePen, cx, cy, lastX, lastY);
                lastX = cx;
                lastY = cy;
            }

            // render the head
            gr.FillRectangle(_whiteBrush, x, y, width, height);
        }
    }
}
