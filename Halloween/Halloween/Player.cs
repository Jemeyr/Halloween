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
    class Player
    {

        public Pawn super;
        public List<Pawn> horde;

        public Player(Pawn super, Vector2 startingPos)
        {
            this.super = super;
            this.super.pos = startingPos;

            this.horde = new List<Pawn>();
            horde.Add(super);
        }

        public void cycle(int dir)
        {
            int oldSuper = horde.IndexOf(super);

            int pos = oldSuper+ dir ;

            pos %= horde.Count;

            if(horde.ElementAt(pos) != null)
            {
                super = horde.ElementAt(pos);
                //change the thing here for which is super
            }
 
            
        }

    }
}
