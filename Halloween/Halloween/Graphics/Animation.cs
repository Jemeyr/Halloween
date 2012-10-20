using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Halloween.Graphics
{
    public class Animation
    {
        public Texture2D tex;
        public float frameTime;
        public bool isLooping;
        public int frameWidth = 32;
        public int frameHeight = 64;

        public int FrameCount
        {
            get { return tex.Width / frameWidth; }
        }

        public Animation(Texture2D tex, float frameTime, bool isLooping)
        {
            this.tex = tex;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }

        public Animation(Texture2D tex)
            : this(tex, 0f, false)
        {
        }

        public void render(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(tex, position, Color.White);
        }
    }
}
