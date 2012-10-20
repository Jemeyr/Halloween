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
using Halloween.Entities;

namespace Halloween.Entities
{
    enum PlayerState { Run, Jump, Lunge, Sit, LungeStunned }
    //you can run with speed zero and that's still running. 

    class Zombie : Pawn
    {
        public const float CHARSPEED = 3f;
        public const float GRAVITY = .2f;
        public const float GROUNDCOOLDOWN = 0.2f;//how your velocity slows down on the ground
        public const float AIRCOOLDOWN = 0.2f;//how your velocity slows down in the air
        public const float JUMPSPEED = 3f;



        GameTime actionStart;
        public PlayerState playerState;
        public bool isSuper;
        public bool facesRight;

        public Zombie(Vector2 pos)
        {
            this.pos = pos;

            this.facesRight = true;
            this.playerState = PlayerState.Jump;
            this.isSuper = false;
            animationPlayer.PlayAnimation(G.animations["zombie"]);
        }


        public override void update(GameTime gameTime)
        {
            if (isSuper)
            {
                playerUpdate(gameTime);
            }
            else
            {
                zombieUpdate(gameTime);
            }
        }

        public override void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spriteEffects = facesRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            base.render(gameTime, spriteBatch);
        }

        public void playerUpdate(GameTime gameTime)
        {
            Rectangle intersect;
            Rectangle trans = this.collisionBox;

            trans.X += (int)this.pos.X;
            trans.Y += (int)this.pos.Y;

            switch (playerState)
            {
                case PlayerState.Run:
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        facesRight = false;
                        vel.X = -CHARSPEED;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        facesRight = true;
                        vel.X = CHARSPEED;
                    }
                    else
                    {
                        vel.X *= GROUNDCOOLDOWN;
                    }



                    if (G.input.Keyboard[Keys.Space].IsPressed)
                    {
                        vel.Y = JUMPSPEED;
                        this.playerState = PlayerState.Jump;
                        actionStart = gameTime;
                    }

                    if ((G.input.Keyboard[Keys.L].IsPressed))
                    {
                        this.playerState = PlayerState.Lunge;
                        actionStart = gameTime;
                    }

                    //this.pos += vel;
                    this.pos.X += (int)vel.X;
                    this.pos.Y += (int)vel.Y;


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
                                    this.pos.X += x;
                                }
                                else
                                {
                                    this.pos.X -= x;
                                }
                            }
                            else
                            {
                                //resolve in y axis
                                if (r.Y < trans.Y)
                                {
                                    this.pos.Y += y;
                                }
                                else
                                {
                                    this.pos.Y -= y;
                                }
                            }
                            break;
                        }
                    }

                    //do collision detection here.


                    break;

                case PlayerState.Jump:
                    //gravity accel
                    vel.Y += GRAVITY;

                    //decide here if we want air control and how much
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        facesRight = false;
                        vel.X = CHARSPEED;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        facesRight = true;
                        vel.X = CHARSPEED;
                    }
                    else
                    {
                        vel.X *= AIRCOOLDOWN;
                    }


                    //update position
                    this.pos += vel;

                    //do collision detection here
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
                                    this.pos.X += x;
                                }
                                else
                                {
                                    this.pos.X -= x;
                                }
                            }
                            else
                            {
                                //resolve in y axis
                                if (r.Y < trans.Y)
                                {
                                    this.pos.Y += y;
                                }
                                else
                                {
                                    this.pos.Y -= y;
                                }
                                this.playerState = PlayerState.Run;
                                this.vel.Y = 0f;
                            }
                            break;
                        }
                    }

                    break;

                case PlayerState.Lunge:

                    break;

                case PlayerState.LungeStunned:

                    break;

                case PlayerState.Sit:

                    break;

                default:
                    Console.Out.WriteLine("Player in invalid state");
                    break;
            }

        }

        public void zombieUpdate(GameTime gameTime)
        {
            Rectangle intersect;
            Rectangle trans = this.collisionBox;

            trans.X += (int)this.pos.X;
            trans.Y += (int)this.pos.Y;

            switch (playerState)
            {
                case PlayerState.Run:

                    /*
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        facesRight = false;
                        vel.X = -1f;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        facesRight = true;
                        vel.X = 1f;
                    }
                    else
                    {
                        vel.X *= 0.2f;
                    }


                    this.pos += vel;
                    */


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
                                    this.pos.X += x;
                                }
                                else
                                {
                                    this.pos.X -= x;
                                }
                            }
                            else
                            {
                                //resolve in y axis
                                if (r.Y < trans.Y)
                                {
                                    this.pos.Y += y;
                                }
                                else
                                {
                                    this.pos.Y -= y;
                                }
                            }
                            break;
                        }
                    }

                    //do collision detection here.


                    break;

                case PlayerState.Jump:
                    //gravity accel
                    vel.Y += GRAVITY;
                    /*
                    //decide here if we want air control and how much
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        facesRight = false;
                        vel.X = -1f;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        facesRight = true;
                        vel.X = 1f;
                    }
                    else
                    {
                        vel.X *= 0.5f;
                    }
                    */

                    //update position
                    this.pos += vel;

                    //do collision detection here

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
                                    this.pos.X += x;
                                }
                                else
                                {
                                    this.pos.X -= x;
                                }
                            }
                            else
                            {
                                //resolve in y axis
                                if (r.Y < trans.Y)
                                {
                                    this.pos.Y += y;
                                }
                                else
                                {
                                    this.pos.Y -= y;
                                }
                                this.playerState = PlayerState.Run;
                                this.vel.Y = 0f;
                            }
                            break;
                        }
                    }

                    break;


            }

        }


    }
}