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
    enum Order { Follow, Stay, Charge }

    enum PlayerState { Run, Jump, Lunge, Sit, LungeStunned }
    //you can run with speed zero and that's still running. 

    class Zombie : Pawn
    {
        public const float CHARSPEED = 3f;
        public const float GRAVITY = .12f;
        public const float GROUNDCOOLDOWN = 0.2f;//how your velocity slows down on the ground
        public const float AIRCOOLDOWN = 0.2f;//how your velocity slows down in the air
        public const float JUMPSPEED = 3.2f;
        public const float LUNGESPEED = 15f;
        public const float LUNGEHEIGHT = 1.2f;

        public Order order;
        public PlayerState playerState;

        public Zombie(Vector2 pos)
        {
            this.pos = pos;
            this.playerState = PlayerState.Jump;
            this.order = Order.Follow;
            animationPlayer.PlayAnimation(G.animations["zombie"]);
        }


        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
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
            base.render(gameTime, spriteBatch);
            spriteBatch.DrawString(G.spriteFont, playerState.ToString(), pos + Vector2.UnitY * -10f, Color.White);

        }

        public void playerUpdate(GameTime gameTime)
        {

            handlePlayerInput(playerState);

        }



        public void zombieUpdate(GameTime gameTime)
        {
            switch (order)
            {
                case Order.Stay:
                    break;
                case Order.Charge:
                    break;
                case Order.Follow:
                    break;
            }

        }


        public void handleAIInput()
        {
            vel = Vector2.Zero;
            vel.Y += GRAVITY;
        }

        public void handlePlayerInput(PlayerState playerState)
        {

            Vector2 targetPos = Vector2.Zero;

            switch (playerState)
            {
                case PlayerState.Run:
                    //vel = Vector2.Zero;
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


                    if (G.input.Keyboard[Keys.L].IsDown)
                    {
                        playerState = PlayerState.Lunge;
                        vel.X = LUNGESPEED * (facesRight ? 1f : -1f);
                        vel.Y = -LUNGEHEIGHT;
                    }

                    if (G.input.Keyboard[Keys.Space].IsPressed)
                    {
                        vel.Y = -JUMPSPEED;
                        this.playerState = PlayerState.Jump;
                        return;
                    }

                    
                    targetPos = this.pos + vel;

                    //targetpos is position before gravity
                    vel.Y += 1f;

                    break;
                case PlayerState.Jump:
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


                    if (G.input.Keyboard[Keys.Space].IsDown)
                    {
                        vel.Y += GRAVITY * .75f;
                    }
                    else
                    {
                        vel.Y += GRAVITY;
                    }

                    targetPos = this.pos + vel;

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




            Vector2 nextPos = checkPosition(this.pos + vel);
            nextPos.X = (int)nextPos.X;
            nextPos.Y = (int)nextPos.Y;

            targetPos.X = (int)targetPos.X;
            targetPos.Y = (int)targetPos.Y;


            this.pos = nextPos;


            //make it so it doesn't expect gravity
            if (playerState == PlayerState.Run)
            {
                vel.Y = GRAVITY;
            }



            //fall off edge
            if (nextPos.Y > targetPos.Y)
            {
                if (playerState == PlayerState.Run)
                {
                    this.playerState = PlayerState.Jump;
                   // vel.Y = GRAVITY;
                    return;
                }

            }

            //fall into ledge
            if (nextPos.Y < targetPos.Y)
            {
                if (playerState == PlayerState.Jump)
                {
                    this.vel.Y = 0;
                    this.playerState = PlayerState.Run;
                    return;

                }

            }

            //hit wall
            if (nextPos.X != targetPos.X)
            {

            }



        }



    }
}