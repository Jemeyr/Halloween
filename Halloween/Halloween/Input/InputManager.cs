using System;
using Microsoft.Xna.Framework;

#if !WINDOWS_PHONE
using Microsoft.Xna.Framework.Input;
#endif

namespace Halloween.Input
{
    /// <summary>
    /// Handles input devices.
    /// </summary>
    public sealed class InputManager : IGameComponent, Microsoft.Xna.Framework.IUpdateable
    {
#if !WINDOWS_PHONE
        internal readonly static KeyboardState[] KeyboardStates = new KeyboardState[4];
        internal readonly static GamePadState[] GamepadStates = new GamePadState[4];
        internal static MouseState MouseState;
#endif

        event EventHandler<EventArgs> Microsoft.Xna.Framework.IUpdateable.EnabledChanged
        {
            add { }
            remove { }
        }

        event EventHandler<EventArgs> Microsoft.Xna.Framework.IUpdateable.UpdateOrderChanged
        {
            add { }
            remove { }
        }

        bool Microsoft.Xna.Framework.IUpdateable.Enabled
        {
            get { return true; }
        }

        int Microsoft.Xna.Framework.IUpdateable.UpdateOrder
        {
            get { return int.MinValue; }
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// Gets the <see cref="PlayerInputCollection"/> associated with the <see cref="InputManager"/>.
        /// </summary>
        public PlayerInputCollection Players { get; private set; }
        /// <summary>
        /// Gets the <see cref="Mouse"/> associated with the <see cref="InputManager"/>.
        /// </summary>
        public Mouse Mouse { get; private set; }
        /// <summary>
        /// Gets the <see cref="Keyboard"/> associated with the <see cref="InputManager"/>.
        /// </summary>
        public Keyboard Keyboard
        {
            get { return Players[0].Keyboard; }
        }
#endif

		internal InputManager()
		{
#if !WINDOWS_PHONE
            Players = new PlayerInputCollection();
            Mouse = new Mouse();
#endif
		}

        void IGameComponent.Initialize()
        {
        }

        void Microsoft.Xna.Framework.IUpdateable.Update(GameTime gameTime)
        {
#if !WINDOWS_PHONE
            for (var x = 0; x < 4; x++)
            {
                KeyboardStates[x] = Microsoft.Xna.Framework.Input.Keyboard.GetState((PlayerIndex)x);
                GamepadStates[x] = GamePad.GetState((PlayerIndex)x);
            }

            MouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            Mouse.Update(ref MouseState, gameTime);
            Players.Update(gameTime);
#endif
        }
    }
 }
 