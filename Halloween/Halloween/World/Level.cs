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

namespace Halloween.World
{
    class Level
    {
        public TileArray tileArray;

        public List<Entity> entities = new List<Entity>();


        public Level()
        {
            this.tileArray = new TileArray();
        }

        public void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tileArray.render(spriteBatch);
            foreach (Entity entity in entities)
            {
                entity.render(gameTime, spriteBatch);
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (Entity entity in entities)
            {
                entity.update(gameTime);
            }
        }
    }
}
