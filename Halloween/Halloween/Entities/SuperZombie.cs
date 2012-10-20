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
    enum PlayerState { Run, Jump, Lunge, Sit, LungeStunned }
    //you can run with speed zero and that's still running. 

    class SuperZombie : Pawn
    {

        GameTime actionStart;
        public PlayerState playerState;
        public bool isSuper;

        public SuperZombie(Vector2 pos, Animation anim)
        {
            this.pos = pos;
            this.animation = anim;


            this.playerState = PlayerState.Run;
            this.isSuper = true;
        }


        public void update(GameTime gameTime)
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

        public void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.animation.render(gameTime, spriteBatch, this.pos);

        }

        public void playerUpdate(GameTime gameTime)
        {
            switch (playerState)
            {
                case PlayerState.Run:
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        vel.X = -1f;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        vel.X = 1f;
                    }
                    else
                    {
                        vel.X *= 0.2f;
                    }


                    if (G.input.Keyboard[Keys.Space].IsPressed)
                    {
                        vel.Y = -3f;
                        this.playerState = PlayerState.Jump;
                        actionStart = gameTime;
                    }

                    if ((G.input.Keyboard[Keys.L].IsPressed))
                    {
                        this.playerState = PlayerState.Lunge;
                        actionStart = gameTime;
                    }

                    this.pos += vel;


                    //do collision detection here.


                    break;

                case PlayerState.Jump:
                    //gravity accel
                    vel.Y += 0.05f;

                    //decide here if we want air control and how much
                    if (G.input.Keyboard[Keys.A].IsDown)
                    {
                        vel.X = -1f;
                    }
                    else if (G.input.Keyboard[Keys.D].IsDown)
                    {
                        vel.X = 1f;
                    }
                    else
                    {
                        vel.X *= 0.5f;
                    }


                    //update position
                    this.pos += vel;

                    //do collision detection here


                    //temp
                    if (this.pos.Y > 200)
                    {
                        vel.Y = 0f;
                        this.playerState = PlayerState.Run;
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

        }

    }
}
