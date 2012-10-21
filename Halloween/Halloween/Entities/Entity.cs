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
    public class Entity
    {
        public Vector2 pos;
        public Rectangle collisionBox;
        public bool facesRight;

        public virtual void update(GameTime gameTime)
        {
            if (G.input.Keyboard[Keys.A].IsDown)
                facesRight = false;
            else if (G.input.Keyboard[Keys.D].IsDown)
                facesRight = true;
        }

        public virtual void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

    }
}
