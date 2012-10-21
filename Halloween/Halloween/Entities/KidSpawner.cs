using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halloween.Entities
{
    public class KidSpawner : Spawner
    {
        public override Pawn create()
        {
            var kid = new Kid();
            kid.pos = pos;
            return kid;
        }

    }
}
