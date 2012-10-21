using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    class Kid : Pawn
    {
        public Kid() 
        {
            collisionBox = new Rectangle(0, 0, 24, 32);
            health = 1;
            friendly = false;
            animationPlayer.PlayAnimation(G.animations["zombie"]);
        }

        public override void render(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.render(gameTime, spriteBatch);
        }
    }
}
