#if !WINDOWS_PHONE

using System;
using Halloween;
using Microsoft.Xna.Framework;

namespace Halloween.Input
{
    public class ButtonState
    {
        public bool IsDown { get; private set; }
        public bool IsUp { get { return !IsDown; } }
        public bool WasDown { get; private set; }
        public bool WasUp { get { return !WasDown; } }
        public TimeSpan DownDuration { get; private set; }
        public TimeSpan UpDuration { get; private set; }
        public bool IsPressed { get { return IsDown && !WasDown; } }
        public bool IsReleased { get { return IsUp && WasDown; } }

        internal ButtonState()
        {
        }

        internal void UpdateButton(bool isDown, GameTime gameTime)
        {
            WasDown = IsDown;
            IsDown = isDown;
            if (IsDown)
            {
                UpDuration = TimeSpan.Zero;
                if (!WasDown)
                    DownDuration = G.CachedSecond;
                else
                    DownDuration += gameTime.ElapsedGameTime;
            }
            else
            {
                DownDuration = TimeSpan.Zero;
                if (!WasUp)
                    UpDuration = G.CachedSecond;
                else
                    UpDuration += gameTime.ElapsedGameTime;
            }
        }

        public override string ToString()
        {
            return String.Format("Down: {0}, Was Down: {1}", IsDown, WasDown);
        }
    }
}

#endif