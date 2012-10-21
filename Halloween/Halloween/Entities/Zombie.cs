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
    enum Order {Follow, Stay, Charge}

    enum PlayerState { Run, Jump, Lunge, Sit, LungeStunned }
    //you can run with speed zero and that's still running. 

    class Zombie : Pawn
    {
        public const float CHARSPEED = 3f;
        public const float GRAVITY = .12f;
        public const float GROUNDCOOLDOWN = 0.2f;//how your velocity slows down on the ground
        public const float AIRCOOLDOWN = 0.2f;//how your velocity slows down in the air
        public const float JUMPSPEED = 4.2f;
        public const float LUNGESPEED = 5f;
        public const float LUNGEHEIGHT = 1.2f;

        public Order order;
        public PlayerState playerState;
        public bool isPlayer
        {
            get { return this == Player.currentPawn; }
        }

        public Zombie(Vector2 pos)
        {
            this.pos = pos;

            this.facesRight = true;
            this.playerState = PlayerState.Jump;
            this.order = Order.Follow;
            animationPlayer.PlayAnimation(G.animations["zombie"]);
        }


        public override void update(GameTime gameTime)
        {
            if (isPlayer)
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

            switch (playerState)
            {
                case PlayerState.Run:
                    handlePlayerInput();
                    move();
                    break;
                case PlayerState.Jump:
                    handlePlayerInput();
                    move();
                    break;

                case PlayerState.Lunge:
                    move();
                    break;

                case PlayerState.LungeStunned:
                    move();
                    break;

                case PlayerState.Sit:
                    handlePlayerInput();
                    break;

                default:
                    Console.Out.WriteLine("Player in invalid state");
                    break;
            }

        }



        public void zombieUpdate(GameTime gameTime)
        {
            switch(order)
            {
                case Order.Stay:
                    handlePlayerInput();
                    move();
                    break;
                case Order.Charge:
                    handlePlayerInput();
                    move();
                    break;
                case Order.Follow:
                    handlePlayerInput();
                    move();
                    break;
            }

        }


        public void handleAIInput()
        {
            vel = Vector2.Zero;
            vel.Y += GRAVITY;
        }

        public void handlePlayerInput()
        {
            bool inAir = playerState == PlayerState.Jump;
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

            if (G.input.Keyboard[Keys.L].IsDown && playerState == PlayerState.Run)
            {
                playerState = PlayerState.Lunge;
                vel.X = LUNGESPEED * (facesRight ? 1f : -1f);
            }

            if (G.input.Keyboard[Keys.Space].IsDown)
            {
                if (!inAir)
                {
                    vel.Y = -JUMPSPEED;
                    this.playerState = PlayerState.Jump;
                }
                else
                {
                    vel.Y += GRAVITY * .75f;
                }
            }
            else
            {
                vel.Y += GRAVITY;
            }

        }


        public void move()
        {

            Vector2 newPos = this.pos + vel;

            Rectangle intersect;
            Rectangle trans = this.collisionBox;

            trans.X += (int)newPos.X;
            trans.Y += (int)newPos.Y;

            bool hitY = false;

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
                            newPos.X += x;
                        }
                        else
                        {
                            newPos.X -= x;
                        }
                    }
                    else
                    {
                        hitY = true;
                        //resolve in y axis
                        if (r.Y < trans.Y)
                        {
                            newPos.Y += y;
                        }
                        else
                        {
                            newPos.Y -= y;
                        }
                    }
                }
            }

            if ((Math.Abs(this.pos.Y - newPos.Y) < 2f) && hitY)
            {
                this.playerState = PlayerState.Run;
                this.vel.Y = 0f;
            }
            else
            {
                this.pos.Y = newPos.Y;
            }
            this.pos.X = newPos.X;

        }



    }
}