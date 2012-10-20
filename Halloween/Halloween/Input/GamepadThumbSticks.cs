#if !WINDOWS_PHONE

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Halloween.Input
{
    public sealed class GamepadThumbSticks
    {
        public Vector2 Left { get; internal set; }
        public Vector2 Right { get; internal set; }

        internal GamepadThumbSticks()
        {
        }

        internal void Update(ref GamePadState gamePadState, GameTime gameTime)
        {
            Left = gamePadState.ThumbSticks.Left;
            Right = gamePadState.ThumbSticks.Right;
        }

        public override string ToString()
        {
            return String.Format("Left Stick: {0}, Right Stick: {1}", Left, Right);
        }
    }

}

#endif