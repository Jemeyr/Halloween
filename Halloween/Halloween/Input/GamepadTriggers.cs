#if !WINDOWS_PHONE

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Halloween.Input
{
    public sealed class GamepadTriggers
    {
        public float LeftTrigger { get; internal set; }
        public float RightTrigger { get; internal set; }

        internal GamepadTriggers()
        {
        }

        internal void Update(ref GamePadState gamePadState, GameTime gameTime)
        {
            LeftTrigger = gamePadState.Triggers.Left;
            RightTrigger = gamePadState.Triggers.Right;
        }

        public override string ToString()
        {
            return String.Format("Left Trigger: {0}, Right Trigger: {1}", LeftTrigger, RightTrigger);
        }
    }
}

#endif
