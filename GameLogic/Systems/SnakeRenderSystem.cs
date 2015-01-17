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
            var px = position.X;
            var py = position.Y;

            var hw = width/2;
            var hh = height/2;

            // prepare rendering
            var gr = _buffer.CurrentGraphics;

            // render the tail
            var lastX = px;
            var lastY = py;
            foreach (var crease in tail.Creases)
            {
                var cx = crease.X;
                var cy = crease.Y;

                // crease is left of the head
                if (cx < lastX && cy == lastY)
                {
                    gr.FillRectangle(_whiteBrush, cx * gridStep-hw, cy * gridStep-hh, (lastX - cx) * gridStep, height);
                }
                else if (cx > lastX && cy == lastY) // crease is right of the head
                {
                    gr.FillRectangle(_whiteBrush, lastX * gridStep + hw, lastY * gridStep - hh, (cx - lastX) * gridStep, height);
                }
                else if (cy < lastY && cx == lastX) // crease is above the head
                {
                    gr.FillRectangle(_whiteBrush, cx * gridStep - hw, cy * gridStep - hh, width, (lastY - cy) * gridStep + height);
                }
                else if (cy > lastY && cx == lastX) // crease is below the head
                {
                    gr.FillRectangle(_whiteBrush, lastX * gridStep - hw, lastY * gridStep - hh, width, (cy - lastY) * gridStep + height);
                }

                lastX = cx;
                lastY = cy;
            }

            // render the head
            var x = px * gridStep - hw;
            var y = py * gridStep - hh;
            gr.FillRectangle(_whiteBrush, x, y, width, height);
        }
    }
}
