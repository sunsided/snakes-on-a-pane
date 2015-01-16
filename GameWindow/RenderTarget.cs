using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace GameWindow
{
    /// <summary>
    /// Class RenderTarget.
    /// </summary>
    public partial class RenderTarget : UserControl
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
                                               | ControlStyles.OptimizedDoubleBuffer
                                               | ControlStyles.DoubleBuffer
                                               | ControlStyles.AllPaintingInWmPaint; // moves all paint events to Paint(args)
            SetStyle(enableStyles, true);

            const ControlStyles disableStyles = ControlStyles.UserPaint; // disables all external Paint(args) calls
            SetStyle(disableStyles, false);

            _graphics = CreateGraphics();
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // will never be called
        }

        /// <summary>
        /// Paints this instance.
        /// </summary>
        public void Render()
        {
            var gr = _graphics;

            const int gridWidth = 10;

            var left = ClientRectangle.Left;
            var top = ClientRectangle.Top;
            var width = ClientRectangle.Width;
            var height = ClientRectangle.Height;

            var pen = new Pen(Color.FromArgb(10, 10, 10));

            gr.Clear(Color.Red);

            for (var y = top; y <= height; y += gridWidth)
            {
                gr.DrawLine(pen, 0, y, width, y);
            }

            for (var x = left; x <= width; x += gridWidth)
            {
                gr.DrawLine(pen, x, 0, x, height);
            }

            var frame = Interlocked.Increment(ref _frame);

            gr.DrawString(frame.ToString(CultureInfo.InvariantCulture), DefaultFont, new SolidBrush(Color.GreenYellow), 0, 0);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // HACK: in order to test the code, clicking into the frame invokes the render function
            Render();
        }
    }
}
