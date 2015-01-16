using System.Windows.Forms;

namespace GameWindow
{
    /// <summary>
    /// Class MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Renders the frame.
        /// </summary>
        public void RenderFrame()
        {
            renderTarget1.Render();
        }
    }
}
