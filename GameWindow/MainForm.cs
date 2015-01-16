using System;
using System.Windows.Forms;
using GameWindow.Rendering;

namespace GameWindow
{
    /// <summary>
    /// Class MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Occurs when the buffer factory is ready.
        /// </summary>
        public event EventHandler<BufferFactoryEventArgs> BufferFactoryReady;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            OnBufferFactoryReady(new BufferFactoryEventArgs(renderTarget1));
            base.OnShown(e);
        }

        /// <summary>
        /// Renders the frame.
        /// </summary>
        public void RenderFrame()
        {
            renderTarget1.Render();
        }

        /// <summary>
        /// Handles the <see cref="E:BufferFactoryReady" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BufferFactoryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBufferFactoryReady(BufferFactoryEventArgs e)
        {
            var handler = BufferFactoryReady;
            if (handler != null) handler(this, e);
        }
    }
}
