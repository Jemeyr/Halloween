
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Halloween.Graphics
{
    public class Cam2d
    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float zoom;
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Vector2 origin;

        public Cam2d(Viewport viewport)
        {
            zoom = 1.0f;
            position = Vector2.Zero;
            rotation = 0.0f;
            origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        }

        /// <summary>
        /// Move the camera in the x,y axes given the delta position value.
        /// </summary>
        /// <param name="delta_position">Amount to translate the camera.</param>
        public void MoveCamera(Vector2 delta_position)
        {
            position += delta_position;
        }

        /// <summary>
        /// Rotate the camera by a give delta rotation value.
        /// </summary>
        /// <param name="delta_rotation">Value to rotate the camera by.</param>
        public void RotateCamera(float delta_rotation)
        {
            rotation += delta_rotation;
        }

        /// <summary>
        /// Increase the zoom of the camera. Clamped between 0.5 and 2.0.
        /// </summary>
        /// <param name="delta_zoom">Negative values zoom out, positive values zoom in.</param>
        public void ZoomCamera(float delta_zoom)
        {
            zoom += delta_zoom;
            zoom = MathHelper.Clamp(zoom, 0.5f, 2.0f);
        }

        public void LookAt(Vector2 position)
        {
            this.position = position - origin;
        }

        /// <summary>
        /// Returns the transformation of the camera after being translated, rotated and scaled upon 
        /// the center of the screen.
        /// </summary>
        /// <returns>The camera transformation matrix that is translated, rotated, and scaled.</returns>
        public Matrix GetCameraTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(origin, 0));
        }
    }
}

