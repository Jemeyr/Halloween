#if !WINDOWS_PHONE

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Halloween.Input
{
    public sealed class Keyboard
    {
        MonocerosKeyboardState _keyboardState;
        readonly ButtonState[] _keyButtonStates = new ButtonState[256];

        public ButtonState this[Keys key]
        {
            get { return _keyButtonStates[(int)key]; }
        }

        internal Keyboard()
        {
            for (var i = 0; i < 256; i++)
                _keyButtonStates[i] = new ButtonState();
        }

        internal void Update(ref KeyboardState keyboardState, GameTime gameTime)
        {
            _keyboardState.State = keyboardState;
            for (var i = 0; i < 32; i++)
            {
                _keyButtonStates[32 * 0 + i].UpdateButton((_keyboardState.PackedState0 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 1 + i].UpdateButton((_keyboardState.PackedState1 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 2 + i].UpdateButton((_keyboardState.PackedState2 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 3 + i].UpdateButton((_keyboardState.PackedState3 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 4 + i].UpdateButton((_keyboardState.PackedState4 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 5 + i].UpdateButton((_keyboardState.PackedState5 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 6 + i].UpdateButton((_keyboardState.PackedState6 & ((uint)1) << i) != 0, gameTime);
                _keyButtonStates[32 * 7 + i].UpdateButton((_keyboardState.PackedState7 & ((uint)1) << i) != 0, gameTime);
            }
        }
    }
}

#endif
