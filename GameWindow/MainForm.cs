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
        /// Occurs when the blit instance is ready.
        /// </summary>
        public event EventHandler<BlitEventArgs> BlitReady;

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
            OnBlitReady(new BlitEventArgs(renderTarget));
            OnBufferFactoryReady(new BufferFactoryEventArgs(renderTarget));
            base.OnShown(e);
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

        /// <summary>
        /// Handles the <see cref="E:BlitReady" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BlitEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBlitReady(BlitEventArgs e)
        {
            var handler = BlitReady;
            if (handler != null) handler(this, e);
        }
    }
}
