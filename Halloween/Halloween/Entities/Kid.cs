using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Halloween.Entities
{
    class Kid : Pawn
    {
        public Kid() {
            collisionBox = new Rectangle(0, 0, 24, 32);
            health = 1;
            friendly = false;
        }
    }
}
