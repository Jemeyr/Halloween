using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Halloween.Graphics
{
    public class AnimationPlayer
    {
        public Animation animation;
        public int frameIndex;
        private float frameTimer;

        public Vector2 Origin
        {
            get { return new Vector2(animation.frameWidth / 2.0f, animation.frameHeight); }
        }

        public void PlayAnimation(Animation animation)
        {
            if (this.animation == animation)
                return;

            this.animation = animation;
            this.frameIndex = 0;
            this.frameTimer = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (frameTimer > animation.frameTime)
            {
                frameTimer -= animation.frameTime;

                // Advance the frame index; looping or clamping as appropriate.
                if (animation.isLooping)
                {
                    frameIndex = (frameIndex + 1) % animation.FrameCount;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, animation.FrameCount - 1);
                }
            }

            Rectangle source = new Rectangle(frameIndex * animation.frameWidth, 0, animation.frameWidth, animation.frameHeight);

            spriteBatch.Draw(animation.tex, position, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
        }
    }
}
