using System;
using System.Windows.Forms;
using GameLogic;
using JetBrains.Annotations;

namespace GameWindow.Systems
{
    /// <summary>
    /// Class InputSystem. This class cannot be inherited.
    /// </summary>
    sealed class InputSystem : ISystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputSystem"/> class.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <exception cref="System.ArgumentNullException">The form must not be null</exception>
        public InputSystem([NotNull] Form form)
        {
            if (ReferenceEquals(form, null)) throw new ArgumentNullException("form", "The form must not be null");

            form.KeyDown += FormKeyDown;
            form.KeyUp += FormKeyUp;
        }

        /// <summary>
        /// Handles the KeyUp event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void FormKeyUp(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// Handles the KeyDown event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void FormKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
