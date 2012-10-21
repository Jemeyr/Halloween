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
    public class Pawn : Entity
    {
        public Vector2 vel;
        public int health;
        public bool friendly;
        public Animation animation = null;
        public AnimationPlayer animationPlayer = new AnimationPlayer();
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public bool facesRight = true;

        public bool isPlayer
        {
            get { return this == Player.currentPawn; }
        }

        public Pawn()
        {
            collisionBox.Width = 24;
            collisionBox.Height = 64;
        }

        public override void update(GameTime gameTime)
        {
            if (isPlayer)
            {
                if (G.input.Keyboard[Keys.A].IsDown)
                    facesRight = false;
                else if (G.input.Keyboard[Keys.D].IsDown)
                    facesRight = true;
                this.spriteEffects = facesRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            }
        }

        public override void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationPlayer.Draw(gameTime, spriteBatch, pos, spriteEffects);
           // spriteBatch.Draw(G.pixelTexture, new Rectangle((int)pos.X,(int)pos.Y,collisionBox.Width, collisionBox.Height), Color.Red);
        }




        public Vector2 checkPosition(Vector2 nextPosition)
        {

            Rectangle intersect;
            Rectangle trans = this.collisionBox;

            trans.X += (int)nextPosition.X;
            trans.Y += (int)nextPosition.Y;


            foreach (Rectangle r in G.level.rectangles)
            {
                intersect = Rectangle.Intersect(trans, r);
                if (!intersect.IsEmpty)
                {
                    //find minor axis
                    int y = intersect.Height;
                    int x = intersect.Width;

                    if (x < y)
                    {
                        //resolve in x axis
                        if (r.X < trans.X)
                        {
                            nextPosition.X += x;
                        }
                        else
                        {
                            nextPosition.X -= x;
                        }
                    }
                    else
                    {
                        //resolve in y axis
                        if (r.Y < trans.Y)
                        {
                            nextPosition.Y += y;
                        }
                        else
                        {
                            nextPosition.Y -= y;
                        }
                    }
                }
            }

            return nextPosition;
        }



    }
}
