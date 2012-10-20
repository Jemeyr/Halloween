using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    class Zombie : Pawn
    {
        public Zombie() 
        {
            health = 5;
            friendly = true;
        }
    }
}
