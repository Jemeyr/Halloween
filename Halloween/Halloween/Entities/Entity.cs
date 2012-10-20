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
using Halloween.Graphics;

namespace Halloween.Entities
{
    public abstract class Entity
    {
        public Vector2 pos;
        public Rectangle collisionBox;
        public bool right;

        public abstract void update(GameTime gameTime);
        public abstract void render(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
