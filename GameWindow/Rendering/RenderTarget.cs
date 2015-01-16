using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace GameWindow.Rendering
{
    /// <summary>
    /// Class RenderTarget. This class cannot be inherited.
    /// </summary>
    public sealed partial class RenderTarget : UserControl, IBufferFactory, IBlit
    {
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        [NotNull]
        private readonly Graphics _graphics;

        /// <summary>
        /// The frame being rendered
        /// </summary>
        private int _frame = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTarget"/> class.
        /// </summary>
        public RenderTarget()
        {
            InitializeComponent();

            const ControlStyles enableStyles = ControlStyles.Opaque
                                               | ControlStyles.FixedHeight
                                               | ControlStyles.FixedWidth
                                               | ControlStyles.OptimizedDoubleBuffer
                                               | ControlStyles.DoubleBuffer
                                               | ControlStyles.AllPaintingInWmPaint; // moves all paint events to Paint(args)
            SetStyle(enableStyles, true);

            const ControlStyles disableStyles = ControlStyles.UserPaint; // disables all external Paint(args) calls
            SetStyle(disableStyles, false);

            var gr = _graphics = CreateGraphics();
            gr.InterpolationMode = InterpolationMode.Low;
            gr.CompositingQuality = CompositingQuality.HighSpeed;
            gr.SmoothingMode = SmoothingMode.HighSpeed;
            gr.TextRenderingHint = TextRenderingHint.SystemDefault;
            gr.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            gr.SetClip(ClientRectangle);
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            throw new InvalidOperationException("This event should never raise.");
        }

        /// <summary>
        /// Blits the specified bitmap onto the render target
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="System.ArgumentNullException">The buffer passed to the Blit function must not be null</exception>
        public void Blit([NotNull] Bitmap buffer)
        {
            if (ReferenceEquals(buffer, null)) throw new ArgumentNullException("buffer", "The buffer passed to the Blit function must not be null");

            var gr = _graphics;
            gr.DrawImageUnscaledAndClipped(buffer, ClientRectangle);
        }

        /// <summary>
        /// Creates a buffer.
        /// </summary>
        /// <returns>Bitmap.</returns>
        public Bitmap CreateBuffer()
        {
            return new Bitmap(ClientRectangle.Width, ClientRectangle.Height, _graphics);
        }
	}
}
