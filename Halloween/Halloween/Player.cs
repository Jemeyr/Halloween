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
using Halloween.Audio;
using Halloween.Input;
using Halloween.Entities;

namespace Halloween
{
    public static class Player
    {
        public static Pawn currentPawn
        {
            get 
            { 
                return horde[currentPawnIndex];
            }
        }

        public static List<Pawn> horde = new List<Pawn>();
        public static int currentPawnIndex = 0;

        static Player()
        {
            horde.Add(new Zombie(Vector2.Zero));
        }

        public static void render(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Pawn p in horde)
            {
                if (p == currentPawn)
                    continue;
            }

            currentPawn.render(gameTime, spriteBatch);
        }

        public static void update(GameTime gameTime)
        {

            if (G.input.Keyboard[Keys.Q].IsPressed)
            {
                currentPawnIndex -= 1;
                if (currentPawnIndex < 0)
                    currentPawnIndex = horde.Count - 1;
            }

            if (G.input.Keyboard[Keys.E].IsPressed)
            {
                currentPawnIndex += 1;
                if (currentPawnIndex >= horde.Count)
                    currentPawnIndex = 0;
            }

            foreach (Pawn p in horde)
            {
                p.update(gameTime);
            }

        }
    }
}
