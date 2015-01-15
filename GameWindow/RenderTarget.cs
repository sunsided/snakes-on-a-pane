using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameWindow
{
    /// <summary>
    /// Class RenderTarget.
    /// </summary>
    public partial class RenderTarget : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTarget"/> class.
        /// </summary>
        public RenderTarget()
        {
            InitializeComponent();

            const ControlStyles styles = ControlStyles.Opaque
                                         | ControlStyles.OptimizedDoubleBuffer
                                         | ControlStyles.AllPaintingInWmPaint
                                         | ControlStyles.DoubleBuffer
                                         | ControlStyles.UserPaint;
            SetStyle(styles, true);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);

            var gr = e.Graphics;
            
            const int gridWidth = 10;
            
            var left = ClientRectangle.Left;
            var top = ClientRectangle.Top;
            var width = ClientRectangle.Width;
            var height = ClientRectangle.Height;

            var pen = new Pen(Color.FromArgb(10, 10, 10));

            gr.Clear(Color.Black);

            for (var y = top; y <= height; y += gridWidth)
            {
                gr.DrawLine(pen, 0, y, width, y);
            }

            for (var x = left; x <= width; x += gridWidth)
            {
                gr.DrawLine(pen, x, 0, x, height);
            }
        }
    }
}
