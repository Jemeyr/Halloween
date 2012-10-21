using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    class Kid : Pawn
    {
        public int goRight;

        public Kid() 
        {
            goRight = -1;
            collisionBox = new Rectangle(0, 0, 24, 32);
            health = 1;
            friendly = false;
            animationPlayer.PlayAnimation(G.animations["zombie"]);

            this.vel = Vector2.Zero;
        }

        public override void render(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.render(gameTime, spriteBatch);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);


            Vector2 targetPos = Vector2.Zero;

            switch (true)
            {
                case true:
                    vel.Y = 0;
                    vel.X = 3f * goRight;
                    

                    targetPos = this.pos + vel;

                    //targetpos is position before gravity
                    vel.Y = 1f;

                    break;
                
                default:
                    Console.Out.WriteLine("Player in invalid state");
                    break;
            }




            Vector2 nextPos = checkPosition(this.pos + vel);
            nextPos.X = (int)nextPos.X;
            nextPos.Y = (int)nextPos.Y;

            targetPos.X = (int)targetPos.X;
            targetPos.Y = (int)targetPos.Y;


            //fall off edge
            if (nextPos.Y > targetPos.Y)
            {
                goRight *= -1;
            }
            else if (nextPos.X != targetPos.X)
            {
                goRight *= -1;
            }
            else
            {
                this.pos = nextPos;
            }
        }

        public virtual void onHit(Pawn otherPawn)
        {
            if (otherPawn is Zombie)
            {
                G.level.pawns.Remove(this);
                Player.horde.Add(this);
            }
        }
    }
}
