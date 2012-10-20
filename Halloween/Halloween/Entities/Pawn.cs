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
    class Pawn : Entity
    {
        public Vector2 vel;
        public int health;
        public bool friendly;
        public Animation animation = null;
        public AnimationPlayer animationPlayer = new AnimationPlayer();
        public SpriteEffects spriteEffects = SpriteEffects.None;

        public Pawn()
        {
            collisionBox.Width = 24;
            collisionBox.Height = 64;
        }

        public override void update(GameTime gameTime)
        {
        }

        public override void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationPlayer.Draw(gameTime, spriteBatch, pos, spriteEffects);
        }
    }
}
