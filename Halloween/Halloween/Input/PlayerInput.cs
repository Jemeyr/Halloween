#if !WINDOWS_PHONE

using Microsoft.Xna.Framework;

namespace Halloween.Input
{
    public sealed class PlayerInput
    {
        internal readonly int Index;

        public PlayerIndex PlayerIndex { get; private set; }
        public Keyboard Keyboard { get; private set; }
        public Gamepad Gamepad { get; private set; }

        internal PlayerInput(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
            Index = (int)playerIndex;
            Keyboard = new Keyboard();
            Gamepad = new Gamepad(PlayerIndex);
        }

        internal void Update(GameTime gameTime)
        {
            Keyboard.Update(ref InputManager.KeyboardStates[Index], gameTime);
            Gamepad.Update(ref InputManager.GamepadStates[Index], gameTime);
        }
    }
}

#endif