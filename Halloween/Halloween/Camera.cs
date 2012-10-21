using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Halloween
{
    /// <summary>
    /// TODO: Add rotation support, add queue zoom support, fix transitions, add interpolation method support.
    /// </summary>
    public class Camera
    {
        float _rotationRate;
        float _goalRotation;
        float _desiredScale;
        Vector2 _desiredPosition;
        float _translationRate = 1;
        float _scaleRate;
        float _scaleDuration;
        float _scaleElapsed;
        float _moveDuration;
        float _moveElapsed;
        float _shakeDuration;
        float _shakeElapsed;
        float _shakeMagnitude;
        float _shakeSpeed;
        float _shakeLastDirectionChange;
        float _shakeAngle;
        float _shakeDistance;
        Vector2 _shakeOffset;
        Vector2 _shakeAnchorPosition;
        ShakeAxis _shakeOptions;
        ShakeMode _shakeMode;
        float _flashElapsed;
        float _flashDuration;
        Color _flashColor = Color.White;

        float _scale;
        public float Scale
        {
            get
            {
                return _scale;
            }
            private set
            {
                _scale = value;
            }
        }

        float _rotation;
        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }

        Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return new Vector2((float)Math.Round(_position.X, 0), (float)Math.Round(_position.Y, 0));
            }
            set
            {
                _position = value;
            }
        }

        Vector2 _offset;
        /// <summary>
        /// Defines the Camera's origin. Ex: An offset of the ScreenCenter defines (0,0) to be the center of the screen.
        /// </summary>
        public Vector2 Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }

        public CameraMode Mode { get; set; }

        /// <summary>
        /// The area in the world the camera is locked inside.
        /// </summary>
        public Rectangle BoundedArea { get; set; }
        public bool BoundsEnabled { get; set; }

        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        public Matrix World { get; private set; }
        public Entities.Entity Tracking { get; set; }

        public bool IsTracking
        {
            get { return Tracking != null; }
        }

        /// <summary>
        /// Drawing a fraction of a pixel introduces visual glitches. Fix: Round to the nearest pixel.
        /// </summary>
        public bool PixelFix { get; set; }
        public bool IsMoving { get; private set; }
        public bool IsZooming { get; set; }
        public bool IsShaking { get; set; }

        public Camera()
            : base()
        {
            _scaleRate = 0.5f;
            _rotationRate = 0.5f;
            _translationRate = 0.5f;
            Position = _desiredPosition = Vector2.Zero;
            Scale = _desiredScale = 1;
            Rotation = 0;
            BoundedArea = Rectangle.Empty;
            PixelFix = false; // broken.
        }

        internal virtual void Update(GameTime time)
        {
            if (IsTracking && Mode != CameraMode.FollowPlayer)
            {
                _position.X = MathHelper.SmoothStep(_position.X, Tracking.pos.X, _translationRate) - 100;
                //_position.Y = MathHelper.SmoothStep(_position.Y, Tracking.pos.Y, _translationRate) - 175;
            }

            UpdateView();
            UpdateProjection();
            UpdateWorld();
        }

        protected virtual void UpdateView()
        {
            if (PixelFix)
            {
                View = Matrix.CreateTranslation(-Position.X + _shakeOffset.X, -Position.Y + _shakeOffset.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Scale) *
                    Matrix.CreateTranslation(Offset.X, Offset.Y, 0);
            }
            else
            {
                View = Matrix.CreateTranslation(-Position.X + _shakeOffset.X, -Position.Y + _shakeOffset.Y, 0) *
                  Matrix.CreateRotationZ(Rotation) *
                  Matrix.CreateScale(Scale) *
                  Matrix.CreateTranslation(Offset.X, Offset.Y, 0);
            }
        }

        protected virtual void UpdateProjection()
        {
            Projection = Matrix.CreateOrthographicOffCenter(0, G.graphicsDevice.Viewport.Width, G.graphicsDevice.Viewport.Height, 0, 0, 1);
        }

        protected virtual void UpdateWorld()
        {
            World = Matrix.Identity;
        }

        public void Follow(Entities.Entity target)
        {
            if (target == null)
                return;
            Tracking = target;
            Mode = CameraMode.TrackTarget;
        }

        public void StopFollow()
        {
            Mode = CameraMode.Stationary;
            Tracking = null;
        }

        public void MoveToOverDuration(Vector2 position, float duration)
        {
            if (_position == position)
                return;

            _desiredPosition = position;
            _moveDuration = duration;
            _moveElapsed = 0;
            Tracking = null;
        }

        public void MoveToImmediate(Vector2 position)
        {
            if (_desiredPosition == position)
                return;

            Mode = CameraMode.Stationary;

            // imeditately move to position
            _position = position;
        }

        public void ZoomImmediate(float amount)
        {
            Scale = _desiredScale = amount;
        }

        /// <summary>
        /// Zoom in or out by this amount. The speed is determined by the ZoomRate.
        /// </summary>
        /// <param name="amount">Amount to Zoom in or out by.</param>
        public void ZoomByAmount(float amount)
        {
            IsZooming = true;
            _desiredScale = amount + Scale;
        }

        /// <summary>
        /// Zoom to this level within the given duration.
        /// </summary>
        /// <param name="zoom">Zoom Level</param>
        /// <param name="duration">Duration of zoom in seconds.</param>
        public void ZoomOverDuration(float zoom, float duration)
        {
            if (_desiredScale == zoom)
                return;

            IsZooming = true;
            _desiredScale = zoom;
            _scaleDuration = duration;
        }

        #region HACK: THESE SHOULDN'T BE HERE
        public static Vector2 Shift(Vector2 location, double angle, float distance)
        {
            location.X += (float)(Math.Cos(angle) * (double)distance);
            location.Y -= (float)(Math.Sin(angle) * (double)distance);
            return location;
        }

        public static double ClampAngle(double angle)
        {
            return angle - 6.2831853071795862 * Math.Floor(angle / 6.2831853071795862);
        }

        public static double AngleBetween(Vector2 location1, Vector2 location2)
        {
            float xDistance = location2.X - location1.X;
            float num = location2.Y - location1.Y;
            num *= -1f;
            return Camera.AngleBetween(num, xDistance);
        }

        public static double AngleBetween(float yDistance, float xDistance)
        {
            double angle = Math.Atan2((double)yDistance, (double)xDistance);
            return Camera.ClampAngle(angle);
        }
        #endregion

    }

    /// <summary>
    /// Flags to control which axis to shake.
    /// </summary>
    [Flags]
    public enum ShakeAxis
    {
        /// <summary>
        /// Specifies to shake the X-Axis.
        /// </summary>
        X = 2 << 0,

        /// <summary>
        /// Specifies to shake the Y-Axis.
        /// </summary>
        Y = 2 << 1,
    }

    public enum ShakeMode
    {
        Random,
        Directional,
        Modes
    }

    public enum CameraMode
    {
        FollowPlayer,
        TrackTarget,
        Stationary,
        Modes
    }
}

