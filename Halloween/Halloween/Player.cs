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
using Halloween.World;
using Halloween.Audio;
using Halloween.Input;
using Halloween.Entities;

namespace Halloween
{
    public class Player
    {
        public Pawn currentPawn;
        public List<Pawn> horde;

        public Player(Vector2 startingPos)
        {
            this.currentPawn = new Zombie(startingPos);
            ((Zombie)this.currentPawn).isSuper = true;
            
            this.horde = new List<Pawn>();
            horde.Add(currentPawn);
        }

        public void cycle(int dir)
        {

            Pawn oldSuper = currentPawn;

            int pos = horde.IndexOf(currentPawn)+ dir ;

            pos %= horde.Count;

            if(horde.ElementAt(pos) != null)
            {
                currentPawn = horde.ElementAt(pos);
                ((Zombie)currentPawn).isSuper = true;
                ((Zombie)oldSuper).isSuper = false;
                //change the thing here for which is super
            }
        }

    }
}
