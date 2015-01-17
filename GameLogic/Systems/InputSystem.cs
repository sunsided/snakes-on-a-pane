using System;
using System.Diagnostics;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace GameLogic.Systems
{
    /// <summary>
    /// Class InputSystem. This class cannot be inherited.
    /// </summary>
    sealed class InputSystem : ISystem
    {
        /// <summary>
        /// Gets the player north key.
        /// </summary>
        /// <value>The player north key.</value>
        private Keys _playerNorthKey = Keys.W;

        /// <summary>
        /// Gets the player south key.
        /// </summary>
        /// <value>The player south key.</value>
        private Keys _playerSouthKey = Keys.A;

        /// <summary>
        /// Gets the player west key.
        /// </summary>
        /// <value>The player west key.</value>
        private Keys _playerWestKey = Keys.S;

        /// <summary>
        /// Gets the player east key.
        /// </summary>
        /// <value>The player east key.</value>
        private Keys _playerEastKey = Keys.D;

        /// <summary>
        /// Gets the player north key.
        /// </summary>
        /// <value>The player north key.</value>
        public Keys PlayerNorthKey
        {
            get { return _playerNorthKey; }
            private set { _playerNorthKey = value; }
        }

        /// <summary>
        /// Gets the player south key.
        /// </summary>
        /// <value>The player south key.</value>
        public Keys PlayerSouthKey
        {
            get { return _playerSouthKey; }
            private set { _playerSouthKey = value; }
        }

        /// <summary>
        /// Gets the player west key.
        /// </summary>
        /// <value>The player west key.</value>
        public Keys PlayerWestKey
        {
            get { return _playerWestKey; }
            private set { _playerWestKey = value; }
        }

        /// <summary>
        /// Gets the player east key.
        /// </summary>
        /// <value>The player east key.</value>
        public Keys PlayerEastKey
        {
            get { return _playerEastKey; }
            private set { _playerEastKey = value; }
        }
        
        /// <summary>
        /// The player north state
        /// </summary>
        private KeyState _playerNorth;

        /// <summary>
        /// The player north state
        /// </summary>
        private KeyState _playerSouth;

        /// <summary>
        /// The player west state
        /// </summary>
        private KeyState _playerWest;

        /// <summary>
        /// The player east state
        /// </summary>
        private KeyState _playerEast;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputSystem"/> class.
        /// </summary>
        /// <param name="control">The form.</param>
        /// <exception cref="System.ArgumentNullException">The form must not be null</exception>
        public InputSystem([NotNull] Control control)
        {
            if (ReferenceEquals(control, null)) throw new ArgumentNullException("control", "The control must not be null");

            control.KeyDown += FormKeyDown;
            control.KeyUp += FormKeyUp;
        }

        /// <summary>
        /// Handles the KeyUp event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void FormKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.KeyCode;
            e.Handled = true;

            if (key == PlayerNorthKey)
            {
                _playerNorth = KeyState.Unpressed;
                Trace.WriteLine("North key up");
            }
            else if (key == PlayerSouthKey)
            {
                _playerSouth = KeyState.Unpressed;
                Trace.WriteLine("South key up");
            }
            else if (key == PlayerWestKey)
            {
                _playerWest = KeyState.Unpressed;
                Trace.WriteLine("West key up");
            }
            else if (key == PlayerEastKey)
            {
                _playerEast = KeyState.Unpressed;
                Trace.WriteLine("East key up");
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void FormKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyCode;
            e.Handled = true;

            if (key == PlayerNorthKey)
            {
                _playerNorth = KeyState.Pressed;
                Trace.WriteLine("North key down");
            }
            else if (key == PlayerSouthKey)
            {
                _playerSouth = KeyState.Pressed;
                Trace.WriteLine("South key down");
            }
            else if (key == PlayerWestKey)
            {
                _playerWest = KeyState.Pressed;
                Trace.WriteLine("West key down");
            }
            else if (key == PlayerEastKey)
            {
                _playerEast = KeyState.Pressed;
                Trace.WriteLine("East key down");
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
