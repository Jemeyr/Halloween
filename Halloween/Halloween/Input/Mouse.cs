#if !WINDOWS_PHONE

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Halloween.Input
{
	public sealed class Mouse
	{
        //Vector2 _deltaPosition = Vector2.Zero;
        //int _scrollWheelDelta = 0;

        public Vector2 Position { get; private set; }
        public Vector2 PreviousPosition { get; private set; }
        public ButtonState LeftButton { get; private set; }
        public ButtonState RightButton { get; private set; }
        public ButtonState MiddleButton { get; private set; }
        public ButtonState XButton1 { get; private set; }
        public ButtonState XButton2 { get; private set; }
        public int ScrollWheelValue { get; private set; }
        public int PreviousScrollWheelValue { get; private set; }

        internal Mouse()
        {
            LeftButton = new ButtonState();
            RightButton = new ButtonState();
            MiddleButton = new ButtonState();
            XButton1 = new ButtonState();
            XButton2 = new ButtonState();
        }
		
		internal void Update(ref MouseState mouseState, GameTime gameTime)
		{
            PreviousPosition = Position;
            Position = new Vector2(mouseState.X, mouseState.Y);
            //_deltaPosition = Position - PositionOld;
			//_scrollWheelDelta = mouseState.ScrollWheelValue - ScrollWheelValue;
		    PreviousScrollWheelValue = ScrollWheelValue;
			ScrollWheelValue = mouseState.ScrollWheelValue;

            LeftButton.UpdateButton(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            RightButton.UpdateButton(mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            MiddleButton.UpdateButton(mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            XButton1.UpdateButton(mouseState.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            XButton2.UpdateButton(mouseState.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
		}
	}
}

#endif