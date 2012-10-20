#if !WINDOWS_PHONE

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Halloween.Input
{
    public sealed class Gamepad
    {
        readonly PlayerIndex _playerIndex;
        TimeSpan _viberationTimer;

        readonly ButtonState[] _gamepadButtonStates = new ButtonState[14];

        public GamepadThumbSticks ThumbSticks { get; internal set; }
        public GamepadTriggers Triggers { get; internal set; }
        public bool IsConnected { get; internal set; }
        public bool IsVibrating { get; internal set; }

        public ButtonState this[GamepadButtons gamePadButton]
        {
            get { return _gamepadButtonStates[(int)gamePadButton - 1]; }
        }

        internal Gamepad(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
            for (var x = 0; x < 14; x++)
                _gamepadButtonStates[x] = new ButtonState();
            Triggers = new GamepadTriggers();
            ThumbSticks = new GamepadThumbSticks();
        }

        internal void Update(ref GamePadState gamePadState, GameTime gameTime)
        {
            UpdateButtons(ref gamePadState, gameTime);
            ThumbSticks.Update(ref gamePadState, gameTime);
            Triggers.Update(ref gamePadState, gameTime);
            IsConnected = gamePadState.IsConnected;
            if (!IsVibrating) 
                return;
            _viberationTimer -= gameTime.ElapsedGameTime;
            if (_viberationTimer <= TimeSpan.Zero)
                Vibrate(TimeSpan.Zero, 0, 0);
        }

        private void UpdateButtons(ref GamePadState gamePadState, GameTime gameTime)
        {
            _gamepadButtonStates[0].UpdateButton(gamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[1].UpdateButton(gamePadState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[2].UpdateButton(gamePadState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[3].UpdateButton(gamePadState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[4].UpdateButton(gamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[5].UpdateButton(gamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[6].UpdateButton(gamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[7].UpdateButton(gamePadState.Buttons.RightStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[8].UpdateButton(gamePadState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[9].UpdateButton(gamePadState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[10].UpdateButton(gamePadState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[11].UpdateButton(gamePadState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[12].UpdateButton(gamePadState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
            _gamepadButtonStates[13].UpdateButton(gamePadState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed, gameTime);
        }

        public bool Vibrate(TimeSpan duration, float leftMotor, float rightMotor)
        {
            if (duration <= TimeSpan.Zero || (leftMotor == 0 && rightMotor == 0))
            {
                IsVibrating = false;
                GamePad.SetVibration(_playerIndex, 0, 0);
                _viberationTimer = TimeSpan.Zero;
                return false;
            }
            IsVibrating = GamePad.SetVibration(_playerIndex, leftMotor, rightMotor);
            _viberationTimer = duration;
            return IsVibrating;
        }

    }
}

#endif